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

namespace PCAxis.Desktop.OperationDialogs
{
    public partial class SumDialog : Form
    {

        private PXModel _Model;  
        public SumDialog()
        {
            InitializeComponent();
            SetLanguage();       
        }           

        public PXModel SelectedModel {
            get { return _Model; }
            set
            {
                _Model = value;              
                RefreshVariables();
            } 
        }

        private void SetLanguage()
        {
            Text = Lang.GetLocalizedString("OperationSum");
            lblSelectVar.Text = Lang.GetLocalizedString("OperationSumSelectVariable");
            gbInitialValue.Text = Lang.GetLocalizedString("OperationSumInitialValue");
            gbNewValue.Text = Lang.GetLocalizedString("OperationSumNewValue");
            rbInclude.Text = Lang.GetLocalizedString("OperationSumInclude");
            rbExclude.Text = Lang.GetLocalizedString("OperationSumExclude");
            lblName.Text = Lang.GetLocalizedString("OperationSumName");
            lblCode.Text = Lang.GetLocalizedString("OperationSumCode");
            btnOK.Text = Lang.GetLocalizedString("Ok");
            btnCancel.Text = Lang.GetLocalizedString("Cancel");
        }

        private void RefreshVariables()
        {
            comboVariables.DisplayMember = "Name";
            comboVariables.DataSource = SelectedModel.Meta.Variables;
        }

        public SumDescription GetDescription()
        {
            var sumDesc = new SumDescription();

            sumDesc.KeepValues = rbInclude.Checked;
            sumDesc.NewValueCode = tbCode.Text;
            sumDesc.NewValueName = tbName.Text;
            sumDesc.VariableCode = ((Variable)comboVariables.SelectedItem).Code;
            sumDesc.Operation = SumOperationType.Addition;
            sumDesc.CalculateWithConstant = false;
            var sel = ucValueSelect.GetSelection();

            foreach (string code in sel.ValueCodes)
            {
                sumDesc.ValueCodes.Add(code);
            }

            // If we summarize all values and exclude the operands (i.e resulting number of values is 1)
            // we should delete the variable.
            sumDesc.DoEliminationForSumAll = true;
            return sumDesc;
        }


        private void btnOK_Click(object sender, EventArgs e)
        {           
            if (ucValueSelect.GetSelectionCount() > 0)
            {
                if (!string.IsNullOrWhiteSpace(tbName.Text) && !string.IsNullOrWhiteSpace(tbCode.Text))
                {
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show(Lang.GetLocalizedString("OperationPercentValueNeededText"),
                    Lang.GetLocalizedString("OperationPercentRequiredValue"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show(Lang.GetLocalizedString("OperationSumSelectValueMessage"),
                    Lang.GetLocalizedString("OperationSum"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void comboVariables_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboVariables.SelectedItem == null)
            {
                ucValueSelect.Variable = null;
            }
            else
            {
                ucValueSelect.Variable = (Variable) comboVariables.SelectedItem;
            }
        }

        private void ucValueSelect_Load(object sender, EventArgs e)
        {
            var vsc = (ValueSelectControl)sender;
            var lst = vsc.lstValues;
            lst.SelectionMode = SelectionMode.MultiExtended;         
        }
    }
}
