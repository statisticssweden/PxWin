using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PCAxis.Paxiom;
using PCAxis.Query;
using PCAxis.Excel;
using PCAxis.Serializers.JsonStat;
using PX.Plugin.Interfaces.Attributes;
using PX.Plugin.Interfaces;
using System.IO;

namespace PCAxis.Desktop
{
    /// <summary>
    /// Class for encapsulation the loading, running saved queries. Also handles the serialization of the result to different 
    /// output formats.
    /// </summary>
    public class SavedQueryResult
    {
        public class SerializerInfo
        {
            public Func<IPXModelStreamSerializer> CreateSerializer { get; set; }
            public ISerializerMetadata Metadata { get; set; }    

           
        }
        private PXModel _model;
        private TableQuery _tableQuery;
        private PCAxis.Query.SavedQuery _savedQuery;
        private string _filename;
        private static Dictionary<string, SerializerInfo> _serializerRegister;
        private static Dictionary<string, IDataSource> _datasourceRegister;

        public static bool AddSerializer(Func<IPXModelStreamSerializer> creator, ISerializerMetadata metadata)
        {
            if (_serializerRegister.ContainsKey(metadata.Id))
            {
                return false;
            }
            _serializerRegister.Add(metadata.Id,new SerializerInfo(){CreateSerializer = creator, Metadata = metadata});
            return true;
        }

        public static bool AddDatasource(string sourceType, IDataSource datasource)
        {
            if (_datasourceRegister.ContainsKey(sourceType))
            {
                return false;
            }
            _datasourceRegister.Add(sourceType, datasource);
            return true;
        }

        static SavedQueryResult()
        {
            _serializerRegister = new Dictionary<string, SerializerInfo>();
            _datasourceRegister = new Dictionary<string,IDataSource>();
        }
        /// <summary>
        /// Create SavedQueryResult object from a saved query file
        /// </summary>
        /// <param name="filename">Path to the saved query file</param>
        /// <returns>SavedQueryResult object</returns>
        public static SavedQueryResult Create(string filename)
        {
            SavedQueryResult res = new SavedQueryResult();
            PCAxis.Query.SavedQuery sq = null;

            sq = LoadSavedQuery(filename);

            res.SavedQuery = sq;
            res.Model = res.LoadData(sq);
            res.Model = QueryHelper.RunWorkflow(sq, res.Model);
            res._filename = filename;
            return res;
        }

        private static Query.SavedQuery LoadSavedQuery(string path)
        {
            if (File.Exists(path))
            {
                string query = File.ReadAllText(path);
                Query.SavedQuery sq = JsonHelper.Deserialize<Query.SavedQuery>(query) as Query.SavedQuery;
                return sq;
            }

            return null;
        }

        /// <summary>
        /// Save the model to the destination folder in the specified format
        /// </summary>
        /// <param name="destination">Destination folder</param>
        /// <param name="format">Output format</param>
        /// <returns>True if the model could be saved successfully, else false</returns>
        public bool Save(string destination, string format)
        {
            //TODO implement properly just test code right now
            //System.Threading.Thread.Sleep(400);

            //if (_model.Meta.Matrix == "BE0101F1") return false;

            if (!System.IO.Directory.Exists(destination))
            {
                return false;
            }

            IPXModelStreamSerializer serializer = null;
            string extension = "";

            var serInfo = _serializerRegister[format];
            if (serInfo == null)
            {
                return false;
            }
            serializer = serInfo.CreateSerializer();
            extension = "." + serInfo.Metadata.Extension;


            string sqName = System.IO.Path.GetFileNameWithoutExtension(_filename);
            //string path = System.IO.Path.Combine(destination, _model.Meta.Matrix + extension);
            string path = GetResultPath(destination, sqName, extension);
            serializer.Serialize(_model, path);

            return true;
        }

        /// <summary>
        /// Get result path. Combines destination path, saved query name and file extension. If a file with that name already exist an ordinal number is added 
        /// to the saved query name until we get an unique filename.
        /// </summary>
        /// <param name="destination">Destination path</param>
        /// <param name="sqName">Name of the saved query</param>
        /// <param name="extension">File extension</param>
        /// <returns>A unique file path</returns>
        private string GetResultPath(string destination, string sqName, string extension)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(sqName);
            sb.Append(extension);
            string path = System.IO.Path.Combine(destination, sb.ToString());
            //int i = 1;

            //while (System.IO.File.Exists(path))
            //{
            //    sb.Clear();
            //    sb.Append(sqName);
            //    sb.Append("_");
            //    sb.Append(i.ToString());
            //    sb.Append(extension);
            //    path = System.IO.Path.Combine(destination, sb.ToString());
            //    i++;
            //}

            return path;
        }

        /// <summary>
        /// PXModel object for the table after the saved query has been run
        /// </summary>
        public PXModel Model {
            get 
            { 
                return _model; 
            }
            set 
            {
                _model = value;
            }
        }

