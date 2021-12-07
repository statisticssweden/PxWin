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

namespace PCAxis.Desktop.OperationDialogs
{
    public partial class ChangeValueOrderDialog : Form
    {
        #region "Private members"
        private ChangeValueOrderDescription _changeValueOrderDescription;
        

        #endregion

        public ChangeValueOrderDialog()
        {
            InitializeComponent();
            SetLanguage();
        }

        public ChangeValueOrderDescription GetOperationDescription()
        {
            return _changeValueOrderDescription;
        }

        private void SetLanguage()
        {
            Text = Lang.GetLocalizedString("OperationDeleteValueError");
            lblFromOrder.Text = Lang.GetLocalizedString("OperationChangeValueOrderFromOrder");
            lblToOrder.Text = Lang.GetLocalizedString("OperationChangeValueOrderToOrder");
            btnOk.Text = Lang.GetLocalizedString("Ok");
            btnCancel.Text = Lang.GetLocalizedString("Cancel");
            btnSortAscending.Text = Lang.GetLocalizedString("OperationChangeValueOrderSortAsce");
            btnSortDescending.Text = Lang.GetLocalizedString("OperationChangeValueOrderSortDesc");
            btnMoveRest.Text = Lang.GetLocalizedString("OperationChangeValueOrderMoveRest");
            
        }

        #region "Private"
        private Selection _newVariableSelection;

        private ListBox _currentListBox;
        #endregion

        #region "Properties"
        /// <summary>
        /// Current PXModel to perform operations on
        /// </summary>
        /// <remarks></remarks>
        public PXModel SelectedModel { get; set; }

        /// <summary>
        /// Selected variable
        /// </summary>
        /// <remarks></remarks>
        private PCAxis.Paxiom.Variable _variable;
        public Variable SelectedVariable
        {
            get { return _variable; }
            set
            {
                _variable = value;
                Init();
            }
        }

        #endregion

        private void Init()
        {
            // initialize controls
            lbFromOrder.Items.Clear();
            lbToOrder.Items.Clear();
            btnOk.Enabled = false;

            foreach (var variableValue in _variable.Values)
            {
                lbFromOrder.Items.Add(variableValue);
            }

            lbFromOrder.SelectedIndex = -1;

            _currentListBox = lbFromOrder;

            Text = _variable.Name;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (ChangeValueOrder())
            {
                DialogResult = DialogResult.OK;
            }
        }

        private int[] GetModWeight(Variable oldVariable, Variable newVariable)
        {
            var modWeight = new int[oldVariable.Values.Count];

            for (var i = 0; i <= oldVariable.Values.Count - 1; i++)
            {
                modWeight[i] = newVariable.Values.GetIndexByCode(oldVariable.Values[i].Code);
            }

            return modWeight;
        }

        /// <summary>
        /// Sort a listbox
        /// </summary>
        /// <param name="listBox">ByRef listbox to sort</param>
        /// <param name="direction">sort direction: D or A</param>
        /// <remarks></remarks>
        private void SortListBox(string direction)
        {
            if (lbToOrder.Items.Count < 2) return;
            IOrderedEnumerable<Value> sortedList = null;
            switch (direction)
            {
                case "D":
                    sortedList = lbToOrder.Items.Cast<Value>().ToList().OrderByDescending(x => x.Code);         
                    break;
                case "A":
                    sortedList = lbToOrder.Items.Cast<Value>().ToList().OrderBy(x => x.Code);         
                    break;
            }

            if (sortedList == null) return;
            lbToOrder.Items.Clear();
            foreach (var item in sortedList)
            {
                lbToOrder.Items.Add(item);
            }   
        }


        /// <summary>
        /// Checks if the OK button should be enabled. Criteria is that no items is left in the from-listbox
        /// </summary>
        /// <remarks></remarks>
        private void ShouldWeEnableTheOKButton()
        {
            btnOk.Enabled = lbFromOrder.Items.Count == 0;
        }

