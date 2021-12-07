using System.Windows.Forms;

namespace PXWin.AggregationTool
{
    partial class ValuesetForm
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
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblDomain = new System.Windows.Forms.Label();
            this.txtDomain = new System.Windows.Forms.TextBox();
            this.gridValues = new System.Windows.Forms.DataGridView();
            this.Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Text = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctxMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsInsertEmpty = new System.Windows.Forms.ToolStripMenuItem();
            this.tsCut = new System.Windows.Forms.ToolStripMenuItem();
            this.tsPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.sslFileName = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnNewAggregation = new System.Windows.Forms.Button();
            this.pnlAggFiles = new System.Windows.Forms.FlowLayoutPanel();
            this.lblPrestext = new System.Windows.Forms.Label();
            this.txtPrestext = new System.Windows.Forms.TextBox();
            this.lblType = new System.Windows.Forms.Label();
            this.txtType = new System.Windows.Forms.TextBox();
            this.lblContainsDuplicates = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.tsbImport = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridValues)).BeginInit();
            this.ctxMenu.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(12, 15);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(38, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Name:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(67, 12);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(308, 20);
            this.txtName.TabIndex = 1;
            // 
            // lblDomain
            // 
            this.lblDomain.AutoSize = true;
            this.lblDomain.Location = new System.Drawing.Point(12, 63);
            this.lblDomain.Name = "lblDomain";
            this.lblDomain.Size = new System.Drawing.Size(46, 13);
            this.lblDomain.TabIndex = 2;
            this.lblDomain.Text = "Domain:";
            // 
            // txtDomain
            // 
            this.txtDomain.Location = new System.Drawing.Point(67, 60);
            this.txtDomain.Name = "txtDomain";
            this.txtDomain.Size = new System.Drawing.Size(308, 20);
            this.txtDomain.TabIndex = 3;
            // 
            // gridValues
            // 
            this.gridValues.AllowDrop = true;
            this.gridValues.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridValues.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridValues.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridValues.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Code,
            this.Text});
            this.gridValues.ContextMenuStrip = this.ctxMenu;
            this.gridValues.Location = new System.Drawing.Point(-4, 0);
            this.gridValues.Name = "gridValues";
            this.gridValues.Size = new System.Drawing.Size(833, 398);
            this.gridValues.TabIndex = 5;
            this.gridValues.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridValues_CellMouseUp);
            this.gridValues.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridValues_CellValueChanged);
            this.gridValues.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.gridValues_RowsRemoved);
            this.gridValues.DragDrop += new System.Windows.Forms.DragEventHandler(this.gridValues_DragDrop);
            this.gridValues.DragOver += new System.Windows.Forms.DragEventHandler(this.gridValues_DragOver);
            this.gridValues.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gridValues_MouseDown);
            // 
            // Code
            // 
            this.Code.Name = "Code";
            // 
            // Text
            // 
            this.Text.Name = "Text";
            // 
            // ctxMenu
            // 
            this.ctxMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsInsertEmpty,
            this.tsCut,
            this.tsPaste});
            this.ctxMenu.Name = "ctxMenu";
            this.ctxMenu.Size = new System.Drawing.Size(164, 70);
            // 
            // tsInsertEmpty
            // 
            this.tsInsertEmpty.Name = "tsInsertEmpty";
            this.tsInsertEmpty.Size = new System.Drawing.Size(163, 22);
            this.tsInsertEmpty.Text = "Insert empty row";
            this.tsInsertEmpty.Click += new System.EventHandler(this.tsInsertEmpty_Click);
            // 
            // tsCut
            // 
            this.tsCut.Name = "tsCut";
            this.tsCut.Size = new System.Drawing.Size(163, 22);
            this.tsCut.Text = "Cut";
            this.tsCut.Click += new System.EventHandler(this.tsCut_Click);
            // 
            // tsPaste
            // 
            this.tsPaste.Name = "tsPaste";
            this.tsPaste.Size = new System.Drawing.Size(163, 22);
            this.tsPaste.Text = "Paste";
            this.tsPaste.Click += new System.EventHandler(this.tsPaste_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbSave,
            this.tsbImport});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(837, 25);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.Visible = false;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.HeaderText = "Code";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.HeaderText = "Text";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sslFileName});
            this.statusStrip1.Location = new System.Drawing.Point(0, 526);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(837, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 10;
            this.statusStrip1.Text = "statusStrip1";
            this.statusStrip1.Visible = false;
            // 
            // sslFileName
            // 
            this.sslFileName.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.sslFileName.MergeAction = System.Windows.Forms.MergeAction.Replace;
            this.sslFileName.MergeIndex = 0;
            this.sslFileName.Name = "sslFileName";
            this.sslFileName.Size = new System.Drawing.Size(822, 17);
            this.sslFileName.Spring = true;
            this.sslFileName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Location = new System.Drawing.Point(0, 121);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(837, 424);
            this.tabControl.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.gridValues);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(829, 398);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Values";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnNewAggregation);
            this.tabPage2.Controls.Add(this.pnlAggFiles);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(829, 398);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Aggregation files";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnNewAggregation
            // 
            this.btnNewAggregation.Enabled = false;
            this.btnNewAggregation.Location = new System.Drawing.Point(11, 16);
            this.btnNewAggregation.Name = "btnNewAggregation";
            this.btnNewAggregation.Size = new System.Drawing.Size(167, 23);
            this.btnNewAggregation.TabIndex = 10;
            this.btnNewAggregation.Text = "Create new aggregation file";
            this.btnNewAggregation.UseVisualStyleBackColor = true;
            this.btnNewAggregation.Click += new System.EventHandler(this.btnNewAggregation_Click);
            // 
            // pnlAggFiles
            // 
            this.pnlAggFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlAggFiles.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pnlAggFiles.Location = new System.Drawing.Point(11, 54);
            this.pnlAggFiles.Name = "pnlAggFiles";
            this.pnlAggFiles.Size = new System.Drawing.Size(802, 372);
            this.pnlAggFiles.TabIndex = 9;
            // 
            // lblPrestext
            // 
            this.lblPrestext.AutoSize = true;
            this.lblPrestext.Location = new System.Drawing.Point(12, 39);
            this.lblPrestext.Name = "lblPrestext";
            this.lblPrestext.Size = new System.Drawing.Size(48, 13);
            this.lblPrestext.TabIndex = 12;
            this.lblPrestext.Text = "Prestext:";
            // 
            // txtPrestext
            // 
            this.txtPrestext.Location = new System.Drawing.Point(67, 36);
            this.txtPrestext.Name = "txtPrestext";
            this.txtPrestext.Size = new System.Drawing.Size(308, 20);
            this.txtPrestext.TabIndex = 2;
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(12, 85);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(34, 13);
            this.lblType.TabIndex = 14;
            this.lblType.Text = "Type:";
            // 
            // txtType
            // 
            this.txtType.Location = new System.Drawing.Point(67, 84);
            this.txtType.Name = "txtType";
            this.txtType.ReadOnly = true;
            this.txtType.Size = new System.Drawing.Size(308, 20);
            this.txtType.TabIndex = 4;
            // 
            // lblContainsDuplicates
            // 
            this.lblContainsDuplicates.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblContainsDuplicates.AutoSize = true;
            this.lblContainsDuplicates.BackColor = System.Drawing.Color.LightPink;
            this.lblContainsDuplicates.Location = new System.Drawing.Point(221, 110);
            this.lblContainsDuplicates.Name = "lblContainsDuplicates";
            this.lblContainsDuplicates.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblContainsDuplicates.Size = new System.Drawing.Size(157, 13);
            this.lblContainsDuplicates.TabIndex = 15;
            this.lblContainsDuplicates.Text = "The valuset contains duplicates";
            this.lblContainsDuplicates.Visible = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(185, 110);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(0, 0);
            this.tableLayoutPanel1.TabIndex = 16;
            // 
            // tsbSave
            // 
            this.tsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSave.Image = global::PXWin.AggregationTool.Properties.Resources.save;
            this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Size = new System.Drawing.Size(23, 22);
            this.tsbSave.Text = "toolStripButton1";
            this.tsbSave.Click += new System.EventHandler(this.tsbSave_Click);
            // 
            // tsbImport
            // 
            this.tsbImport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbImport.Image = global::PXWin.AggregationTool.Properties.Resources.import;
            this.tsbImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbImport.Name = "tsbImport";
            this.tsbImport.Size = new System.Drawing.Size(23, 22);
            this.tsbImport.Text = "toolStripButton2";
            this.tsbImport.Click += new System.EventHandler(this.tsbImport_Click);
            // 
            // ValuesetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblContainsDuplicates);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.txtType);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.txtPrestext);
            this.Controls.Add(this.lblPrestext);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.txtDomain);
            this.Controls.Add(this.lblDomain);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "ValuesetForm";
            this.Size = new System.Drawing.Size(837, 548);
            this.Load += new System.EventHandler(this.ValuesetForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridValues)).EndInit();
            this.ctxMenu.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblDomain;
        private System.Windows.Forms.TextBox txtDomain;
        private System.Windows.Forms.DataGridView gridValues;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbSave;
        private System.Windows.Forms.ToolStripButton tsbImport;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel sslFileName;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.FlowLayoutPanel pnlAggFiles;
        private System.Windows.Forms.Button btnNewAggregation;
        private System.Windows.Forms.ContextMenuStrip ctxMenu;
        private System.Windows.Forms.ToolStripMenuItem tsInsertEmpty;
        private System.Windows.Forms.ToolStripMenuItem tsCut;
        private System.Windows.Forms.ToolStripMenuItem tsPaste;
        private System.Windows.Forms.Label lblPrestext;
        private System.Windows.Forms.TextBox txtPrestext;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.TextBox txtType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn Text;
        private Label lblContainsDuplicates;
        private TableLayoutPanel tableLayoutPanel1;
    }
}
