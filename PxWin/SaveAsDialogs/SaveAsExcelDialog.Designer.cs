namespace PCAxis.Desktop.SaveAsDialogs
{
    partial class SaveAsExcelDialog
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.rbAllInfo = new System.Windows.Forms.RadioButton();
            this.rbAllFootnotes = new System.Windows.Forms.RadioButton();
            this.rbMandatory = new System.Windows.Forms.RadioButton();
            this.rbNone = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(104, 122);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(12, 122);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 16;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // rbAllInfo
            // 
            this.rbAllInfo.AutoSize = true;
            this.rbAllInfo.Location = new System.Drawing.Point(12, 90);
            this.rbAllInfo.Name = "rbAllInfo";
            this.rbAllInfo.Size = new System.Drawing.Size(90, 17);
            this.rbAllInfo.TabIndex = 15;
            this.rbAllInfo.TabStop = true;
            this.rbAllInfo.Text = "All information";
            this.rbAllInfo.UseVisualStyleBackColor = true;
            // 
            // rbAllFootnotes
            // 
            this.rbAllFootnotes.AutoSize = true;
            this.rbAllFootnotes.Location = new System.Drawing.Point(12, 67);
            this.rbAllFootnotes.Name = "rbAllFootnotes";
            this.rbAllFootnotes.Size = new System.Drawing.Size(83, 17);
            this.rbAllFootnotes.TabIndex = 14;
            this.rbAllFootnotes.TabStop = true;
            this.rbAllFootnotes.Text = "All footnotes";
            this.rbAllFootnotes.UseVisualStyleBackColor = true;
            // 
            // rbMandatory
            // 
            this.rbMandatory.AutoSize = true;
            this.rbMandatory.Location = new System.Drawing.Point(12, 44);
            this.rbMandatory.Name = "rbMandatory";
            this.rbMandatory.Size = new System.Drawing.Size(122, 17);
            this.rbMandatory.TabIndex = 13;
            this.rbMandatory.TabStop = true;
            this.rbMandatory.Text = "Mandatory footnotes";
            this.rbMandatory.UseVisualStyleBackColor = true;
            // 
            // rbNone
            // 
            this.rbNone.AutoSize = true;
            this.rbNone.Location = new System.Drawing.Point(12, 21);
            this.rbNone.Name = "rbNone";
            this.rbNone.Size = new System.Drawing.Size(51, 17);
            this.rbNone.TabIndex = 12;
            this.rbNone.TabStop = true;
            this.rbNone.Text = "None";
            this.rbNone.UseVisualStyleBackColor = true;
            // 
            // SaveAsExcelDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(261, 163);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.rbAllInfo);
            this.Controls.Add(this.rbAllFootnotes);
            this.Controls.Add(this.rbMandatory);
            this.Controls.Add(this.rbNone);
            this.Name = "SaveAsExcelDialog";
            this.Text = "Save as Excel";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.RadioButton rbAllInfo;
        private System.Windows.Forms.RadioButton rbAllFootnotes;
        private System.Windows.Forms.RadioButton rbMandatory;
        private System.Windows.Forms.RadioButton rbNone;
    }
}