using Prism.Mvvm;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuEditor.ViewModels
{
    class PageViewModel: BindableBase
    {
        public PageViewModel(Models.Page page, Views.EditMenu menu)
        {
            Page = page;
        }

        public Models.Page Page { get; set; }

        public string FileName
        {
            get => Page.FileName;
            set
            {
                Page.FileName = value;
                this.RaisePropertyChanged();
            }
        }

        public string Title
        {
            get => Page.Title;
            set
            {
                Page.Title = value;
                this.RaisePropertyChanged();
            }
        }

        public string Content
        {
            get => Page.Content;
            set
            {
                Page.Content = value;
                this.RaisePropertyChanged();
            }
        }


        public Views.EditMenu EditMenu { get; set; }

    }
}
