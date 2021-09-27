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
            if (value == null)
            {
                return false;
            }

            using (var sw = new StreamWriter(path, false, Encoding.UTF8))
            {
                string json = JsonConvert.SerializeObject(value);
                sw.AutoFlush = true;
                sw.Write(json);
                return true;
            }
        }
    }
}
