using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuEditor.Services
{
    interface IDataService
    {
        public TModel LoadData<TModel>(string path);
        public bool SaveData<TModel>(string path, TModel value);
    }
}
