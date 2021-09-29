using Newtonsoft.Json;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuEditor.ViewModels
{
    public class Button : BindableBase
    {
        public Button()
        {

        }

        public Button(string text, string image = "", ButtonType type = ButtonType.Command, string execute = "")
        {
            Text = text;
            Image = image;
            Type = type;
            Execute = execute;
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

        private ButtonType type;

        public ButtonType Type
        {
            get => type;
            set => SetProperty(ref type, value);
        }

        private string execute;

        public string Execute
        {
            get => execute;
            set => SetProperty(ref execute, value);
        }

        private string[] types = System.Enum.GetNames(typeof(ButtonType));
        [JsonIgnore]
        public string[] Types
        {
            get => types;
            set => SetProperty(ref types, value);
        }

    }

    public enum ButtonType
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
