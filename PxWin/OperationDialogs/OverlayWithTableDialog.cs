using System;
using System.Windows.Forms;

namespace PCAxis.Desktop.OperationDialogs
{
    public partial class OverlayWithTableDialog : Form
    {
        public OverlayWithTableDialog()
        {
            InitializeComponent();
            SetLanguage();
        }

        public string OverlayVariable { get; set; }

        public string OverlayValue1 { get; set; }

        public string OverlayValue2 { get; set; }

        public string OverlayCode1 { get; set; }

        public string OverlayCode2 { get; set; }

        private void SetLanguage()
        {
            Text = Lang.GetLocalizedString("OverlayTableDialogText");
            btnOk.Text = Lang.GetLocalizedString("Ok");
            btnCancel.Text = Lang.GetLocalizedString("Cancel");
            lblHelpText.Text = Lang.GetLocalizedString("OverlayTableHelpText");
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                DialogResult = DialogResult.OK;
            }
        }

        private bool ValidateInput()
        {
	        string message = string.Empty;

	        if (tbVariable.Text.Trim().Length == 0) {
                message += Lang.GetLocalizedString("OverlayTableEnterVariable") + Environment.NewLine;
	        }

	        if (tbValue1.Text.Trim().Length == 0) {
                message += Lang.GetLocalizedString("OverlayTableEnterValue1") + Environment.NewLine;
	        }
	        if (tbValue2.Text.Trim().Length == 0) {
                message += Lang.GetLocalizedString("OverlayTableEnterValue2") + Environment.NewLine;
	        }
            //if (tbCode1.Text.Trim().Length == 0) {
            //    message += "Enter text for Code(1)." + Environment.NewLine;
            //}
            //if (tbCode2.Text.Trim().Length == 0) {
            //    message += "Enter text for Code(2)." + Environment.NewLine;
            //}


	        if (message.Length > 0) {
                MessageBox.Show(message, Lang.GetLocalizedString("MenuHelp"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
		        return false;
	        }

	        if (tbCode1.Text.Trim() == tbCode2.Text.Trim()) {
		        MessageBox.Show("Enter unique Codes.");
		        return false;
	        }

	        if (tbValue1.Text.Trim() == tbValue2.Text.Trim()) {
		        MessageBox.Show("Enter unique Values.");
		        return false;
	        }

	        OverlayCode1 = tbCode1.Text.Trim();
	        OverlayCode2 = tbCode2.Text.Trim();
	        OverlayValue1 = tbValue1.Text.Trim();
	        OverlayValue2 = tbValue2.Text.Trim();
	        OverlayVariable = tbVariable.Text.Trim();

	        return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
