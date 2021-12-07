using PCAxis.Menu;
using PCAxis.Menu.Implementations;
using PX.Plugin.Interfaces;
using PX.Plugin.Interfaces.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PCAxis.Desktop.DataSources
{
    [Export(typeof(IDataSource))]
    [DataSourceMetadata(SourceType = "PX")]
    public class PxFileDataSource : IDataSource
    {
        public Menu.PxMenuBase CreateMenu(IDatabaseInfo dbi, string menu, string selection, string language)
        {
            if (dbi == null)
            {
                return null;
            }
            if (string.IsNullOrEmpty(dbi.GetValue(DatabaseInfo.PATH)))
            {
                return null;
            }

            string rootFolderName = System.IO.Path.GetFileName(dbi.GetValue(DatabaseInfo.PATH));

            string xmlFile = System.IO.Path.Combine(dbi.GetValue(DatabaseInfo.PATH), "Menu.xml");
            XmlMenu retMenu = new XmlMenu(XDocument.Load(xmlFile), language,
                m =>
                {
                    m.Restriction = item =>
                    {
                        return true;
                    };
                    m.AlterItemBeforeStorage = item =>
                    {
                        //Removes database folder name from the selection
                        item.ID = new ItemSelection(item.ID.Menu, item.ID.Selection.Substring(rootFolderName.Length).TrimStart('\\'));
                    };
                });

            ItemSelection itm;
            if (string.IsNullOrEmpty(menu) && string.IsNullOrEmpty(selection))
            {
                //node = new PxMenuItem(menu);
                itm = new ItemSelection();
            }
            else
            {
                itm = new ItemSelection(menu, selection);
            }
            retMenu.SetCurrentItemBySelection(itm.Menu, itm.Selection);
            return retMenu;
        }

        public Paxiom.IPXModelBuilder CreateBuilder(IDatabaseInfo dbi, string menu, string selection, string language)
        {
            var builder = new PCAxis.Paxiom.PXFileBuilder();
            string pathWithDb;
            if (string.IsNullOrEmpty(dbi.GetValue(DatabaseInfo.PATH)))
            {
                pathWithDb = "";
            }
            else
            {
                pathWithDb = dbi.GetValue(DatabaseInfo.PATH);
            }
            
            string path = System.IO.Path.Combine(pathWithDb, selection.TrimStart('/', '\\'));
            builder.SetPath(path);
            return builder;
        }


        public string GetSource(IDatabaseInfo dbi, PCAxis.Paxiom.PXModel model, string language)
        {
            return GetRelativePxPath(dbi.GetValue(DatabaseInfo.PATH), model.Meta.MainTable);
        }

        /// <summary>
        /// Get relative path to table in a mapped PX-file database
        /// </summary>
        /// <param name="dbPath">Path to the PX-file database</param>
        /// <param name="tablePath">Path to the PX-file</param>
        /// <returns></returns>
        private string GetRelativePxPath(string dbPath, string tablePath)
        {
            if (string.IsNullOrWhiteSpace(dbPath))
            {
                return System.IO.Path.GetFullPath(tablePath).Replace("\\", "/");
            }
            string fullDbPath = System.IO.Path.GetFullPath(dbPath).Replace("\\", "/");
            string fullTablePath = System.IO.Path.GetFullPath(tablePath).Replace("\\", "/");

            var fullDbParts = fullDbPath.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
            var fullTableParts = fullTablePath.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);

            int index = 0;

            while ((index < fullDbParts.Length) && (fullDbParts[index] == fullTableParts[index]))
            {
                index++;
            }

            StringBuilder path = new StringBuilder();

            for (int i = index; i < fullTableParts.Length; i++)
            {
                path.Append("/");
                path.Append(fullTableParts[i]);
            }

            return path.ToString();
        }

    }

}
