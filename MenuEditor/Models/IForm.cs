using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuEditor.Models
{
    interface IForm
    {
        public string FileName { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }
    }
}
