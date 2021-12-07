namespace PCAxis.Desktop.OperationDialogs
{
    partial class SumDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SumDialog));
            this.comboVariables = new System.Windows.Forms.ComboBox();
            this.gbInitialValue = new System.Windows.Forms.GroupBox();
            this.rbExclude = new System.Windows.Forms.RadioButton();
            this.rbInclude = new System.Windows.Forms.RadioButton();
            this.gbNewValue = new System.Windows.Forms.GroupBox();
            this.lblCode = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.tbCode = new System.Windows.Forms.TextBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblSelectVar = new System.Windows.Forms.Label();
            this.ucValueSelect = new PCAxis.Desktop.UserControls.ValueSelectControl();
            this.gbInitialValue.SuspendLayout();
            this.gbNewValue.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboVariables
            // 
            this.comboVariables.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboVariables.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboVariables.FormattingEnabled = true;
            this.comboVariables.Location = new System.Drawing.Point(12, 27);
            this.comboVariables.Name = "comboVariables";
            this.comboVariables.Size = new System.Drawing.Size(353, 21);
            this.comboVariables.TabIndex = 1;
            this.comboVariables.SelectedIndexChanged += new System.EventHandler(this.comboVariables_SelectedIndexChanged);
            // 
            // gbInitialValue
            // 
            this.gbInitialValue.Controls.Add(this.rbExclude);
            this.gbInitialValue.Controls.Add(this.rbInclude);
            this.gbInitialValue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gbInitialValue.Location = new System.Drawing.Point(13, 291);
            this.gbInitialValue.Name = "gbInitialValue";
            this.gbInitialValue.Size = new System.Drawing.Size(115, 66);
            this.gbInitialValue.TabIndex = 3;
            this.gbInitialValue.TabStop = false;
            this.gbInitialValue.Text = "Initial value";
            // 
            // rbExclude
            // 
            this.rbExclude.AutoSize = true;
            this.rbExclude.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbExclude.Location = new System.Drawing.Point(7, 38);
            this.rbExclude.Name = "rbExclude";
            this.rbExclude.Size = new System.Drawing.Size(62, 17);
            this.rbExclude.TabIndex = 1;
            this.rbExclude.Text = "Exclude";
            this.rbExclude.UseVisualStyleBackColor = true;
            // 
            // rbInclude
            // 
            this.rbInclude.AutoSize = true;
            this.rbInclude.Checked = true;
            this.rbInclude.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbInclude.Location = new System.Drawing.Point(7, 20);
            this.rbInclude.Name = "rbInclude";
            this.rbInclude.Size = new System.Drawing.Size(59, 17);
            this.rbInclude.TabIndex = 0;
            this.rbInclude.TabStop = true;
            this.rbInclude.Text = "Include";
            this.rbInclude.UseVisualStyleBackColor = true;
            // 
            // gbNewValue
            // 
            this.gbNewValue.Controls.Add(this.lblCode);
            this.gbNewValue.Controls.Add(this.lblName);
            this.gbNewValue.Controls.Add(this.tbCode);
            this.gbNewValue.Controls.Add(this.tbName);
            this.gbNewValue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gbNewValue.Location = new System.Drawing.Point(134, 291);
            this.gbNewValue.Name = "gbNewValue";
            this.gbNewValue.Size = new System.Drawing.Size(231, 66);
            this.gbNewValue.TabIndex = 4;
            this.gbNewValue.TabStop = false;
            this.gbNewValue.Text = "New value";
            // 
            // lblCode
            // 
            this.lblCode.AutoSize = true;
            this.lblCode.Location = new System.Drawing.Point(6, 40);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(32, 13);
            this.lblCode.TabIndex = 3;
            this.lblCode.Text = "Code";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(6, 20);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(35, 13);
            this.lblName.TabIndex = 2;
            this.lblName.Text = "Name";
            // 
            // tbCode
            // 
            this.tbCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbCode.Location = new System.Drawing.Point(47, 40);
            this.tbCode.Name = "tbCode";
            this.tbCode.Size = new System.Drawing.Size(178, 20);
            this.tbCode.TabIndex = 1;
            // 
            // tbName
            // 
            this.tbName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbName.Location = new System.Drawing.Point(47, 17);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(178, 20);
            this.tbName.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Location = new System.Drawing.Point(208, 365);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(289, 365);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.button2_Click);
            // 
            // lblSelectVar
            // 
            this.lblSelectVar.AutoSize = true;
            this.lblSelectVar.Location = new System.Drawing.Point(10, 9);
            this.lblSelectVar.Name = "lblSelectVar";
            this.lblSelectVar.Size = new System.Drawing.Size(77, 13);
            this.lblSelectVar.TabIndex = 0;
            this.lblSelectVar.Text = "Select variable";
            // 
            // ucValueSelect
            // 
            this.ucValueSelect.Location = new System.Drawing.Point(13, 54);
            this.ucValueSelect.Name = "ucValueSelect";
            this.ucValueSelect.ShowBottomButtons = false;
            this.ucValueSelect.ShowTopButtons = false;
            this.ucValueSelect.Size = new System.Drawing.Size(352, 231);
            this.ucValueSelect.TabIndex = 2;
            this.ucValueSelect.Variable = null;
            this.ucValueSelect.Load += new System.EventHandler(this.ucValueSelect_Load);
            // 
            // SumDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(377, 398);
            this.Controls.Add(this.ucValueSelect);
            this.Controls.Add(this.lblSelectVar);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.gbNewValue);
            this.Controls.Add(this.gbInitialValue);
            this.Controls.Add(this.comboVariables);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(393, 436);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(393, 436);
            this.Name = "SumDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SumDialog";
            this.gbInitialValue.ResumeLayout(false);
            this.gbInitialValue.PerformLayout();
            this.gbNewValue.ResumeLayout(false);
            this.gbNewValue.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboVariables;
        private System.Windows.Forms.GroupBox gbInitialValue;
        private System.Windows.Forms.RadioButton rbExclude;
        private System.Windows.Forms.RadioButton rbInclude;
        private System.Windows.Forms.GroupBox gbNewValue;
        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox tbCode;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblSelectVar;
        private UserControls.ValueSelectControl ucValueSelect;
    }
}