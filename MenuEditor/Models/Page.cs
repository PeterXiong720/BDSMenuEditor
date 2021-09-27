using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuEditor.Models
{
    public class Page : Models.IForm
    {
        public Page(string title) : this(title, "") { }

        public Page(string title, string content) : this(title, content, new List<Button>()) { }

        public Page(string title, List<Button> buttons) : this(title, "", buttons) { }

        public Page(string title, string content, List<Button> buttons)
        {
            this.Title = title;
            this.Content = content;
            this.Buttons = buttons;
        }

        public string FileName { get; set; }

        private string title;
        public string Title
        {
            get => title;
            set => title = value.Trim();
        }

        public string Content { get; set; }

        public List<Button> Buttons { get; set; }

    }
}
