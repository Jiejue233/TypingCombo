using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.IO;
using System.Threading.Tasks;
using System.Xml;

namespace TypingCombo.src.Helpers
{
    public static class JsonHelper
    {
        private static readonly string DEFAULT_CONFIG_PATH = "Assets/Configs/config.json";
        public static void WriteConfig<T>(T config, string? path = null)
        {
            string jsonContent = JsonSerializer.Serialize(config, new JsonSerializerOptions() { WriteIndented = true });
            if (path != null)
            {
                File.WriteAllText(path, jsonContent);
            }
            else
            {
                File.WriteAllText(DEFAULT_CONFIG_PATH, jsonContent);
            }
        }
        public static T ReadConfig<T>(string? path = null)
        {

            string jsonContent;
            if (path != null)
            {
                if (File.Exists(path))
                {
                    jsonContent = File.ReadAllText(path);
                }
                else
                {
                    throw new FileNotFoundException($"there is no such file in {path}");
                }
            }
            else
            {
                jsonContent = File.ReadAllText(DEFAULT_CONFIG_PATH);
            }
            return JsonSerializer.Deserialize<T>(json: jsonContent);
        }
    }
}
