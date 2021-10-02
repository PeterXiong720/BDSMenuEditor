using MenuEditor.Views;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    public class Command : BindableBase
    {
        public Command()
        {
            AddCommandCmd = new DelegateCommand(new Action(AddCommand));
            RemoveCommandCmd = new DelegateCommand(new Action(RemoveCommand));
        }

        public Command(string execute = "", CommandType type = CommandType.Command)
        {
            Execute = execute;
            Type = type;
        }

        [JsonIgnore]
        public AddButtonCommandDialog AddButtonCommand { get; set; }

        private string execute;

        public string Execute
        {
            get => execute;
            set => SetProperty(ref execute, value);
        }

        private CommandType type;

        public CommandType Type
        {
            get => type;
            set => SetProperty(ref type, value);
        }

        private string[] types = Enum.GetNames(typeof(CommandType));
        [JsonIgnore]
        public string[] Types
        {
            get => types;
            set => SetProperty(ref types, value);
        }

        private ObservableCollection<Command> commands = new() { };
        [JsonIgnore]
        public ObservableCollection<Command> Commands
        {
            get => commands;
            set => SetProperty(ref commands, value);
        }

        private Command _SelectedItem;
        [JsonIgnore]
        public Command SelectedItem
        {
            get => _SelectedItem;
            set => SetProperty(ref _SelectedItem, value);
        }

        [JsonIgnore]
        public DelegateCommand AddCommandCmd { get; set; }
        private void AddCommand()
        {
            var cmd = new Command()
            {
                Execute = "",
                Type = CommandType.Command
            };
            Commands.Add(cmd);
        }

        [JsonIgnore]
        public DelegateCommand RemoveCommandCmd { get; set; }
        private void RemoveCommand()
        {
            if (SelectedItem != null)
            {
                try
                {
                    Commands.Remove(SelectedItem);
                }
                catch
                {
                    throw;
                }
            }
        }

        public enum CommandType
        {
            Command,
            OperatorCommand,
            RequireOpCommand,
            JavaScriptCode,
            JavaScriptFile,
            Menu,
            AdministratorMenu
        }
    }
}
