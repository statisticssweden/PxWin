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

namespace PCAxis.Desktop.DataSources
{
    [Export(typeof(IDataSource))]
    [DataSourceMetadata(SourceType = "CNMM")]
    public class CnmmDataSource : IDataSource
    {
        public Menu.PxMenuBase CreateMenu(IDatabaseInfo dbi, string menu, string selection, string language)
        {
            string dbid = dbi.Id;


            //Create database object to return
            DatamodelMenu retMenu = ConfigDatamodelMenu.Create(
                        language,
                        PCAxis.Sql.DbConfig.SqlDbConfigsStatic.DataBases[dbid],
                        m =>
                        {
                            //m.RootSelection = string.IsNullOrEmpty(nodeId) ? new ItemSelection() : PathHandlerFactory.Create(PCAxis.Web.Core.Enums.DatabaseType.CNMM).GetSelection(nodeId);
                            m.RootSelection = string.IsNullOrEmpty(menu) && string.IsNullOrEmpty(selection) ? new ItemSelection() : new ItemSelection(menu, selection);
                            m.AlterItemBeforeStorage = item =>
                            {
                                if (item is PCAxis.Menu.Url)
                                {
                                    PCAxis.Menu.Url url = (PCAxis.Menu.Url)item;
                                }

                                if (item is TableLink)
                                {
                                    TableLink tbl = (TableLink)item;
                                    string tblId = tbl.ID.Selection;
                                    //if (!string.IsNullOrEmpty(dbid))
                                    //{
                                    //    tbl.ID = new ItemSelection(item.ID.Menu, dbid + ":" + tbl.ID.Selection); // Hantering av flera databaser!
                                    //}



                                    if (tbl.Published.HasValue)
                                    {
                                        // Store date in the PC-Axis date format
                                        tbl.SetAttribute("modified", tbl.Published.Value.ToString(PCAxis.Paxiom.PXConstant.PXDATEFORMAT));
                                    }

                                }

                                if (String.IsNullOrEmpty(item.SortCode))
                                {
                                    item.SortCode = item.Text;
                                }
                            };
                            m.Restriction = item =>
                            {
                                return true;
                            };
                        });

            retMenu.RootItem.Sort();

            return retMenu;
        }

        public Paxiom.IPXModelBuilder CreateBuilder(IDatabaseInfo dbi, string menu, string selection, string language)
        {
            var builder = new PCAxis.PlugIn.Sql.PXSQLBuilder();
            builder.SetPath(dbi.Id + ":" + selection);

            return builder;
        }

        public string GetSource(IDatabaseInfo dbi, PCAxis.Paxiom.PXModel model, string language)
        {
            return model.Meta.MainTable;
        }
    }
}
