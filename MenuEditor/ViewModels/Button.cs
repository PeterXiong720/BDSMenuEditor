using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuEditor.ViewModels
{
    public class Button : BindableBase
    {
        public Button()
        {
            EditButtonContent = new Views.EditButton()
            {
                DataContext = this
            };
            AddExecuteCommand = new DelegateCommand(new Action(AddExecute));
            RemoveExecuteCommand = new DelegateCommand(new Action(RemoveExecute));
        }

        public Button(string text, string image = "", ObservableCollection<ButtonExecute> buttonExecutes = null) : this()
        {
            Text = text;
            Image = image;
            ButtonExecutes = buttonExecutes ?? new ObservableCollection<ButtonExecute>();
        }

        private string text;

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        private string image;

        public string Image
        {
            get => image;
            set => SetProperty(ref image, value);
        }

        private ObservableCollection<ButtonExecute> executes;

        public ObservableCollection<ButtonExecute> ButtonExecutes
        {
            get => executes;
            set => SetProperty(ref executes, value);
        }

        private Views.EditButton editButton;
        [JsonIgnore]
        public Views.EditButton EditButtonContent
        {
            get => editButton;
            set => SetProperty(ref editButton, value);
        }

        private ButtonExecute _SelectExecuteItem;
        [JsonIgnore]
        public ButtonExecute SelectExecuteItem
        {
            get => _SelectExecuteItem;
            set => SetProperty(ref _SelectExecuteItem, value);
        }

        [JsonIgnore]
        public DelegateCommand AddExecuteCommand { get; set; }
        private void AddExecute()
        {
            ButtonExecutes.Add(new ButtonExecute());
        }

        [JsonIgnore]
        public DelegateCommand RemoveExecuteCommand { get; set; }
        private void RemoveExecute()
        {
            if (SelectExecuteItem != null)
            {
                ButtonExecutes.Remove(SelectExecuteItem);
            }
        }

        //弃用
        private ButtonType type;

        public ButtonType Type
        {
            get => type;
            set => SetProperty(ref type, value);
        }

        //弃用
        private string execute;

        public string Execute
        {
            get => execute;
            set => SetProperty(ref execute, value);
        }

        //弃用
        private string[] types = System.Enum.GetNames(typeof(ButtonType));
        [JsonIgnore]
        public string[] Types
        {
            get => types;
            set => SetProperty(ref types, value);
        }

    }

    //弃用
    public enum ButtonType
    {
        Command,
        OperatorCommand,
        RequireOpCommand,
        Console,
        JavaScriptCode,
        JavaScriptFile,
        Menu,
        AdministratorMenu
    }
}
