using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace PCAxis.Desktop
{
    /// <summary>
    /// Handles predefined databases in file databases.config
    /// </summary>
    public class DatabaseRepository
    {
        private static DatabaseRepository _current;
        private Dictionary<string, DatabaseInfo> _databases;
        private DatabaseInfo _defaultDb;

        public static DatabaseRepository Current
        {
            get
            {
                if (_current == null)
                {
                    _current = new DatabaseRepository();
                }
                return _current;
            }
        }

        public Dictionary<string, DatabaseInfo> Databases
        {
            get
            {
                return _databases;
            }
        }

        /// <summary>
        /// Private constructor
        /// </summary>
        private DatabaseRepository()
        {
            CreateRepository();
        }

        /// <summary>
        /// create database repository from the databases.config file
        /// </summary>
        private void CreateRepository()
        {
            _databases = new Dictionary<string, DatabaseInfo>();
            CreateDefaultDb();

            string config = System.Configuration.ConfigurationManager.AppSettings.Get("databasesConfigFile");
            if (!Path.IsPathRooted(config))
            {
                config = Path.Combine(Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory), config);
            }


            DatabaseInfo di;

            if (System.IO.File.Exists(config))
            {
                System.Xml.XmlDocument doc = new XmlDocument();
                doc.Load(config);

                foreach (System.Xml.XmlNode xnode in doc.SelectNodes("/databases/database"))
                {
                    di = ReadDatabaseConfig(xnode);

                    if (di != null && !string.IsNullOrEmpty(di.Type))
                    {
                        switch (di.Type.ToUpper())
                        {
                            case "CNMM":
                                var db = PCAxis.Sql.DbConfig.SqlDbConfigsStatic.DataBases[di.Id];
                                di.DefaultLanguage = db.Database.MainLanguage.code;
                                foreach (Sql.DbConfig.LanguageType lang in db.Database.Languages)
                                {
                                    di.Languages.Add(lang.code);
                                }
                                break;
                            case "PX":
                                string path = di.Params[DatabaseInfo.PATH];
                                path = System.IO.Path.Combine(path, "Menu.xml");
                                if (System.IO.File.Exists(path))
                                {
                                    System.Xml.XmlDocument dbFile = new XmlDocument();
                                    dbFile.Load(path);

                                    XmlNodeList nodeList = dbFile.SelectNodes("/Menu/Language");

                                    foreach (XmlNode node in nodeList)
                                    {
                                        di.Languages.Add(node.Attributes["lang"].Value);
                                        if (node.Attributes["default"].Value.ToLower().Equals("true"))
                                        {
                                            di.DefaultLanguage = node.Attributes["lang"].Value;
                                        }
                                    }
                                }
                                break;
                            case "PXAPI":
                                if (!di.Params.ContainsKey(DatabaseInfo.BASEURL) || !di.Params.ContainsKey(DatabaseInfo.SUPPORTED_LANGUAGES))
                                {
                                    continue;
                                }
                                if (string.IsNullOrEmpty(di.Params[DatabaseInfo.BASEURL]) || string.IsNullOrEmpty(di.Params[DatabaseInfo.SUPPORTED_LANGUAGES]))
                                {
                                    continue;
                                }

                                var langs = di.Params[DatabaseInfo.SUPPORTED_LANGUAGES].Split(',');
                                //var pxwinLangs = LanguageHelper.GetLanguages();

                                foreach (string lang in langs)
                                {
                                    //string url = string.Format(di.Params[DatabaseInfo.BASEURL], lang);

                                    //var http = new HttpClient();
                                    //try
                                    //{
                                    //    var response = http.GetAsync(url).Result;

                                    //    if (response.IsSuccessStatusCode)
                                    //    {
                                            di.Languages.Add(lang);
                                            if (LanguageHelper.IsDefaultLanguage(lang))
                                            {
                                                di.DefaultLanguage = lang;
                                            }
                                    //    }
                                    //}
                                    //catch (Exception)
                                    //{
                                    //    continue;
                                    //}
                                }

                                if (string.IsNullOrEmpty(di.DefaultLanguage) && di.Languages.Count > 0)
                                {
                                    if (di.Languages.Contains("en"))
                                    {
                                        di.DefaultLanguage = "en";
                                    }
                                    else
                                    { 
                                        di.DefaultLanguage = di.Languages[0];
                                    }
                                }

                                break;
                            default:
                                continue;
                        }

                        _databases.Add(di.Id, di);
                    }
                }
            }
        }

        /// <summary>
        /// Read information for one database from the database configuration file
        /// </summary>
        /// <param name="node">XML-node containing database info</param>
        /// <returns>DatabaseInfo object</returns>
        private DatabaseInfo ReadDatabaseConfig(System.Xml.XmlNode node)
        {
            DatabaseInfo info = new DatabaseInfo();

            try
            {
                info.Id = node.Attributes[DatabaseInfo.ID].Value;
                info.Type = node.Attributes[DatabaseInfo.TYPE].Value;
                info.Name = node.Attributes[DatabaseInfo.NAME].Value;

                if (node.SelectSingleNode("params") != null)
                {
                    foreach (System.Xml.XmlNode cnode in node.SelectSingleNode("params").ChildNodes)
                    {
                        info.Params.Add(cnode.Name, cnode.InnerText);
                    }
                }
            }
            catch (Exception)
            {
                info = null;
            }

            return info;
        }

        /// <summary>
        /// Create the default database object
        /// </summary>
        private void CreateDefaultDb()
        {
            var di = new DatabaseInfo();
            di.Id = "FileSystem";
            di.DefaultLanguage = string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings.Get("defaultLanguage")) ? "en" : System.Configuration.ConfigurationManager.AppSettings.Get("defaultLanguage");
            di.Name = Lang.GetLocalizedString("OpenTableFileSystem");
            di.Params.Add(DatabaseInfo.PATH, "");
            di.Type = "PX";
            _databases.Add(di.Id, di);
            _defaultDb = di;
        }

        /// <summary>
        /// Get the specified database
        /// </summary>
        /// <param name="id">Id of the database</param>
        /// <returns>DatabaseInfo object for the specified database. If the spedcified database is not contained in the repository the default DatabaseInfo object is returned</returns>
        public DatabaseInfo GetDatabase(string id)
        {
            if (id == null)
            {
                return _defaultDb;
            }


            if (_databases.ContainsKey(id))
            {
                return _databases[id];
            }
            return _defaultDb;
        }
    }
}
