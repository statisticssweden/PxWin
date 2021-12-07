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
    public partial class SaveAsExcelDialog : Form
    {
        public SaveAsExcelDialog()
        {
            InitializeComponent();
        }

        public PCAxis.Paxiom.InformationLevelType InformationLevel
        {
            get
            {
                if (rbAllFootnotes.Checked)
                {
                    return Paxiom.InformationLevelType.AllFootnotes;
                }
                if (rbAllInfo.Checked)
                {
                    return Paxiom.InformationLevelType.AllInformation;
                }
                if (rbMandatory.Checked)
                {
                    return Paxiom.InformationLevelType.MandantoryFootnotesOnly;
                }  
                             
                return Paxiom.InformationLevelType.None;               
            }
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
