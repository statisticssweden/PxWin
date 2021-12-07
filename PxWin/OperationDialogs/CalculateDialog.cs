using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using PCAxis.Desktop.UserControls;
using PCAxis.Paxiom;
using PCAxis.Paxiom.Operations;
using PCAxis.Query;

namespace PCAxis.Desktop.OperationDialogs
{
    public partial class CalculateDialog : Form
    {
        private PXModel _Model;
        private SumOperationType _operation;
        private List<object> _selectedItems = new List<object>();
        private OperationsTracker _operationsTracker;
        private bool _isRecording;


        public CalculateDialog(bool isRecording, OperationsTracker operationsTracker)
        {
            InitializeComponent();
            SetLanguage();
            _isRecording = isRecording;
            _operationsTracker = operationsTracker;
        }

        private void SetLanguage()
        {
            Text = Lang.GetLocalizedString("OperationCalculate");
            gbInitialValues.Text = Lang.GetLocalizedString("OperationCalculateInitialValues");
            gbNewValue.Text = Lang.GetLocalizedString("OperationCalculateNewValue");
            rbInclusive.Text = Lang.GetLocalizedString("OperationCalculateInclusive");
            rbExclusive.Text = Lang.GetLocalizedString("OperationCalculateExclusive");
            lblName.Text = Lang.GetLocalizedString("OperationCalculateName");
            lblCode.Text = Lang.GetLocalizedString("OperationCalculateCode");
            btnOk.Text = Lang.GetLocalizedString("Ok");
            btnCancel.Text = Lang.GetLocalizedString("Cancel");
            lblConstant.Text = Lang.GetLocalizedString("OperationCalculateConstant");            
        }

        public PXModel SelectedModel
        {
            get { return _Model; }
            set
            {
                _Model = value;
                MyInit();
            }
        }

        public SumOperationType SelectedOperation
        {
            get { return _operation; }
            set
            {
                _operation = value;
                switch (_operation)
                {
                    case SumOperationType.Addition:
                        Text = Lang.GetLocalizedString("OperationCalculateAddition");
                        break;
                    case SumOperationType.Division:
                        Text = Lang.GetLocalizedString("OperationCalculateDivision");
                        break;
                    case SumOperationType.Multiplication:
                        Text = Lang.GetLocalizedString("OperationCalculateMultiplication");
                        break;
                    case SumOperationType.Subtraction:
                        Text = Lang.GetLocalizedString("OperationCalculateSubtraction");
                        break;
                    default:
                        Text = _operation.ToString();
                        break;
                }
               
            }
        }

        private void MyInit()
        {
            //ComboBox
            for (var i = 0; i <= SelectedModel.Meta.Variables.Count - 1; i++)
            {
                Variable v = SelectedModel.Meta.Variables[i];
                comboVariables.Items.Add(v);
            }
            comboVariables.DisplayMember = "Name";
            comboVariables.SelectedItem = comboVariables.Items[0];

            //ValueSelectControl
            //Me.vscSelectedVariable.Height = 200
            //Me.vscSelectedVariable.Width = 120
            ucValueSelect.ShowBottomButtons = false;
            ucValueSelect.ShowTopButtons = false;
        }

        private void CalculateValues(double constantValue, string valueName, string valueCode, bool calculateWithConstant)
        {
            if ((comboVariables.SelectedItem != null))
            {
                //Get the selected variable
                var pcaxisVariable = (Variable)comboVariables.SelectedItem;
                
                //If there are values to add with constant
                if (pcaxisVariable.Values.Count > 0 && ucValueSelect.GetSelectionCount() > 0)
                {
                    var sumOperation = new Sum();
                    
                    var sumDesc = new SumDescription();

                    //SumDescription properties
                    sumDesc.VariableCode = pcaxisVariable.Code;
                    sumDesc.ValueCodes.Add(lblHidden1.Text);

                    if (this.ucValueSelect.GetSelectionCount() > 1)
                    {
                        sumDesc.ValueCodes.Add(lblHidden2.Text);
                    }

                    sumDesc.NewValueName = valueName;
                    sumDesc.NewValueCode = valueCode;
                    sumDesc.KeepValues = rbInclusive.Checked;
                    sumDesc.ConstantValue = constantValue;
                    sumDesc.CalculateWithConstant = calculateWithConstant;
                    sumDesc.Operation = _operation;

                    //Execute
                    SelectedModel = sumOperation.Execute(SelectedModel, sumDesc);

                    if (_isRecording)
                    {
                        _operationsTracker.AddStep(OperationConstants.SUM, sumDesc);
                        Variable va = SelectedModel.Meta.Variables.FirstOrDefault(v => v.Code == sumDesc.VariableCode);
                        if (va != null && va.IsTime)
                        {
                            _operationsTracker.IsTimeDependent = true;
                        }
                    }
                }
            }
        }

