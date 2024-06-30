using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypingCombo.src.Helpers
{
    public static class FolderExplorer
    {
        public static List<string> Explore(string path, string? extension = null)
        {
            if (!Directory.Exists(path)){
                throw new DirectoryNotFoundException($"{path} this folder is not exist");
            }
            List<string> result = new List<string>();
            if (string.IsNullOrEmpty(extension))
            {
                foreach (string directory in Directory.GetDirectories(path))
                {
                    result.Add(directory);
                }
            }
            else
            {
                foreach (string file in Directory.GetFiles(path, $"*{extension}"))
                {
                    result.Add(file);
                }
            }
            return result;
        }
    }
}
