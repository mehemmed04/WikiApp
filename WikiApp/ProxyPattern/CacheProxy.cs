using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikiApp.ProxyPattern
{
    public class CacheProxy : ICache
    {
        private List<string> CachedConfiguration;
        public CacheProxy()
        {
            CachedConfiguration = FakeRepo.GetDatas();
        }
        public List<string> GetValue(string key)
        {
            var List = new List<string>();
            foreach (var item in CachedConfiguration)
            {
                if (List.Count <= 10)
                {
                    if (item.StartsWith(key))
                    {
                        List.Add(item);
                    }
                }
                else
                {
                    break;
                }
            }
            return List;
        }

        public void SetData(string data)
        {
            CachedConfiguration.Insert(0, data);
        }
    }
}
