using System.Windows.Forms;
using System;
using System.Drawing;
using System.Linq;
using PCAxis.Paxiom;
using PCAxis.Paxiom.Operations;
using PCAxis.Desktop.UserControls;

namespace PCAxis.Desktop.OperationDialogs
{
    public partial class PercentDialog : Form
    {

        #region "Private members"

        private PXModel _model;
        private CalculatePerPartType _operationType;
        private CalculatePerPartDescription _description;

        #endregion

        private SelectVariableValuesControl[] _vsControls;

        #region "Buttons"

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (CalculatePerPart())
            {
                DialogResult = DialogResult.OK;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        #endregion

        #region "Properties"

        public PXModel SelectedModel
        {
            get { return _model; }
            set
            {
                _model = value;
                RefreshLists();
            }
        }

        public CalculatePerPartType OperationType
        {
            get { return _operationType; }
            set
            {
                _operationType = value;

                switch (_operationType)
                {
                    case CalculatePerPartType.PerCent:
                        this.Text = Lang.GetLocalizedString("PxcPercent");
                        //textBoxNewValue.Text = Lang.GetLocalizedString("PxcPercent");
                        break;
                    case CalculatePerPartType.PerMil:
                        this.Text = Lang.GetLocalizedString("PxcPermil");
                        //textBoxNewValue.Text = Lang.GetLocalizedString("PxcPermil");
                        break;
                }
            }
        }

        #endregion

        public PercentDialog()
        {
            InitializeComponent();
            SetLanguage();
        }

        public CalculatePerPartDescription GetOperationDescription()
        {
            return _description;
        }

        private void SetLanguage()
        {
            this.Text = Lang.GetLocalizedString("OperationPercent");
            grpBase.Text = Lang.GetLocalizedString("OperationPercentBaseHeading");
            rbBaseVariable.Text = Lang.GetLocalizedString("OperationPercentBaseVariable");
            rbBaseVariableValue.Text = Lang.GetLocalizedString("OperationPercentBaseVariableValue");
            rbBaseCellValue.Text = Lang.GetLocalizedString("OperationPercentBaseCellValue");
            lblInstructionHeading.Text = Lang.GetLocalizedString("OperationPercentBaseSelectionHeading");
            SetInstructionText();
            btnOk.Text = Lang.GetLocalizedString("Ok");
            btnCancel.Text = Lang.GetLocalizedString("Cancel");
            rbInclude.Text = Lang.GetLocalizedString("OperationPercentInclude");
            rbExclude.Text = Lang.GetLocalizedString("OperationPercentExclude");
            gbInitialValue.Text = Lang.GetLocalizedString("OperationPercentInitialValue");
            grpNewValue.Text = Lang.GetLocalizedString("OperationPercentNewValue");
            tbNewValue.Text = Lang.GetLocalizedString("OperationPercentNewValueDefault");
        }

        private void SetInstructionText()
        {
            if (rbBaseVariable.Checked)
            {
                lblInstruction.Text = Lang.GetLocalizedString("OperationPercentBaseInstructionVariable");
            }
            else if (rbBaseVariableValue.Checked)
            {
                lblInstruction.Text = Lang.GetLocalizedString("OperationPercentBaseInstructionVariableValue");
            }
            else
            {
                lblInstruction.Text = Lang.GetLocalizedString("OperationPercentBaseInstructionCellValue");
            }
        }

        private void RefreshLists()
        {
            flPanel.Controls.Clear();

            _vsControls = new SelectVariableValuesControl[SelectedModel.Meta.Variables.Count];

            var defaultSize = new Size(155, 250);

            for (var i = 0; i <= SelectedModel.Meta.Variables.Count - 1; i++)
            {
                var selectValueUserControl = new SelectVariableValuesControl
                {
                    Variable = SelectedModel.Meta.Variables[i],
                    Size = defaultSize                   
                    
                };

                flPanel.Controls.Add(selectValueUserControl);
                _vsControls[i] = selectValueUserControl;
                var lstValues = (ListBox)selectValueUserControl.Controls.Find("lstValues", false).Single();
                lstValues.SelectionMode = SelectionMode.MultiExtended;
            }
        }


        private bool CalculatePerPart()
        {
            var s = new Selection[_vsControls.Length];
            var calculatePerPartOperation = new CalculatePerPart();
            _description = new CalculatePerPartDescription();
            var oneSelectedCounter = 0;
            var allSelectedCounter = 0;
            var anySelectedCounter = 0;

            if (tbNewValue.Enabled & tbNewValue.Text.Trim() == string.Empty)
            {
                MessageBox.Show(Lang.GetLocalizedString("OperationPercentValueNeededText"),
                        Lang.GetLocalizedString("OperationPercentRequiredValue"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            // Check that the selection is valid for this type of operation - All values for one Variable or ONE value from all Variables
            for (var index = 0; index <= _vsControls.Length - 1; index++)
            {
                if (_vsControls[index].GetNumberOfSelectedValues() == 1)
                {
                    oneSelectedCounter = oneSelectedCounter + 1;
                }
                else if (_vsControls[index].GetItemCount() > 0 & _vsControls[index].GetItemCount() == _vsControls[index].GetNumberOfSelectedValues())
                {
                    allSelectedCounter = allSelectedCounter + 1;
                }

                if (_vsControls[index].GetNumberOfSelectedValues() > 0)
                    anySelectedCounter = anySelectedCounter + 1;
            }

            if (oneSelectedCounter == 1 & anySelectedCounter == 1)
            {
                // One VariableValue is the base
                _description.CalculationVariant = CalculatePerPartSelectionType.OneVariableOneValue;
            }
            else if (oneSelectedCounter == _vsControls.Length)
            {
                // One Matrix value is the base
                _description.CalculationVariant = CalculatePerPartSelectionType.OneMatrixValue;
            }
            else if (allSelectedCounter == 1 & anySelectedCounter == 1)
            {
                // Sum of the values is the base???
                _description.CalculationVariant = CalculatePerPartSelectionType.OneVariableAllValues;
            }
            else
            {
                MessageBox.Show(Lang.GetLocalizedString("OperationPercentAllowText"),
                      Lang.GetLocalizedString("OperationCalculateSelectErrorMessage"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                return false;
            }

            for (int index = 0; index <= _vsControls.Length - 1; index++)
            {
                s[index] = _vsControls[index].GetSelection();
            }

            try
            {
                _description.KeepValue = rbInclude.Checked;
                _description.OperationType = this.OperationType;
                _description.ValueSelection = s;
                _description.ValueName = tbNewValue.Text;

                SelectedModel = calculatePerPartOperation.Execute(this.SelectedModel, _description);
            }
            catch (PXOperationException ex)
            {
                MessageBox.Show(ex.Message, "Operation error");
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                return false;
            }

            return true;
        }

        private void ucValueSelect_Load(object sender, EventArgs e)
        {
            var vsc = (ValueSelectControl)sender;
            var lst = vsc.lstValues;
            lst.SelectionMode = SelectionMode.MultiExtended;
        }

        private void rbBaseVariable_CheckedChanged(object sender, EventArgs e)
        {
            SetInstructionText();
        }

        private void rbBaseVariableValue_CheckedChanged(object sender, EventArgs e)
        {
            SetInstructionText();
        }

        private void rbBaseCellValue_CheckedChanged(object sender, EventArgs e)
        {
            SetInstructionText();
        }


    }
}