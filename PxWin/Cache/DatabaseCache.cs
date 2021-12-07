using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using PCAxis.Menu;
using PCAxis.Paxiom.Extensions;

namespace PCAxis.Desktop
{
    /// <summary>
    /// Class for handling caching of database menus
    /// </summary>
    public class DatabaseCache
    {
        private static DatabaseCache _current;
        private static string _baseDir;

        public static DatabaseCache Current
        {
            get
            {
                if (_current == null)
                {
                    _current = new DatabaseCache();
                }
                return _current;
            }
        }

        /// <summary>
        /// Private constructor
        /// </summary>
        private DatabaseCache()
        {
            _baseDir = Path.Combine(System.Windows.Forms.Application.UserAppDataPath, "cache");
            if (!Directory.Exists(_baseDir))
            {
                Directory.CreateDirectory(_baseDir);
            }
        }

        /// <summary>
        /// Save database to cache
        /// </summary>
        /// <param name="dbId"></param>
        /// <param name="dbLang"></param>
        /// <param name="tree"></param>
        /// <returns></returns>
        public bool Add(string dbId, string dbLang, TreeView tree)
        {
            if (!CacheEnabled())
            {
                return false;
            }

            string path = GetDbFilename(dbId, dbLang);

            XmlDocument xdoc = new XmlDocument();

            if (File.Exists(path))
            {
                xdoc.Load(path);
            }
            else
            {
                string pxDate = DateTime.Now.ToString("yyyyMMdd HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                //string createDate = DateTime.Now.ToString(System.Globalization.CultureInfo.InvariantCulture);
                xdoc.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\" ?><cache><created>" + pxDate + "</created><items></items></cache>");
            }

            string xpath = "//items";
            XmlNode root = xdoc.SelectSingleNode(xpath);
            root.RemoveAll();            

            foreach (TreeNode node in tree.Nodes)
            {
                AddXmlNode(xdoc, root, node);
            }

            xdoc.Save(path); // Overwrite any existing file...

            return true;
        }

        /// <summary>
        /// Get database from cache
        /// </summary>
        /// <param name="dbId">Id of the database</param>
        /// <param name="dbLang">Language</param>
        /// <param name="tree">Treeview control</param>
        /// <returns>True if the database could be retrieved from cache, else false</returns>
        public bool Get(string dbId, string dbLang, TreeView tree)
        {
            if (!CacheEnabled())
            {
                return false;
            }

            string dbFile = GetDbFilename(dbId, dbLang);

            if (File.Exists(dbFile))
            {
                XmlDocument xdoc = new XmlDocument();
                xdoc.Load(dbFile);

                if (CacheRefreshNeeded(xdoc))
                {
                    File.Delete(dbFile);
                    return false;
                }

                string xpath = "//items";
                XmlNode root = xdoc.SelectSingleNode(xpath);
                foreach (XmlNode node in root.SelectNodes("./item"))
                {
                    TreeNode treeNode = GetTreeNode(node);
                    tree.Nodes.Add(treeNode);
                    BuildSubNodes(node, treeNode);
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Removed database from cache so it can be reloaded
        /// </summary>
        /// <param name="dbId">Id of the database</param>
        /// <param name="dbLang">Language</param>
        public void Refresh(string dbId, string dbLang)
        {
            if (!CacheEnabled())
            {
                return;
            }

           string dbFile = GetDbFilename(dbId, dbLang);

            if (File.Exists(dbFile))
            {
                File.Delete(dbFile);
            }

        }

        /// <summary>
        /// Checks if database menu cache shall be used or not
        /// </summary>
        /// <returns>True if menu cache shall be used, else false</returns>
        public bool CacheEnabled()
        {
            bool enabled;

            if (System.Configuration.ConfigurationManager.AppSettings.Get("databaseMenuCacheEnabled") == null)
            {
                return true; // Enabled by default
            }
            else
            {
                if (!bool.TryParse(System.Configuration.ConfigurationManager.AppSettings.Get("databaseMenuCacheEnabled").ToString(), out enabled))
                {
                    return true; // Enabled by default
                }
                else
                {
                    return enabled;
                }
            }
        }

        /// <summary>
        /// Check if the cached menu file shall be auto-refreshed
        /// </summary>
        /// <param name="xdoc"></param>
        /// <returns>True if the file shall be refreshed, else false</returns>
        private bool CacheRefreshNeeded(XmlDocument xdoc)
        {
            long autoRefresh;

            // 1. Get configuration setting for when the file shall be automatically refreshed
            if (System.Configuration.ConfigurationManager.AppSettings.Get("databaseMenuCacheRefreshInterval") == null)
            {
                autoRefresh = 168; // Default to 7 days (168 hours)...
            }
            else
            {
                if (!long.TryParse(System.Configuration.ConfigurationManager.AppSettings.Get("databaseMenuCacheRefreshInterval").ToString(), out autoRefresh))
                {
                    autoRefresh = 168; // Default to 7 days (168 hours)...
                }
            }

            // 2. Perform check
            string xpath = "//created";
            XmlNode root = xdoc.SelectSingleNode(xpath);

            if (root == null)
            {
                return true;
            }

            //DateTime date;

            //if (!DateTime.TryParse(root.InnerText, out date))
            //{
            //    return true;
            //}
            //else
            //{
            //    if (date.AddHours(autoRefresh) < DateTime.Now)
            //    {
            //        return true;
            //    }
            //}

            if (!PxDate.IsPxDate(root.InnerText))
            {
                return true;
            }
            else
            {
                DateTime date = PxDate.PxDateStringToDateTime(root.InnerText);

                if (date.AddHours(autoRefresh) < DateTime.Now)
                {
                    return true;
                }
            }

            return false;
        }

        private void AddXmlNode(XmlDocument xdoc, XmlNode xmlNode, TreeNode treeNode)
        {
            XmlNode node = xdoc.CreateNode(XmlNodeType.Element, "item", null);

            XmlAttribute att = xdoc.CreateAttribute("text");
            att.Value = treeNode.Text;
            node.Attributes.Append(att);

            ItemSelection itm = (ItemSelection)treeNode.Tag;
            
            att = xdoc.CreateAttribute("menu");
            att.Value = itm.Menu;
            node.Attributes.Append(att);

            att = xdoc.CreateAttribute("selection");
            att.Value = itm.Selection;
            node.Attributes.Append(att);

            foreach (TreeNode child in treeNode.Nodes)
            {
                AddXmlNode(xdoc, node, child);
            }

            xmlNode.AppendChild(node);
        }


        private TreeNode GetTreeNode(XmlNode xmlNode)
        {
            TreeNode node = new TreeNode();
            node.Text = xmlNode.Attributes["text"].Value;
            ItemSelection itm = new ItemSelection(xmlNode.Attributes["menu"].Value, xmlNode.Attributes["selection"].Value);
            node.Tag = itm;

            return node;
        }

        private void BuildSubNodes(XmlNode xmlNode, TreeNode treeNode)
        {
            foreach (XmlNode node in xmlNode.SelectNodes("./item"))
            {
                TreeNode newNode = GetTreeNode(node);
                treeNode.Nodes.Add(newNode);
                BuildSubNodes(node, newNode);
            }
        }

        private string GetDbFilename(string dbId, string dbLang)
        {
            StringBuilder dbFile = new StringBuilder();
            dbFile.Append(dbId);
            dbFile.Append(".");
            dbFile.Append(dbLang);
            dbFile.Append(".xml");
            string dbPath = Path.Combine(_baseDir, dbFile.ToString());

            return dbPath;
        }
    }
}
