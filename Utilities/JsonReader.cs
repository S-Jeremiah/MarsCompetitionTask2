using Newtonsoft.Json;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MARSCOMPETITION.Utilities
{
    public static class JsonReader
    {
        public static T LoadJson<T>(string fileName)
        {
            string filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            var json = File.ReadAllText(filepath);
            return JsonConvert.DeserializeObject<T>(json) ?? throw new InvalidOperationException($"Failed to deserialize JSON file: {fileName}");

        }
    }
}
