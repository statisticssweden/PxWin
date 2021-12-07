using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PX.Plugin.Interfaces.Attributes
{
    public interface ISerializerMetadata
    {
        string Id { get; }
        string Extension { get; }
    }
}
