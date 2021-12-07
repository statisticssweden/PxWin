using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PX.Desktop.Interfaces;
using System.ComponentModel.Composition;
using System.Windows.Forms;

namespace PXWin.AggregationTool
{
    [Export(typeof(IToolsMenu))]
    public class AggToolMenu : IToolsMenu, ILocalizable
    {
        [Import]
        private IHost _host;
        private ToolStripMenuItem[] _items;
        public ToolStripMenuItem[] MenuItems
        {
            get { return _items; }
        }
        private ToolStripMenuItem root;
        private ToolStripMenuItem openValueset;
        private ToolStripMenuItem newValueset;
        private ToolStripMenuItem newAggregation;

        public AggToolMenu()
        {
            root = new ToolStripMenuItem("Aggregations");

            openValueset = new ToolStripMenuItem("Open valueset");
            openValueset.Click += openValueset_Click;

            newValueset = new ToolStripMenuItem("New valueset");
            newValueset.Click += newValueset_Click;

            newAggregation = new ToolStripMenuItem("New aggregation");
            newAggregation.Click += newAggregation_Click;

            root.DropDownItems.AddRange(new ToolStripMenuItem[] { openValueset, newValueset, newAggregation });

            _items = new ToolStripMenuItem[] { root };
        }

        private void openValueset_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openDialog = new OpenFileDialog())
            {
                openDialog.Filter = "Valueset-file (*.vs)|*.vs";
                openDialog.FilterIndex = 0;

                if (openDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    _host.Show(openDialog.FileName);
                }
            }
        }

        private void newValueset_Click(object sender, EventArgs e)
        {
            var dlg = new ValuesetForm();
            _host.ComposeParts(dlg);
            _host.Show(dlg);
        }

        private void newAggregation_Click(object sender, EventArgs e)
        {
            var dlg = new AggregationForm();
            _host.ComposeParts(dlg);
            _host.Show(dlg);
        }

        public void SwitchLanguage(string language)
        {
            root.Text = _host.Language.GetString("toolAggMenuRoot");
            openValueset.Text = _host.Language.GetString("toolAggMenuOpenValueset");
            newValueset.Text = _host.Language.GetString("toolAggMenuNewValueset");
            newAggregation.Text = _host.Language.GetString("toolAggMenuNewAggregation"); 
        }
    }
}
