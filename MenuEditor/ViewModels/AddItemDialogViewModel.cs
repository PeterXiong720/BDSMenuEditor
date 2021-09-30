using Newtonsoft.Json;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuEditor.ViewModels
{
    class AddItemDialogViewModel : BindableBase
    {
        private bool _isDialogOPen;
        [JsonIgnore]
        public bool IsDialogOpen
        {
            get => _isDialogOPen;
            set => SetProperty(ref _isDialogOPen, value);
        }

        private Views.AddItemDialog _dialogContent;
        [JsonIgnore]
        public Views.AddItemDialog DialogContent
        {
            get => _dialogContent;
            set => SetProperty(ref _dialogContent, value);
        }
    }
}
