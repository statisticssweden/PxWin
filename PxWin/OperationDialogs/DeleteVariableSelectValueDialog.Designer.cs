namespace PCAxis.Desktop.OperationDialogs
{
    partial class DeleteVariableSelectValueDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeleteVariableSelectValueDialog));
            this.lbValues = new System.Windows.Forms.ListBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cbAddToContents = new System.Windows.Forms.CheckBox();
            this.lblListboxText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbValues
            // 
            this.lbValues.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbValues.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbValues.FormattingEnabled = true;
            this.lbValues.Location = new System.Drawing.Point(12, 32);
            this.lbValues.Name = "lbValues";
            this.lbValues.Size = new System.Drawing.Size(196, 210);
            this.lbValues.TabIndex = 0;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Location = new System.Drawing.Point(214, 32);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
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
            this.btnCancel.Location = new System.Drawing.Point(214, 61);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cbAddToContents
            // 
            this.cbAddToContents.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbAddToContents.AutoSize = true;
            this.cbAddToContents.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbAddToContents.Location = new System.Drawing.Point(12, 251);
            this.cbAddToContents.Name = "cbAddToContents";
            this.cbAddToContents.Size = new System.Drawing.Size(98, 17);
            this.cbAddToContents.TabIndex = 3;
            this.cbAddToContents.Text = "Add to contents";
            this.cbAddToContents.UseVisualStyleBackColor = true;
            // 
            // lblListboxText
            // 
            this.lblListboxText.AutoSize = true;
            this.lblListboxText.Location = new System.Drawing.Point(13, 13);
            this.lblListboxText.Name = "lblListboxText";
            this.lblListboxText.Size = new System.Drawing.Size(35, 13);
            this.lblListboxText.TabIndex = 4;
            this.lblListboxText.Text = "label1";
            // 
            // DeleteVariableSelectValueDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 273);
            this.Controls.Add(this.lblListboxText);
            this.Controls.Add(this.cbAddToContents);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lbValues);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DeleteVariableSelectValueDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "DeleteVariableSelectValueDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbValues;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox cbAddToContents;
        private System.Windows.Forms.Label lblListboxText;
    }
}