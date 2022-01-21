using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PCAxis.Paxiom;
using PCAxis.Desktop.UserControls;
using PCAxis.Query;

namespace PCAxis.Desktop
{
    public partial class SelectValuesDialog : Form
    {

        private IPXModelBuilder _builder;
        private SelectVariableValuesControl[] _controls;
        private long maxCells;
        private TableQuery _previousTableQuery;
        private PXModel _previousModel;
        
        public SelectValuesDialog(TableQuery previousTableQuery, PXModel previousModel)
        {
            InitializeComponent();
            SetLanguage();

            _previousTableQuery = previousTableQuery;
            _previousModel = previousModel;

            this.Text = Lang.GetLocalizedString("SelectValuesTitle");
            lblSelectedCells.Text = string.Format(Lang.GetLocalizedString("SelectValuesCtrlNumberOfSelectedCells"), 0);
            if (System.Configuration.ConfigurationManager.AppSettings.Get("maxCells") == null)
            {
                maxCells = long.MaxValue;
            }
            else
            {
                if (!long.TryParse(System.Configuration.ConfigurationManager.AppSettings.Get("maxCells").ToString(), out maxCells))
                {
                    maxCells = long.MaxValue;
                }
            }
        }

        public IPXModelBuilder Builder 
        {
            get
            { 
                return _builder;
            }
            set 
            {
                _builder = value;
                InitiateDialog();
            }
        }

        public PCAxis.Paxiom.PXMeta Meta
        {
            get
            {
                return Builder.Model.Meta;
            }
        }

        private void InitiateDialog()
        {
            //lblTableName.Text = Lang.GetTranslatedTitleForModel(Builder.Model.Meta.Title, Builder.Model.Meta.Language);
            lblTableName.Text = PCAxis.Util.GetModelTitle(Builder.Model); // Show DESCRIPTION if possible, else TITLE
            
            SelectVariableValuesControl ctrl;
            _controls = new SelectVariableValuesControl[Meta.Variables.Count];

            PXModel model = Builder.Model;

            //Check number of ContentCode variables. They should always be at the start of the array
            var startIndex = model.Meta.Variables.Count(variable => variable.IsContentVariable);
            var contentsCodeIndex = 0;

            var variablesForPresentation = new PCAxis.Paxiom.Variable[Meta.Variables.Count];

            if (startIndex > 0)
            {
                for (int i = 0; i < Meta.Variables.Count; i++)
                {
                    if (model.Meta.Variables[i].IsContentVariable) //ContentCodes goes to start of array
                    {
                        variablesForPresentation[contentsCodeIndex] = Meta.Variables[i];
                        contentsCodeIndex++;
                    }
                    else
                    {
                        variablesForPresentation[startIndex] = Meta.Variables[i];
                        startIndex++;
                    }

                }
            }
            else
            {

                Meta.Variables.CopyTo(variablesForPresentation);
            }
            for (int i = 0; i < Meta.Variables.Count; i++)
            {
                ctrl = new SelectVariableValuesControl();
                ctrl.Variable = variablesForPresentation[i];
                pnlVariables.Controls.Add(ctrl);
                _controls[i] = ctrl;
                _controls[i].GroupingSelected += SelectValuesDialog_GroupingSelected;
                _controls[i].ValuesetSelected += SelectValuesDialog_ValuesetSelected;
                _controls[i].ControlResized += SelectValuesDialog_ControlResized;

                if (_previousTableQuery != null)
                {
                    _controls[i].ApplyPreviousSelection(_previousTableQuery, _previousModel);
                }

                _controls[i].SelectionChanged += SelectValuesDialog_SelectionChanged;
            }

            GetNumberOfSelectedCells();
        }
        private void SetLanguage()
        {
            btnSelectAll.Text = Lang.GetLocalizedString("SelectValuesCtrlSelectAllButton");
            this.toolTipSelectAll.SetToolTip(btnSelectAll, Lang.GetLocalizedString("SelectValuesCtrlSelectAllTooltip"));
            btnOk.Text = Lang.GetLocalizedString("Ok");
            btnCancel.Text = Lang.GetLocalizedString("Cancel");
        }
        /// <summary>
        /// Get the selections made for all the variables
        /// </summary>
        /// <returns>An array with Selection objects</returns>
        public PCAxis.Paxiom.Selection[] GetSelection()
        {
            List<Selection> sels = new List<Selection>();
            Selection sel;

            for (int i = 0; i < _controls.Length; i++)
            {
                sel = _controls[i].GetSelection();
                sels.Add(sel);
            }

            return sels.ToArray();
        }

