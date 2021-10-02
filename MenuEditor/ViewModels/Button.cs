using Newtonsoft.Json;
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

        }

        public Button(string text, string image = null, ObservableCollection<Command> cmd = null)
        {
            Text = text;
            Image = image;
            Cmd = cmd;
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

        private ObservableCollection<Command> cmd;

        public ObservableCollection<Command> Cmd
        {
            get => cmd;
            set => SetProperty(ref cmd, value);
        }
    }
}
