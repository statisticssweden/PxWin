using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PX.Plugin.Interfaces;
using PX.Plugin.Interfaces.Attributes;
using System.ComponentModel.Composition;
using PCAxis.Menu;

namespace PX.Api.Client
{
    [Export(typeof(IDataSource))]
    [DataSourceMetadata(SourceType = "PXAPI")]
    public class ApiDataSource : IDataSource
    {
        public PCAxis.Menu.PxMenuBase CreateMenu(IDatabaseInfo dbi, string menu, string selection, string language)
        {
            if (string.IsNullOrEmpty(dbi.GetValue("baseUrl")))
            {
                return null;
            }

            PxMenuBase retMenu;
            if (string.IsNullOrEmpty(menu) && string.IsNullOrEmpty(selection))
            {
                retMenu = new ApiMenu(dbi.GetValue("baseUrl"), "", language);
            }
            else
            {
                retMenu = new ApiMenu(dbi.GetValue("baseUrl"), menu + "/" + selection, language);
            }

            return retMenu;
        }

        public PCAxis.Paxiom.IPXModelBuilder CreateBuilder(IDatabaseInfo dbi, string menu, string selection, string language)
        {
            char[] separator = new char[] { '/' };
            var builder = new ApiModelBuilder();
            
            StringBuilder path = new StringBuilder();
            string baseUrl = dbi.GetValue("baseUrl");

            if (!string.IsNullOrEmpty(baseUrl))
            {
                baseUrl = baseUrl.TrimEnd(separator);
                path.Append(string.Format(baseUrl, language));
                
                if (!string.IsNullOrEmpty(menu))
                {
                    menu = menu.TrimStart(separator);
                    menu = menu.TrimEnd(separator);

                    path.Append("/");
                    path.Append(menu);
                }
                
                if (!string.IsNullOrEmpty(selection))
                {
                    selection = selection.TrimStart(separator);

                    path.Append("/");
                    path.Append(selection);
                }
            }

            //builder.SetPath(string.Format(baseUrl, language) + (baseUrl.EndsWith("/") ? "" : "/") + menu + "/" + selection);
            builder.SetPath(path.ToString());

            return builder;
        }


        public string GetSource(IDatabaseInfo dbi, PCAxis.Paxiom.PXModel model, string language)
        {
            string baseUrl = string.Format(dbi.GetValue("baseUrl"), language);

            string ret = model.Meta.MainTable.Substring(baseUrl.Length);
           
            return ret;
        }
    }
}
