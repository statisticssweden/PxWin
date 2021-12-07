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
    public partial class DeleteVariableSelectValueDialog : Form
    {
        private Variable _variable;  

        public DeleteVariableSelectValueDialog()
        {
            InitializeComponent();
            SetLanguage();
        }

        public Variable SelectedVariable
        {
            get { return _variable; }
            set
            {
                _variable = value;
                RefreshList();
            }
        }

        public Value SelectedValue
        {
            get { return (Value)lbValues.SelectedItem; }
        }

        public bool AddToContents
        {
            get { return cbAddToContents.Checked; }
        }

        private void SetLanguage()
        {
            Text = Lang.GetLocalizedString("OperationDeleteSelectValue");
            cbAddToContents.Text = Lang.GetLocalizedString("OperationDeleteVariableAddToContents");
            btnOk.Text = Lang.GetLocalizedString("Ok");
            btnCancel.Text = Lang.GetLocalizedString("Cancel");
            lblListboxText.Text = Lang.GetLocalizedString("OperationDeleteSelectValueHelpText");
        }

        private void RefreshList()
        {
            lbValues.Items.Clear();

            foreach (var value in _variable.Values)
            {
                lbValues.Items.Add(value);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (lbValues.SelectedItem == null)
            {
                MessageBox.Show(Lang.GetLocalizedString("OperationDeleteSelectValueErrorMessage"),
                    Lang.GetLocalizedString("OperationDeleteSelectValueErrorMessage"), MessageBoxButtons.OK);
                return;
            }
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
