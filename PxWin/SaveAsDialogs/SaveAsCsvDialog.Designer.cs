namespace PCAxis.Desktop.SaveAsDialogs
{
    partial class SaveAsCsvDialog
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
            this.tbOther = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.rbOther = new System.Windows.Forms.RadioButton();
            this.rbTabulator = new System.Windows.Forms.RadioButton();
            this.rbBlank = new System.Windows.Forms.RadioButton();
            this.rbComma = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbWriteTitle = new System.Windows.Forms.CheckBox();
            this.cbDoubleColumn = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbOther
            // 
            this.tbOther.Enabled = false;
            this.tbOther.Location = new System.Drawing.Point(63, 88);
            this.tbOther.Name = "tbOther";
            this.tbOther.Size = new System.Drawing.Size(33, 20);
            this.tbOther.TabIndex = 13;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(278, 141);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(197, 141);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 11;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // rbOther
            // 
            this.rbOther.AutoSize = true;
            this.rbOther.Location = new System.Drawing.Point(6, 88);
            this.rbOther.Name = "rbOther";
            this.rbOther.Size = new System.Drawing.Size(51, 17);
            this.rbOther.TabIndex = 10;
            this.rbOther.TabStop = true;
            this.rbOther.Text = "Other";
            this.rbOther.UseVisualStyleBackColor = true;
            this.rbOther.CheckedChanged += new System.EventHandler(this.rbOther_CheckedChanged);
            // 
            // rbTabulator
            // 
            this.rbTabulator.AutoSize = true;
            this.rbTabulator.Location = new System.Drawing.Point(6, 65);
            this.rbTabulator.Name = "rbTabulator";
            this.rbTabulator.Size = new System.Drawing.Size(70, 17);
            this.rbTabulator.TabIndex = 9;
            this.rbTabulator.TabStop = true;
            this.rbTabulator.Text = "Tabulator";
            this.rbTabulator.UseVisualStyleBackColor = true;
            // 
            // rbBlank
            // 
            this.rbBlank.AutoSize = true;
            this.rbBlank.Location = new System.Drawing.Point(6, 42);
            this.rbBlank.Name = "rbBlank";
            this.rbBlank.Size = new System.Drawing.Size(52, 17);
            this.rbBlank.TabIndex = 8;
            this.rbBlank.TabStop = true;
            this.rbBlank.Text = "Blank";
            this.rbBlank.UseVisualStyleBackColor = true;
            // 
            // rbComma
            // 
            this.rbComma.AutoSize = true;
            this.rbComma.Location = new System.Drawing.Point(6, 19);
            this.rbComma.Name = "rbComma";
            this.rbComma.Size = new System.Drawing.Size(60, 17);
            this.rbComma.TabIndex = 7;
            this.rbComma.TabStop = true;
            this.rbComma.Text = "Comma";
            this.rbComma.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbComma);
            this.groupBox1.Controls.Add(this.tbOther);
            this.groupBox1.Controls.Add(this.rbBlank);
            this.groupBox1.Controls.Add(this.rbTabulator);
            this.groupBox1.Controls.Add(this.rbOther);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(341, 123);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Choose delimiter";
            // 
            // cbWriteTitle
            // 
            this.cbWriteTitle.AutoSize = true;
            this.cbWriteTitle.Location = new System.Drawing.Point(12, 145);
            this.cbWriteTitle.Name = "cbWriteTitle";
            this.cbWriteTitle.Size = new System.Drawing.Size(70, 17);
            this.cbWriteTitle.TabIndex = 15;
            this.cbWriteTitle.Text = "Write title";
            this.cbWriteTitle.UseVisualStyleBackColor = true;
            // 
            // cbDoubleColumn
            // 
            this.cbDoubleColumn.AutoSize = true;
            this.cbDoubleColumn.Location = new System.Drawing.Point(88, 145);
            this.cbDoubleColumn.Name = "cbDoubleColumn";
            this.cbDoubleColumn.Size = new System.Drawing.Size(97, 17);
            this.cbDoubleColumn.TabIndex = 16;
            this.cbDoubleColumn.Text = "Double column";
            this.cbDoubleColumn.UseVisualStyleBackColor = true;
            // 
            // SaveAsCsvDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(363, 179);
            this.Controls.Add(this.cbDoubleColumn);
            this.Controls.Add(this.cbWriteTitle);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Name = "SaveAsCsvDialog";
            this.Text = "Save as csv";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbOther;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.RadioButton rbOther;
        private System.Windows.Forms.RadioButton rbTabulator;
        private System.Windows.Forms.RadioButton rbBlank;
        private System.Windows.Forms.RadioButton rbComma;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cbWriteTitle;
        private System.Windows.Forms.CheckBox cbDoubleColumn;
    }
}