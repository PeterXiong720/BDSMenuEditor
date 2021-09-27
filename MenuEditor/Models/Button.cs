using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuEditor.Models
{
    public class Button
    {
        public Button(string text, string iamge = "", ButtonType type = ButtonType.Command, string execute = "")
        {
            this.Text = text;
            this.Image = iamge;
            this.Type = type;
            this.Execute = execute;
        }

        public string Text { get; set; }

        public string Image { get; set; }

        public ButtonType Type { get; set; }

        public string Execute { get; set; }
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
