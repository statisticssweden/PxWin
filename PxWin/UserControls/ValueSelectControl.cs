using System.Windows.Forms;
using System;
using System.Runtime.InteropServices;
using PCAxis.Paxiom;
using PCAxis.Desktop.OperationDialogs;

namespace PCAxis.Desktop.UserControls
{
    public class ValueSelectControl : UserControl
    {
        public ValueSelectControl()
        {
            InitializeComponent();
            SetLanguage();
        }

        private void SetLanguage()
        {
            btnSelectAll.Text = Lang.GetLocalizedString("ValueSelectSelectAll");
            btnDeselectAll.Text = Lang.GetLocalizedString("ValueSelectDeselectAll");
        }

        #region "API stuff"

        //Används för att göra API anrop till listboxen för att markera/avmarkera alla värden i en listbox
        public const int LB_SELITEMRANGE = 0x19b;
        [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        #endregion

        #region "Private members"

        private Variable _variable = null;
        private bool _showTopButtons = true;

        private bool _showBottomButtons = true;
        #endregion

        #region "handlers"

        public void SelectAll()
        {
            //SendMessage(lstValues.Handle, LB_SELITEMRANGE, 1, (lstValues.Items.Count << 16))
            int count = 0;
            int i = 0;
            count = this.lstValues.Items.Count;
            for (i = 0; i <= count - 1; i++)
            {
                lstValues.SetSelected(i, true);
            }
        }

        public void DeselectAll()
        {
            //SendMessage(lstValues.Handle, LB_SELITEMRANGE, 0, (lstValues.Items.Count << 16))
            int count = 0;
            int i = 0;
            count = this.lstValues.Items.Count;
            for (i = 0; i <= count - 1; i++)
            {
                lstValues.SetSelected(i, false);
            }
        }

        private void SelectStart()
        {
            string name = null;
            for (int index = 0; index <= lstValues.Items.Count - 1; index++)
            {
                name = lstValues.Items[index].ToString();
                if (name.StartsWith(txtSearch.Text))
                {
                    lstValues.SetSelected(index, true);
                }
            }
        }

        #endregion

        public class GroupingSelectEvent : EventArgs
        {
            public Variable Variable { get; private set; }
            
            public GroupingInfo GroupingInfo { get; private set; }

            public GroupingIncludesType GroupingInclude { get; private set; }

            public GroupingSelectEvent(Variable variable, GroupingInfo groupingInfo, GroupingIncludesType groupInclude)
            {
                Variable = variable;
                GroupingInfo = groupingInfo;
                GroupingInclude = groupInclude;
            }
        }

        public class ValueSetSelectEvent : EventArgs
        {
            public Variable Variable { get; private set; }

            public ValueSetInfo ValueSetInfo { get; private set; }

            public ValueSetSelectEvent(Variable variable, ValueSetInfo valuesetInfo)
            {
                Variable = variable;
                ValueSetInfo = valuesetInfo;
            }
        }

        #region "Custom Eventhandlers"

        public event VariableSelectedEventHandler VariableSelected;
        public delegate void VariableSelectedEventHandler(object sender, EventArgs e);
        public event GroupSelectedEventHandler GroupSelected;
        public delegate void GroupSelectedEventHandler(object sender, GroupingSelectEvent e);
        public event ValueSetSelectedEventHandler ValueSetSelected;
        public delegate void ValueSetSelectedEventHandler(object sender, ValueSetSelectEvent e);

        private void lstValues_SelectedIndexChanged(System.Object sender, EventArgs e)
        {
            if (VariableSelected != null)
            {
                VariableSelected(this, e);
            }
        }

        #endregion

        #region "Button Eventhandlers"

        private void btnSelectAll_Click_1(object sender, EventArgs e)
        {
            SelectAll();
            lstValues.Focus();
        }

        private void btnDeselectAll_Click_1(object sender, EventArgs e)
        {
            DeselectAll();
            lstValues.Focus();
        }

        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            SelectStart();
        }


        private void btnGroup_Click_1(object sender, EventArgs e)
        {
            using (var frmGroup = new GroupingSelectDialog())
            {
                frmGroup.Variable = this.Variable;
                if (frmGroup.ShowDialog() == DialogResult.OK)
                {
                    btnGroup.Enabled = false;

                    var info = frmGroup.OperationsInfo as GroupingInfo;

                    if (info != null)
                    {
                        if (GroupSelected != null)
                        {
                            GroupSelected(this, new GroupingSelectEvent(Variable, info, frmGroup.GroupInclude));
                        }
                    }
                    else
                    {
                        if (ValueSetSelected != null)
                        {
                            ValueSetSelected(this, new ValueSetSelectEvent(Variable, (PCAxis.Paxiom.ValueSetInfo)frmGroup.OperationsInfo));
                        }
                    }
                }
            }
        }

        #endregion

        #region "Properties"

        public Variable Variable
        {
            get { return this._variable; }
            set
            {
                if ((value != null))
                {
                    this._variable = value;

                    this.lstValues.BeginUpdate();
                    this.lstValues.Items.Clear();
                    foreach (var v in value.Values)
                    {
                        this.lstValues.Items.Add(v);
                    }
                    this.lstValues.EndUpdate();


                    this.lblVariablename.Text = this._variable.Name;
                }

            }
        }

        public bool ShowTopButtons
        {
            get { return this._showTopButtons; }
            set
            {
                if (DesignMode && Parent != null)
                {
                    Parent.Refresh();
                }
                this._showTopButtons = value;
                this.pnlTools.Visible = this._showTopButtons;
            }
        }

