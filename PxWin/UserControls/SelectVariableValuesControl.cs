using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PCAxis.Paxiom;
using System.Diagnostics;
using PCAxis.Query;

namespace PCAxis.Desktop.UserControls
{
    public partial class SelectVariableValuesControl : UserControl
    {

        #region "Dialog events"

        /// <summary>
        /// Class for holding data for the GroupinSelect event
        /// </summary>
        public class GroupingSelectEventArgs : System.EventArgs
        {
            private PCAxis.Paxiom.Variable _variable;
            private PCAxis.Paxiom.GroupingInfo _groupingInfo;
            private PCAxis.Paxiom.GroupingIncludesType _groupingInclude;

            public PCAxis.Paxiom.Variable Variable
            {
                get
                {
                    return _variable;
                }
            }
            
            public PCAxis.Paxiom.GroupingInfo GroupingInfo
            {
                get
                {
                    return _groupingInfo;
                }
            }
            
            public PCAxis.Paxiom.GroupingIncludesType GroupingInclude
            {
                get
                {
                    return _groupingInclude;
                }
            }

            public GroupingSelectEventArgs(PCAxis.Paxiom.Variable variable, PCAxis.Paxiom.GroupingInfo groupingInfo, PCAxis.Paxiom.GroupingIncludesType groupInclude)
            {
                _variable = variable;
                _groupingInfo = groupingInfo;
                _groupingInclude = groupInclude;
            }
        }

        /// <summary>
        /// Class for holding data for the ValuesetSelect event
        /// </summary>
        public class ValuesetSelectEventArgs : System.EventArgs
        {
            private PCAxis.Paxiom.Variable _variable;
            private PCAxis.Paxiom.ValueSetInfo _valuesetInfo;

            public PCAxis.Paxiom.Variable Variable
            {
                get
                {
                    return _variable;
                }
            }

            public PCAxis.Paxiom.ValueSetInfo ValuesetInfo
            {
                get
                {
                    return _valuesetInfo;
                }
            }

            public ValuesetSelectEventArgs(PCAxis.Paxiom.Variable variable, PCAxis.Paxiom.ValueSetInfo valuesetInfo)
            {
                _variable = variable;
                _valuesetInfo = valuesetInfo;
            }
        }

        public class ControlResizedEventArgs : System.EventArgs
        {
            private int _width;
            private int _height;

            public int Width
            {
                get
                {
                    return _width;
                }
            }

            public int Height
            {
                get
                {
                    return _height;
                }
            }

            public ControlResizedEventArgs(int width, int height)
            {
                _width = width;
                _height = height;
            }
        }

        public delegate void GroupingSelectedEventHandler(object sender, GroupingSelectEventArgs e);
        public delegate void ValuesetSelectedEventHandler(object sender, ValuesetSelectEventArgs e);
        public delegate void ControlResizedEventHandler(object sender, ControlResizedEventArgs e);

        public event GroupingSelectedEventHandler GroupingSelected;
        public event ValuesetSelectedEventHandler ValuesetSelected;
        public event ControlResizedEventHandler ControlResized;
        public event EventHandler SelectionChanged;

