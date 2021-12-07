using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel.Composition;
using PX.Desktop.Interfaces;

namespace PXWin.AggregationTool
{
    public partial class DelimiterCsvDialog : Form
    {
        [Import]
        private IHost _host;
        public CsvDelimiter Delimiter { get; set; }

        public enum CsvDelimiter
        {
            Comma = ',',
            Tabulator = '\t',
            Space = ' '

        }

        public DelimiterCsvDialog(string filename)
        {

            InitializeComponent();
            lbFilename.Text = @""""+filename +@"""";

        }
  

        private void DelimiterCsvDialog_Load(object sender, EventArgs e)
        {
            SwitchLanguage(_host.Language.CurrentLanguage);      
        }

        public void SwitchLanguage(string language)
        {
            lblQuestion.Text = _host.Language.GetString("formDelimiterQuestion");
            rbCsvWithTabulator.Text = _host.Language.GetString("formDelimiterTabulator");
            rbCsvWithComma.Text = _host.Language.GetString("formDelimiterComma");
            rbCsvWithSpace.Text = _host.Language.GetString("formDelimiterSpace");
            btnCancel.Text = _host.Language.GetString("Cancel");
            btnOk.Text = _host.Language.GetString("Ok");
            this.Text = _host.Language.GetString("formDelimiterCaption");
        }


        private void btnOk_Click(object sender, EventArgs e)
        {
            var checkedButton = this.groupBoxRadioButtons.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
            if (rbCsvWithComma.Checked)
            {
                Delimiter = CsvDelimiter.Comma;
            }
            else if (rbCsvWithSpace.Checked)
            {
                Delimiter = CsvDelimiter.Space;
            }
            else
            {
                Delimiter = CsvDelimiter.Tabulator;
            }
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }


    }
}
