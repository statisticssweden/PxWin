using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace PCAxis.Desktop
{
    class VariableFilterHelper
    {
        private static string _baseDir;
        /// <summary>
        /// Takes a list of values codes and create a xml file
        /// </summary>
        /// <param name="valueCodes"></param>
        /// <param name="domain"></param>
        /// <param name="filterName"></param>
        public static bool SaveFilter(List<string> valueCodes, string domain, string filterName)
        {
            StringBuilder fileName = new StringBuilder();
            fileName.Append(filterName);
            fileName.Append(".xml");

            if (!FilterExists(fileName.ToString()))
            {
                XmlDocument xdoc = new XmlDocument();
                string pxDate = DateTime.Now.ToString("yyyyMMdd HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                xdoc.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\" ?><variablefilter><name>" + filterName + "</name><domain>" + domain + "</domain><created>" + pxDate + "</created><values><value></value></values></variablefilter>");

                string xpath = "//values";
                XmlNode root = xdoc.SelectSingleNode(xpath);
                root.RemoveAll();

                foreach (String node in valueCodes)
                {
                    AddXmlNode(xdoc, root, node);
                }

                xdoc.Save(System.IO.Path.Combine(_baseDir, fileName.ToString()));
                return true;
            }
            else
            {
                return false;
            }
            
        }
        /// <summary>
        /// Append nodes to the xml document containing the valuecode for the selected 
        /// value
        /// </summary>
        /// <param name="xdoc"></param>
        /// <param name="xmlNode"></param>
        /// <param name="treeNode"></param>
        private static void AddXmlNode(XmlDocument xdoc, XmlNode xmlNode, string treeNode)
        {
            XmlNode node = xdoc.CreateNode(XmlNodeType.Element, "value", null);

            XmlAttribute att = xdoc.CreateAttribute("code");
            att.Value = treeNode;
            node.Attributes.Append(att);

            xmlNode.AppendChild(node);
        }

        /// <summary>
        /// Return a list of value codes imported from file
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static List<string> ImportValues(string fileName ) 
        {
            var valueCode = new List<string>();
            try
            {
                using (var sr = new StreamReader(fileName, System.Text.Encoding.Default))
                {
                    string s;
                    char delimiter = ';';
                    while ((s = sr.ReadLine()) != null)
                    {
                        var textRow = s.Split(delimiter);;
                        valueCode.AddRange(textRow);
                    }

                }
            }
            catch (Exception e)
            {
                throw;
            }
            return valueCode;
        }
        /// <summary>
        /// Check if the filter allrady exist in the folder filter.
        /// </summary>
        /// <param name="filterName">The name of the filter and filter file</param>
        /// <returns></returns>
        public static bool FilterExists(string filterName)
        {
            CreateFilterFolder();
            var path = System.IO.Path.Combine(_baseDir, filterName + ".xml");

            if (File.Exists(path))
            {
                return true;
            }
            else
            {
                return false;
            }
                
        }
        /// <summary>
        /// Create folder for filter if no one exist
        /// </summary>
        private static void CreateFilterFolder()
        {
            var dir = Directory.GetParent(System.Windows.Forms.Application.UserAppDataPath);
            _baseDir = Path.Combine(dir.ToString(), "variableFilter");

            if (!Directory.Exists(_baseDir))
            {
                Directory.CreateDirectory(_baseDir);
            }        
        }
        /// <summary>
        /// Fetch all filter from the personal directory.
        /// </summary>
        /// <returns>A List contaning one to manny variablefilter objects. 
        /// Namne, path to the filter and if the filter is global or not</returns>
        public static List<VariableFilter> GetAllPersonalFilterWithoutValues()
        { 
            List<VariableFilter> personalFilter = new List<VariableFilter>();
            VariableFilter variableFilter ;
            CreateFilterFolder();

            string [] files = Directory.GetFiles(_baseDir,"*.xml");

            foreach (string f in files)
            {
                variableFilter = new VariableFilter();
                variableFilter.FilterName = Path.GetFileNameWithoutExtension(f);
                variableFilter.Path = f;
                variableFilter.GlobalFilter = false;
                personalFilter.Add(variableFilter);
            }

            return personalFilter;
        }

        /// <summary>
        /// Fetch all filter in the global directory.
        /// </summary>
        /// <returns>A List contaning one to manny variablefilter objects. 
        /// Namne, path to the filter and if the filter is global or not</returns>
        public static List<VariableFilter> GetAllGlobalFilterWithoutValues()
        {
            List<VariableFilter> commonFilter = new List<VariableFilter>();
            VariableFilter variableFilter;
            string path = System.Configuration.ConfigurationManager.AppSettings.Get("globalFilter");

            if (!string.IsNullOrEmpty(path) && System.IO.Directory.Exists(path))
            {
                string[] files = Directory.GetFiles(path, "*.xml");

                foreach (string f in files)
                {
                    variableFilter = new VariableFilter();
                    variableFilter.FilterName = Path.GetFileNameWithoutExtension(f);
                    variableFilter.Path = f;
                    variableFilter.GlobalFilter = true;
                    commonFilter.Add(variableFilter);
                }
            }

            return commonFilter;
        }
        /// <summary>
        /// Fetch all filter from the personal and the global directory.
        /// </summary>
        /// <returns>A List contaning one to manny variablefilter objects. 
        /// Namne, path to the filter and if the filter is global or not</returns>
        public static List<VariableFilter> GetAllFilterWithoutValues()
        {
            List<VariableFilter> allFilter = new List<VariableFilter>();

            allFilter = GetAllPersonalFilterWithoutValues().Concat(GetAllGlobalFilterWithoutValues()).ToList();

            return allFilter;
        }
        /// <summary>
        /// Remove/Delete the file connected to the varaibalefilter objekt
        /// </summary>
        /// <param name="variableFilter">The varaiableFilter object to remove</param>
        /// <returns>If the file was removed</returns>
        public static bool  DeleteVariableFilterFile(VariableFilter variableFilter)
        {
            bool removed = false;

            if (!string.IsNullOrEmpty(variableFilter.Path) && File.Exists(variableFilter.Path) && ! variableFilter.GlobalFilter)
            {
                try
                {
                    File.Delete(variableFilter.Path);
                    return removed = true;
                }
                catch (Exception e)
                {

                    return removed;
                }                

            }
            return removed;
        }

    }
}
