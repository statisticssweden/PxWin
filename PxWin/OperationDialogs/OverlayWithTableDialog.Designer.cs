namespace PCAxis.Desktop.OperationDialogs
{
    partial class OverlayWithTableDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OverlayWithTableDialog));
            this.tbVariable = new System.Windows.Forms.TextBox();
            this.tbValue1 = new System.Windows.Forms.TextBox();
            this.tbValue2 = new System.Windows.Forms.TextBox();
            this.tbCode1 = new System.Windows.Forms.TextBox();
            this.tbCode2 = new System.Windows.Forms.TextBox();
            this.lblVariable = new System.Windows.Forms.Label();
            this.lblValue1 = new System.Windows.Forms.Label();
            this.lblValue2 = new System.Windows.Forms.Label();
            this.lblCode1 = new System.Windows.Forms.Label();
            this.lblCode2 = new System.Windows.Forms.Label();
            this.lblHelpText = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbVariable
            // 
            this.tbVariable.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbVariable.Location = new System.Drawing.Point(60, 76);
            this.tbVariable.Name = "tbVariable";
            this.tbVariable.Size = new System.Drawing.Size(119, 20);
            this.tbVariable.TabIndex = 0;
            // 
            // tbValue1
            // 
            this.tbValue1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbValue1.Location = new System.Drawing.Point(60, 102);
            this.tbValue1.Name = "tbValue1";
            this.tbValue1.Size = new System.Drawing.Size(119, 20);
            this.tbValue1.TabIndex = 1;
            // 
            // tbValue2
            // 
            this.tbValue2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbValue2.Location = new System.Drawing.Point(60, 128);
            this.tbValue2.Name = "tbValue2";
            this.tbValue2.Size = new System.Drawing.Size(119, 20);
            this.tbValue2.TabIndex = 2;
            // 
            // tbCode1
            // 
            this.tbCode1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbCode1.Location = new System.Drawing.Point(243, 102);
            this.tbCode1.Name = "tbCode1";
            this.tbCode1.Size = new System.Drawing.Size(122, 20);
            this.tbCode1.TabIndex = 3;
            // 
            // tbCode2
            // 
            this.tbCode2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbCode2.Location = new System.Drawing.Point(243, 128);
            this.tbCode2.Name = "tbCode2";
            this.tbCode2.Size = new System.Drawing.Size(122, 20);
            this.tbCode2.TabIndex = 4;
            // 
            // lblVariable
            // 
            this.lblVariable.AutoSize = true;
            this.lblVariable.Location = new System.Drawing.Point(12, 79);
            this.lblVariable.Name = "lblVariable";
            this.lblVariable.Size = new System.Drawing.Size(45, 13);
            this.lblVariable.TabIndex = 5;
            this.lblVariable.Text = "Variable";
            // 
            // lblValue1
            // 
            this.lblValue1.AutoSize = true;
            this.lblValue1.Location = new System.Drawing.Point(12, 105);
            this.lblValue1.Name = "lblValue1";
            this.lblValue1.Size = new System.Drawing.Size(43, 13);
            this.lblValue1.TabIndex = 6;
            this.lblValue1.Text = "Value 1";
            // 
            // lblValue2
            // 
            this.lblValue2.AutoSize = true;
            this.lblValue2.Location = new System.Drawing.Point(12, 128);
            this.lblValue2.Name = "lblValue2";
            this.lblValue2.Size = new System.Drawing.Size(43, 13);
            this.lblValue2.TabIndex = 7;
            this.lblValue2.Text = "Value 2";
            // 
            // lblCode1
            // 
            this.lblCode1.AutoSize = true;
            this.lblCode1.Location = new System.Drawing.Point(196, 105);
            this.lblCode1.Name = "lblCode1";
            this.lblCode1.Size = new System.Drawing.Size(41, 13);
            this.lblCode1.TabIndex = 8;
            this.lblCode1.Text = "Code 1";
            // 
            // lblCode2
            // 
            this.lblCode2.AutoSize = true;
            this.lblCode2.Location = new System.Drawing.Point(196, 131);
            this.lblCode2.Name = "lblCode2";
            this.lblCode2.Size = new System.Drawing.Size(41, 13);
            this.lblCode2.TabIndex = 9;
            this.lblCode2.Text = "Code 2";
            // 
            // lblHelpText
            // 
            this.lblHelpText.Location = new System.Drawing.Point(12, 18);
            this.lblHelpText.Name = "lblHelpText";
            this.lblHelpText.Size = new System.Drawing.Size(260, 55);
            this.lblHelpText.TabIndex = 10;
            this.lblHelpText.Text = "Fyll i namn på ny variabel (dimension), värdetexter och koder (före och efter red" +
    "igering)";
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Location = new System.Drawing.Point(290, 13);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 11;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(290, 42);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // OverlayWithTableDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 159);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lblHelpText);
            this.Controls.Add(this.lblCode2);
            this.Controls.Add(this.lblCode1);
            this.Controls.Add(this.lblValue2);
            this.Controls.Add(this.lblValue1);
            this.Controls.Add(this.lblVariable);
            this.Controls.Add(this.tbCode2);
            this.Controls.Add(this.tbCode1);
            this.Controls.Add(this.tbValue2);
            this.Controls.Add(this.tbValue1);
            this.Controls.Add(this.tbVariable);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "OverlayWithTableDialog";
            this.Text = "OverlayWithTableDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbVariable;
        private System.Windows.Forms.TextBox tbValue1;
        private System.Windows.Forms.TextBox tbValue2;
        private System.Windows.Forms.TextBox tbCode1;
        private System.Windows.Forms.TextBox tbCode2;
        private System.Windows.Forms.Label lblVariable;
        private System.Windows.Forms.Label lblValue1;
        private System.Windows.Forms.Label lblValue2;
        private System.Windows.Forms.Label lblCode1;
        private System.Windows.Forms.Label lblCode2;
        private System.Windows.Forms.Label lblHelpText;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
    }
}