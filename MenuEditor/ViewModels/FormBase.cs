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
                SetProperty(ref fileName, value);
            }
        }

        private string title;

        public string Title
        {
            get => title;
            set
            {
                SetProperty(ref title, value.Trim());
            }
        }

        private string content;

        public string Content
        {
            get => content;
            set
            {
                SetProperty(ref content, value);
            }
        }
    }
}
