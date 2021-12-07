using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PX.Plugin.Interfaces;

namespace PX.Desktop.Interfaces
{
    public interface IHost
    {
        ILanguageService Language { get; }
        IDatabaseRegistry DbRegistry { get; }
        void Show(IToolWindow view);
        bool Show(string path, Dictionary<string,string> param);
        bool Show(string path);
        void ComposeParts(object obj);
        //void RegisterFileNotifier(string path, Action<string> callback, string callerId);
        void RegisterFileNotifier(string path, string callerId);
    }
}
