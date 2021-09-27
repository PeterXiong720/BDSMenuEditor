using MenuEditor.Services;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuEditor.ViewModels
{
    class TopMenuViewModel : BindableBase
    {
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
            this.AutoSave = Configuration.GetValue<bool>("autosave");
            this.SaveCommand = new DelegateCommand(new Action(saveData));
            this.NewCommand = new DelegateCommand(new Action(newProj));
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
    }
}
