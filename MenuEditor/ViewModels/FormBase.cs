using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuEditor.ViewModels
{
    public class FormBase : BindableBase
    {
        private string fileName;

        public string FileName
        {
            get => fileName;
            set
            {
                if (value == null) value = "";
                SetProperty(ref fileName, value);
            }
        }

        private string title;

        public string Title
        {
            get => title;
            set
            {
                if (value == null) value = "";
                SetProperty(ref title, value.Trim());
            }
        }

        private string content;

        public string Content
        {
            get => content;
            set
            {
                if(value == null) value = "";
                SetProperty(ref content, value);
            }
        }
    }
}
