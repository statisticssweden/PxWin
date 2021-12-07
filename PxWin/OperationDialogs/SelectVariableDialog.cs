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
    public partial class SelectVariableDialog : Form
    {

        public SelectVariableDialog()
        {
            InitializeComponent();
            SetLanguage();
        }


        private PXModel _pxModel;
        public PXModel SelectedModel
        {
            get { return _pxModel; }
            set
            {
                _pxModel = value;
                Init();
            }
        }

        public Variable SelectedVariable { get; set; }

        private void Init()
        {
            lbSelectValue.Items.Clear();
            foreach (var var in SelectedModel.Meta.Variables)
            {
                lbSelectValue.Items.Add(var);
            }
            lbSelectValue.SelectedIndex = -1;
            btnOk.Enabled = false;
        }



        private void SetLanguage()
        {
            Text = Lang.GetLocalizedString("OperationDeleteSelectVariableErrorMessage");
            btnOk.Text = Lang.GetLocalizedString("Ok");
            btnCancel.Text = Lang.GetLocalizedString("Cancel");
        }

        private void lbSelectValue_SelectedValueChanged(object sender, EventArgs e)
        {
            if (lbSelectValue.SelectedIndex > -1)
            {
                SelectedVariable = SelectedModel.Meta.Variables[lbSelectValue.SelectedIndex];
                btnOk.Enabled = true;
            }
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
