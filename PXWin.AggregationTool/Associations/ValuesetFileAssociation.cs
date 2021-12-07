using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using PX.Desktop.Interfaces;
using PX.Desktop.Interfaces.Attributes;

namespace PXWin.AggregationTool.Associations
{
    public class ValuesetFileAssociation
    {
        [Import]
        private IHost _host;

        [Export]
        [FileAssociationMetadata(AlwaysNew = false, Extension = "vs")]
        public IToolWindow Create(string path, Dictionary<string, string> param)
        {
            var frm = new ValuesetForm() { Path = path };
            _host.ComposeParts(frm);
            frm.LoadData(path);
            return frm;
        }
    }
}
