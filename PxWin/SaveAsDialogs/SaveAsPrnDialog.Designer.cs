namespace PCAxis.Desktop.SaveAsDialogs
{
    partial class SaveAsPrnDialog
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
            this.rbComma = new System.Windows.Forms.RadioButton();
            this.rbBlank = new System.Windows.Forms.RadioButton();
            this.rbTabulator = new System.Windows.Forms.RadioButton();
            this.rbOther = new System.Windows.Forms.RadioButton();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tbOther = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // rbComma
            // 
            this.rbComma.AutoSize = true;
            this.rbComma.Location = new System.Drawing.Point(23, 28);
            this.rbComma.Name = "rbComma";
            this.rbComma.Size = new System.Drawing.Size(60, 17);
            this.rbComma.TabIndex = 0;
            this.rbComma.TabStop = true;
            this.rbComma.Text = "Comma";
            this.rbComma.UseVisualStyleBackColor = true;
            // 
            // rbBlank
            // 
            this.rbBlank.AutoSize = true;
            this.rbBlank.Location = new System.Drawing.Point(23, 51);
            this.rbBlank.Name = "rbBlank";
            this.rbBlank.Size = new System.Drawing.Size(52, 17);
            this.rbBlank.TabIndex = 1;
            this.rbBlank.TabStop = true;
            this.rbBlank.Text = "Blank";
            this.rbBlank.UseVisualStyleBackColor = true;
            // 
            // rbTabulator
            // 
            this.rbTabulator.AutoSize = true;
            this.rbTabulator.Location = new System.Drawing.Point(23, 74);
            this.rbTabulator.Name = "rbTabulator";
            this.rbTabulator.Size = new System.Drawing.Size(70, 17);
            this.rbTabulator.TabIndex = 2;
            this.rbTabulator.TabStop = true;
            this.rbTabulator.Text = "Tabulator";
            this.rbTabulator.UseVisualStyleBackColor = true;
            // 
            // rbOther
            // 
            this.rbOther.AutoSize = true;
            this.rbOther.Location = new System.Drawing.Point(23, 97);
            this.rbOther.Name = "rbOther";
            this.rbOther.Size = new System.Drawing.Size(51, 17);
            this.rbOther.TabIndex = 3;
            this.rbOther.TabStop = true;
            this.rbOther.Text = "Other";
            this.rbOther.UseVisualStyleBackColor = true;
            this.rbOther.CheckedChanged += new System.EventHandler(this.rbOther_CheckedChanged);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(23, 130);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(115, 130);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tbOther
            // 
            this.tbOther.Enabled = false;
            this.tbOther.Location = new System.Drawing.Point(115, 97);
            this.tbOther.Name = "tbOther";
            this.tbOther.Size = new System.Drawing.Size(33, 20);
            this.tbOther.TabIndex = 6;
            // 
            // SaveAsPrnDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(279, 179);
            this.Controls.Add(this.tbOther);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.rbOther);
            this.Controls.Add(this.rbTabulator);
            this.Controls.Add(this.rbBlank);
            this.Controls.Add(this.rbComma);
            this.Name = "SaveAsPrnDialog";
            this.Text = "Save as PRN - choose delimiter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbComma;
        private System.Windows.Forms.RadioButton rbBlank;
        private System.Windows.Forms.RadioButton rbTabulator;
        private System.Windows.Forms.RadioButton rbOther;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox tbOther;
    }
}