using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PCAxis.Desktop.UserControls;
using PCAxis.Paxiom;
using PCAxis.Paxiom.Operations;
using PCAxis.Query;

namespace PCAxis.Desktop.OperationDialogs
{
    public partial class DeleteValueDialog : Form
    {
        private PXModel _Model;
        private SelectVariableValuesControl[] vsControls;
        private bool _isRecording;
        private OperationsTracker _operationsTracker;

        public DeleteValueDialog(bool isRecording, OperationsTracker operationsTracker)
        {
            InitializeComponent();
            SetLanguage();

            _isRecording = isRecording;
            _operationsTracker = operationsTracker;
        }

        private void SetLanguage()
        {
            Text = Lang.GetLocalizedString("OperationDeleteValue");
            btnOk.Text = Lang.GetLocalizedString("Ok");
            btnCancel.Text = Lang.GetLocalizedString("Cancel");
            lblHelpText.Text = Lang.GetLocalizedString("OperationDeleteValueHelpText"); 
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
            flPanel.Controls.Clear();

            vsControls = new SelectVariableValuesControl[SelectedModel.Meta.Variables.Count];
            var defaultSize = new Size(155, 250);
            
            for (var i = 0; i <= SelectedModel.Meta.Variables.Count - 1; i++)
            {
                var vsControl = new SelectVariableValuesControl
                {
                    Variable = SelectedModel.Meta.Variables[i],
                    Size = defaultSize
                };
                flPanel.Controls.Add(vsControl);
                vsControls[i] = vsControl;
            }      
        }


        private bool DeleteValue()
        {
            

            var s = new Selection[vsControls.Length];
            var del = new Paxiom.Operations.DeleteValue();
            var hasValues = false;

            var valueDescription = new List<Selection>();

            for (int index = 0; index <= vsControls.Length - 1; index++)
            {
                var selection = vsControls[index].GetSelection();
                
                if (selection.ValueCodes.Count > 0)
                {
                    hasValues = true;
                }
                s[index] = selection;

                valueDescription.Add(vsControls[index].GetSelection());
            }

            if (!hasValues)
            {
                MessageBox.Show(Lang.GetLocalizedString("OperationDeleteSelectValueMessage"),
                      Lang.GetLocalizedString("OperationDeleteSelectValue"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            try
            {
                SelectedModel = del.Execute(SelectedModel, s);

                if (_isRecording)
                {
                    Selection[] sels = valueDescription.ToArray();
                    _operationsTracker.AddStep(OperationConstants.DELETE_VALUE, sels);
                    foreach (Selection selection in sels)
                    {
                        Variable va = SelectedModel.Meta.Variables.FirstOrDefault(v => v.Code == selection.VariableCode);
                        if (va != null && va.IsTime)
                        {
                            if (selection.ValueCodes.Count > 0)
                            {
                                _operationsTracker.IsTimeDependent = true;
                            }
                        }
                    }
                }
            }
            catch (PXOperationException ex)
            {
                MessageBox.Show(ex.Message, Lang.GetLocalizedString("OperationDeleteValueErrorMessage"));
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Lang.GetLocalizedString("OperationDeleteValueError"));
                return false;
            }

            return true;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (DeleteValue())
            {
                DialogResult = DialogResult.OK;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
