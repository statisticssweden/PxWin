namespace PCAxis.Desktop.UserControls
{
    partial class SelectVariableValuesControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectVariableValuesControl));
            this.lblVariableName = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lstValues = new System.Windows.Forms.ListBox();
            this.lblCount = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.toolTipGroup = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipSelectAll = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipDeselect = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipAscending = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipDescending = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipSearch = new System.Windows.Forms.ToolTip(this.components);
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnUnselect = new System.Windows.Forms.Button();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnGroup = new System.Windows.Forms.Button();
            this.imgResize = new System.Windows.Forms.PictureBox();
            this.btnFilter = new System.Windows.Forms.Button();
            this.toolTipFilter = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgResize)).BeginInit();
            this.SuspendLayout();
            // 
            // lblVariableName
            // 
            this.lblVariableName.AutoSize = true;
            this.lblVariableName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVariableName.ForeColor = System.Drawing.SystemColors.Window;
            this.lblVariableName.Location = new System.Drawing.Point(12, 11);
            this.lblVariableName.Name = "lblVariableName";
            this.lblVariableName.Size = new System.Drawing.Size(45, 16);
            this.lblVariableName.TabIndex = 0;
            this.lblVariableName.Text = "label1";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(144)))), ((int)(((byte)(170)))));
            this.panel1.Controls.Add(this.lblVariableName);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(237, 35);
            this.panel1.TabIndex = 1;
            // 
            // lstValues
            // 
            this.lstValues.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstValues.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstValues.FormattingEnabled = true;
            this.lstValues.Location = new System.Drawing.Point(15, 100);
            this.lstValues.Name = "lstValues";
            this.lstValues.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstValues.Size = new System.Drawing.Size(204, 93);
            this.lstValues.TabIndex = 2;
            this.lstValues.SelectedValueChanged += new System.EventHandler(this.lstValues_SelectedValueChanged);
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Location = new System.Drawing.Point(15, 78);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(45, 13);
            this.lblCount.TabIndex = 4;
            this.lblCount.Text = "lblCount";
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Location = new System.Drawing.Point(15, 202);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(174, 20);
            this.txtSearch.TabIndex = 10;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.ForeColor = System.Drawing.SystemColors.Control;
            this.btnSearch.Image = global::PCAxis.Desktop.Properties.Resources.search;
            this.btnSearch.Location = new System.Drawing.Point(195, 200);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(24, 24);
            this.btnSearch.TabIndex = 9;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnDown
            // 
            this.btnDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDown.ForeColor = System.Drawing.SystemColors.Control;
            this.btnDown.Image = ((System.Drawing.Image)(resources.GetObject("btnDown.Image")));
            this.btnDown.Location = new System.Drawing.Point(191, 41);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(28, 28);
            this.btnDown.TabIndex = 8;
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnUp
            // 
            this.btnUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUp.ForeColor = System.Drawing.SystemColors.Control;
            this.btnUp.Image = ((System.Drawing.Image)(resources.GetObject("btnUp.Image")));
            this.btnUp.Location = new System.Drawing.Point(164, 41);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(28, 28);
            this.btnUp.TabIndex = 7;
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnUnselect
            // 
            this.btnUnselect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUnselect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUnselect.ForeColor = System.Drawing.SystemColors.Control;
            this.btnUnselect.Image = ((System.Drawing.Image)(resources.GetObject("btnUnselect.Image")));
            this.btnUnselect.Location = new System.Drawing.Point(137, 41);
            this.btnUnselect.Name = "btnUnselect";
            this.btnUnselect.Size = new System.Drawing.Size(28, 28);
            this.btnUnselect.TabIndex = 6;
            this.btnUnselect.UseVisualStyleBackColor = true;
            this.btnUnselect.Click += new System.EventHandler(this.btnUnselect_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectAll.ForeColor = System.Drawing.SystemColors.Control;
            this.btnSelectAll.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectAll.Image")));
            this.btnSelectAll.Location = new System.Drawing.Point(110, 41);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(28, 28);
            this.btnSelectAll.TabIndex = 5;
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnGroup
            // 
            this.btnGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGroup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGroup.ForeColor = System.Drawing.SystemColors.Control;
            this.btnGroup.Image = ((System.Drawing.Image)(resources.GetObject("btnGroup.Image")));
            this.btnGroup.Location = new System.Drawing.Point(56, 41);
            this.btnGroup.Name = "btnGroup";
            this.btnGroup.Size = new System.Drawing.Size(28, 28);
            this.btnGroup.TabIndex = 3;
            this.btnGroup.UseVisualStyleBackColor = true;
            this.btnGroup.Click += new System.EventHandler(this.btnGroup_Click);
            // 
            // imgResize
            // 
            this.imgResize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.imgResize.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.imgResize.Image = global::PCAxis.Desktop.Properties.Resources.resize;
            this.imgResize.Location = new System.Drawing.Point(221, 226);
            this.imgResize.Name = "imgResize";
            this.imgResize.Size = new System.Drawing.Size(12, 12);
            this.imgResize.TabIndex = 11;
            this.imgResize.TabStop = false;
            this.imgResize.MouseDown += new System.Windows.Forms.MouseEventHandler(this.imgResize_MouseDown);
            this.imgResize.MouseMove += new System.Windows.Forms.MouseEventHandler(this.imgResize_MouseMove);
            this.imgResize.MouseUp += new System.Windows.Forms.MouseEventHandler(this.imgResize_MouseUp);
            // 
            // btnFilter
            // 
            this.btnFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFilter.ForeColor = System.Drawing.SystemColors.Control;
            this.btnFilter.Image = global::PCAxis.Desktop.Properties.Resources.filter;
            this.btnFilter.Location = new System.Drawing.Point(83, 41);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(28, 28);
            this.btnFilter.TabIndex = 12;
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // SelectVariableValuesControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.btnFilter);
            this.Controls.Add(this.imgResize);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.btnUp);
            this.Controls.Add(this.btnUnselect);
            this.Controls.Add(this.btnSelectAll);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.btnGroup);
            this.Controls.Add(this.lstValues);
            this.Controls.Add(this.panel1);
            this.MinimumSize = new System.Drawing.Size(200, 200);
            this.Name = "SelectVariableValuesControl";
            this.Size = new System.Drawing.Size(234, 239);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgResize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblVariableName;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListBox lstValues;
        private System.Windows.Forms.Button btnGroup;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnUnselect;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ToolTip toolTipGroup;
        private System.Windows.Forms.ToolTip toolTipSelectAll;
        private System.Windows.Forms.ToolTip toolTipDeselect;
        private System.Windows.Forms.ToolTip toolTipAscending;
        private System.Windows.Forms.ToolTip toolTipDescending;
        private System.Windows.Forms.ToolTip toolTipSearch;
        private System.Windows.Forms.PictureBox imgResize;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.ToolTip toolTipFilter;
    }
}
