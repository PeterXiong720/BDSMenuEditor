using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MenuEditor.ViewModels;
using Microsoft.ClearScript;
using Microsoft.ClearScript.V8;
using Newtonsoft.Json;

namespace MenuEditor.Services
{
    class ExportMenuService
    {
        public ExportMenuService(FileInfo script, string wokdir, bool isDebug) : this(script.FullName, isDebug)
        {
            WorkDir = new DirectoryInfo(wokdir);
        }

        private ExportMenuService(string path, bool isDebug)
        {
            if (isDebug)
            {
                ScriptEngine = new V8ScriptEngine(
                    "debug-v8engine",
                    V8ScriptEngineFlags.EnableDebugging |
                    V8ScriptEngineFlags.AwaitDebuggerAndPauseOnStart,
                    9222
                );
            }
            else
            {
                ScriptEngine = new V8ScriptEngine();
            }
            if (File.Exists(path))
            {
                using (var reader = new StreamReader(path))
                {
                    ScriptCode = reader.ReadToEnd();
                }
            }

            ScriptEngine.AddHostObject("dotnet", new HostTypeCollection("mscorlib", "System"));
        }

        ~ExportMenuService()
        {
            ScriptEngine = null;
        }

        private V8ScriptEngine ScriptEngine;

        private string ScriptCode;

        public DirectoryInfo WorkDir;

        public void ExportMenu(MainWindowViewModel model)
        {
            if (model == null)
            {
                return;
            }

            var HostData = new
            {
                Menus = JsonConvert.SerializeObject(model.MenuCollection.ToList()),
                Modals = JsonConvert.SerializeObject(model.ModalCollection.ToList())
            };
            ScriptEngine.AddHostObject("DATA", HostData);
            ScriptEngine.AddHostObject("DIRECTORY", WorkDir);

            ScriptEngine.AddHostTypes(
                typeof(System.Windows.MessageBox),
                typeof(System.Windows.MessageBoxButton),
                typeof(System.Windows.MessageBoxImage));

            try
            {
                ScriptEngine.Execute(ScriptCode);
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(
                    e.ToString(),
                    "脚本执行出错",
                    System.Windows.MessageBoxButton.OK,
                    System.Windows.MessageBoxImage.Error);
                //throw;
            }
        }
    }
}
