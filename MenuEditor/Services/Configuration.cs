using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Prism.Common;

namespace MenuEditor.Services
{
    internal static class Configuration
    {
        static string Path = "./Data/configuration.json";

        static JsonDataService DataService = new JsonDataService();

        static Dictionary<string, object> config = DataService.LoadData<Dictionary<string, object>>(Path);

        public static Dictionary<string,object> Get()
        {
            return config;
        }

        public static T GetValue<T>(string name)
        {
            T value;
            if (config.TryGetValue(name, out value)) 
            {
                return value;
            }
            else
            {
                return default;
            }
        }

        public static bool SaveValue<T>(string key, T value)
        {
            T old;
            bool result = false;
            if (config.TryGetValue(key, out old))
            {
                config[key] = value;
                result = true;
            }
            else
            {
                result = config.TryAdd(key, value);
            }
            result = Save() && result;
            return result;
        }

        public static bool Save()
        {
            return DataService.SaveData(Path, config);
        }
    }
}
