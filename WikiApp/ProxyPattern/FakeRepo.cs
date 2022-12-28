using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikiApp.ProxyPattern
{

    public class FakeRepo
    {
        public static List<string> GetDatas()
        {
            var result = File.ReadAllText(@"~/../../../Repo/Words.txt").Split('\n');
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = result[i].Remove(result[i].Length - 2, 2);
            }
            return result.ToList();
        }
    }
}
