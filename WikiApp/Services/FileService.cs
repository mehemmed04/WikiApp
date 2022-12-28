using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikiApp.Services
{
    public class FileService
    {
        public static string _path = @"~/../../../Repo/SearchedPageIds.txt";
        public static void WriteToFile(string text)
        {
            var list = ReadAllDataAsListFromFile();
            if (!list.Contains(text))
            {
                var alltext = ReadFromFile();
                var newtxt = text + "\n";
                alltext += newtxt;
                File.WriteAllText(_path, alltext);
            }
        }
        public static string ReadFromFile()
        {
            return File.ReadAllText(_path);
        }
        public static List<string> ReadAllDataAsListFromFile()
        {
            var alltext = ReadFromFile();
            var list = alltext.Split('\n').ToList();
            return list;
        }
    }
}
