using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MenuEditor.Services
{
    internal static class Configuration
    {
        static string Path = "./Data/configuration.json";

        static JsonDataService DataService = new JsonDataService();

        static Dictionary<string, object> Get(string name)
        {
            var config = DataService.LoadData<Dictionary<string, object>>(Path);
            return config;
        }

        static void Save(Dictionary<string, object> config)
        {
            DataService.SaveData(Path, config);
        }
    }
}
