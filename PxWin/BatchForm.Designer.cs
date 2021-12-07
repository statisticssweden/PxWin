namespace PCAxis.Desktop
{
    partial class BatchForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BatchForm));
            this.btnRun = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnOpenSq = new System.Windows.Forms.Button();
            this.btnSaveToPxt = new System.Windows.Forms.Button();
            this.btnSaveToBat = new System.Windows.Forms.Button();
            this.comboFormat = new System.Windows.Forms.ComboBox();
            this.lblFileFormats = new System.Windows.Forms.Label();
            this.lblDestinationFolder = new System.Windows.Forms.Label();
            this.tbOutputDirectory = new System.Windows.Forms.TextBox();
            this.lblSaveToBat = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.openSqDialog = new System.Windows.Forms.OpenFileDialog();
            this.lvSavedQueries = new System.Windows.Forms.ListView();
            this.btnBrowsePxt = new System.Windows.Forms.Button();
            this.tbBrowsePxt = new System.Windows.Forms.TextBox();
            this.tabBatch = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lblAction = new System.Windows.Forms.Label();
            this.lblOpen = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.toolTipRemove = new System.Windows.Forms.ToolTip(this.components);
            this.openPxtDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveBatDialog = new System.Windows.Forms.SaveFileDialog();
            this.btnRemove = new System.Windows.Forms.Button();
            this.tabBatch.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRun
            // 
            this.btnRun.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnRun.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRun.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F);
            this.btnRun.Location = new System.Drawing.Point(10, 493);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(75, 25);
            this.btnRun.TabIndex = 0;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = false;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F);
            this.btnClose.Location = new System.Drawing.Point(488, 581);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 25);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOpenSq
            // 
            this.btnOpenSq.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnOpenSq.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenSq.Location = new System.Drawing.Point(10, 337);
            this.btnOpenSq.Name = "btnOpenSq";
            this.btnOpenSq.Size = new System.Drawing.Size(76, 25);
            this.btnOpenSq.TabIndex = 2;
            this.btnOpenSq.Text = "Open";
            this.btnOpenSq.UseVisualStyleBackColor = false;
            this.btnOpenSq.Click += new System.EventHandler(this.btnOpenSq_Click);
            // 
            // btnSaveToPxt
            // 
            this.btnSaveToPxt.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnSaveToPxt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveToPxt.Location = new System.Drawing.Point(91, 493);
            this.btnSaveToPxt.Name = "btnSaveToPxt";
            this.btnSaveToPxt.Size = new System.Drawing.Size(75, 25);
            this.btnSaveToPxt.TabIndex = 3;
            this.btnSaveToPxt.Text = "Save pxt";
            this.btnSaveToPxt.UseVisualStyleBackColor = false;
            this.btnSaveToPxt.Click += new System.EventHandler(this.btnSaveToPxt_Click);
            // 
            // btnSaveToBat
            // 
            this.btnSaveToBat.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnSaveToBat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveToBat.Location = new System.Drawing.Point(455, 66);
            this.btnSaveToBat.Name = "btnSaveToBat";
            this.btnSaveToBat.Size = new System.Drawing.Size(75, 23);
            this.btnSaveToBat.TabIndex = 4;
            this.btnSaveToBat.Text = "Save bat";
            this.btnSaveToBat.UseVisualStyleBackColor = false;
            this.btnSaveToBat.Click += new System.EventHandler(this.btnSaveToBat_Click);
            // 
            // comboFormat
            // 
            this.comboFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboFormat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboFormat.FormattingEnabled = true;
            this.comboFormat.Location = new System.Drawing.Point(10, 393);
            this.comboFormat.Name = "comboFormat";
            this.comboFormat.Size = new System.Drawing.Size(353, 21);
            this.comboFormat.TabIndex = 6;
            // 
            // lblFileFormats
            // 
            this.lblFileFormats.AutoSize = true;
            this.lblFileFormats.Location = new System.Drawing.Point(7, 377);
            this.lblFileFormats.Name = "lblFileFormats";
            this.lblFileFormats.Size = new System.Drawing.Size(100, 13);
            this.lblFileFormats.TabIndex = 7;
            this.lblFileFormats.Text = "2. Select file format ";
            // 
            // lblDestinationFolder
            // 
            this.lblDestinationFolder.AutoSize = true;
            this.lblDestinationFolder.Location = new System.Drawing.Point(7, 427);
            this.lblDestinationFolder.Name = "lblDestinationFolder";
            this.lblDestinationFolder.Size = new System.Drawing.Size(132, 13);
            this.lblDestinationFolder.TabIndex = 8;
            this.lblDestinationFolder.Text = "3. Select destination folder";
            // 
            // tbOutputDirectory
            // 
            this.tbOutputDirectory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbOutputDirectory.Location = new System.Drawing.Point(10, 443);
            this.tbOutputDirectory.Name = "tbOutputDirectory";
            this.tbOutputDirectory.ReadOnly = true;
            this.tbOutputDirectory.Size = new System.Drawing.Size(428, 20);
            this.tbOutputDirectory.TabIndex = 9;
            // 
            // lblSaveToBat
            // 
            this.lblSaveToBat.Location = new System.Drawing.Point(11, 16);
            this.lblSaveToBat.Name = "lblSaveToBat";
            this.lblSaveToBat.Size = new System.Drawing.Size(240, 18);
            this.lblSaveToBat.TabIndex = 12;
            this.lblSaveToBat.Text = "Browse a .pxt-file and save to .bat";
            // 
            // btnBrowse
            // 
            this.btnBrowse.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowse.Location = new System.Drawing.Point(444, 443);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(59, 23);
            this.btnBrowse.TabIndex = 13;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = false;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // openSqDialog
            // 
            this.openSqDialog.Multiselect = true;
            // 
            // lvSavedQueries
            // 
            this.lvSavedQueries.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvSavedQueries.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvSavedQueries.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvSavedQueries.FullRowSelect = true;
            this.lvSavedQueries.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvSavedQueries.Location = new System.Drawing.Point(10, 10);
            this.lvSavedQueries.Name = "lvSavedQueries";
            this.lvSavedQueries.Size = new System.Drawing.Size(501, 293);
            this.lvSavedQueries.TabIndex = 14;
            this.lvSavedQueries.UseCompatibleStateImageBehavior = false;
            this.lvSavedQueries.View = System.Windows.Forms.View.Details;
            // 
            // btnBrowsePxt
            // 
            this.btnBrowsePxt.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnBrowsePxt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowsePxt.Location = new System.Drawing.Point(456, 37);
            this.btnBrowsePxt.Name = "btnBrowsePxt";
            this.btnBrowsePxt.Size = new System.Drawing.Size(74, 23);
            this.btnBrowsePxt.TabIndex = 14;
            this.btnBrowsePxt.Text = "Browse ";
            this.btnBrowsePxt.UseVisualStyleBackColor = false;
            this.btnBrowsePxt.Click += new System.EventHandler(this.btnBrowsePxt_Click);
            // 
            // tbBrowsePxt
            // 
            this.tbBrowsePxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbBrowsePxt.Location = new System.Drawing.Point(15, 37);
            this.tbBrowsePxt.Name = "tbBrowsePxt";
            this.tbBrowsePxt.ReadOnly = true;
            this.tbBrowsePxt.Size = new System.Drawing.Size(435, 20);
            this.tbBrowsePxt.TabIndex = 13;
            // 
            // tabBatch
            // 
            this.tabBatch.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabBatch.Controls.Add(this.tabPage1);
            this.tabBatch.Controls.Add(this.tabPage2);
            this.tabBatch.Location = new System.Drawing.Point(12, 12);
            this.tabBatch.Name = "tabBatch";
            this.tabBatch.SelectedIndex = 0;
            this.tabBatch.Size = new System.Drawing.Size(555, 555);
            this.tabBatch.TabIndex = 20;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tabPage1.Controls.Add(this.btnRemove);
            this.tabPage1.Controls.Add(this.lblAction);
            this.tabPage1.Controls.Add(this.btnSaveToPxt);
            this.tabPage1.Controls.Add(this.btnOpenSq);
            this.tabPage1.Controls.Add(this.lblOpen);
            this.tabPage1.Controls.Add(this.lvSavedQueries);
            this.tabPage1.Controls.Add(this.btnRun);
            this.tabPage1.Controls.Add(this.btnBrowse);
            this.tabPage1.Controls.Add(this.lblFileFormats);
            this.tabPage1.Controls.Add(this.tbOutputDirectory);
            this.tabPage1.Controls.Add(this.comboFormat);
            this.tabPage1.Controls.Add(this.lblDestinationFolder);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(547, 529);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Batch";
            // 
            // lblAction
            // 
            this.lblAction.AutoSize = true;
            this.lblAction.Location = new System.Drawing.Point(7, 477);
            this.lblAction.Name = "lblAction";
            this.lblAction.Size = new System.Drawing.Size(87, 13);
            this.lblAction.TabIndex = 17;
            this.lblAction.Text = "4. Perform action";
            // 
            // lblOpen
            // 
            this.lblOpen.AutoSize = true;
            this.lblOpen.Location = new System.Drawing.Point(7, 321);
            this.lblOpen.Name = "lblOpen";
            this.lblOpen.Size = new System.Drawing.Size(225, 13);
            this.lblOpen.TabIndex = 16;
            this.lblOpen.Text = "1. Open saved query file (.pxsq) or list file (.pxt)";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tabPage2.Controls.Add(this.btnSaveToBat);
            this.tabPage2.Controls.Add(this.btnBrowsePxt);
            this.tabPage2.Controls.Add(this.tbBrowsePxt);
            this.tabPage2.Controls.Add(this.lblSaveToBat);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(547, 529);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Create BAT file";
            // 
            // openPxtDialog
            // 
            this.openPxtDialog.FileName = "openPxtDialog";
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemove.ForeColor = System.Drawing.SystemColors.Control;
            this.btnRemove.Image = global::PCAxis.Desktop.Properties.Resources.trash;
            this.btnRemove.Location = new System.Drawing.Point(514, 8);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(28, 28);
            this.btnRemove.TabIndex = 18;
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // BatchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(575, 620);
            this.Controls.Add(this.tabBatch);
            this.Controls.Add(this.btnClose);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BatchForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "BatchForm";
            this.tabBatch.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnOpenSq;
        private System.Windows.Forms.Button btnSaveToPxt;
        private System.Windows.Forms.Button btnSaveToBat;
        private System.Windows.Forms.ComboBox comboFormat;
        private System.Windows.Forms.Label lblFileFormats;
        private System.Windows.Forms.Label lblDestinationFolder;
        private System.Windows.Forms.TextBox tbOutputDirectory;
        private System.Windows.Forms.Label lblSaveToBat;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.OpenFileDialog openSqDialog;
        private System.Windows.Forms.ListView lvSavedQueries;
        private System.Windows.Forms.Button btnBrowsePxt;
        private System.Windows.Forms.TextBox tbBrowsePxt;
        private System.Windows.Forms.TabControl tabBatch;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label lblAction;
        private System.Windows.Forms.Label lblOpen;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.ToolTip toolTipRemove;
        private System.Windows.Forms.OpenFileDialog openPxtDialog;
        private System.Windows.Forms.SaveFileDialog saveBatDialog;
    }
}