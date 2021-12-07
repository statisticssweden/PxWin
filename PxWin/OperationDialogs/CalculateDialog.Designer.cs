namespace PCAxis.Desktop.OperationDialogs
{
    partial class CalculateDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CalculateDialog));
            this.comboVariables = new System.Windows.Forms.ComboBox();
            this.gbInitialValues = new System.Windows.Forms.GroupBox();
            this.rbExclusive = new System.Windows.Forms.RadioButton();
            this.rbInclusive = new System.Windows.Forms.RadioButton();
            this.gbNewValue = new System.Windows.Forms.GroupBox();
            this.tbCode = new System.Windows.Forms.TextBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.lblCode = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblSelectedValue1 = new System.Windows.Forms.Label();
            this.lblSelectedValue2 = new System.Windows.Forms.Label();
            this.lblOperator = new System.Windows.Forms.Label();
            this.tbConstant = new System.Windows.Forms.TextBox();
            this.lblConstant = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblHidden1 = new System.Windows.Forms.Label();
            this.lblHidden2 = new System.Windows.Forms.Label();
            this.ucValueSelect = new PCAxis.Desktop.UserControls.ValueSelectControl();
            this.gbInitialValues.SuspendLayout();
            this.gbNewValue.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboVariables
            // 
            this.comboVariables.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboVariables.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboVariables.FormattingEnabled = true;
            this.comboVariables.Location = new System.Drawing.Point(12, 25);
            this.comboVariables.Name = "comboVariables";
            this.comboVariables.Size = new System.Drawing.Size(350, 21);
            this.comboVariables.TabIndex = 1;
            this.comboVariables.SelectedIndexChanged += new System.EventHandler(this.comboVariables_SelectedIndexChanged);
            // 
            // gbInitialValues
            // 
            this.gbInitialValues.Controls.Add(this.rbExclusive);
            this.gbInitialValues.Controls.Add(this.rbInclusive);
            this.gbInitialValues.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gbInitialValues.Location = new System.Drawing.Point(12, 344);
            this.gbInitialValues.Name = "gbInitialValues";
            this.gbInitialValues.Size = new System.Drawing.Size(126, 74);
            this.gbInitialValues.TabIndex = 7;
            this.gbInitialValues.TabStop = false;
            this.gbInitialValues.Text = "Initial values";
            // 
            // rbExclusive
            // 
            this.rbExclusive.AutoSize = true;
            this.rbExclusive.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbExclusive.Location = new System.Drawing.Point(7, 43);
            this.rbExclusive.Name = "rbExclusive";
            this.rbExclusive.Size = new System.Drawing.Size(69, 17);
            this.rbExclusive.TabIndex = 1;
            this.rbExclusive.TabStop = true;
            this.rbExclusive.Text = "Exclusive";
            this.rbExclusive.UseVisualStyleBackColor = true;
            // 
            // rbInclusive
            // 
            this.rbInclusive.AutoSize = true;
            this.rbInclusive.Checked = true;
            this.rbInclusive.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbInclusive.Location = new System.Drawing.Point(7, 20);
            this.rbInclusive.Name = "rbInclusive";
            this.rbInclusive.Size = new System.Drawing.Size(66, 17);
            this.rbInclusive.TabIndex = 0;
            this.rbInclusive.TabStop = true;
            this.rbInclusive.Text = "Inclusive";
            this.rbInclusive.UseVisualStyleBackColor = true;
            // 
            // gbNewValue
            // 
            this.gbNewValue.Controls.Add(this.tbCode);
            this.gbNewValue.Controls.Add(this.tbName);
            this.gbNewValue.Controls.Add(this.lblCode);
            this.gbNewValue.Controls.Add(this.lblName);
            this.gbNewValue.Location = new System.Drawing.Point(144, 344);
            this.gbNewValue.Name = "gbNewValue";
            this.gbNewValue.Size = new System.Drawing.Size(218, 74);
            this.gbNewValue.TabIndex = 8;
            this.gbNewValue.TabStop = false;
            this.gbNewValue.Text = "New value";
            // 
            // tbCode
            // 
            this.tbCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbCode.Location = new System.Drawing.Point(70, 42);
            this.tbCode.Name = "tbCode";
            this.tbCode.Size = new System.Drawing.Size(142, 20);
            this.tbCode.TabIndex = 3;
            // 
            // tbName
            // 
            this.tbName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbName.Location = new System.Drawing.Point(70, 20);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(142, 20);
            this.tbName.TabIndex = 2;
            // 
            // lblCode
            // 
            this.lblCode.AutoSize = true;
            this.lblCode.Location = new System.Drawing.Point(6, 43);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(32, 13);
            this.lblCode.TabIndex = 1;
            this.lblCode.Text = "Code";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(6, 20);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(35, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Name";
            // 
            // lblSelectedValue1
            // 
            this.lblSelectedValue1.AutoSize = true;
            this.lblSelectedValue1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedValue1.Location = new System.Drawing.Point(9, 313);
            this.lblSelectedValue1.Name = "lblSelectedValue1";
            this.lblSelectedValue1.Size = new System.Drawing.Size(105, 16);
            this.lblSelectedValue1.TabIndex = 2;
            this.lblSelectedValue1.Text = "Selected value1";
            // 
            // lblSelectedValue2
            // 
            this.lblSelectedValue2.AutoSize = true;
            this.lblSelectedValue2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedValue2.Location = new System.Drawing.Point(150, 313);
            this.lblSelectedValue2.Name = "lblSelectedValue2";
            this.lblSelectedValue2.Size = new System.Drawing.Size(108, 16);
            this.lblSelectedValue2.TabIndex = 4;
            this.lblSelectedValue2.Text = "Selected value 2";
            // 
            // lblOperator
            // 
            this.lblOperator.AutoSize = true;
            this.lblOperator.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOperator.Location = new System.Drawing.Point(133, 312);
            this.lblOperator.Name = "lblOperator";
            this.lblOperator.Size = new System.Drawing.Size(17, 18);
            this.lblOperator.TabIndex = 3;
            this.lblOperator.Text = "+";
            // 
            // tbConstant
            // 
            this.tbConstant.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbConstant.Location = new System.Drawing.Point(330, 309);
            this.tbConstant.Name = "tbConstant";
            this.tbConstant.Size = new System.Drawing.Size(26, 20);
            this.tbConstant.TabIndex = 6;
            // 
            // lblConstant
            // 
            this.lblConstant.AutoSize = true;
            this.lblConstant.Location = new System.Drawing.Point(278, 312);
            this.lblConstant.Name = "lblConstant";
            this.lblConstant.Size = new System.Drawing.Size(52, 13);
            this.lblConstant.TabIndex = 5;
            this.lblConstant.Text = "Constant:";
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Location = new System.Drawing.Point(200, 424);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 9;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(281, 424);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblHidden1
            // 
            this.lblHidden1.AutoSize = true;
            this.lblHidden1.Location = new System.Drawing.Point(9, 328);
            this.lblHidden1.Name = "lblHidden1";
            this.lblHidden1.Size = new System.Drawing.Size(35, 13);
            this.lblHidden1.TabIndex = 11;
            this.lblHidden1.Text = "label1";
            this.lblHidden1.Visible = false;
            // 
            // lblHidden2
            // 
            this.lblHidden2.AutoSize = true;
            this.lblHidden2.Location = new System.Drawing.Point(150, 329);
            this.lblHidden2.Name = "lblHidden2";
            this.lblHidden2.Size = new System.Drawing.Size(35, 13);
            this.lblHidden2.TabIndex = 12;
            this.lblHidden2.Text = "label2";
            this.lblHidden2.Visible = false;
            // 
            // ucValueSelect
            // 
            this.ucValueSelect.Location = new System.Drawing.Point(12, 52);
            this.ucValueSelect.Name = "ucValueSelect";
            this.ucValueSelect.ShowBottomButtons = true;
            this.ucValueSelect.ShowTopButtons = true;
            this.ucValueSelect.Size = new System.Drawing.Size(350, 257);
            this.ucValueSelect.TabIndex = 2;
            this.ucValueSelect.Variable = null;
            this.ucValueSelect.VariableSelected += new PCAxis.Desktop.UserControls.ValueSelectControl.VariableSelectedEventHandler(this.ucValueSelect_VariableSelected);
            // 
            // CalculateDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 456);
            this.Controls.Add(this.lblHidden2);
            this.Controls.Add(this.lblHidden1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lblConstant);
            this.Controls.Add(this.tbConstant);
            this.Controls.Add(this.lblOperator);
            this.Controls.Add(this.lblSelectedValue2);
            this.Controls.Add(this.lblSelectedValue1);
            this.Controls.Add(this.gbNewValue);
            this.Controls.Add(this.gbInitialValues);
            this.Controls.Add(this.ucValueSelect);
            this.Controls.Add(this.comboVariables);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(386, 494);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(386, 494);
            this.Name = "CalculateDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CalculateDialog";
            this.Load += new System.EventHandler(this.CalculateDialog_Load);
            this.gbInitialValues.ResumeLayout(false);
            this.gbInitialValues.PerformLayout();
            this.gbNewValue.ResumeLayout(false);
            this.gbNewValue.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboVariables;
        private UserControls.ValueSelectControl ucValueSelect;
        private System.Windows.Forms.GroupBox gbInitialValues;
        private System.Windows.Forms.GroupBox gbNewValue;
        private System.Windows.Forms.RadioButton rbExclusive;
        private System.Windows.Forms.RadioButton rbInclusive;
        private System.Windows.Forms.TextBox tbCode;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblSelectedValue1;
        private System.Windows.Forms.Label lblSelectedValue2;
        private System.Windows.Forms.Label lblOperator;
        private System.Windows.Forms.TextBox tbConstant;
        private System.Windows.Forms.Label lblConstant;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblHidden1;
        private System.Windows.Forms.Label lblHidden2;
    }
}