namespace PCAxis.Desktop
{
    partial class GridModelForm
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

            if (disposing)
            {
                //TODO: dispose managed state (managed objects).
                if (mModel != null)
                {
                    mModel.Dispose();
                }

                if (undoModels != null)
                {
                    foreach (PCAxis.Paxiom.PXModel m in undoModels)
                    {
                        m.Dispose();
                    }
                }
            }

            // TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            // TODO: set large fields to null.

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GridModelForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grid = new PCAxis.Desktop.MyDataGridView();
            this.lblTableTitle = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabTable = new System.Windows.Forms.TabPage();
            this.tabFootnote = new System.Windows.Forms.TabPage();
            this.txtFootnote = new System.Windows.Forms.TextBox();
            this.tabInformation = new System.Windows.Forms.TabPage();
            this.gridFootnote = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.miSave = new System.Windows.Forms.ToolStripMenuItem();
            this.miSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.miSaveQuery = new System.Windows.Forms.ToolStripMenuItem();
            this.miEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.miPivot = new System.Windows.Forms.ToolStripMenuItem();
            this.miPivotCW = new System.Windows.Forms.ToolStripMenuItem();
            this.miPivotCCW = new System.Windows.Forms.ToolStripMenuItem();
            this.miDeleteVariable = new System.Windows.Forms.ToolStripMenuItem();
            this.miDeleteValue = new System.Windows.Forms.ToolStripMenuItem();
            this.miChangeValueOrder = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.miOverlayTable = new System.Windows.Forms.ToolStripMenuItem();
            this.miLinkTable = new System.Windows.Forms.ToolStripMenuItem();
            this.miChangeDecimals = new System.Windows.Forms.ToolStripMenuItem();
            this.miChangeTextCode = new System.Windows.Forms.ToolStripMenuItem();
            this.miCalculate = new System.Windows.Forms.ToolStripMenuItem();
            this.miSum = new System.Windows.Forms.ToolStripMenuItem();
            this.miAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.miSubstract = new System.Windows.Forms.ToolStripMenuItem();
            this.miMultiply = new System.Windows.Forms.ToolStripMenuItem();
            this.miDivide = new System.Windows.Forms.ToolStripMenuItem();
            this.miPercent = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbUndo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbPivotCW = new System.Windows.Forms.ToolStripButton();
            this.tsbPivot = new System.Windows.Forms.ToolStripButton();
            this.tsbPivotCCW = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbSum = new System.Windows.Forms.ToolStripButton();
            this.tsbPercent = new System.Windows.Forms.ToolStripButton();
            this.tsbAdd = new System.Windows.Forms.ToolStripButton();
            this.tsbSubstract = new System.Windows.Forms.ToolStripButton();
            this.tsbMultiply = new System.Windows.Forms.ToolStripButton();
            this.tsbDivide = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbStopRecording = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.sslTableName = new System.Windows.Forms.ToolStripStatusLabel();
            this.tbColumns = new System.Windows.Forms.ToolStripStatusLabel();
            this.tbRows = new System.Windows.Forms.ToolStripStatusLabel();
            this.tbCells = new System.Windows.Forms.ToolStripStatusLabel();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblDatabase = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabTable.SuspendLayout();
            this.tabFootnote.SuspendLayout();
            this.tabInformation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridFootnote)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grid
            // 
            this.grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.grid.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid.Location = new System.Drawing.Point(-1, -1);
            this.grid.Margin = new System.Windows.Forms.Padding(4);
            this.grid.Name = "grid";
            this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.grid.Size = new System.Drawing.Size(1150, 546);
            this.grid.TabIndex = 0;
            // 
            // lblTableTitle
            // 
            this.lblTableTitle.AutoSize = true;
            this.lblTableTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTableTitle.Location = new System.Drawing.Point(1, 30);
            this.lblTableTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTableTitle.Name = "lblTableTitle";
            this.lblTableTitle.Padding = new System.Windows.Forms.Padding(11, 7, 0, 7);
            this.lblTableTitle.Size = new System.Drawing.Size(95, 30);
            this.lblTableTitle.TabIndex = 1;
            this.lblTableTitle.Text = "lblTableTitle";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabTable);
            this.tabControl.Controls.Add(this.tabFootnote);
            this.tabControl.Controls.Add(this.tabInformation);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl.Location = new System.Drawing.Point(0, 68);
            this.tabControl.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1157, 573);
            this.tabControl.TabIndex = 9;
            // 
            // tabTable
            // 
            this.tabTable.Controls.Add(this.grid);
            this.tabTable.Location = new System.Drawing.Point(4, 24);
            this.tabTable.Margin = new System.Windows.Forms.Padding(4);
            this.tabTable.Name = "tabTable";
            this.tabTable.Padding = new System.Windows.Forms.Padding(4);
            this.tabTable.Size = new System.Drawing.Size(1149, 545);
            this.tabTable.TabIndex = 0;
            this.tabTable.Text = "tabTable";
            this.tabTable.UseVisualStyleBackColor = true;
            // 
            // tabFootnote
            // 
            this.tabFootnote.BackColor = System.Drawing.Color.Transparent;
            this.tabFootnote.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.tabFootnote.Controls.Add(this.txtFootnote);
            this.tabFootnote.Location = new System.Drawing.Point(4, 24);
            this.tabFootnote.Margin = new System.Windows.Forms.Padding(4);
            this.tabFootnote.Name = "tabFootnote";
            this.tabFootnote.Padding = new System.Windows.Forms.Padding(4);
            this.tabFootnote.Size = new System.Drawing.Size(1149, 545);
            this.tabFootnote.TabIndex = 1;
            this.tabFootnote.Text = "tabFootnote";
            this.tabFootnote.Enter += new System.EventHandler(this.EnterFootnoteTab);
            this.tabFootnote.Leave += new System.EventHandler(this.LeaveFootnoteTab);
            // 
            // txtFootnote
            // 
            this.txtFootnote.BackColor = System.Drawing.SystemColors.Window;
            this.txtFootnote.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFootnote.Location = new System.Drawing.Point(4, 4);
            this.txtFootnote.Margin = new System.Windows.Forms.Padding(4);
            this.txtFootnote.Multiline = true;
            this.txtFootnote.Name = "txtFootnote";
            this.txtFootnote.ReadOnly = true;
            this.txtFootnote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtFootnote.Size = new System.Drawing.Size(1141, 537);
            this.txtFootnote.TabIndex = 1;
            // 
            // tabInformation
            // 
            this.tabInformation.Controls.Add(this.gridFootnote);
            this.tabInformation.Location = new System.Drawing.Point(4, 24);
            this.tabInformation.Margin = new System.Windows.Forms.Padding(4);
            this.tabInformation.Name = "tabInformation";
            this.tabInformation.Size = new System.Drawing.Size(1149, 545);
            this.tabInformation.TabIndex = 2;
            this.tabInformation.Text = "tabInformation";
            this.tabInformation.UseVisualStyleBackColor = true;
            this.tabInformation.Enter += new System.EventHandler(this.EnterInformationTab);
            this.tabInformation.Leave += new System.EventHandler(this.LeaveInformationTab);
            // 
            // gridFootnote
            // 
            this.gridFootnote.AllowUserToAddRows = false;
            this.gridFootnote.AllowUserToDeleteRows = false;
            this.gridFootnote.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridFootnote.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gridFootnote.BackgroundColor = System.Drawing.SystemColors.Window;
            this.gridFootnote.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridFootnote.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.gridFootnote.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridFootnote.ColumnHeadersVisible = false;
            this.gridFootnote.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.gridFootnote.Location = new System.Drawing.Point(3, 1);
            this.gridFootnote.Margin = new System.Windows.Forms.Padding(4);
            this.gridFootnote.Name = "gridFootnote";
            this.gridFootnote.ReadOnly = true;
            this.gridFootnote.RowHeadersVisible = false;
            this.gridFootnote.Size = new System.Drawing.Size(1142, 462);
            this.gridFootnote.TabIndex = 1;
            // 
            // Column1
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle1;
            this.Column1.HeaderText = "";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 120;
            // 
            // Column2
            // 
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Column2.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column2.HeaderText = "";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 320;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.miEdit,
            this.miCalculate});
            this.menuStrip1.Location = new System.Drawing.Point(0, 34);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1157, 30);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.Visible = false;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.miSave,
            this.miSaveAs,
            this.miSaveQuery});
            this.fileToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 26);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(128, 6);
            // 
            // miSave
            // 
            this.miSave.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.miSave.MergeIndex = 1;
            this.miSave.Name = "miSave";
            this.miSave.Size = new System.Drawing.Size(131, 22);
            this.miSave.Text = "Save";
            this.miSave.Click += new System.EventHandler(this.miSave_Click);
            // 
            // miSaveAs
            // 
            this.miSaveAs.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.miSaveAs.MergeIndex = 2;
            this.miSaveAs.Name = "miSaveAs";
            this.miSaveAs.Size = new System.Drawing.Size(131, 22);
            this.miSaveAs.Text = "Save as";
            this.miSaveAs.Click += new System.EventHandler(this.miSaveAs_Click);
            // 
            // miSaveQuery
            // 
            this.miSaveQuery.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.miSaveQuery.MergeIndex = 5;
            this.miSaveQuery.Name = "miSaveQuery";
            this.miSaveQuery.Size = new System.Drawing.Size(131, 22);
            this.miSaveQuery.Text = "Save query";
            this.miSaveQuery.Click += new System.EventHandler(this.miSaveQuery_Click);
            // 
            // miEdit
            // 
            this.miEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miPivot,
            this.miPivotCW,
            this.miPivotCCW,
            this.miDeleteVariable,
            this.miDeleteValue,
            this.miChangeValueOrder,
            this.toolStripSeparator7,
            this.miOverlayTable,
            this.miLinkTable,
            this.miChangeDecimals,
            this.miChangeTextCode});
            this.miEdit.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.miEdit.MergeIndex = 1;
            this.miEdit.Name = "miEdit";
            this.miEdit.Size = new System.Drawing.Size(39, 26);
            this.miEdit.Text = "Edit";
            // 
            // miPivot
            // 
            this.miPivot.Name = "miPivot";
            this.miPivot.Size = new System.Drawing.Size(237, 22);
            this.miPivot.Text = "Pivot...";
            this.miPivot.Click += new System.EventHandler(this.miPivot_Click);
            // 
            // miPivotCW
            // 
            this.miPivotCW.Name = "miPivotCW";
            this.miPivotCW.Size = new System.Drawing.Size(237, 22);
            this.miPivotCW.Text = "Pivot clockwise";
            this.miPivotCW.Click += new System.EventHandler(this.miPivotCW_Click);
            // 
            // miPivotCCW
            // 
            this.miPivotCCW.Name = "miPivotCCW";
            this.miPivotCCW.Size = new System.Drawing.Size(237, 22);
            this.miPivotCCW.Text = "Pivot counter clockwise";
            this.miPivotCCW.Click += new System.EventHandler(this.miPivotCCW_Click);
            // 
            // miDeleteVariable
            // 
            this.miDeleteVariable.Name = "miDeleteVariable";
            this.miDeleteVariable.Size = new System.Drawing.Size(237, 22);
            this.miDeleteVariable.Text = "Delete variable...";
            this.miDeleteVariable.Click += new System.EventHandler(this.miDeleteVariable_Click);
            // 
            // miDeleteValue
            // 
            this.miDeleteValue.Name = "miDeleteValue";
            this.miDeleteValue.Size = new System.Drawing.Size(237, 22);
            this.miDeleteValue.Text = "Delete value...";
            this.miDeleteValue.Click += new System.EventHandler(this.miDeleteValue_Click);
            // 
            // miChangeValueOrder
            // 
            this.miChangeValueOrder.Name = "miChangeValueOrder";
            this.miChangeValueOrder.Size = new System.Drawing.Size(237, 22);
            this.miChangeValueOrder.Text = "Change value order...";
            this.miChangeValueOrder.Click += new System.EventHandler(this.miChangeValueOrder_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(234, 6);
            // 
            // miOverlayTable
            // 
            this.miOverlayTable.Name = "miOverlayTable";
            this.miOverlayTable.Size = new System.Drawing.Size(237, 22);
            this.miOverlayTable.Text = "Overlay with table...";
            this.miOverlayTable.Click += new System.EventHandler(this.miOverlayTable_Click);
            // 
            // miLinkTable
            // 
            this.miLinkTable.Name = "miLinkTable";
            this.miLinkTable.Size = new System.Drawing.Size(237, 22);
            this.miLinkTable.Text = "Link with table...";
            this.miLinkTable.Click += new System.EventHandler(this.miLinkTable_Click);
            // 
            // miChangeDecimals
            // 
            this.miChangeDecimals.Name = "miChangeDecimals";
            this.miChangeDecimals.Size = new System.Drawing.Size(237, 22);
            this.miChangeDecimals.Text = "Change decimals...";
            this.miChangeDecimals.Click += new System.EventHandler(this.miChangeDecimals_Click);
            // 
            // miChangeTextCode
            // 
            this.miChangeTextCode.Name = "miChangeTextCode";
            this.miChangeTextCode.Size = new System.Drawing.Size(237, 22);
            this.miChangeTextCode.Text = "Change text/code presentation";
            this.miChangeTextCode.Click += new System.EventHandler(this.miChangeTextCode_Click);
            // 
            // miCalculate
            // 
            this.miCalculate.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miSum,
            this.miAdd,
            this.miSubstract,
            this.miMultiply,
            this.miDivide,
            this.miPercent});
            this.miCalculate.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.miCalculate.MergeIndex = 2;
            this.miCalculate.Name = "miCalculate";
            this.miCalculate.Size = new System.Drawing.Size(68, 26);
            this.miCalculate.Text = "Calculate";
            // 
            // miSum
            // 
            this.miSum.Name = "miSum";
            this.miSum.Size = new System.Drawing.Size(149, 22);
            this.miSum.Text = "Sum...";
            this.miSum.Click += new System.EventHandler(this.miSum_Click);
            // 
            // miAdd
            // 
            this.miAdd.Name = "miAdd";
            this.miAdd.Size = new System.Drawing.Size(149, 22);
            this.miAdd.Text = "Add...";
            this.miAdd.Click += new System.EventHandler(this.miAdd_Click);
            // 
            // miSubstract
            // 
            this.miSubstract.Name = "miSubstract";
            this.miSubstract.Size = new System.Drawing.Size(149, 22);
            this.miSubstract.Text = "Substraction...";
            this.miSubstract.Click += new System.EventHandler(this.miSubstract_Click);
            // 
            // miMultiply
            // 
            this.miMultiply.Name = "miMultiply";
            this.miMultiply.Size = new System.Drawing.Size(149, 22);
            this.miMultiply.Text = "Multiply...";
            this.miMultiply.Click += new System.EventHandler(this.miMultiply_Click);
            // 
            // miDivide
            // 
            this.miDivide.Name = "miDivide";
            this.miDivide.Size = new System.Drawing.Size(149, 22);
            this.miDivide.Text = "Divide...";
            this.miDivide.Click += new System.EventHandler(this.miDivide_Click);
            // 
            // miPercent
            // 
            this.miPercent.Name = "miPercent";
            this.miPercent.Size = new System.Drawing.Size(149, 22);
            this.miPercent.Text = "Percent...";
            this.miPercent.Click += new System.EventHandler(this.miPercent_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.Window;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbSave,
            this.toolStripSeparator3,
            this.tsbUndo,
            this.toolStripSeparator4,
            this.tsbPivotCW,
            this.tsbPivot,
            this.tsbPivotCCW,
            this.toolStripSeparator5,
            this.tsbSum,
            this.tsbPercent,
            this.tsbAdd,
            this.tsbSubstract,
            this.tsbMultiply,
            this.tsbDivide,
            this.toolStripSeparator6,
            this.tsbStopRecording});
            this.toolStrip1.Location = new System.Drawing.Point(0, 34);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1157, 33);
            this.toolStrip1.TabIndex = 11;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.Visible = false;
            // 
            // tsbSave
            // 
            this.tsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSave.ForeColor = System.Drawing.SystemColors.Control;
            this.tsbSave.Image = ((System.Drawing.Image)(resources.GetObject("tsbSave.Image")));
            this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Size = new System.Drawing.Size(24, 30);
            this.tsbSave.Text = "Save";
            this.tsbSave.Click += new System.EventHandler(this.tsbSave_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 33);
            // 
            // tsbUndo
            // 
            this.tsbUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbUndo.Enabled = false;
            this.tsbUndo.ForeColor = System.Drawing.SystemColors.Control;
            this.tsbUndo.Image = ((System.Drawing.Image)(resources.GetObject("tsbUndo.Image")));
            this.tsbUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbUndo.Name = "tsbUndo";
            this.tsbUndo.Size = new System.Drawing.Size(24, 30);
            this.tsbUndo.Text = "Undo";
            this.tsbUndo.Click += new System.EventHandler(this.tsbUndo_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 33);
            // 
            // tsbPivotCW
            // 
            this.tsbPivotCW.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPivotCW.ForeColor = System.Drawing.SystemColors.Control;
            this.tsbPivotCW.Image = global::PCAxis.Desktop.Properties.Resources.pivot_clockwise;
            this.tsbPivotCW.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPivotCW.Name = "tsbPivotCW";
            this.tsbPivotCW.Size = new System.Drawing.Size(24, 30);
            this.tsbPivotCW.Text = "Pivot clockwise";
            this.tsbPivotCW.Click += new System.EventHandler(this.tsbPivotCW_Click);
            // 
            // tsbPivot
            // 
            this.tsbPivot.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPivot.ForeColor = System.Drawing.SystemColors.Control;
            this.tsbPivot.Image = ((System.Drawing.Image)(resources.GetObject("tsbPivot.Image")));
            this.tsbPivot.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPivot.Name = "tsbPivot";
            this.tsbPivot.Size = new System.Drawing.Size(24, 30);
            this.tsbPivot.Text = "Pivot...";
            this.tsbPivot.Click += new System.EventHandler(this.tsbPivot_Click);
            // 
            // tsbPivotCCW
            // 
            this.tsbPivotCCW.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPivotCCW.ForeColor = System.Drawing.SystemColors.Control;
            this.tsbPivotCCW.Image = global::PCAxis.Desktop.Properties.Resources.pivot_counterclockwise;
            this.tsbPivotCCW.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPivotCCW.Name = "tsbPivotCCW";
            this.tsbPivotCCW.Size = new System.Drawing.Size(24, 30);
            this.tsbPivotCCW.Text = "Pivot counter-clockwise";
            this.tsbPivotCCW.Click += new System.EventHandler(this.tsbPivotCCW_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 33);
            // 
            // tsbSum
            // 
            this.tsbSum.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSum.ForeColor = System.Drawing.SystemColors.Control;
            this.tsbSum.Image = global::PCAxis.Desktop.Properties.Resources.sum;
            this.tsbSum.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSum.Name = "tsbSum";
            this.tsbSum.Size = new System.Drawing.Size(24, 30);
            this.tsbSum.Text = "Sum...";
            this.tsbSum.Click += new System.EventHandler(this.tsbSum_Click);
            // 
            // tsbPercent
            // 
            this.tsbPercent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPercent.ForeColor = System.Drawing.SystemColors.Control;
            this.tsbPercent.Image = global::PCAxis.Desktop.Properties.Resources.percentage;
            this.tsbPercent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPercent.Name = "tsbPercent";
            this.tsbPercent.Size = new System.Drawing.Size(24, 30);
            this.tsbPercent.Text = "Percent";
            this.tsbPercent.Click += new System.EventHandler(this.tsbPercent_Click);
            // 
            // tsbAdd
            // 
            this.tsbAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAdd.ForeColor = System.Drawing.SystemColors.Control;
            this.tsbAdd.Image = global::PCAxis.Desktop.Properties.Resources.plus;
            this.tsbAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAdd.Name = "tsbAdd";
            this.tsbAdd.Size = new System.Drawing.Size(24, 30);
            this.tsbAdd.Text = "Add";
            this.tsbAdd.Click += new System.EventHandler(this.tsbAdd_Click);
            // 
            // tsbSubstract
            // 
            this.tsbSubstract.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSubstract.ForeColor = System.Drawing.SystemColors.Control;
            this.tsbSubstract.Image = global::PCAxis.Desktop.Properties.Resources.subtract;
            this.tsbSubstract.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSubstract.Name = "tsbSubstract";
            this.tsbSubstract.Size = new System.Drawing.Size(24, 30);
            this.tsbSubstract.Text = "Substract";
            this.tsbSubstract.Click += new System.EventHandler(this.tsbSubstract_Click);
            // 
            // tsbMultiply
            // 
            this.tsbMultiply.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbMultiply.ForeColor = System.Drawing.SystemColors.Control;
            this.tsbMultiply.Image = global::PCAxis.Desktop.Properties.Resources.multiply;
            this.tsbMultiply.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMultiply.Name = "tsbMultiply";
            this.tsbMultiply.Size = new System.Drawing.Size(24, 30);
            this.tsbMultiply.Text = "Multiply";
            this.tsbMultiply.Click += new System.EventHandler(this.tsbMultiply_Click);
            // 
            // tsbDivide
            // 
            this.tsbDivide.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbDivide.ForeColor = System.Drawing.SystemColors.Control;
            this.tsbDivide.Image = global::PCAxis.Desktop.Properties.Resources.divide;
            this.tsbDivide.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDivide.Name = "tsbDivide";
            this.tsbDivide.Size = new System.Drawing.Size(24, 30);
            this.tsbDivide.Text = "Divide";
            this.tsbDivide.Click += new System.EventHandler(this.tsbDivide_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 33);
            // 
            // tsbStopRecording
            // 
            this.tsbStopRecording.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbStopRecording.Image = global::PCAxis.Desktop.Properties.Resources.save_query;
            this.tsbStopRecording.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbStopRecording.Name = "tsbStopRecording";
            this.tsbStopRecording.Size = new System.Drawing.Size(24, 30);
            this.tsbStopRecording.Text = "toolStripButton2";
            this.tsbStopRecording.Click += new System.EventHandler(this.tsbStopRecording_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sslTableName,
            this.tbColumns,
            this.tbRows,
            this.tbCells});
            this.statusStrip1.Location = new System.Drawing.Point(0, 612);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1157, 30);
            this.statusStrip1.TabIndex = 12;
            this.statusStrip1.Text = "statusStrip1";
            this.statusStrip1.Visible = false;
            // 
            // sslTableName
            // 
            this.sslTableName.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.sslTableName.MergeAction = System.Windows.Forms.MergeAction.Replace;
            this.sslTableName.MergeIndex = 0;
            this.sslTableName.Name = "sslTableName";
            this.sslTableName.Size = new System.Drawing.Size(1011, 25);
            this.sslTableName.Spring = true;
            this.sslTableName.Text = "Name";
            this.sslTableName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbColumns
            // 
            this.tbColumns.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.tbColumns.MergeIndex = 1;
            this.tbColumns.Name = "tbColumns";
            this.tbColumns.Size = new System.Drawing.Size(55, 25);
            this.tbColumns.Text = "Columns";
            // 
            // tbRows
            // 
            this.tbRows.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.tbRows.MergeIndex = 2;
            this.tbRows.Name = "tbRows";
            this.tbRows.Size = new System.Drawing.Size(35, 25);
            this.tbRows.Text = "Rows";
            // 
            // tbCells
            // 
            this.tbCells.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.tbCells.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.tbCells.MergeIndex = 3;
            this.tbCells.Name = "tbCells";
            this.tbCells.Size = new System.Drawing.Size(36, 25);
            this.tbCells.Text = "Cells";
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewTextBoxColumn1.HeaderText = "";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 120;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewTextBoxColumn2.HeaderText = "";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 320;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblDatabase);
            this.panel1.Controls.Add(this.lblTableTitle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1157, 68);
            this.panel1.TabIndex = 13;
            // 
            // lblDatabase
            // 
            this.lblDatabase.AutoSize = true;
            this.lblDatabase.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDatabase.Location = new System.Drawing.Point(1, 1);
            this.lblDatabase.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDatabase.Name = "lblDatabase";
            this.lblDatabase.Padding = new System.Windows.Forms.Padding(11, 7, 0, 7);
            this.lblDatabase.Size = new System.Drawing.Size(104, 30);
            this.lblDatabase.TabIndex = 2;
            this.lblDatabase.Text = "lblDatabase";
            // 
            // GridModelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1157, 641);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "GridModelForm";
            this.Load += new System.EventHandler(this.GridModelForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tabTable.ResumeLayout(false);
            this.tabFootnote.ResumeLayout(false);
            this.tabFootnote.PerformLayout();
            this.tabInformation.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridFootnote)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyDataGridView grid;
        private System.Windows.Forms.Label lblTableTitle;
        private System.Windows.Forms.TabControl tabControl;
        public System.Windows.Forms.TabPage tabTable;
        public System.Windows.Forms.TabPage tabFootnote;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.TextBox txtFootnote;
        private System.Windows.Forms.TabPage tabInformation;
        private System.Windows.Forms.DataGridView gridFootnote;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        public System.Windows.Forms.ToolStripMenuItem miSave;
        private System.Windows.Forms.ToolStripMenuItem miSaveAs;
        private System.Windows.Forms.ToolStripMenuItem miEdit;
        private System.Windows.Forms.ToolStripMenuItem miPivot;
        private System.Windows.Forms.ToolStripMenuItem miPivotCW;
        private System.Windows.Forms.ToolStripMenuItem miPivotCCW;
        private System.Windows.Forms.ToolStripMenuItem miDeleteVariable;
        private System.Windows.Forms.ToolStripMenuItem miDeleteValue;
        private System.Windows.Forms.ToolStripMenuItem miChangeValueOrder;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem miOverlayTable;
        private System.Windows.Forms.ToolStripMenuItem miLinkTable;
        private System.Windows.Forms.ToolStripMenuItem miChangeDecimals;
        private System.Windows.Forms.ToolStripMenuItem miChangeTextCode;
        private System.Windows.Forms.ToolStripMenuItem miCalculate;
        private System.Windows.Forms.ToolStripMenuItem miSum;
        private System.Windows.Forms.ToolStripMenuItem miAdd;
        private System.Windows.Forms.ToolStripMenuItem miSubstract;
        private System.Windows.Forms.ToolStripMenuItem miMultiply;
        private System.Windows.Forms.ToolStripMenuItem miDivide;
        private System.Windows.Forms.ToolStripMenuItem miPercent;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsbUndo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton tsbPivotCW;
        private System.Windows.Forms.ToolStripButton tsbPivot;
        private System.Windows.Forms.ToolStripButton tsbPivotCCW;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton tsbSum;
        private System.Windows.Forms.ToolStripButton tsbPercent;
        private System.Windows.Forms.ToolStripButton tsbAdd;
        private System.Windows.Forms.ToolStripButton tsbSubstract;
        private System.Windows.Forms.ToolStripButton tsbMultiply;
        private System.Windows.Forms.ToolStripButton tsbDivide;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton tsbStopRecording;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tbColumns;
        private System.Windows.Forms.ToolStripStatusLabel tbRows;
        private System.Windows.Forms.ToolStripStatusLabel tbCells;
        private System.Windows.Forms.ToolStripStatusLabel sslTableName;
        private System.Windows.Forms.ToolStripMenuItem miSaveQuery;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblDatabase;
    }
}