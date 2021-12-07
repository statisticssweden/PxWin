using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PCAxis.Desktop.SaveAsDialogs
{
    public partial class SaveAsCsvDialog : Form
    {
        public SaveAsCsvDialog()
        {
            InitializeComponent();
        }

        public bool WriteTitle
        {
            get { return cbWriteTitle.Checked; }
        }

        public bool DoubleColumn
        {
            get { return cbDoubleColumn.Checked; }
        }

        public char Delimiter
        {
            get
            {
                char d;
                if (rbComma.Checked)
                {
                    d = ',';
                }
                else if (rbBlank.Checked)
                {
                    d = ' ';
                }
                else if (rbTabulator.Checked)
                {
                    d = (char)Keys.Tab;
                }
                else
                {
                    d = string.IsNullOrEmpty(tbOther.Text) ? ',' : tbOther.Text[0];
                }
                return d;
            }
        }

        private void rbOther_CheckedChanged(object sender, EventArgs e)
        {
            tbOther.Enabled = rbOther.Checked;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }      
    }
}
