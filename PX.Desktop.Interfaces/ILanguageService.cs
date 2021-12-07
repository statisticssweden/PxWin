using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PX.Desktop.Interfaces
{
    public interface ILanguageService
    {
        string GetString(string key);
        string CurrentLanguage { get ; }
    }
}
