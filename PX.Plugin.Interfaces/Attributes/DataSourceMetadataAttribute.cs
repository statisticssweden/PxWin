using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PX.Plugin.Interfaces.Attributes
{
    [MetadataAttribute]
    public class DataSourceMetadataAttribute : Attribute, IDataSourceMetadata
    {
        public string SourceType
        {
            get;
            set;
        }
    }
}
