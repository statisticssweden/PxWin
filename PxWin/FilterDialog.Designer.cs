namespace PCAxis.Desktop
{
    partial class FilterDialog
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FilterDialog));
            this.tabControlFilter = new System.Windows.Forms.TabControl();
            this.tabSelect = new System.Windows.Forms.TabPage();
            this.lblSelectDescription = new System.Windows.Forms.Label();
            this.btnRemove = new System.Windows.Forms.Button();
            this.lvFilters = new System.Windows.Forms.ListView();
            this.tabCreate = new System.Windows.Forms.TabPage();
            this.lvValues = new System.Windows.Forms.ListView();
            this.colValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblValues = new System.Windows.Forms.Label();
            this.btnImport = new System.Windows.Forms.Button();
            this.lblSaveError = new System.Windows.Forms.Label();
            this.lblCreateDescription = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.btnAction = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolTipRemove = new System.Windows.Forms.ToolTip(this.components);
            this.tabControlFilter.SuspendLayout();
            this.tabSelect.SuspendLayout();
            this.tabCreate.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlFilter
            // 
            this.tabControlFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlFilter.Controls.Add(this.tabSelect);
            this.tabControlFilter.Controls.Add(this.tabCreate);
            this.tabControlFilter.Location = new System.Drawing.Point(12, 12);
            this.tabControlFilter.Name = "tabControlFilter";
            this.tabControlFilter.SelectedIndex = 0;
            this.tabControlFilter.Size = new System.Drawing.Size(580, 327);
            this.tabControlFilter.TabIndex = 0;
            this.tabControlFilter.SelectedIndexChanged += new System.EventHandler(this.tabControlFilter_SelectedIndexChanged);
            // 
            // tabSelect
            // 
            this.tabSelect.Controls.Add(this.lblSelectDescription);
            this.tabSelect.Controls.Add(this.btnRemove);
            this.tabSelect.Controls.Add(this.lvFilters);
            this.tabSelect.Location = new System.Drawing.Point(4, 22);
            this.tabSelect.Name = "tabSelect";
            this.tabSelect.Padding = new System.Windows.Forms.Padding(3);
            this.tabSelect.Size = new System.Drawing.Size(572, 301);
            this.tabSelect.TabIndex = 0;
            this.tabSelect.Text = "Select";
            this.tabSelect.UseVisualStyleBackColor = true;
            // 
            // lblSelectDescription
            // 
            this.lblSelectDescription.AutoSize = true;
            this.lblSelectDescription.Location = new System.Drawing.Point(7, 7);
            this.lblSelectDescription.Name = "lblSelectDescription";
            this.lblSelectDescription.Size = new System.Drawing.Size(60, 13);
            this.lblSelectDescription.TabIndex = 20;
            this.lblSelectDescription.Text = "Description";
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemove.ForeColor = System.Drawing.SystemColors.Control;
            this.btnRemove.Image = global::PCAxis.Desktop.Properties.Resources.trash;
            this.btnRemove.Location = new System.Drawing.Point(536, 47);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(28, 28);
            this.btnRemove.TabIndex = 19;
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // lvFilters
            // 
            this.lvFilters.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvFilters.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvFilters.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvFilters.FullRowSelect = true;
            this.lvFilters.HideSelection = false;
            this.lvFilters.Location = new System.Drawing.Point(6, 50);
            this.lvFilters.Name = "lvFilters";
            this.lvFilters.Size = new System.Drawing.Size(526, 245);
            this.lvFilters.TabIndex = 0;
            this.lvFilters.UseCompatibleStateImageBehavior = false;
            this.lvFilters.View = System.Windows.Forms.View.Details;
            this.lvFilters.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvFilters_ColumnClick);
            this.lvFilters.SelectedIndexChanged += new System.EventHandler(this.lvFilters_SelectedIndexChanged);
            // 
            // tabCreate
            // 
            this.tabCreate.Controls.Add(this.lvValues);
            this.tabCreate.Controls.Add(this.lblValues);
            this.tabCreate.Controls.Add(this.btnImport);
            this.tabCreate.Controls.Add(this.lblSaveError);
            this.tabCreate.Controls.Add(this.lblCreateDescription);
            this.tabCreate.Controls.Add(this.txtName);
            this.tabCreate.Controls.Add(this.lblName);
            this.tabCreate.Location = new System.Drawing.Point(4, 22);
            this.tabCreate.Name = "tabCreate";
            this.tabCreate.Padding = new System.Windows.Forms.Padding(3);
            this.tabCreate.Size = new System.Drawing.Size(572, 301);
            this.tabCreate.TabIndex = 1;
            this.tabCreate.Text = "Create";
            this.tabCreate.UseVisualStyleBackColor = true;
            // 
            // lvValues
            // 
            this.lvValues.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lvValues.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvValues.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvValues.CheckBoxes = true;
            this.lvValues.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colValue});
            this.lvValues.FullRowSelect = true;
            this.lvValues.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvValues.Location = new System.Drawing.Point(6, 68);
            this.lvValues.Name = "lvValues";
            this.lvValues.Size = new System.Drawing.Size(360, 227);
            this.lvValues.TabIndex = 8;
            this.lvValues.UseCompatibleStateImageBehavior = false;
            this.lvValues.View = System.Windows.Forms.View.Details;
            // 
            // colValue
            // 
            this.colValue.Width = 340;
            // 
            // lblValues
            // 
            this.lblValues.AutoSize = true;
            this.lblValues.Location = new System.Drawing.Point(7, 52);
            this.lblValues.Name = "lblValues";
            this.lblValues.Size = new System.Drawing.Size(42, 13);
            this.lblValues.TabIndex = 7;
            this.lblValues.Text = "Values:";
            // 
            // btnImport
            // 
            this.btnImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImport.Location = new System.Drawing.Point(372, 68);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(75, 23);
            this.btnImport.TabIndex = 6;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // lblSaveError
            // 
            this.lblSaveError.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSaveError.ForeColor = System.Drawing.Color.Red;
            this.lblSaveError.Location = new System.Drawing.Point(369, 147);
            this.lblSaveError.Name = "lblSaveError";
            this.lblSaveError.Size = new System.Drawing.Size(193, 60);
            this.lblSaveError.TabIndex = 5;
            this.lblSaveError.Text = "Error";
            // 
            // lblCreateDescription
            // 
            this.lblCreateDescription.AutoSize = true;
            this.lblCreateDescription.Location = new System.Drawing.Point(7, 7);
            this.lblCreateDescription.Name = "lblCreateDescription";
            this.lblCreateDescription.Size = new System.Drawing.Size(60, 13);
            this.lblCreateDescription.TabIndex = 4;
            this.lblCreateDescription.Text = "Description";
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(372, 124);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(194, 20);
            this.txtName.TabIndex = 3;
            this.txtName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtName_KeyDown);
            // 
            // lblName
            // 
            this.lblName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(372, 108);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(38, 13);
            this.lblName.TabIndex = 2;
            this.lblName.Text = "Name:";
            // 
            // btnAction
            // 
            this.btnAction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAction.AutoSize = true;
            this.btnAction.Location = new System.Drawing.Point(432, 346);
            this.btnAction.Name = "btnAction";
            this.btnAction.Size = new System.Drawing.Size(75, 23);
            this.btnAction.TabIndex = 1;
            this.btnAction.Text = "Action";
            this.btnAction.UseVisualStyleBackColor = true;
            this.btnAction.Click += new System.EventHandler(this.btnAction_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(513, 346);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Filter";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // FilterDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 381);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.tabControlFilter);
            this.Controls.Add(this.btnAction);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(489, 349);
            this.Name = "FilterDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FilterDialog";
            this.Resize += new System.EventHandler(this.FilterDialog_Resize);
            this.tabControlFilter.ResumeLayout(false);
            this.tabSelect.ResumeLayout(false);
            this.tabSelect.PerformLayout();
            this.tabCreate.ResumeLayout(false);
            this.tabCreate.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlFilter;
        private System.Windows.Forms.TabPage tabSelect;
        private System.Windows.Forms.TabPage tabCreate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAction;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblCreateDescription;
        private System.Windows.Forms.Label lblSaveError;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Label lblValues;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.ListView lvFilters;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.ToolTip toolTipRemove;
        private System.Windows.Forms.Label lblSelectDescription;
        private System.Windows.Forms.ListView lvValues;
        private System.Windows.Forms.ColumnHeader colValue;
    }
}