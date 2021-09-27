using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuEditor.Models
{
    public class ModalDialog: Models.IForm
    {
        public ModalDialog(string title, string content = "", string button1 = "确定", string button2 = "取消")
        {
            this.Title = title;
            this.Content = content;
            this.Button1 = button1;
            this.Button2 = button2;
        }

        public string FileName { get; set; }

        private string title;
        public string Title
        {
            get => title;
            set => title = value.Trim();
        }

        public string Content { get; set; }

        public string Button1 { get; set; }

        public string Button2 { get; set; }
    }
}
