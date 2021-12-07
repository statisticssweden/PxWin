using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PX.Plugin.Interfaces
{
    public interface IDatabaseRegistry
    {
        bool Add(string id, string type, Dictionary<string, string> parameters);
        IDatabaseInfo Find(string id);
    }
}
