using Newtonsoft.Json;
using Prism.Commands;
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
            ChangeEditSapceContentCommand = new DelegateCommand<string>(new Action<string>(ChangeContent));
        }

        public ModalDialogVewModel(string filename, string title, string content, Button btn1, Button btn2) : this()
        {
            FileName = filename;
            Title = title;
            Content = content;
            ButtonOne = btn1;
            ButtonTwo = btn2;
        }

        private Button btn1 = new()
        {
            Text = "确定",
            ButtonExecutes = new() { }
        };

        public Button ButtonOne
        {
            get => btn1;
            set => SetProperty(ref btn1, value);
        }

        private Button btn2 = new()
        {
            Text = "取消",
            ButtonExecutes = new() { }
        };

        public Button ButtonTwo
        {
            get => btn2;
            set => SetProperty(ref btn2, value);
        }

        [JsonIgnore]
        public Views.EditModalDialog EditModalDialog { get; set; }

        private Views.EditButton _EditButtonContent;
        [JsonIgnore]
        public Views.EditButton EditButtonContent
        {
            get => _EditButtonContent;
            set
            {
                _ = SetProperty(ref _EditButtonContent, value);
            }
        }

        public DelegateCommand<string> ChangeEditSapceContentCommand { get; set; }
        private void ChangeContent(string btn)
        {
            if (btn == "button1")
            {
                EditButtonContent = ButtonOne.EditButtonContent;
            }
            else if (btn == "button2")
            {
                EditButtonContent = ButtonTwo.EditButtonContent;
            }
        }
    }
}
