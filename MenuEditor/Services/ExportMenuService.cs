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
        public ExportMenuService(string path, bool isDebug)
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

        private readonly V8ScriptEngine ScriptEngine;

        private string ScriptCode;

        public async Task ExportMenuAsync(MainWindowViewModel model)
        {
            if (model == null)
            {
                return;
            }

            ScriptEngine.AddHostObject("MENUS", model.MenuCollection.ToList());
            ScriptEngine.AddHostObject("MODALS", model.ModalCollection.ToList());
            await Task.Run(() =>
            {
                ScriptEngine.Execute(ScriptCode);
            });
        }
    }
}