        private void SetValue(Selection sel)
        {                
            var selectedCount = ucValueSelect.GetSelection().ValueCodes.Count;

            if (selectedCount == 0)
            {
                lblHidden1.Text = string.Empty;
                lblSelectedValue1.Text = string.Empty;
            }

            var setfirst = selectedCount == 1;

            foreach (var code in sel.ValueCodes)
            {
                if (setfirst)
                {
                    lblHidden2.Text = string.Empty;
                    lblSelectedValue2.Text = string.Empty;

                    var text1 = ucValueSelect.Variable.Values.GetByCode(code).Value;
                    if (text1.Count() > 16)
                    {
                        text1 = text1.Substring(0, 16) + " ...";
                    }
                    lblHidden1.Text = code;
                    lblSelectedValue1.Text = text1;
                }
                    //Otherwise two values has been selected and we want to persist
                    //the first selection, regardless the index of the second selection
                else if(code != lblHidden1.Text)
                {
                    var text2 = ucValueSelect.Variable.Values.GetByCode(code).Value;
                    if (text2.Count() > 16)
                    {
                        text2 = text2.Substring(0, 16) + " ...";
                    }
                    lblHidden2.Text = code;
                    lblSelectedValue2.Text = text2;
                }
            }            
        }

        private void CalculateDialog_Load(Object sender, EventArgs e)
        {
            lblSelectedValue1.Text = string.Empty;
            lblHidden1.Text = string.Empty;
            lblSelectedValue2.Text = string.Empty;
            lblHidden2.Text = string.Empty;

            switch (_operation)
            {
                case SumOperationType.Addition:
                    lblOperator.Text = "+";
                    break;
                case SumOperationType.Division:
                    lblOperator.Text = "/";
                    break;
                case SumOperationType.Multiplication:
                    lblOperator.Text = "*";
                    break;
                case SumOperationType.Subtraction:
                    lblOperator.Text = "-";
                    break;
                default:
                    lblOperator.Text = "";
                    break;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        { 
            //Check that one value and a constant or two values are selected
            {
                if (!((ucValueSelect.lstValues.SelectedItems.Count == 1 && tbConstant.Text.Length > 0) || (ucValueSelect.lstValues.SelectedItems.Count == 2)))
                {
                    MessageBox.Show(Lang.GetLocalizedString("OperationCalculateSelectValueMessage"),
                        Lang.GetLocalizedString("OperationCalculateSelectErrorMessage"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                //Check that the new value has text and code
                if (string.IsNullOrWhiteSpace(tbName.Text) || string.IsNullOrWhiteSpace(tbCode.Text))
                {
                    MessageBox.Show(Lang.GetLocalizedString("OperationPercentValueNeededText"),
                    Lang.GetLocalizedString("OperationPercentRequiredValue"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                //Calculate
                if (ucValueSelect.lstValues.SelectedItems.Count > 0)
                {
                    double constant = 0;
                    var calcWithConstant = false;
                    if (tbConstant.Text.Length > 0)
                    {
                        constant = Convert.ToDouble(tbConstant.Text);
                        calcWithConstant = true;
                    }

                    try
                    {
                        CalculateValues(constant, tbName.Text, tbCode.Text, calcWithConstant);
                        DialogResult = DialogResult.OK;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, Lang.GetLocalizedString("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);                    
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void comboVariables_SelectedIndexChanged(object sender, EventArgs e)
        {
            ucValueSelect.Variable = (Variable)comboVariables.SelectedItem;
        }

        private void ucValueSelect_VariableSelected(object sender, EventArgs e)
        {
            var vsc = (ValueSelectControl)sender;
            var lst =  vsc.lstValues;
            
            //Making sure that only two variables can be selected
            if (lst.SelectedItems.Count > 2)
            {                                  

                for (int i = 0; i < lst.SelectedItems.Count; i++)
                {
                    if (!_selectedItems.Any(x => x.Equals(lst.SelectedItems[i])))
                    {
                        lst.SelectedItems.Remove(lst.SelectedItems[i]);
                    }
                }

                MessageBox.Show(Lang.GetLocalizedString("OperationCalculateTooManyValues"),
                       Lang.GetLocalizedString("OperationCalculateSelectErrorMessage"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //One variable is selected - calculation with constant is possible
            if (lst.SelectedItems.Count == 1)
            {
                tbConstant.Enabled = true;
            }
            else
            {
                tbConstant.Text = "";
                tbConstant.Enabled = false;
            }

            //More than two variables are selected - calculation with variable values are possible
            //Only two values are allowed, deselect first until only two are left
            if (lst.SelectedItems.Count > 2)
            {               
                var goOn = true;
                while (goOn)
                {
                    lst.SetSelected(lst.Items.IndexOf(lst.SelectedItems[0]), false);
                    if (lst.SelectedItems.Count == 2)
                    {
                        goOn = false;
                    }
                }
            }   
  
            _selectedItems.Clear();
             
            foreach (var item in lst.SelectedItems)
            {
                _selectedItems.Add(item);
            }

            // Set selected           
            SetValue(vsc.GetSelection());
        }
    }
}