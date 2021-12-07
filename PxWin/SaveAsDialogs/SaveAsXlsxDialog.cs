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
    public partial class SaveAsXlsxDialog : Form
    {
        public SaveAsXlsxDialog()
        {
            InitializeComponent();
            SetLanguage();
        }

        public bool WithCodeAndText
        {
            get { return cbCodeAndText.Checked; }
        }

        private void SetLanguage()
        {
            Text = Lang.GetLocalizedString("SaveAsXlsx");
            btnOk.Text = Lang.GetLocalizedString("Ok");
            btnCancel.Text = Lang.GetLocalizedString("Cancel");
            cbCodeAndText.Text = Lang.GetLocalizedString("SaveAsXlsxWithCodeAndText");
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
