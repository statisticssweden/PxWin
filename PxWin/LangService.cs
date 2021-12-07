using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using PX.Desktop.Interfaces;

namespace PCAxis.Desktop
{
    [Export(typeof(ILanguageService))]
    class LangService : ILanguageService
    {
        public string GetString(string key)
        {
            return Lang.GetLocalizedString(key);
        }


        public string CurrentLanguage
        {
            get { return Lang.CurrentLanguage(); }
        }
    }
}
