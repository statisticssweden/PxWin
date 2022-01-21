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
        /// Previous table query
        /// </summary>
        public TableQuery PreviousTableQuery;

        /// <summary>
        /// Previous PXModel
        /// </summary>
        public PXModel PreviousModel;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="builder"></param>
        public OpenPXTableEventArgs(DatabaseInfo dbInfo, IPXModelBuilder builder, TableQuery previousTableQuery = null, PXModel previousModel = null)
        {
            DbInfo = dbInfo;
            Builder = builder;
            PreviousTableQuery = previousTableQuery;
            PreviousModel = previousModel;
        }
    }

    public delegate void OpenPXTableEventHandler(object sender, OpenPXTableEventArgs e);

}
