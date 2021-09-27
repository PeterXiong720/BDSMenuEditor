using Prism.Mvvm;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace MenuEditor.ViewModels
{
    public class PageViewModel : FormBase
    {
        public PageViewModel()
        {

        }

        public PageViewModel(string filename, bool isop, string title, string content, ObservableCollection<Button> buttons = null)
        {
            FileName = filename;
            IsOpMenu = isop;
            Title = title;
            Content = content;
            Buttons = (buttons != null) ? buttons : new() { };
        }

        private bool isOpMenu = false;

        public bool IsOpMenu
        {
            get => isOpMenu;
            set
            {
                SetProperty(ref isOpMenu, value);
            }
        }

        private ObservableCollection<Button> buttons = new() { };

        public ObservableCollection<Button> Buttons
        {
            get => buttons;
            set
            {
                SetProperty(ref buttons, value);
            }
        }

        [JsonIgnore]
        public Views.EditMenu EditMenu { get; set; }

    }
}
