namespace PCAxis.Desktop
{
    partial class StopRecordingDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StopRecordingDialog));
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lvSteps = new System.Windows.Forms.ListView();
            this.rbFixed = new System.Windows.Forms.RadioButton();
            this.rbRolling = new System.Windows.Forms.RadioButton();
            this.rbSame = new System.Windows.Forms.RadioButton();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.lblAlert = new System.Windows.Forms.Label();
            this.imgAlert = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.imgAlert)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Location = new System.Drawing.Point(374, 12);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(374, 41);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lvSteps
            // 
            this.lvSteps.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvSteps.BackColor = System.Drawing.SystemColors.Window;
            this.lvSteps.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvSteps.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvSteps.FullRowSelect = true;
            this.lvSteps.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvSteps.Location = new System.Drawing.Point(17, 12);
            this.lvSteps.Name = "lvSteps";
            this.lvSteps.Size = new System.Drawing.Size(340, 195);
            this.lvSteps.TabIndex = 8;
            this.lvSteps.UseCompatibleStateImageBehavior = false;
            this.lvSteps.View = System.Windows.Forms.View.Details;
            // 
            // rbFixed
            // 
            this.rbFixed.AutoSize = true;
            this.rbFixed.Checked = true;
            this.rbFixed.Location = new System.Drawing.Point(17, 214);
            this.rbFixed.Name = "rbFixed";
            this.rbFixed.Size = new System.Drawing.Size(350, 17);
            this.rbFixed.TabIndex = 11;
            this.rbFixed.TabStop = true;
            this.rbFixed.Text = "Fixed starting time: Show the same search and add new time periods.";
            this.rbFixed.UseVisualStyleBackColor = true;
            // 
            // rbRolling
            // 
            this.rbRolling.AutoSize = true;
            this.rbRolling.Location = new System.Drawing.Point(17, 237);
            this.rbRolling.Name = "rbRolling";
            this.rbRolling.Size = new System.Drawing.Size(440, 17);
            this.rbRolling.TabIndex = 12;
            this.rbRolling.Text = "Rolling starting time: Show the number of time periods and update with new time p" +
    "eriods.";
            this.rbRolling.UseVisualStyleBackColor = true;
            // 
            // rbSame
            // 
            this.rbSame.AutoSize = true;
            this.rbSame.Location = new System.Drawing.Point(17, 260);
            this.rbSame.Name = "rbSame";
            this.rbSame.Size = new System.Drawing.Size(252, 17);
            this.rbSame.TabIndex = 13;
            this.rbSame.Text = "Same time period: Show the same search again.";
            this.rbSame.UseVisualStyleBackColor = true;
            // 
            // lblAlert
            // 
            this.lblAlert.AutoSize = true;
            this.lblAlert.Location = new System.Drawing.Point(39, 286);
            this.lblAlert.MaximumSize = new System.Drawing.Size(360, 40);
            this.lblAlert.Name = "lblAlert";
            this.lblAlert.Size = new System.Drawing.Size(45, 13);
            this.lblAlert.TabIndex = 1;
            this.lblAlert.Text = "Alerttext";
            // 
            // imgAlert
            // 
            this.imgAlert.Image = global::PCAxis.Desktop.Properties.Resources.alert_16;
            this.imgAlert.Location = new System.Drawing.Point(17, 283);
            this.imgAlert.Name = "imgAlert";
            this.imgAlert.Size = new System.Drawing.Size(19, 19);
            this.imgAlert.TabIndex = 0;
            this.imgAlert.TabStop = false;
            // 
            // StopRecordingDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(461, 331);
            this.Controls.Add(this.imgAlert);
            this.Controls.Add(this.lblAlert);
            this.Controls.Add(this.rbSame);
            this.Controls.Add(this.rbRolling);
            this.Controls.Add(this.rbFixed);
            this.Controls.Add(this.lvSteps);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(477, 370);
            this.Name = "StopRecordingDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "StopRecordingDialog";
            ((System.ComponentModel.ISupportInitialize)(this.imgAlert)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ListView lvSteps;
        private System.Windows.Forms.RadioButton rbFixed;
        private System.Windows.Forms.RadioButton rbRolling;
        private System.Windows.Forms.RadioButton rbSame;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.PictureBox imgAlert;
        private System.Windows.Forms.Label lblAlert;
    }
}