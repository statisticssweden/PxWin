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
    public class AggregationFileAssociation
    {
        [Import]
        private IHost _host;

        [Export]
        [FileAssociationMetadata(AlwaysNew = false, Extension = "agg")]
        public IToolWindow Create(string path, Dictionary<string, string> param)
        {
            //TODO set the path on the view
            var frm = new AggregationForm() { Path = path };
            _host.ComposeParts(frm);
            if (param.ContainsKey("Valueset"))
            {
                if (!frm.LoadData(path, param["Valueset"]))
                {
                    return null;
                }
            }
            return frm;
        }
    }
}
