using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuEditor.Models
{
    class RawMenu
    {
        public RawMenu()
        {
            this.Pages = new List<Page>();
            this.ModalDialogs = new List<ModalDialog>();
        }
        public List<Models.Page> Pages { get; set; }
        public List<ModalDialog> ModalDialogs { get; set; }
    }
}
