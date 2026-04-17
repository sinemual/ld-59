using System.IO;
using Client.DevTools.MyTools;
using Newtonsoft.Json;
using UnityEngine;

namespace Client.Infrastructure.Services
{
    public class JsonSaveLoadService : ISaveLoadService
    {
        public T Load<T>(string identification = "") where T : class
        {
            var filename = $"{typeof(T)}{identification}.json";
            string dataPath = Path.Combine(Utility.GetDataPath(), filename);
            if (File.Exists(dataPath))
            {
                string json = File.ReadAllText(dataPath);
                return JsonConvert.DeserializeObject<T>(json);
            }
            return null;
        }

        public void Save<T>(T obj, string identification = "")
        {
            var filename = $"{typeof(T)}{identification}.json";
            string dataPath = Path.Combine(Utility.GetDataPath(), filename);
            if (!Directory.Exists(Utility.GetDataPath()))
                Directory.CreateDirectory(Utility.GetDataPath());
            string json = JsonConvert.SerializeObject(obj, Formatting.Indented);
            File.WriteAllText(dataPath, json);
        }
    }
}