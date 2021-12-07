using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PCAxis.Paxiom;
using PX.Plugin.Interfaces;

namespace PCAxis.Desktop
{
    /// <summary>
    /// Holds information about a database that can be opened from PxWin
    /// </summary>
    public class DatabaseInfo : IDatabaseInfo
    {
        public string Id { get; set; }
        public string  Name { get; set; }
        public string Type;
        public string DefaultLanguage;
        public List<string> Languages = new List<string>();
        public Dictionary<string, string> Params = new Dictionary<string, string>();

        public const string ID = "id";
        public const string TYPE = "type";
        public const string NAME = "name";

        // PX-file database parameter
        public const string PATH = "path";

        // API parameters
        public const string BASEURL = "baseUrl";
        public const string SUPPORTED_LANGUAGES = "supportedLanguages";

        // Protected database
        public const string AUTHORIZATION_METHOD = "authorizationMethod";

        /// <summary>
        /// Get value of specified parameter
        /// </summary>
        /// <param name="param">Parameter to get value for</param>
        /// <returns>Parameter value if parameter exists, else ""</returns>
        public string GetParam(string param)
        {
            return Params.ContainsKey(param) ? Params[param] : "";
        }

        public IPXModelBuilder CreateBuilder(string table) 
        {
            IPXModelBuilder builder = null;
            if (Type.Equals("CNMM"))
            {
                builder = new PCAxis.PlugIn.Sql.PXSQLBuilder();
                builder.SetPath(table);
            }
            else if (Type.Equals("PX"))
            {
                builder = new PCAxis.Paxiom.PXFileBuilder();
                string pathWithoutDb = GetParam(DatabaseInfo.PATH).Substring(0, GetParam(DatabaseInfo.PATH).TrimEnd(new Char[] { '\\' }).LastIndexOf(@"\"));
                string path = System.IO.Path.Combine(pathWithoutDb, table);
                builder.SetPath(path);
            }
            return builder;
            
        }

        
        public string GetValue(string parameter)
        {
            return GetParam(parameter);
        }
    }
}
