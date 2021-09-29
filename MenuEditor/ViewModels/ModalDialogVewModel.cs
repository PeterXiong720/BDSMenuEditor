using Newtonsoft.Json;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuEditor.ViewModels
{
    public class ModalDialogVewModel : FormBase
    {
        public ModalDialogVewModel()
        {
            //EditModalDialog = new Views.EditModalDialog(this);
        }

        public ModalDialogVewModel(string filename, string title, string content, Button btn1, Button btn2) : this()
        {
            FileName = filename;
            Title = title;
            Content = content;
            ButtonOne = btn1;
            ButtonTwo = btn2;
        }

        private Button btn1;

        public Button ButtonOne
        {
            get => btn1;
            set => SetProperty(ref btn1, value);
        }

        private Button btn2;

        public Button ButtonTwo
        {
            get => btn2;
            set => SetProperty(ref btn2, value);
        }

        [JsonIgnore]
        public Views.EditModalDialog EditModalDialog { get; set; }
    }
}
