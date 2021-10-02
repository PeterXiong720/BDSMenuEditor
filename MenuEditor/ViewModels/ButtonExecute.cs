using Newtonsoft.Json;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuEditor.ViewModels
{
    public class ButtonExecute : BindableBase
    {
        private ExecuteType type = ExecuteType.Command;

        public ExecuteType Type
        {
            get => type;
            set => SetProperty(ref type, value);
        }

        private string execute = "";

        public string Execute
        {
            get => execute;
            set => SetProperty(ref execute, value);
        }

        private string[] types = System.Enum.GetNames(typeof(ExecuteType));
        [JsonIgnore]
        public string[] Types
        {
            get => types;
            set => SetProperty(ref types, value);
        }
    }

    public enum ExecuteType
    {
        Command = 0,
        OperatorCommand = 1,
        RequireOpCommand = 2,
        Console = 3,
        JavaScriptCode = 4,
        JavaScriptFile = 5,
        Menu = 6,
        AdministratorMenu = 7
    }
}
