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
            //EditMenu = new Views.EditMenu(this);
            AddButtonCommand = new DelegateCommand(new Action(AddButton));
            RemoveButtonCommand = new DelegateCommand(new Action(RemoveButton));
        }

        public PageViewModel(string filename, bool isop, string title, string content, ObservableCollection<Button> buttons = null) : this()
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
            set => SetProperty(ref isOpMenu, value);
        }

        private ObservableCollection<Button> buttons = new() { };

        public ObservableCollection<Button> Buttons
        {
            get => buttons;
            set => SetProperty(ref buttons, value);
        }

        private Button _SelectedItem;

        public Button SelectedItem
        {
            get => _SelectedItem;
            set => SetProperty(ref _SelectedItem, value);
        }


        [JsonIgnore]
        public Views.EditMenu EditMenu { get; set; }

        public DelegateCommand AddButtonCommand { get; set; }
        private void AddButton()
        {
            var btn = new Button()
            {
                Text = "",
                Image = "",
                Type = ButtonType.Command,
                Execute = ""
            };
            Buttons.Add(btn);
        }

        public DelegateCommand RemoveButtonCommand { get; set; }
        private void RemoveButton()
        {
            if (SelectedItem != null)
            {
                try
                {
                    Buttons.Remove(SelectedItem);
                }
                catch
                {
                    throw;
                }
            }
        }
    }
}
