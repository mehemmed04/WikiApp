using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikiApp.ProxyPattern
{
    public interface ICache
    {
        List<string> GetValue(string text);
        void SetData(string data);
    }
}