        /// <summary>
        /// Check that a valid selection is made. If a variable cannot be eliminated at least one value must be selected
        /// </summary>
        /// <returns>True if a valid selection has been made for all variables, else false</returns>
        private bool CheckSelection()
        {
            bool ok = true;
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < _controls.Length; i++)
            {
                if (_controls[i].CheckSelection() == false)
                {
                    ok = false;
                    sb.Append(_controls[i].Variable.Name + '\n');
                }
            }

            if (!ok)
            {
                MessageBox.Show(Lang.GetLocalizedString("SelectValuesAtLeastOneValue") + "\n\n" + sb.ToString(), Lang.GetLocalizedString("SelectValuesAtLeastOneValueTitle"));
            }

            return ok;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (CheckSelection())
            {
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        /// <summary>
        /// A valueset has been selected in the grouping dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SelectValuesDialog_ValuesetSelected(object sender, SelectVariableValuesControl.ValuesetSelectEventArgs e)
        {
            Builder.ApplyValueSet(e.Variable.Code, e.ValuesetInfo);

            SelectVariableValuesControl ctrl = (SelectVariableValuesControl)sender;
            ctrl.Variable = e.Variable;
        }

        /// <summary>
        /// A grouping has been selected in the grouping dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SelectValuesDialog_GroupingSelected(object sender, SelectVariableValuesControl.GroupingSelectEventArgs e)
        {
            Builder.ApplyGrouping(e.Variable.Code, e.GroupingInfo, e.GroupingInclude);
            
            SelectVariableValuesControl ctrl = (SelectVariableValuesControl)sender;
            ctrl.Variable = e.Variable;
        }

        /// <summary>
        /// One of the control has been resized. Set the same height on the other controls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SelectValuesDialog_ControlResized(object sender, SelectVariableValuesControl.ControlResizedEventArgs e)
        {
            foreach (SelectVariableValuesControl ctrl in _controls)
            {
                if (ctrl != sender)
                {
                    ctrl.Height = e.Height;
                    ctrl.Width = e.Width;
                }
            }
        }

        private void SelectValuesDialog_Resize(object sender, EventArgs e)
        {
            lblTableName.MaximumSize = new Size(this.Width - 50, 100);
        }

        /// <summary>
        /// Selection has been changed in one of the controls. Calculate the total number of selected cells
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SelectValuesDialog_SelectionChanged(object sender, EventArgs e)
        {
            GetNumberOfSelectedCells();
        }

        private void GetNumberOfSelectedCells()
        {
            long total = 1;
            long number;
            bool hasValue = false;
            //bool tooManyValues = false; // Temporary bugfix for handling problem in SqlBuilder TODO: Remove after bug has been fixed

            foreach (SelectVariableValuesControl ctrl in _controls)
            {
                number = (long)ctrl.GetNumberOfSelectedValues();

                if (number > 0)
                {
                    total = total * number;
                    hasValue = true;
                }

                //// Temporary bugfix for handling problem in SqlBuilder TODO: Remove after bug has been fixed
                //if (Builder is PCAxis.PlugIn.Sql.PXSQLBuilder && number > 2000)
                //{
                //    tooManyValues = true;
                //}
            }

            if (!hasValue)
            {
                total = 0;
            }
            //Reqtest 105. TFS 48750 Thousands Separator on lable number of selected cells
            if (maxCells >= total)
            {
                btnOk.Enabled = true;
                lblSelectedCells.ForeColor = Color.Black;
                lblSelectedCells.Text = string.Format(Lang.GetLocalizedString("SelectValuesCtrlNumberOfSelectedCells"), String.Format("{0:### ### ###}", total));
                
            }
            else
            {
                btnOk.Enabled = false;
                lblSelectedCells.ForeColor = Color.Red;
                lblSelectedCells.Text = string.Format(Lang.GetLocalizedString("SelectValuesCtrlNumberOfSelectedCellsMaximum"), String.Format("{0:### ### ###}", total), String.Format("{0:### ### ###}", maxCells));
            }

            //// Temporary bugfix for handling problem in SqlBuilder TODO: Remove after bug has been fixed
            //if (tooManyValues == true)
            //{
            //    btnOk.Enabled = false;
            //    lblSelectedCells.ForeColor = Color.Red;
            //    lblSelectedCells.Text = "At the moment you cannot select more than 2000 values for a variable. This problem will be fixed as soon as possible...";
            //}

        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < _controls.Length; i++)
            {
                _controls[i].SelectAllValues();

            }
        }
    }
}
