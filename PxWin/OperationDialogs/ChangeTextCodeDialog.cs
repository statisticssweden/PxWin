using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PCAxis.Desktop.UserControls;
using PCAxis.Paxiom;

namespace PCAxis.Desktop.OperationDialogs
{
    public partial class ChangeTextCodeDialog : Form
    {
        public PXModel SelectedModel;
        public TextCodeControl[] _textCodeControls;

        public ChangeTextCodeDialog(PXModel model)
        {
            SelectedModel = model;
            InitializeComponent();
            InitPanel();
            
        }        

        private void InitPanel()
        {
            Text = Lang.GetLocalizedString("ChangeTextCode");
            lblHelpText.Text = Lang.GetLocalizedString("ChangeTextCodeHelpText");
            btnChange.Text = Lang.GetLocalizedString("Change");
            btnCancel.Text = Lang.GetLocalizedString("Cancel");

            _textCodeControls = new TextCodeControl[SelectedModel.Meta.Variables.Count];

            var counter = 0;
            foreach (var variable in SelectedModel.Meta.Variables)
            {                
                var textCodeControl = new TextCodeControl(variable);
                _textCodeControls[counter] = textCodeControl;
                flowPanel.Controls.Add(textCodeControl);
                counter++;
            }
        }

        public Dictionary<string, HeaderPresentationType> GetSelections()
        {
            var dictionary = new Dictionary<string, HeaderPresentationType>();

            for (int i = 0; i < _textCodeControls.Length; i++)
            {
                var keyValuePair = _textCodeControls[i].GetSelection();
                if (!string.IsNullOrWhiteSpace(keyValuePair.Key))
                {
                    dictionary.Add(keyValuePair.Key, keyValuePair.Value);
                }                
            }

            return dictionary;
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
