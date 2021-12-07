namespace PCAxis.Desktop.OperationDialogs
{
    partial class PivotDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PivotDialog));
            this.lvStub = new System.Windows.Forms.ListView();
            this.chStub = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvHeading = new System.Windows.Forms.ListView();
            this.chHeading = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnStubToHeading = new System.Windows.Forms.Button();
            this.btnHeadingToStub = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lvStub
            // 
            this.lvStub.AllowDrop = true;
            this.lvStub.BackColor = System.Drawing.SystemColors.Window;
            this.lvStub.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvStub.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chStub});
            this.lvStub.Location = new System.Drawing.Point(12, 12);
            this.lvStub.MultiSelect = false;
            this.lvStub.Name = "lvStub";
            this.lvStub.Size = new System.Drawing.Size(148, 303);
            this.lvStub.TabIndex = 0;
            this.lvStub.UseCompatibleStateImageBehavior = false;
            this.lvStub.View = System.Windows.Forms.View.Details;
            this.lvStub.DragDrop += new System.Windows.Forms.DragEventHandler(this.listview_DragDrop);
            this.lvStub.DragEnter += new System.Windows.Forms.DragEventHandler(this.listview_DragEnter);
            this.lvStub.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listview_MouseDown);
            // 
            // chStub
            // 
            this.chStub.Text = "Stub";
            this.chStub.Width = 144;
            // 
            // lvHeading
            // 
            this.lvHeading.AllowDrop = true;
            this.lvHeading.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvHeading.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chHeading});
            this.lvHeading.Location = new System.Drawing.Point(201, 12);
            this.lvHeading.MultiSelect = false;
            this.lvHeading.Name = "lvHeading";
            this.lvHeading.Size = new System.Drawing.Size(148, 303);
            this.lvHeading.TabIndex = 1;
            this.lvHeading.UseCompatibleStateImageBehavior = false;
            this.lvHeading.View = System.Windows.Forms.View.Details;
            this.lvHeading.DragDrop += new System.Windows.Forms.DragEventHandler(this.listview_DragDrop);
            this.lvHeading.DragEnter += new System.Windows.Forms.DragEventHandler(this.listview_DragEnter);
            this.lvHeading.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listview_MouseDown);
            // 
            // chHeading
            // 
            this.chHeading.Text = "Heading";
            this.chHeading.Width = 144;
            // 
            // btnStubToHeading
            // 
            this.btnStubToHeading.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnStubToHeading.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStubToHeading.Location = new System.Drawing.Point(167, 124);
            this.btnStubToHeading.Name = "btnStubToHeading";
            this.btnStubToHeading.Size = new System.Drawing.Size(28, 23);
            this.btnStubToHeading.TabIndex = 2;
            this.btnStubToHeading.Text = ">>";
            this.btnStubToHeading.UseVisualStyleBackColor = false;
            this.btnStubToHeading.Click += new System.EventHandler(this.btnStubToHeading_Click);
            // 
            // btnHeadingToStub
            // 
            this.btnHeadingToStub.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnHeadingToStub.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHeadingToStub.Location = new System.Drawing.Point(167, 163);
            this.btnHeadingToStub.Name = "btnHeadingToStub";
            this.btnHeadingToStub.Size = new System.Drawing.Size(28, 23);
            this.btnHeadingToStub.TabIndex = 3;
            this.btnHeadingToStub.Text = "<<";
            this.btnHeadingToStub.UseVisualStyleBackColor = false;
            this.btnHeadingToStub.Click += new System.EventHandler(this.btnHeadingToStub_Click);
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Location = new System.Drawing.Point(193, 332);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "&Ok";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(274, 332);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // PivotDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 369);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnHeadingToStub);
            this.Controls.Add(this.btnStubToHeading);
            this.Controls.Add(this.lvHeading);
            this.Controls.Add(this.lvStub);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(378, 407);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(378, 407);
            this.Name = "PivotDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Pivot";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvStub;
        private System.Windows.Forms.ColumnHeader chStub;
        private System.Windows.Forms.ListView lvHeading;
        private System.Windows.Forms.ColumnHeader chHeading;
        private System.Windows.Forms.Button btnStubToHeading;
        private System.Windows.Forms.Button btnHeadingToStub;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
    }
}