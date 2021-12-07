using System.Windows.Forms;

namespace PCAxis.Desktop
{
    partial class OpenTableForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OpenTableForm));
            this.lstDatabases = new System.Windows.Forms.ListView();
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.scDatabase = new System.Windows.Forms.SplitContainer();
            this.tvDatabase = new System.Windows.Forms.TreeView();
            this.lstTables = new System.Windows.Forms.ListView();
            this.chName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pnlDatabases = new System.Windows.Forms.Panel();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnOpen = new System.Windows.Forms.Button();
            this.dbContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsRefresh = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.scDatabase)).BeginInit();
            this.scDatabase.Panel1.SuspendLayout();
            this.scDatabase.Panel2.SuspendLayout();
            this.scDatabase.SuspendLayout();
            this.pnlDatabases.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.dbContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstDatabases
            // 
            this.lstDatabases.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstDatabases.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstDatabases.LargeImageList = this.imgList;
            this.lstDatabases.Location = new System.Drawing.Point(0, 0);
            this.lstDatabases.MultiSelect = false;
            this.lstDatabases.Name = "lstDatabases";
            this.lstDatabases.Size = new System.Drawing.Size(96, 373);
            this.lstDatabases.TabIndex = 0;
            this.lstDatabases.UseCompatibleStateImageBehavior = false;
            this.lstDatabases.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstDatabases_KeyPress);
            this.lstDatabases.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lstDatabases_MouseClick);
            // 
            // imgList
            // 
            this.imgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList.ImageStream")));
            this.imgList.TransparentColor = System.Drawing.Color.Transparent;
            this.imgList.Images.SetKeyName(0, "explorer.png");
            this.imgList.Images.SetKeyName(1, "database.png");
            this.imgList.Images.SetKeyName(2, "sql.png");
            this.imgList.Images.SetKeyName(3, "API-cloud.png");
            // 
            // scDatabase
            // 
            this.scDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scDatabase.Location = new System.Drawing.Point(102, 0);
            this.scDatabase.Name = "scDatabase";
            // 
            // scDatabase.Panel1
            // 
            this.scDatabase.Panel1.Controls.Add(this.tvDatabase);
            // 
            // scDatabase.Panel2
            // 
            this.scDatabase.Panel2.Controls.Add(this.lstTables);
            this.scDatabase.Size = new System.Drawing.Size(641, 320);
            this.scDatabase.SplitterDistance = 213;
            this.scDatabase.TabIndex = 2;
            // 
            // tvDatabase
            // 
            this.tvDatabase.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tvDatabase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvDatabase.Location = new System.Drawing.Point(0, 0);
            this.tvDatabase.Name = "tvDatabase";
            this.tvDatabase.Size = new System.Drawing.Size(213, 320);
            this.tvDatabase.TabIndex = 0;
            this.tvDatabase.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvDatabase_BeforeExpand);
            this.tvDatabase.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvDatabase_AfterSelect);
            this.tvDatabase.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tvDatabase_KeyPress);
            // 
            // lstTables
            // 
            this.lstTables.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstTables.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chName});
            this.lstTables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstTables.Location = new System.Drawing.Point(0, 0);
            this.lstTables.MultiSelect = false;
            this.lstTables.Name = "lstTables";
            this.lstTables.Size = new System.Drawing.Size(424, 320);
            this.lstTables.TabIndex = 0;
            this.lstTables.UseCompatibleStateImageBehavior = false;
            this.lstTables.View = System.Windows.Forms.View.Details;
            this.lstTables.SelectedIndexChanged += new System.EventHandler(this.lstTables_SelectedIndexChanged);
            this.lstTables.DoubleClick += new System.EventHandler(this.lstTables_DoubleClick);
            this.lstTables.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstTables_KeyPress);
            // 
            // chName
            // 
            this.chName.Text = "Name";
            this.chName.Width = 424;
            // 
            // pnlDatabases
            // 
            this.pnlDatabases.Controls.Add(this.lstDatabases);
            this.pnlDatabases.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlDatabases.Location = new System.Drawing.Point(0, 0);
            this.pnlDatabases.Name = "pnlDatabases";
            this.pnlDatabases.Size = new System.Drawing.Size(96, 373);
            this.pnlDatabases.TabIndex = 3;
            // 
            // pnlButtons
            // 
            this.pnlButtons.Controls.Add(this.btnOpen);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtons.Location = new System.Drawing.Point(96, 326);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(646, 47);
            this.pnlButtons.TabIndex = 4;
            // 
            // btnOpen
            // 
            this.btnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpen.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpen.Location = new System.Drawing.Point(559, 12);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 23);
            this.btnOpen.TabIndex = 1;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = false;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // dbContextMenuStrip
            // 
            this.dbContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsRefresh});
            this.dbContextMenuStrip.Name = "dbContextMenuStrip";
            this.dbContextMenuStrip.Size = new System.Drawing.Size(114, 26);
            // 
            // tsRefresh
            // 
            this.tsRefresh.Name = "tsRefresh";
            this.tsRefresh.Size = new System.Drawing.Size(113, 22);
            this.tsRefresh.Text = "Refresh";
            this.tsRefresh.Click += new System.EventHandler(this.tsRefresh_Click);
            // 
            // OpenTableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 373);
            this.ControlBox = false;
            this.Controls.Add(this.pnlButtons);
            this.Controls.Add(this.scDatabase);
            this.Controls.Add(this.pnlDatabases);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "OpenTableForm";
            this.Text = "OpenTableForm";
            this.Activated += new System.EventHandler(this.OpenTableForm_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OpenTableForm_FormClosing);
            this.Load += new System.EventHandler(this.OpenTableForm_Load);
            this.SizeChanged += new System.EventHandler(this.OpenTableForm_SizeChanged);
            this.scDatabase.Panel1.ResumeLayout(false);
            this.scDatabase.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scDatabase)).EndInit();
            this.scDatabase.ResumeLayout(false);
            this.pnlDatabases.ResumeLayout(false);
            this.pnlButtons.ResumeLayout(false);
            this.dbContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lstDatabases;
        private System.Windows.Forms.ImageList imgList;
        private System.Windows.Forms.SplitContainer scDatabase;
        private System.Windows.Forms.TreeView tvDatabase;
        private System.Windows.Forms.ListView lstTables;
        private System.Windows.Forms.Panel pnlDatabases;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.ColumnHeader chName;
        private ContextMenuStrip dbContextMenuStrip;
        private ToolStripMenuItem tsRefresh;
    }
}