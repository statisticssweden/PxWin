using PCAxis.Paxiom;
using PCAxis.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCAxis.Desktop.Events
{
    /// <summary>
    /// Argument class for the OpenPXTable event
    /// </summary>
    public class OpenPXTableEventArgs : System.EventArgs
    {
        /// <summary>
        /// Selected database
        /// </summary>
        public DatabaseInfo DbInfo;

        /// <summary>
        /// Builder object
        /// </summary>
        public IPXModelBuilder Builder;

        /// <summary>
        /// PXModel object representing the old model
        /// </summary>
        public TableQuery PreviousTableQuery;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="builder"></param>
        public OpenPXTableEventArgs(DatabaseInfo dbInfo, IPXModelBuilder builder, TableQuery previousTableQuery = null)
        {
            DbInfo = dbInfo;
            Builder = builder;
            PreviousTableQuery = previousTableQuery;
        }
    }

    public delegate void OpenPXTableEventHandler(object sender, OpenPXTableEventArgs e);

}