        /// <summary>
        /// Set values for variable
        /// </summary>
        /// <remarks></remarks>
        private Variable GetVariableWithChangedValue()
        {
            //Dim paxiomSelection As New PCAxis.Paxiom.Selection(_variable.Code)
            Value paxiomValue;
            Variable newVar;

            newVar = _variable.CreateCopy();
            newVar.RecreateValues();

            // Now add them again in new order
            for (int index = 0; index <= lbToOrder.Items.Count - 1; index++)
            {
                paxiomValue = (Value)lbToOrder.Items[index];
                newVar.Values.Add(paxiomValue);
            }

            return newVar;
        }

        /// <summary>
        /// Calls Paxiom Operation for ChangeValueOrder
        /// </summary>
        /// <returns>Boolean</returns>
        /// <remarks></remarks>
        private bool ChangeValueOrder()
        {
            _changeValueOrderDescription = new PCAxis.Paxiom.Operations.ChangeValueOrderDescription();
            PCAxis.Paxiom.Operations.ChangeValueOrder changeValueOrderOperation = new PCAxis.Paxiom.Operations.ChangeValueOrder();
            PCAxis.Paxiom.Variable changedVariable;

            changedVariable = GetVariableWithChangedValue();

            _changeValueOrderDescription.VariableCode = changedVariable.Code;
            _changeValueOrderDescription.ModifiedVariableValueWeight = GetModWeight(_variable, changedVariable);

            try
            {
                SelectedModel = changeValueOrderOperation.Execute(this.SelectedModel, _changeValueOrderDescription);
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

        private void btnSortAscending_Click(object sender, EventArgs e)
        {
            SortListBox("A");
            lbToOrder.Focus();
        }

        private void btnSortDescending_Click(object sender, EventArgs e)
        {
            SortListBox("D");
            lbToOrder.Focus();
        }

        private void btnMoveRest_Click(object sender, EventArgs e)
        {
            // add all items from from-listbox to the to-listbox           
            for (int index = 0; index <= lbFromOrder.Items.Count - 1; index++)
            {
                object obj = lbFromOrder.Items[index];
                lbToOrder.Items.Add(obj);
            }
            // remove all items from from-listbox
            for (int index = lbFromOrder.Items.Count - 1; index >= 0; index += -1)
            {
                lbFromOrder.Items.RemoveAt(index);
            }

            ShouldWeEnableTheOKButton();
            lbToOrder.Focus();
        }

        private void btnToOrder_Click(object sender, EventArgs e)
        {
            // Loop from-listbox and add selected items to the to-listbox

            {
                for (int index = 0; index <= lbFromOrder.Items.Count - 1; index++)
                {
                    if (lbFromOrder.GetSelected(index))
                    {
                        object obj = lbFromOrder.Items[index];
                        lbToOrder.Items.Add(obj);
                    }
                }
                // remove selected items from from-listbox
                for (int index = lbFromOrder.Items.Count - 1; index >= 0; index += -1)
                {
                    if (lbFromOrder.GetSelected(index))
                    {
                        lbFromOrder.Items.RemoveAt(index);
                    }
                }

                ShouldWeEnableTheOKButton();
                lbToOrder.Focus();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnFromOrder_Click(object sender, EventArgs e)
        {          
	        // add selected items in to-listbox to the from-listbox
	        for (int index = 0; index <= lbToOrder.Items.Count - 1; index++) {
		        if (lbToOrder.GetSelected(index))
                {
			        var obj = lbToOrder.Items[index];
			        lbFromOrder.Items.Add(obj);
		        }
	        }
	        // now remove selected items from the to-listbox
	        for (int index = lbToOrder.Items.Count - 1; index >= 0; index += -1)
            {
		        if (lbToOrder.GetSelected(index))
                {
			        lbToOrder.Items.RemoveAt(index);
		        }
	        }
	        ShouldWeEnableTheOKButton();
            lbFromOrder.Focus();
        }

    }
}