        protected virtual void OnGroupingSelected(GroupingSelectEventArgs e)
        {
            GroupingSelectedEventHandler handler = GroupingSelected;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnValuesetSelected(ValuesetSelectEventArgs e)
        {
            ValuesetSelectedEventHandler handler = ValuesetSelected;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnControlResized(ControlResizedEventArgs e)
        {
            ControlResizedEventHandler handler = ControlResized;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnSelectionChanged(EventArgs e)
        {
            EventHandler handler = SelectionChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion

        private class ListboxItem
        {
            public string Text { get; set; }
            public string Value { get; set; }

            public ListboxItem(string text, string value)
            {
                Text = text;
                Value = value;
            }
        }


        private PCAxis.Paxiom.Variable _variable;
        
        /// <summary>
        /// Constructor
        /// </summary>
        public SelectVariableValuesControl()
        {
            InitializeComponent();

            this.toolTipGroup.SetToolTip(btnGroup, Lang.GetLocalizedString("SelectValuesCtrlGroupTooltip"));
            this.toolTipFilter.SetToolTip(btnFilter, Lang.GetLocalizedString("SelectValuesCtrlFilterTooltip"));
            this.toolTipSelectAll.SetToolTip(btnSelectAll, Lang.GetLocalizedString("SelectValuesCtrlSelectAllTooltip"));
            this.toolTipDeselect.SetToolTip(btnUnselect, Lang.GetLocalizedString("SelectValuesCtrlUnselectTooltip"));
            this.toolTipAscending.SetToolTip(btnDown, Lang.GetLocalizedString("SelectValuesCtrlAscendingTooltip"));
            this.toolTipDescending.SetToolTip(btnUp, Lang.GetLocalizedString("SelectValuesCtrlDescendingTooltip"));
            this.toolTipSearch.SetToolTip(btnSearch, Lang.GetLocalizedString("SelectValuesCtrlSearchTooltip"));
        }

        public PCAxis.Paxiom.Variable Variable 
        {
            get
            {
                return _variable;
            }
            set
            {
                if (value != null)
                {
                    _variable = value;

                    // Copy values to another collection so we don´t mess up the order of the values in the variable
                    Values displayValues = new Values(_variable);
                    foreach (Value val in _variable.Values)
                    {
                        displayValues.Add(val);
                    }

                    if (_variable.IsTime)
                    {
                        ////TODO Maria behövs kontrollen IsNumerical?. Samma sorteringsordning oavsett tid med siffror eller tid med sifftor o bokstäver, se SortValues(...)
                        ////I PX-Web utförs kontrollen och om tid inte är numeriskt så utförs en annan sortering.
                        //if (IsNumericalTimeVariable(_variable.Values))
                        //{
                        //    _variable.Values.Sort((e1, e2) => e2.Text.CompareTo(e1.Text));

                        //}
                        //else
                        //{
                        //    _variable.Values.Sort((e1, e2) => e2.Text.CompareTo(e1.Text));

                        //}

                        if (IsNumericalTimeVariable(displayValues))
                        {
                            displayValues.Sort((e1, e2) => int.Parse(e2.Code).CompareTo(int.Parse(e1.Code)));
                        }
                        else
                        {
                            displayValues.Sort((e1, e2) => e2.Code.CompareTo(e1.Code));
                        }
                    }
                    
                    lstValues.BeginUpdate();
                    lstValues.Items.Clear();
                    foreach (PCAxis.Paxiom.Value val in displayValues)
                    {
                        lstValues.Items.Add(new ListboxItem(val.Text, val.Code));
                    }

                    lstValues.DisplayMember = "Text";
                    lstValues.ValueMember = "Value";

                    lstValues.EndUpdate();
                    lblVariableName.Text = _variable.Name;

                    if (_variable.Elimination == false)
                    {
                        lblVariableName.Text = lblVariableName.Text + " *";
                    }

                    if ((_variable.HasGroupings() == false) && (_variable.HasValuesets() == false))
                    {
                        btnGroup.Visible = false;
                    }

                    lblCount.Text = GetSelectionCount();
                }
            }
        }
        /// <summary>
        /// Check if it is a legal time variable
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        private Boolean IsNumericalTimeVariable(Values var)
        {
            int res;

            for (int i = 0; i < var.Count; i++)
            {
                if (!int.TryParse(var[i].Code, out res))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Get selected values from the control
        /// </summary>
        /// <returns>Selection object holding the selected values</returns>
        public PCAxis.Paxiom.Selection GetSelection()
        {
            PCAxis.Paxiom.Selection s = new Selection(_variable.Code);

            for (int i = 0; i < lstValues.Items.Count; i++)
            {
                if (lstValues.GetSelected(i))
                {
                    //s.ValueCodes.Add(_variable.Values[i].Code);
                    s.ValueCodes.Add(((ListboxItem)lstValues.Items[i]).Value);
                }
            }

            // Time variable shall be sorted ascending
            if (_variable.IsTime)
            {
                System.Collections.ArrayList.Adapter(s.ValueCodes).Sort();
            }

            return s;
        }

        /// <summary>
        /// Check that a valid selection is made. If the variable cannot be eliminated at least one value must be selected
        /// </summary>
        /// <returns>True if a valid selection has been made, else false</returns>
        public bool CheckSelection()
        {
            if (lstValues.SelectedItems.Count > 0)
            {
                return true;
            }
            else if ((lstValues.SelectedItems.Count == 0) && (_variable.Elimination))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int GetNumberOfSelectedValues()
        {
            return lstValues.SelectedItems.Count;
        }

        /// <summary>
        /// Number of items in the control
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public int GetItemCount()
        {
            return lstValues.Items.Count;
        }

        private string GetSelectionCount()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(lstValues.SelectedItems.Count.ToString());
            sb.Append(" ");
            sb.Append(Lang.GetLocalizedString("SelectValuesCount"));
            sb.Append(" ");
            sb.Append(lstValues.Items.Count.ToString());

            return sb.ToString();
        }

        private void btnGroup_Click(object sender, EventArgs e)
        {
            using (GroupDialog frmGroup = new GroupDialog())
            {
                frmGroup.Variable = _variable;

                if (frmGroup.ShowDialog() == DialogResult.OK)
                {
                    if (frmGroup.OperationsInfo is PCAxis.Paxiom.GroupingInfo)
                    {
                        OnGroupingSelected(new GroupingSelectEventArgs(frmGroup.Variable, (PCAxis.Paxiom.GroupingInfo)frmGroup.OperationsInfo, GroupingIncludesType.AggregatedValues));
                    }
                    else
                    {
                        OnValuesetSelected(new ValuesetSelectEventArgs(frmGroup.Variable, (PCAxis.Paxiom.ValueSetInfo)frmGroup.OperationsInfo));
                    }
                }
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            using (FilterDialog frmFilter = new FilterDialog())
            {
                frmFilter.Variable = _variable;

                if (frmFilter.ShowDialog() == DialogResult.OK)
                {
                    foreach (string code in frmFilter.ValueCodes)
                    {
                        for (int i = 0; i < lstValues.Items.Count; i++)
                        {
                            ListboxItem itm = (ListboxItem)lstValues.Items[i];
                            if (itm.Value.Equals(code))
                            {
                                lstValues.SetSelected(i, true);
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void lstValues_SelectedValueChanged(object sender, EventArgs e)
        {
            CalculateSelection();
        }

        private void CalculateSelection()
        {
            lblCount.Text = GetSelectionCount();
            OnSelectionChanged(EventArgs.Empty);
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {   
            //Moved code from here to MarkAllValues so the method can be
            //called from several places
            SelectAllValues();
        }

        private void btnUnselect_Click(object sender, EventArgs e)
        {
            lstValues.ClearSelected();
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            SortValues(false);
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            SortValues(true);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string name;
            if (txtSearch.Text.Trim().Length == 0)
            {
                return;
            }

            for (int i = 0; i < lstValues.Items.Count; i++)
            {
                ListboxItem itm = (ListboxItem)lstValues.Items[i];
                name = itm.Text.ToLower();
                if (name.Contains(txtSearch.Text.ToLower()))
                {
                    lstValues.SetSelected(i, true);
                }
            }
        }

        private void SortValues(bool ascending)
        {
            List<ListboxItem> lst = new List<ListboxItem>();

            foreach (ListboxItem itm in lstValues.Items)
            {
                lst.Add(itm);
            }
            
            if (ascending)
            {
                lst.Sort((e1, e2) => e1.Text.CompareTo(e2.Text));
            }
            else
            {
                lst.Sort((e1, e2) => e2.Text.CompareTo(e1.Text));
            }

            lstValues.BeginUpdate();
            lstValues.Items.Clear();

            foreach (ListboxItem itm in lst)
            {
                lstValues.Items.Add(itm);
            }
            lstValues.EndUpdate();
        }

        private bool mouseClicked = false;

        private void imgResize_MouseDown(object sender, MouseEventArgs e)
        {
            mouseClicked = true;
        }

        private void imgResize_MouseUp(object sender, MouseEventArgs e)
        {
            mouseClicked = false;
        }

        private void imgResize_MouseMove(object sender, MouseEventArgs e)
        {
            bool changed = false;

            if (mouseClicked)
            {
                if ((imgResize.Top + e.Y) > this.MinimumSize.Height)
                {
                    this.Height = imgResize.Top + e.Y;
                    changed = true;
                }

                if ((imgResize.Left + e.X) > this.MinimumSize.Width)
                {
                    this.Width = imgResize.Left + e.X;
                    changed = true;
                }

                if (changed)
                {
                    OnControlResized(new ControlResizedEventArgs(this.Width, this.Height));
                }
            }
        }

        /// <summary>
        /// Apply the previous selection
        /// </summary>
        /// <param name="previousTableQuery"></param>
        public void ApplyPreviousSelection(TableQuery previousTableQuery, PXModel previousModel)
        {
            if (previousTableQuery == null)
            {
                return;
            }

            // Try to get previous selction from the TableQuery
            PCAxis.Query.Query query = previousTableQuery.Query.FirstOrDefault(q => q.Code.Equals(_variable.Code));

            if (query != null)
            {
                if (query.Selection.Filter.StartsWith("vs:"))
                {
                    // Valueset is selected
                    if (query.Selection.Filter.Length > 3)
                    {
                        string vsId = query.Selection.Filter.Substring(3);
                        PCAxis.Paxiom.ValueSetInfo vsInfo = _variable.ValueSets.FirstOrDefault(v => v.ID.Equals(vsId));
                        if (vsInfo != null)
                        {
                            OnValuesetSelected(new ValuesetSelectEventArgs(_variable, vsInfo));
                        }
                    }
                }
                else if (query.Selection.Filter.StartsWith("agg:"))
                {
                    // Aggregeation is selected
                    if (query.Selection.Filter.Length > 4)
                    {
                        string groupId = query.Selection.Filter.Substring(4);
                        PCAxis.Paxiom.GroupingInfo grpInfo = _variable.Groupings.FirstOrDefault(g => g.ID.Equals(groupId));
                        if (grpInfo != null)
                        {
                            OnGroupingSelected(new GroupingSelectEventArgs(_variable, grpInfo, GroupingIncludesType.AggregatedValues));
                        }
                    }
                }

                // Preselect the prevoiusly selected values
                for (int i=0; i < lstValues.Items.Count; i++)
                {
                    ListboxItem itm = (ListboxItem)lstValues.Items[i];
                    if (query.Selection.Values.Contains(itm.Value))
                    {
                        lstValues.SetSelected(i, true);
                    }
                }
            }
            else if (previousModel != null)
            {
                // query can be null if all values was selected for the variable. If so try to get the values from the model instead.
                Variable var = previousModel.Meta.Variables.GetByCode(_variable.Code);
                if (var != null)
                {
                    // Preselect the prevoiusly selected values
                    for (int i = 0; i < lstValues.Items.Count; i++)
                    {
                        ListboxItem itm = (ListboxItem)lstValues.Items[i];
                        if (var.Values.GetByCode(itm.Value) != null)
                        {
                            lstValues.SetSelected(i, true);
                        }
                    }
                }
            }
        }

        public void SelectAllValues()
        {
            var oldcur = this.Cursor;
            try
            {
                this.Cursor = Cursors.WaitCursor;

                int count = lstValues.Items.Count;
                lstValues.SelectedValueChanged -= lstValues_SelectedValueChanged;

                lstValues.BeginUpdate();
                for (int i = 0; i < count; i++)
                {
                    lstValues.SetSelected(i, true);
                }
                lstValues.EndUpdate();

                lstValues.SelectedIndexChanged += lstValues_SelectedValueChanged;
                CalculateSelection();

            }
            finally
            {
                this.Cursor = oldcur;
            }
            
        }

     }
}
