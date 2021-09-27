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

        }

        public ModalDialogVewModel(string filename, string title, string content, string btn1, string btn2)
        {
            FileName = filename;
            Title = title;
            Content = content;
            ButtonOne = btn1;
            ButtonTwo = btn2;
        }

        private string btn1;

        public string ButtonOne
        {
            get => btn1;
            set
            {
                SetProperty(ref btn1, value);
            }
        }

        private string btn2;

        public string ButtonTwo
        {
            get => btn2;
            set
            {
                SetProperty(ref btn2, value);
            }
        }
        
        [JsonIgnore]
        public Views.EditModalDialog EditModalDialog { get; set; }
    }
}
