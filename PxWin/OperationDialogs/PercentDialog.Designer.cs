using System.Windows.Forms;

namespace PCAxis.Desktop.OperationDialogs
{
    partial class PercentDialog : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PercentDialog));
            this.flPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tbNewValue = new System.Windows.Forms.TextBox();
            this.gbInitialValue = new System.Windows.Forms.GroupBox();
            this.rbExclude = new System.Windows.Forms.RadioButton();
            this.rbInclude = new System.Windows.Forms.RadioButton();
            this.rbBaseVariable = new System.Windows.Forms.RadioButton();
            this.rbBaseVariableValue = new System.Windows.Forms.RadioButton();
            this.rbBaseCellValue = new System.Windows.Forms.RadioButton();
            this.lblInstructionHeading = new System.Windows.Forms.Label();
            this.lblInstruction = new System.Windows.Forms.Label();
            this.grpBase = new System.Windows.Forms.GroupBox();
            this.grpNewValue = new System.Windows.Forms.GroupBox();
            this.gbInitialValue.SuspendLayout();
            this.grpBase.SuspendLayout();
            this.grpNewValue.SuspendLayout();
            this.SuspendLayout();
            // 
            // flPanel
            // 
            this.flPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flPanel.AutoScroll = true;
            this.flPanel.Location = new System.Drawing.Point(12, 176);
            this.flPanel.Name = "flPanel";
            this.flPanel.Size = new System.Drawing.Size(430, 254);
            this.flPanel.TabIndex = 0;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Location = new System.Drawing.Point(528, 12);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(92, 23);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(528, 41);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(92, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tbNewValue
            // 
            this.tbNewValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbNewValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbNewValue.Location = new System.Drawing.Point(6, 19);
            this.tbNewValue.Name = "tbNewValue";
            this.tbNewValue.Size = new System.Drawing.Size(160, 20);
            this.tbNewValue.TabIndex = 4;
            this.tbNewValue.Text = "Percent";
            // 
            // gbInitialValue
            // 
            this.gbInitialValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbInitialValue.Controls.Add(this.rbExclude);
            this.gbInitialValue.Controls.Add(this.rbInclude);
            this.gbInitialValue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gbInitialValue.Location = new System.Drawing.Point(448, 239);
            this.gbInitialValue.Margin = new System.Windows.Forms.Padding(3, 7, 3, 3);
            this.gbInitialValue.Name = "gbInitialValue";
            this.gbInitialValue.Size = new System.Drawing.Size(172, 81);
            this.gbInitialValue.TabIndex = 5;
            this.gbInitialValue.TabStop = false;
            this.gbInitialValue.Text = "Initial value";
            // 
            // rbExclude
            // 
            this.rbExclude.AutoSize = true;
            this.rbExclude.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbExclude.Location = new System.Drawing.Point(7, 49);
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
            this.rbInclude.Location = new System.Drawing.Point(7, 23);
            this.rbInclude.Name = "rbInclude";
            this.rbInclude.Size = new System.Drawing.Size(59, 17);
            this.rbInclude.TabIndex = 0;
            this.rbInclude.TabStop = true;
            this.rbInclude.Text = "Include";
            this.rbInclude.UseVisualStyleBackColor = true;
            // 
            // rbBaseVariable
            // 
            this.rbBaseVariable.AutoSize = true;
            this.rbBaseVariable.Checked = true;
            this.rbBaseVariable.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbBaseVariable.Location = new System.Drawing.Point(6, 19);
            this.rbBaseVariable.Name = "rbBaseVariable";
            this.rbBaseVariable.Size = new System.Drawing.Size(62, 17);
            this.rbBaseVariable.TabIndex = 7;
            this.rbBaseVariable.TabStop = true;
            this.rbBaseVariable.Text = "Variable";
            this.rbBaseVariable.UseVisualStyleBackColor = true;
            this.rbBaseVariable.CheckedChanged += new System.EventHandler(this.rbBaseVariable_CheckedChanged);
            // 
            // rbBaseVariableValue
            // 
            this.rbBaseVariableValue.AutoSize = true;
            this.rbBaseVariableValue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbBaseVariableValue.Location = new System.Drawing.Point(6, 42);
            this.rbBaseVariableValue.Name = "rbBaseVariableValue";
            this.rbBaseVariableValue.Size = new System.Drawing.Size(91, 17);
            this.rbBaseVariableValue.TabIndex = 8;
            this.rbBaseVariableValue.TabStop = true;
            this.rbBaseVariableValue.Text = "Variable value";
            this.rbBaseVariableValue.UseVisualStyleBackColor = true;
            this.rbBaseVariableValue.CheckedChanged += new System.EventHandler(this.rbBaseVariableValue_CheckedChanged);
            // 
            // rbBaseCellValue
            // 
            this.rbBaseCellValue.AutoSize = true;
            this.rbBaseCellValue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbBaseCellValue.Location = new System.Drawing.Point(6, 66);
            this.rbBaseCellValue.Name = "rbBaseCellValue";
            this.rbBaseCellValue.Size = new System.Drawing.Size(70, 17);
            this.rbBaseCellValue.TabIndex = 9;
            this.rbBaseCellValue.TabStop = true;
            this.rbBaseCellValue.Text = "Cell value";
            this.rbBaseCellValue.UseVisualStyleBackColor = true;
            this.rbBaseCellValue.CheckedChanged += new System.EventHandler(this.rbBaseCellValue_CheckedChanged);
            // 
            // lblInstructionHeading
            // 
            this.lblInstructionHeading.AutoSize = true;
            this.lblInstructionHeading.Location = new System.Drawing.Point(2, 107);
            this.lblInstructionHeading.Name = "lblInstructionHeading";
            this.lblInstructionHeading.Size = new System.Drawing.Size(163, 13);
            this.lblInstructionHeading.TabIndex = 10;
            this.lblInstructionHeading.Text = "Instruction of how to select base:";
            // 
            // lblInstruction
            // 
            this.lblInstruction.AutoSize = true;
            this.lblInstruction.Location = new System.Drawing.Point(2, 129);
            this.lblInstruction.Name = "lblInstruction";
            this.lblInstruction.Size = new System.Drawing.Size(160, 13);
            this.lblInstruction.TabIndex = 11;
            this.lblInstruction.Text = "Select all values for one variable";
            // 
            // grpBase
            // 
            this.grpBase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpBase.Controls.Add(this.rbBaseVariable);
            this.grpBase.Controls.Add(this.lblInstruction);
            this.grpBase.Controls.Add(this.rbBaseVariableValue);
            this.grpBase.Controls.Add(this.lblInstructionHeading);
            this.grpBase.Controls.Add(this.rbBaseCellValue);
            this.grpBase.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.grpBase.Location = new System.Drawing.Point(12, 11);
            this.grpBase.Name = "grpBase";
            this.grpBase.Size = new System.Drawing.Size(393, 159);
            this.grpBase.TabIndex = 12;
            this.grpBase.TabStop = false;
            this.grpBase.Text = "Select base for the calculation";
            // 
            // grpNewValue
            // 
            this.grpNewValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grpNewValue.Controls.Add(this.tbNewValue);
            this.grpNewValue.Location = new System.Drawing.Point(448, 176);
            this.grpNewValue.Name = "grpNewValue";
            this.grpNewValue.Size = new System.Drawing.Size(172, 53);
            this.grpNewValue.TabIndex = 13;
            this.grpNewValue.TabStop = false;
            this.grpNewValue.Text = "Name of new value";
            // 
            // PercentDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 442);
            this.Controls.Add(this.grpNewValue);
            this.Controls.Add(this.grpBase);
            this.Controls.Add(this.flPanel);
            this.Controls.Add(this.gbInitialValue);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(443, 480);
            this.Name = "PercentDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "PercentDialog";
            this.gbInitialValue.ResumeLayout(false);
            this.gbInitialValue.PerformLayout();
            this.grpBase.ResumeLayout(false);
            this.grpBase.PerformLayout();
            this.grpNewValue.ResumeLayout(false);
            this.grpNewValue.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal FlowLayoutPanel flPanel;
        private Button btnOk;
        private Button btnCancel;
        private TextBox tbNewValue;
        private GroupBox gbInitialValue;
        private RadioButton rbExclude;
        private RadioButton rbInclude;
        private RadioButton rbBaseVariable;
        private RadioButton rbBaseVariableValue;
        private RadioButton rbBaseCellValue;
        private Label lblInstructionHeading;
        private Label lblInstruction;
        private GroupBox grpBase;
        private GroupBox grpNewValue;       
    }
}