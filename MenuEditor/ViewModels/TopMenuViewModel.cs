using MenuEditor.Services;
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
        public TopMenuViewModel(IDataService dataService) : this()
        {
            this.dataService = dataService;
        }

        public TopMenuViewModel()
        {

        }

        private IDataService dataService = new JsonDataService();

        private bool autoSave = true;

        public bool AutoSave
        {
            get { return autoSave; }
            set 
            {
                this.SetProperty<bool>(ref autoSave, value);
            }
        }

    }
}
