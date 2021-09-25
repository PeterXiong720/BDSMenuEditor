using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace MenuEditor.Services
{
    class JsonDataService : IDataService
    {
        public TModel LoadData<TModel>(string path)
        {
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                var result = JsonConvert.DeserializeObject<TModel>(json);
                return result;
            }
            else
            {
                return default;
            }
        }

        public bool SaveData<TModel>(string path, TModel value)
        {
            using (var fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
            {
                if (value == null)
                {
                    return false;
                }
                string json = JsonConvert.SerializeObject(value);
                fs.Write(Encoding.Default.GetBytes(json));
                return true;
            }
        }
    }
}
