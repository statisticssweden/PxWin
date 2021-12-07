namespace PCAxis.Desktop.UserControls
{
    partial class TextCodeControl
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
            this.rbText = new System.Windows.Forms.RadioButton();
            this.rbCode = new System.Windows.Forms.RadioButton();
            this.rbCodeText = new System.Windows.Forms.RadioButton();
            this.gbVariable = new System.Windows.Forms.GroupBox();
            this.gbVariable.SuspendLayout();
            this.SuspendLayout();
            // 
            // rbText
            // 
            this.rbText.AutoCheck = false;
            this.rbText.AutoSize = true;
            this.rbText.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbText.Location = new System.Drawing.Point(16, 19);
            this.rbText.Name = "rbText";
            this.rbText.Size = new System.Drawing.Size(60, 17);
            this.rbText.TabIndex = 3;
            this.rbText.TabStop = true;
            this.rbText.Text = "radioBu";
            this.rbText.UseVisualStyleBackColor = true;
            // 
            // rbCode
            // 
            this.rbCode.AutoCheck = false;
            this.rbCode.AutoSize = true;
            this.rbCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbCode.Location = new System.Drawing.Point(92, 19);
            this.rbCode.Name = "rbCode";
            this.rbCode.Size = new System.Drawing.Size(60, 17);
            this.rbCode.TabIndex = 4;
            this.rbCode.TabStop = true;
            this.rbCode.Text = "radioBu";
            this.rbCode.UseVisualStyleBackColor = true;
            // 
            // rbCodeText
            // 
            this.rbCodeText.AutoCheck = false;
            this.rbCodeText.AutoSize = true;
            this.rbCodeText.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbCodeText.Location = new System.Drawing.Point(158, 19);
            this.rbCodeText.Name = "rbCodeText";
            this.rbCodeText.Size = new System.Drawing.Size(84, 17);
            this.rbCodeText.TabIndex = 5;
            this.rbCodeText.TabStop = true;
            this.rbCodeText.Text = "radioButton3";
            this.rbCodeText.UseVisualStyleBackColor = true;
            // 
            // gbVariable
            // 
            this.gbVariable.Controls.Add(this.rbText);
            this.gbVariable.Controls.Add(this.rbCodeText);
            this.gbVariable.Controls.Add(this.rbCode);
            this.gbVariable.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gbVariable.Location = new System.Drawing.Point(3, 3);
            this.gbVariable.Name = "gbVariable";
            this.gbVariable.Size = new System.Drawing.Size(260, 52);
            this.gbVariable.TabIndex = 3;
            this.gbVariable.TabStop = false;
            this.gbVariable.Text = "Variable";
            // 
            // TextCodeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbVariable);
            this.Name = "TextCodeControl";
            this.Size = new System.Drawing.Size(272, 59);
            this.gbVariable.ResumeLayout(false);
            this.gbVariable.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton rbText;
        private System.Windows.Forms.RadioButton rbCode;
        private System.Windows.Forms.RadioButton rbCodeText;
        private System.Windows.Forms.GroupBox gbVariable;
    }
}
