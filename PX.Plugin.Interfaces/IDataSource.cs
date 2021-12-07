using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCAxis.Menu;
using PCAxis.Paxiom;

namespace PX.Plugin.Interfaces
{
    /// <summary>
    /// Interface for a PX datasource
    /// </summary>
    public interface IDataSource
    {
        /// <summary>
        /// Get Menu
        /// </summary>
        /// <param name="dbi">Database</param>
        /// <param name="node">Node</param>
        /// <param name="language">Language</param>
        /// <returns></returns>
        PxMenuBase CreateMenu(IDatabaseInfo dbi, string menu, string selection, string language);
        
        /// <summary>
        /// Create builder
        /// </summary>
        /// <param name="dbi">Database</param>
        /// <param name="menu">Menu</param>
        /// <param name="selection">Selection</param>
        /// <param name="language">Language</param>
        /// <returns></returns>
        IPXModelBuilder CreateBuilder(IDatabaseInfo dbi, string menu, string selection, string language);

        string GetSource(IDatabaseInfo dbi, PCAxis.Paxiom.PXModel model, string language);
    }
}