        /// <summary>
        /// SavedQuery object for the table after the saved query has been run
        /// </summary>
        public PCAxis.Query.SavedQuery SavedQuery
        {
            get
            {
                return _savedQuery;
            }
            set
            {
                _savedQuery = value;
            }
        }
        public TableQuery TableQuery
        {
            get
            {
                return _tableQuery;
            }
            set
            {
                _tableQuery = value;
            }
        }


        private PXModel LoadData(PCAxis.Query.SavedQuery sq)
        {
            //Only loads the first table source otherwise redirects to a page
            if (sq.Sources.Count != 1)
            {
                //TODO redirect to error page incopatable query for PX-Web
            }

            TableSource src = sq.Sources[0];

            IPXModelBuilder builder = null;
            string path = "";

            //TODO do this for all table sources.
            //Validates that the user has the rights to access the table
            if (!AuthorizationUtil.IsAuthorized(src.DatabaseId, null, src.Source)) { throw new Exception("Not authorized"); }

            if (_datasourceRegister[src.Type] == null) return null; //TODO redirect to error page incompatible datasource type
            builder = _datasourceRegister[src.Type].CreateBuilder(DatabaseRepository.Current.GetDatabase(src.DatabaseId), null, src.Source, src.Language);

            //if (src.Type == "CNMM")
            //{
            //    path = src.DatabaseId + ":" + src.Source;
            //    builder = new PCAxis.PlugIn.Sql.PXSQLBuilder();
            //}
            //else if (src.Type == "PX")
            //{
            //    // Mapped PX-File database - We need to combine database path and table path!
            //    DatabaseInfo dbInfo = DatabaseRepository.Current.GetDatabase(src.DatabaseId);
            //    char[] charsToTrim = { '\\' };
                
            //    if (string.IsNullOrWhiteSpace(dbInfo.Params[DatabaseInfo.PATH]))
            //    {
            //        path = src.Source.Replace("/", @"\");
            //    }
            //    else
            //    {
            //        path = System.IO.Path.Combine(dbInfo.Params[DatabaseInfo.PATH], src.Source.Replace("/", @"\").TrimStart(charsToTrim));
            //    }


            //    builder = new PCAxis.Paxiom.PXFileBuilder();
            //}
            //else
            //{
            //    //TODO redirect to error page incompatible datasource type
            //    return null;
            //}

            //builder.SetPath(path);
            builder.SetPreferredLanguage(src.Language);

            builder.BuildForSelection();
            var model = builder.Model;

            List<PCAxis.Paxiom.Selection> sel = new List<PCAxis.Paxiom.Selection>();

            foreach (var variable in model.Meta.Variables)
            {
                var query = src.Quieries.FirstOrDefault(q => q.Code == variable.Code);
                PCAxis.Paxiom.Selection s = null;

                if (query == null)
                {
                    //Selects all values for the variable if it can't be eliminated
                    s = new PCAxis.Paxiom.Selection(variable.Code);
                    if (variable.IsContentVariable || !variable.Elimination)
                    {
                        s.ValueCodes.AddRange(variable.Values.Select(v => v.Code).ToArray());
                    }
                }
                else
                {
                    //if ((query.Selection.Filter.StartsWith("agg:", StringComparison.InvariantCultureIgnoreCase)) || (query.Selection.Filter.StartsWith("agg_", StringComparison.InvariantCultureIgnoreCase)))
                    if (PCAxis.Query.QueryHelper.IsAggregation(query.Selection.Filter))
                    {
                        s = QueryHelper.SelectAggregation(variable, query, builder);
                    }
                    else if (query.Selection.Filter.StartsWith("vs:", StringComparison.InvariantCultureIgnoreCase))
                    {
                        s = QueryHelper.SelectValueSet(variable, query, builder);
                    }
                    else
                    {
                        switch (query.Selection.Filter)
                        {

                            case "item":
                                s = QueryHelper.SelectItem(variable, query);
                                break;
                            case "top":
                                s = QueryHelper.SelectTop(variable, query);
                                break;
                            case "from":
                                s = QueryHelper.SelectFrom(variable, query);
                                break;
                            case "all":
                                s = QueryHelper.SelectAll(variable, query);
                                break;
                            //case "agg":
                            //    s = SelectAggregation(variable, query, builder);
                            //    break;
                            //case "vs":
                            //    s = SelectValueSet(variable, query, builder);
                            //    break;
                            default:

                                //TODO unsupported filter 
                                break;
                        }
                    }
                }

                if (s != null)
                {
                    sel.Add(s);
                }
            }

            var selection = sel.ToArray();

            //TODO fixa till
            //if (sq.Output.Type == "SCREEN")
            //PCAxis.Query.TableQuery tbl = new PCAxis.Query.TableQuery(builder.Model, selection);
            //PaxiomManager.QueryModel = tbl;
            _tableQuery = new PCAxis.Query.TableQuery(builder.Model, selection);

            builder.BuildForPresentation(selection);

            return builder.Model;
        }

    }
}
