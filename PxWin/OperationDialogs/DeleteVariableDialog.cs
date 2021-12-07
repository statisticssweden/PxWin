using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PCAxis.Paxiom;
using PCAxis.Paxiom.Operations;
using PCAxis.Query;

namespace PCAxis.Desktop.OperationDialogs
{
    public partial class DeleteVariableDialog : Form
    {
        private PXModel _Model;
        private readonly bool _isRecording;
        private readonly OperationsTracker _operationsTracker;

        public DeleteVariableDialog(bool isRecording, OperationsTracker operationsTracker)
        {
            InitializeComponent();
            SetLanguage();
            
            _isRecording = isRecording;
            _operationsTracker = operationsTracker;
        }

        private void SetLanguage()
        {
            Text = Lang.GetLocalizedString("OperationDeleteVariable");
            btnOk.Text = Lang.GetLocalizedString("Ok");
            btnCancel.Text = Lang.GetLocalizedString("Cancel");
        }

        public PXModel SelectedModel
        {
            get { return _Model; }
            set
            {
                _Model = value;
                RefreshList();
            }
        }

        private void RefreshList()
        {
            lbVariables.Items.Clear();

            foreach (Variable variable in SelectedModel.Meta.Variables)
            {
                lbVariables.Items.Add(variable);
            }
        }

        private bool DeleteVariable(Variable variable, Value value, bool addToContents)
        {
            
            var delVar = new DeleteVariable();

            var delVarDesc = new DeleteVariableDescription(variable.Code, value.Code, addToContents);
            SelectedModel = delVar.Execute(SelectedModel, delVarDesc);

            return true;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (lbVariables.SelectedItem == null)
            {
                MessageBox.Show(Lang.GetLocalizedString("OperationDeleteSelectVariableMessage"), Lang.GetLocalizedString("OperationDeleteSelectVariableErrorMessage"), MessageBoxButtons.OK);
                return;
            }

            using (var dlg = new DeleteVariableSelectValueDialog())
            {
                dlg.SelectedVariable = (Variable)lbVariables.SelectedItem;
                if (dlg.ShowDialog() != DialogResult.OK) return;
                if (DeleteVariable(dlg.SelectedVariable, dlg.SelectedValue, dlg.AddToContents))
                {
                    DialogResult = DialogResult.OK;
                }

                if(_isRecording)
                {
                    var deleteVariableDesc = new DeleteVariableDescription(dlg.SelectedVariable.Code, dlg.SelectedValue.Code, dlg.AddToContents);
                    _operationsTracker.AddStep(OperationConstants.DELETE_VARIABLE, deleteVariableDesc);

                    if (dlg.SelectedVariable.IsTime)
                    {
                        _operationsTracker.IsTimeDependent = true;
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
