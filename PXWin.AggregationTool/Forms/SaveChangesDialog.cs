using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel.Composition;
using PX.Desktop.Interfaces;

namespace PXWin.AggregationTool
{
    public partial class SaveChangesDialog : Form
    {
        [Import]
        private IHost _host;
        private string _filename;


        public SaveChangesDialog(string filename)
        {
            InitializeComponent();
            _filename = filename;
        }

        private void SaveChangesDialog_Load(object sender, EventArgs e)
        {
            SwitchLanguage(_host.Language.CurrentLanguage);
        }

        public void SwitchLanguage(string language)
        {
            this.Text = _host.Language.GetString("toolAggSaveChangesTitle");
            lblQuestion.Text = string.Format(_host.Language.GetString("toolAggSaveChangesQuestion"), _filename);
            btnSave.Text = _host.Language.GetString("toolAggSaveChangesYes");
            btnDontSave.Text = _host.Language.GetString("toolAggSaveChangesNo");
            btnCancel.Text = _host.Language.GetString("toolAggSaveChangesCancel");
        }

    }
}
