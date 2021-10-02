using MenuEditor.Services;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MenuEditor.ViewModels
{
    class TopMenuViewModel : BindableBase
    {
        public delegate void OpenWorkSpaceEvent();
        public event OpenWorkSpaceEvent OpenWorkSpace;

        public delegate void SaveDataEvent();
        public event SaveDataEvent SaveData;

        public delegate void NewProjEvent();
        public event NewProjEvent NewProject;

        public TopMenuViewModel(IDataService dataService) : this()
        {
            this.dataService = dataService;
        }

        public TopMenuViewModel()
        {
            AutoSave = Configuration.GetValue<bool>("autosave");
            OpenCommand = new DelegateCommand(new Action(openWorkSpace));
            SaveCommand = new DelegateCommand(new Action(saveData));
            NewCommand = new DelegateCommand(new Action(newProj));

            SelectScriptDialogContent.DataContext = this;
            SelectScriptDialogCommand = new DelegateCommand(new Action(SelectScriptDialog));
            CloseSelectScriptDialogCommand = new DelegateCommand(new Action(CloseSelectScriptDialog));

            var dir = new DirectoryInfo("./Script");
            foreach (FileInfo item in dir.GetFiles())
            {
                if (item.Extension == ".js")
                {
                    Scripts.Add(item);
                }
            }
            if (scripts.Count > 0)
            {
                SelectScript = Scripts[0];
            }

            Task task = Task.Run(() =>
            {
                while (true)
                {
                    if (AutoSave)
                    {
                        SaveCommand.Execute();
                        System.Threading.Thread.Sleep(2000);
                    }
                }

            });
        }

        private IDataService dataService = new JsonDataService();

        private bool autoSave;

        public bool AutoSave
        {
            get => autoSave;
            set
            {
                _ = Configuration.SaveValue("autosave", value);
                _ = SetProperty(ref autoSave, value);
            }
        }

        private List<FileInfo> scripts = new List<FileInfo>();

        public List<FileInfo> Scripts
        {
            get => scripts;
            set => SetProperty(ref scripts, value);
        }

        private FileInfo selectScript;

        public FileInfo SelectScript
        {
            get => selectScript;
            set => SetProperty(ref selectScript, value);
        }

        private bool debug;

        public bool ScriptDebug
        {
            get => debug;
            set => SetProperty(ref debug, value);
        }

        private bool _IsDialogOpen = false;

        public bool IsDialogOpen
        {
            get => _IsDialogOpen;
            set => SetProperty(ref _IsDialogOpen, value);
        }

        private Views.SelectScriptDialog _SelectScriptDialogContent = new() { };

        public Views.SelectScriptDialog SelectScriptDialogContent
        {
            get => _SelectScriptDialogContent;
            set => SetProperty(ref _SelectScriptDialogContent, value);
        }

        private System.Windows.Visibility _ProgressBarVisibility = System.Windows.Visibility.Hidden;

        public System.Windows.Visibility ProgressBarVisibility
        {
            get => _ProgressBarVisibility;
            set => SetProperty(ref _ProgressBarVisibility, value);
        }

        public DelegateCommand OpenCommand { get; set; }
        private void openWorkSpace()
        {
            if (OpenWorkSpace != null)
            {
                OpenWorkSpace.Invoke();
            }
        }

        public DelegateCommand SaveCommand { get; set; }
        private void saveData()
        {
            if (SaveData != null)
            {
                SaveData.Invoke();
            }
        }

        public DelegateCommand NewCommand { get; set; }
        private void newProj()
        {
            if (NewProject != null)
            {
                NewProject.Invoke();
            }
        }

        public DelegateCommand ExportCommand { get; set; }

        public DelegateCommand SelectScriptDialogCommand { get; set; }
        private void SelectScriptDialog()
        {
            IsDialogOpen = true;
        }

        public DelegateCommand CloseSelectScriptDialogCommand { get; set; }
        private void CloseSelectScriptDialog()
        {
            IsDialogOpen = false;
        }
    }
}
