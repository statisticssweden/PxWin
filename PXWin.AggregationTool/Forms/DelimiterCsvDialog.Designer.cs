namespace PXWin.AggregationTool
{
    partial class DelimiterCsvDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DelimiterCsvDialog));
            this.lblQuestion = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBoxRadioButtons = new System.Windows.Forms.GroupBox();
            this.rbCsvWithSpace = new System.Windows.Forms.RadioButton();
            this.rbCsvWithComma = new System.Windows.Forms.RadioButton();
            this.rbCsvWithTabulator = new System.Windows.Forms.RadioButton();
            this.lbFilename = new System.Windows.Forms.Label();
            this.groupBoxRadioButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblQuestion
            // 
            this.lblQuestion.AutoSize = true;
            this.lblQuestion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuestion.Location = new System.Drawing.Point(18, 7);
            this.lblQuestion.Name = "lblQuestion";
            this.lblQuestion.Size = new System.Drawing.Size(186, 15);
            this.lblQuestion.TabIndex = 0;
            this.lblQuestion.Text = "Which delimiter is use in the file?";
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Location = new System.Drawing.Point(21, 153);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(125, 153);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBoxRadioButtons
            // 
            this.groupBoxRadioButtons.Controls.Add(this.rbCsvWithSpace);
            this.groupBoxRadioButtons.Controls.Add(this.rbCsvWithComma);
            this.groupBoxRadioButtons.Controls.Add(this.rbCsvWithTabulator);
            this.groupBoxRadioButtons.Location = new System.Drawing.Point(25, 43);
            this.groupBoxRadioButtons.Name = "groupBoxRadioButtons";
            this.groupBoxRadioButtons.Size = new System.Drawing.Size(175, 104);
            this.groupBoxRadioButtons.TabIndex = 4;
            this.groupBoxRadioButtons.TabStop = false;
            // 
            // rbCsvWithSpace
            // 
            this.rbCsvWithSpace.AutoSize = true;
            this.rbCsvWithSpace.Location = new System.Drawing.Point(15, 67);
            this.rbCsvWithSpace.Name = "rbCsvWithSpace";
            this.rbCsvWithSpace.Size = new System.Drawing.Size(56, 17);
            this.rbCsvWithSpace.TabIndex = 2;
            this.rbCsvWithSpace.Text = "Space";
            this.rbCsvWithSpace.UseVisualStyleBackColor = true;
            // 
            // rbCsvWithComma
            // 
            this.rbCsvWithComma.AutoSize = true;
            this.rbCsvWithComma.Location = new System.Drawing.Point(15, 44);
            this.rbCsvWithComma.Name = "rbCsvWithComma";
            this.rbCsvWithComma.Size = new System.Drawing.Size(60, 17);
            this.rbCsvWithComma.TabIndex = 1;
            this.rbCsvWithComma.Text = "Comma";
            this.rbCsvWithComma.UseVisualStyleBackColor = true;
            // 
            // rbCsvWithTabulator
            // 
            this.rbCsvWithTabulator.AutoSize = true;
            this.rbCsvWithTabulator.Checked = true;
            this.rbCsvWithTabulator.Location = new System.Drawing.Point(15, 21);
            this.rbCsvWithTabulator.Name = "rbCsvWithTabulator";
            this.rbCsvWithTabulator.Size = new System.Drawing.Size(70, 17);
            this.rbCsvWithTabulator.TabIndex = 0;
            this.rbCsvWithTabulator.TabStop = true;
            this.rbCsvWithTabulator.Text = "Tabulator";
            this.rbCsvWithTabulator.UseVisualStyleBackColor = true;
            // 
            // lbFilename
            // 
            this.lbFilename.AutoSize = true;
            this.lbFilename.Location = new System.Drawing.Point(18, 27);
            this.lbFilename.Name = "lbFilename";
            this.lbFilename.Size = new System.Drawing.Size(52, 13);
            this.lbFilename.TabIndex = 5;
            this.lbFilename.Text = "File name";
            // 
            // DelimiterCsvDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(231, 198);
            this.Controls.Add(this.lbFilename);
            this.Controls.Add(this.groupBoxRadioButtons);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lblQuestion);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DelimiterCsvDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Delimiter";
            this.Load += new System.EventHandler(this.DelimiterCsvDialog_Load);
            this.groupBoxRadioButtons.ResumeLayout(false);
            this.groupBoxRadioButtons.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblQuestion;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBoxRadioButtons;
        private System.Windows.Forms.RadioButton rbCsvWithSpace;
        private System.Windows.Forms.RadioButton rbCsvWithComma;
        private System.Windows.Forms.RadioButton rbCsvWithTabulator;
        private System.Windows.Forms.Label lbFilename;
    }
}