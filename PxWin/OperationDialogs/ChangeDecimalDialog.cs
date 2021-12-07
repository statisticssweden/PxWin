using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PCAxis.Paxiom;

namespace PCAxis.Desktop.OperationDialogs
{
    public partial class ChangeDecimalDialog : Form
    {
        public int GetDecimals()
        {
            //Validation has already been done.
            return int.Parse(tbDecimals.Text);
        }

        public ChangeDecimalDialog()
        {
            InitializeComponent();
            SetLangugage();
        }

        private void SetLangugage()
        {
            Text = Lang.GetLocalizedString("ChangeDecimal");
            btnOk.Text = Lang.GetLocalizedString("Ok");
            btnCancel.Text = Lang.GetLocalizedString("Cancel");
            lblDecimals.Text = Lang.GetLocalizedString("ChangeDecimalInfo");
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            int decimals;
            if (int.TryParse(tbDecimals.Text, out decimals) && decimals <= 6)
            {
                DialogResult = DialogResult.OK; 
            }
            else
            {
                MessageBox.Show(Lang.GetLocalizedString("ChangeDecimalErrorText"), Lang.GetLocalizedString("ChangeDecimalErrorHead"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }                      
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