        public bool ShowBottomButtons
        {
            get { return this._showBottomButtons; }
            set
            {
                this._showBottomButtons = value;
                this.pnlSelect.Visible = this._showBottomButtons;
            }
        }

        #endregion

        public bool CheckSelection()
        {
            if (lstValues.SelectedItems.Count == 0 & _variable.Elimination)
            {
                return true;
            }
            else if (lstValues.SelectedItems.Count > 0)
            {
                return true;
            }

            return false;
        }

        public Selection GetSelection()
        {
            var s = new Selection(_variable.Code);

            for (var index = 0; index <= lstValues.Items.Count - 1; index++)
            {
                if (lstValues.GetSelected(index))
                {
                    s.ValueCodes.Add(_variable.Values[index].Code);
                }
            }

            return s;
        }

        public int GetSelectionCount()
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
        
        #region Autogenerated from design

        private Button btnGroup;
        internal ListBox lstValues;
        private Button btnSelectAll;
        private Button btnDeselectAll;
        private TextBox txtSearch;
        private Button btnSearch;
        private Label lblVariablename;

        private void InitializeComponent()
        {
            this.btnGroup = new System.Windows.Forms.Button();
            this.lstValues = new System.Windows.Forms.ListBox();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnDeselectAll = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.pnlTools = new System.Windows.Forms.Panel();
            this.lblVariablename = new System.Windows.Forms.Label();
            this.pnlSelect = new System.Windows.Forms.Panel();
            this.pnlTools.SuspendLayout();
            this.pnlSelect.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGroup
            // 
            this.btnGroup.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnGroup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGroup.Location = new System.Drawing.Point(273, 1);
            this.btnGroup.Name = "btnGroup";
            this.btnGroup.Size = new System.Drawing.Size(75, 23);
            this.btnGroup.TabIndex = 0;
            this.btnGroup.Text = "Group";
            this.btnGroup.UseVisualStyleBackColor = false;
            this.btnGroup.Click += new System.EventHandler(this.btnGroup_Click_1);
            // 
            // lstValues
            // 
            this.lstValues.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstValues.FormattingEnabled = true;
            this.lstValues.Location = new System.Drawing.Point(0, 27);
            this.lstValues.Name = "lstValues";
            this.lstValues.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lstValues.Size = new System.Drawing.Size(350, 197);
            this.lstValues.TabIndex = 2;
            this.lstValues.SelectedIndexChanged += new System.EventHandler(this.lstValues_SelectedIndexChanged);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnSelectAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectAll.Location = new System.Drawing.Point(1, 2);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(75, 23);
            this.btnSelectAll.TabIndex = 0;
            this.btnSelectAll.Text = "Select All";
            this.btnSelectAll.UseVisualStyleBackColor = false;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click_1);
            // 
            // btnDeselectAll
            // 
            this.btnDeselectAll.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnDeselectAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeselectAll.Location = new System.Drawing.Point(78, 2);
            this.btnDeselectAll.Name = "btnDeselectAll";
            this.btnDeselectAll.Size = new System.Drawing.Size(75, 23);
            this.btnDeselectAll.TabIndex = 1;
            this.btnDeselectAll.Text = "Deselect All";
            this.btnDeselectAll.UseVisualStyleBackColor = false;
            this.btnDeselectAll.Click += new System.EventHandler(this.btnDeselectAll_Click_1);
            // 
            // txtSearch
            // 
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Location = new System.Drawing.Point(157, 3);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(113, 20);
            this.txtSearch.TabIndex = 2;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Location = new System.Drawing.Point(273, 2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click_1);
            // 
            // pnlTools
            // 
            this.pnlTools.Controls.Add(this.lblVariablename);
            this.pnlTools.Controls.Add(this.btnGroup);
            this.pnlTools.Location = new System.Drawing.Point(0, 1);
            this.pnlTools.Name = "pnlTools";
            this.pnlTools.Size = new System.Drawing.Size(350, 26);
            this.pnlTools.TabIndex = 0;
            // 
            // lblVariablename
            // 
            this.lblVariablename.AutoSize = true;
            this.lblVariablename.Location = new System.Drawing.Point(7, 7);
            this.lblVariablename.Name = "lblVariablename";
            this.lblVariablename.Size = new System.Drawing.Size(0, 13);
            this.lblVariablename.TabIndex = 1;
            // 
            // pnlSelect
            // 
            this.pnlSelect.Controls.Add(this.btnSelectAll);
            this.pnlSelect.Controls.Add(this.btnDeselectAll);
            this.pnlSelect.Controls.Add(this.btnSearch);
            this.pnlSelect.Controls.Add(this.txtSearch);
            this.pnlSelect.Location = new System.Drawing.Point(0, 226);
            this.pnlSelect.Name = "pnlSelect";
            this.pnlSelect.Size = new System.Drawing.Size(350, 31);
            this.pnlSelect.TabIndex = 0;
            // 
            // ValueSelectControl
            // 
            this.Controls.Add(this.pnlSelect);
            this.Controls.Add(this.pnlTools);
            this.Controls.Add(this.lstValues);
            this.Name = "ValueSelectControl";
            this.Size = new System.Drawing.Size(350, 260);
            this.pnlTools.ResumeLayout(false);
            this.pnlTools.PerformLayout();
            this.pnlSelect.ResumeLayout(false);
            this.pnlSelect.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Panel pnlTools;
        internal Panel pnlSelect;

        

    

      

     
    }
}