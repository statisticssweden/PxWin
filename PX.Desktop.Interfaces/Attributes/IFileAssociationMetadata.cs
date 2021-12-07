using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PX.Desktop.Interfaces.Attributes
{
    public interface IFileAssociationMetadata
    {
        string Extension { get; }
        bool AlwaysNew { get; }
    }
}
