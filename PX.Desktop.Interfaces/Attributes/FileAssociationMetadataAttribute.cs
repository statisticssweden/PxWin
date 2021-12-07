using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;

namespace PX.Desktop.Interfaces.Attributes
{
    [MetadataAttribute]
    public class FileAssociationMetadataAttribute : Attribute, IFileAssociationMetadata
    {
        public string Extension
        {
            get;
            set;
        }

        public bool AlwaysNew
        {
            get;
            set;
        }
    }
}
