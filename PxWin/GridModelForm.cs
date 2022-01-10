using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Globalization;
using System.Windows.Forms;
using PCAxis.Paxiom;
using PCAxis.Desktop.Grid;
using PCAxis.Desktop.OperationDialogs;
using PCAxis.Query;
using PCAxis.Paxiom.Operations;
using System.Linq;
using PX.Plugin.Interfaces.Attributes;
using PX.Desktop.Interfaces;

namespace PCAxis.Desktop
{
    [Export()]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class GridModelForm : ToolWindowContent, IToolWindow, IToolbar, IStatusbar, IMenubar, ILocalizable
    {
        [Import]
        private IHost _host;
        
        private string _id;
        
        /// <summary>
        /// List av all available serializers
        /// </summary>
        [ImportMany(AllowRecomposition = true)]
        private IEnumerable<Lazy<Func<IPXModelStreamSerializer>, ISerializerMetadata>> _saveAsFormats;
        /// <summary>
        /// Paxiom model for the table to display
        /// </summary>
        public PXModel mModel;

        /// <summary>
        /// Helper object for building grid presentation for Paxiom model
        /// </summary>
        GridHelper gridHelper;

        /// <summary>
        /// Stack with paxiom models for Undo-functionality
        /// </summary>
        public List<PXModel> undoModels;

        /// <summary>
        /// Database info for the table
        /// </summary>
        public DatabaseInfo DbInfo { get; set; }

        ///// <summary>
        ///// Only one percent operation is permitted per Model. Sets the number on which the percent operation was performed.
        ///// </summary>
        public int PercentModel{ get; set; }
        
        public OperationsTracker OperationsTracker { get; set; }
        public TableQuery TableQuery { get; set; }

        /// <summary>
        /// Is set to true when start recording is initialized
        /// </summary>
        public bool IsRecording { get; set; }

        /// <summary>
        /// Default filename when the table is about to be saved
        /// </summary>
        /// <returns>Default filename when the table is about to be saved. If no model exist an empty string is returned</returns>
        public string Filename
        {
            get
            {
                try
                {
                    if (this.IsFileTable)
                    {
                        return System.IO.Path.GetFileName(mModel.Meta.MainTable);
                    }
                    else
                    {
                        return mModel.Meta.Matrix;
                    }
                }
                catch (Exception)
                {
                    return "";
                }
            }
        }

        public string Directory
        {
            get
            {
                try
                {
                    if (this.IsFileTable)
                    {
                        return System.IO.Path.GetDirectoryName(mModel.Meta.MainTable);
                    }
                    else
                    {
                        return mModel.Meta.Matrix;
                    }
                }
                catch (Exception)
                {
                    return "";
                }
            }
        }

        /// <summary>
        /// True if the table is a PX-file table
        /// False if the table is a CNMM table
        /// </summary>
        public bool IsFileTable
        {
            get
            {
                try
                {
                    if (mModel.Meta.MainTable.EndsWith(".px", StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public GridModelForm()
        {
            InitializeComponent();
            Tool = this;
            _id = Guid.NewGuid().ToString() + ".px";
            this.grid.ReadOnly = true;
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToOrderColumns = false;
            this.grid.SelectionMode = DataGridViewSelectionMode.CellSelect;
            this.grid.VirtualMode = true;
            // Connect the virtual-mode events to event handlers.  
            this.grid.CellValueNeeded += new
                DataGridViewCellValueEventHandler(grid_CellValueNeeded);

            OperationsTracker = new Query.OperationsTracker();
            IsRecording = true;
        }

        public void SwitchLanguage()
        {
            lblDatabase.Text = GetDatabaseName(DbInfo);
            tabTable.Text = Lang.GetLocalizedString("TabTable");
            tabFootnote.Text = Lang.GetLocalizedString("TabFootnote");
            tabInformation.Text = Lang.GetLocalizedString("TabInformation");

            fileToolStripMenuItem.Text = Lang.GetLocalizedString("MenuFile");
            miEdit.Text = Lang.GetLocalizedString("MenuEdit");
            miCalculate.Text = Lang.GetLocalizedString("MenuCalculate");
            miSave.Text = Lang.GetLocalizedString("MenuSave");
            miSaveAs.Text = Lang.GetLocalizedString("MenuSaveAs");
            miSaveQuery.Text = Lang.GetLocalizedString("MenuStopRecording");

            miPivot.Text = Lang.GetLocalizedString("MenuEditPivot");
            miPivotCW.Text = Lang.GetLocalizedString("MenuEditPivotCW");
            miPivotCCW.Text = Lang.GetLocalizedString("MenuEditPivotCCW");
            miDeleteVariable.Text = Lang.GetLocalizedString("MenuEditDeleteVariable");
            miDeleteValue.Text = Lang.GetLocalizedString("MenuEditDeleteValue");
            miChangeValueOrder.Text = Lang.GetLocalizedString("MenuEditChangeValueOrder");
            miPercent.Text = Lang.GetLocalizedString("MenuCalculatePercent");
            miSum.Text = Lang.GetLocalizedString("MenuCalculateSum");
            miMultiply.Text = Lang.GetLocalizedString("MenuCalculateMultiply");
            miDivide.Text = Lang.GetLocalizedString("MenuCalculateDivide");
            miSubstract.Text = Lang.GetLocalizedString("MenuCalculateSubstract");
            miAdd.Text = Lang.GetLocalizedString("MenuCalculateAdd");
            miOverlayTable.Text = Lang.GetLocalizedString("OverlayTableText");
            miLinkTable.Text = Lang.GetLocalizedString("LinkTableText");
            miChangeDecimals.Text = Lang.GetLocalizedString("ChangeDecimalMenu");
            miChangeTextCode.Text = Lang.GetLocalizedString("ChangeTextCodeMenu");

            tsbSave.Text = Lang.GetLocalizedString("MenuSave");
            tsbUndo.Text = Lang.GetLocalizedString("MenuUndo");
            tsbStopRecording.Text = Lang.GetLocalizedString("MenuStopRecording");
            tsbPivot.Text = Lang.GetLocalizedString("MenuEditPivot");
            tsbPivotCW.Text = Lang.GetLocalizedString("MenuEditPivotCW");
            tsbPivotCCW.Text = Lang.GetLocalizedString("MenuEditPivotCCW");
            tsbSum.Text = Lang.GetLocalizedString("MenuCalculateSum");
            tsbPercent.Text = Lang.GetLocalizedString("MenuCalculatePercent");
            tsbAdd.Text = Lang.GetLocalizedString("MenuCalculateAdd");
            tsbSubstract.Text = Lang.GetLocalizedString("MenuCalculateSubstract");
            tsbMultiply.Text = Lang.GetLocalizedString("MenuCalculateMultiply");
            tsbDivide.Text = Lang.GetLocalizedString("MenuCalculateDivide");

            tbColumns.Text = Lang.GetLocalizedString("FooterColumns") + (mModel != null ? ":" + mModel.Data.MatrixColumnCount.ToString() : "");
            tbRows.Text = Lang.GetLocalizedString("FooterRows") + (mModel != null ? ":" + mModel.Data.MatrixRowCount.ToString() : "");
            tbCells.Text = Lang.GetLocalizedString("FooterCells") + (mModel != null ? ":" + mModel.Data.MatrixSize.ToString() : "");
        }


        #region "Event handlers"
        
        /// <summary>
        /// Form load event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridModelForm_Load(object sender, EventArgs e)
        {
            sslTableName.Text = Text;
            SwitchLanguage();
        }
        
        /// <summary>
        /// Called for virtual mode grid when a cell value is needed 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grid_CellValueNeeded(object sender,
            System.Windows.Forms.DataGridViewCellValueEventArgs e)
        {
            e.Value = gridHelper.GetCellValue(e.RowIndex, e.ColumnIndex);
        }
        
        #endregion

        #region "Public methods"
        
        /// <summary>
        /// Set the Paxiom model and display the table
        /// </summary>
        /// <param name="builder"></param>
        public void SetModel(PXModel model)
        {
            mModel = model;                   
            RecreateTable();         
        }

        #endregion


        /// <summary>
        /// Get the table title
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private string GetTableTitle(PXModel model)
        {
            if (model.Meta.DescriptionDefault && !string.IsNullOrEmpty(model.Meta.Description))
            {
                return model.Meta.Description;
            }
            else
            {
                return model.Meta.Title;
            }
        }

        /// <summary>
        /// Display the table
        /// </summary>
        public void RecreateTable()
        {
            Lang.SetTitleForModel(mModel);
            lblDatabase.Text = GetDatabaseName(DbInfo);
            //lblTableTitle.Text = mModel.Meta.Title;
            lblTableTitle.Text = GetTableTitle(mModel);

            grid.Rows.Clear();
            grid.Columns.Clear();           
                      
            gridHelper = new GridHelper();
            gridHelper.CreateGrid(grid, mModel);

            var columnCount = grid.ColumnCount - gridHelper.tableMeta.ColumnOffset;
            var rowCount = grid.RowCount - gridHelper.tableMeta.RowOffset;

            tbColumns.Text = Lang.GetLocalizedString("FooterColumns") + " " + columnCount;
            tbRows.Text = Lang.GetLocalizedString("FooterRows") + " " + rowCount;
            tbCells.Text = Lang.GetLocalizedString("FooterCells") + " " + (columnCount * rowCount);

            grid.ClearSelection();
            Cursor.Current = Cursors.Default;
        }

        private string GetDatabaseName(DatabaseInfo dbInfo)
        {
            if (dbInfo == null)
            {
                return "";
            }
            else if (dbInfo.Id.Equals("FileSystem"))
            {
                return Lang.GetLocalizedString("PxFile");
            }
            else if (string.IsNullOrEmpty(dbInfo.Name))
            {
                return "";
            }
            else
            {
                return dbInfo.Name;
            }
        }

        private void EnterFootnoteTab(object sender, EventArgs e)
        {
	        var title = string.Empty;
	        var data = string.Empty;

            title = Lang.GetLocalizedString("FootnoteNote");
            data = mModel.Meta.GetAllNotes(InformationLevelType.AllFootnotes);
            txtFootnote.Text = data;
        }

        private void LeaveFootnoteTab(object sender, EventArgs e)
        {
        }

        private void EnterInformationTab(object sender, EventArgs e)
        {
            var title = string.Empty;
            var data = string.Empty;
            int rowIndex = 0;

            // Latest update
            title = Lang.GetLocalizedString("FootnoteLatestUpdate");
            if (mModel.Meta.ContentInfo != null && mModel.Meta.ContentInfo.LastUpdated != null)
            {
                data = mModel.Meta.ContentInfo.LastUpdated;
            }
            else
            {
                data = "";
            }
            rowIndex = gridFootnote.Rows.Add();
            gridFootnote.Rows[rowIndex].Cells[0].Value = title;
            gridFootnote.Rows[rowIndex].Cells[1].Value = data;

            // Source
            title = Lang.GetLocalizedString("FootnoteSorce");
            data = mModel.Meta.Source;
            rowIndex = gridFootnote.Rows.Add();
            gridFootnote.Rows[rowIndex].Cells[0].Value = title;
            gridFootnote.Rows[rowIndex].Cells[1].Value = data;

            // Contact
            title = Lang.GetLocalizedString("FootnoteContact");
            if (mModel.Meta.ContentInfo != null && mModel.Meta.ContentInfo.Contact != null)
            {
                data = mModel.Meta.ContentInfo.Contact.Trim().Replace("#", Environment.NewLine);
            }
            else
            {
                data = "";
            }
            rowIndex = gridFootnote.Rows.Add();
            gridFootnote.Rows[rowIndex].Cells[0].Value = title;
            gridFootnote.Rows[rowIndex].Cells[1].Value = data;

            // Units
            title = Lang.GetLocalizedString("FootnoteUnits");
            if (mModel.Meta.ContentInfo != null && mModel.Meta.ContentInfo.Units != null)
            {
                data = mModel.Meta.ContentInfo.Units;
            }
            else
            {
                data = "";
            }
            rowIndex = gridFootnote.Rows.Add();
            gridFootnote.Rows[rowIndex].Cells[0].Value = title;
            gridFootnote.Rows[rowIndex].Cells[1].Value = data;

            // Data type
            title = Lang.GetLocalizedString("FootnoteDataType");
            if (mModel.Meta.ContentInfo != null && mModel.Meta.ContentInfo.StockFa != null)
            {
                data = mModel.Meta.ContentInfo.StockFa;
            }
            else
            {
                data = "";
            }
            rowIndex = gridFootnote.Rows.Add();
            gridFootnote.Rows[rowIndex].Cells[0].Value = title;
            gridFootnote.Rows[rowIndex].Cells[1].Value = data;

            // Reference period
            title = Lang.GetLocalizedString("FootnoteReferencePeriod");
            if (mModel.Meta.ContentInfo != null && mModel.Meta.ContentInfo.RefPeriod != null)
            {
                data = mModel.Meta.ContentInfo.RefPeriod;
            }
            else
            {
                data = "";
            }
            rowIndex = gridFootnote.Rows.Add();
            gridFootnote.Rows[rowIndex].Cells[0].Value = title;
            gridFootnote.Rows[rowIndex].Cells[1].Value = data;

            // Database
            title = Lang.GetLocalizedString("FootnoteDatabase");
            data = mModel.Meta.Database;
            rowIndex = gridFootnote.Rows.Add();
            gridFootnote.Rows[rowIndex].Cells[0].Value = title;
            gridFootnote.Rows[rowIndex].Cells[1].Value = data;

            // Internal reference code
            title = Lang.GetLocalizedString("FootnoteInternalReferenceCode");
            data = mModel.Meta.InfoFile;
            rowIndex = gridFootnote.Rows.Add();
            gridFootnote.Rows[rowIndex].Cells[0].Value = title;
            gridFootnote.Rows[rowIndex].Cells[1].Value = data;

            tbColumns.Visible = false;
            tbCells.Visible = false;
            tbRows.Visible = false;
            gridFootnote.ClearSelection();
        }

        private void LeaveInformationTab(object sender, EventArgs e)
        {
            gridFootnote.Rows.Clear();
            tbColumns.Visible = true;
            tbCells.Visible = true;
            tbRows.Visible = true;
        }

        public DialogResult SaveAs()
        {
            SaveFileDialog dlg = new SaveFileDialog();

            string filter = string.Join("|", (from f in _saveAsFormats
                                              select (Lang.GetLocalizedString(f.Metadata.Id) + " (*." + f.Metadata.Extension + ")|*." + f.Metadata.Extension)).ToArray());

            dlg.Title = Lang.GetLocalizedString("SaveAs");
            dlg.RestoreDirectory = true;
            dlg.AddExtension = true;
            dlg.DefaultExt = "px";
            
            dlg.Filter = filter;//"PC-Axis Files (*.px)|*.px|CSV Files (*.csv)|*.csv|CSV Files with heading (*.csv)|*.csv|TSV Files (*.tsv)|*.tsv";
            if (IsFileTable && !this.DbInfo.Type.Equals("PXAPI"))
            {
                dlg.InitialDirectory = Directory;
            }
            dlg.FileName = Filename;

            
            var res = dlg.ShowDialog();
            if (res != DialogResult.OK) return res;

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                var index = dlg.FilterIndex;
                var fmt = _saveAsFormats.Skip(index - 1).First();

                //Call the function to get the serializer
                var serializer = fmt.Value();

                serializer.Serialize(mModel, dlg.FileName);
                MessageBox.Show(string.Format(Lang.GetLocalizedString("SaveAsComplete"),
                    dlg.FileName),
                    Lang.GetLocalizedString("SaveCaption"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

            }
            finally
            {

                Cursor.Current = Cursors.Default;
            }
      
            return DialogResult.OK;

            

            //var res = saveFileDialog1.ShowDialog();
            //if (res != DialogResult.OK) return res;

            //Cursor.Current = Cursors.WaitCursor;
            //var serializer = SaveHandler.GetSerializer(saveFileDialog1.FilterIndex);

            //serializer.Serialize(mModel, saveFileDialog1.FileName);
            //MessageBox.Show(string.Format(Lang.GetLocalizedString("SaveAsComplete"),
            //    saveFileDialog1.FileName),
            //    Lang.GetLocalizedString("SaveCaption"),
            //    MessageBoxButtons.OK,
            //    MessageBoxIcon.Information);
            //return DialogResult.OK;
        }

        private void miSave_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        private void miSaveAs_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        #region Operations

        private void Sum()
        {
            using (var sumDialog = new SumDialog())
            {
                sumDialog.SelectedModel = mModel;

                if (sumDialog.ShowDialog() != DialogResult.OK) return;

                var d = sumDialog.GetDescription();
                if (d == null) return;

                SetForUndo();
                var p = new Paxiom.Operations.Sum();
                try
                {
                    mModel = p.Execute(mModel, d);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Lang.GetLocalizedString("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                RecreateTable();

                if (IsRecording)
                {
                    OperationsTracker.AddStep(OperationConstants.SUM, d);
                    Variable va = mModel.Meta.Variables.FirstOrDefault(v => v.Code == d.VariableCode);
                    if (va != null && va.IsTime)
                    {
                        OperationsTracker.IsTimeDependent = true;
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation"></param>
        private void CalculateValue(SumOperationType operation)
        {
            if (mModel.Meta.Stub.Count > 0 & mModel.Meta.Stub[0].Values.Count >= 1)
            {
                using (var dlg = new CalculateDialog(IsRecording, OperationsTracker))
                {
                    dlg.SelectedModel = mModel;
                    dlg.SelectedOperation = operation;
                    if (dlg.ShowDialog() != DialogResult.OK) return;
                    SetForUndo();
                    mModel = dlg.SelectedModel;
                    RecreateTable();
                }
            }
        }

        private void CalculatePercent(CalculatePerPartType type)
        {
            using (var dlg = new PercentDialog())
            {
                dlg.SelectedModel = mModel;
                dlg.OperationType = type;

                if (dlg.ShowDialog() != DialogResult.OK) return;
                SetForUndo();

                var opDesc = dlg.GetOperationDescription();
                if (opDesc == null) return;

                mModel = dlg.SelectedModel;
                PercentModel = undoModels.Count;

                miPercent.Enabled = false;
                tsbPercent.Enabled = false;
                RecreateTable();

                if (IsRecording)
                {
                    OperationsTracker.AddStep(OperationConstants.PER_PART, opDesc);

                    foreach (Selection selection in opDesc.ValueSelection)
                    {
                        Variable va = mModel.Meta.Variables.FirstOrDefault(v => v.Code == selection.VariableCode);
                        if (va != null && va.IsTime)
                        {
                            if (selection.ValueCodes.Count > 0)
                            {
                                OperationsTracker.IsTimeDependent = true;
                            }
                        }
                    }
                }
            }
        }

        private void DeleteVariable()
        {
            using (var dlg = new DeleteVariableDialog(IsRecording, OperationsTracker))
            {
                dlg.SelectedModel = mModel;
                if (dlg.ShowDialog() != DialogResult.OK) return;
                SetForUndo();
                mModel = dlg.SelectedModel;
                RecreateTable();
            }
        }

        private void DeleteValue()
        {
            using (var dlg = new DeleteValueDialog(IsRecording, OperationsTracker))
            {
                dlg.SelectedModel = mModel;
                if (dlg.ShowDialog() != DialogResult.OK) return;
                SetForUndo();
                mModel = dlg.SelectedModel;
                RecreateTable();
            }
        }

        private void ChangeValueOrder()
        {
            // Open SelectVariable dialog and recieve the selected variable
            using (var dialogSelectVariable = new SelectVariableDialog())
            {
                dialogSelectVariable.SelectedModel = mModel;

                if (dialogSelectVariable.ShowDialog() != DialogResult.OK) return;
                // Pass the selected variable to the ChangeValueOrder dialog
                var variable = dialogSelectVariable.SelectedVariable;
                using (var dialogChangeValueOrder = new ChangeValueOrderDialog())
                {
                    dialogChangeValueOrder.SelectedModel = mModel;
                    dialogChangeValueOrder.SelectedVariable = variable;

                    if (dialogChangeValueOrder.ShowDialog() != DialogResult.OK) return;
                    // Update model and recreate sheet
                    SetForUndo();

                    var opDesc = dialogChangeValueOrder.GetOperationDescription();
                    if (opDesc == null) return;

                    mModel = dialogChangeValueOrder.SelectedModel;
                    RecreateTable();

                    if (IsRecording)
                    {
                        OperationsTracker.AddStep(OperationConstants.CHANGE_VALUE_ORDER, opDesc);
                        var va = mModel.Meta.Variables.FirstOrDefault(v => v.Code == opDesc.VariableCode);
                        if (va != null && va.IsTime)
                        {
                            OperationsTracker.IsTimeDependent = true;
                        }
                    }
                }
            }
            // Pass on selected variable to ChangeValueOrder dialog
        }

        private void ChangeDecimals()
        {
            using (var decimalDialog = new ChangeDecimalDialog())
            {
                if (decimalDialog.ShowDialog() != DialogResult.OK) return;
                SetForUndo();
                var decimalOperation = new Paxiom.Operations.ChangeDecimals();
                var decimalDescription = new ChangeDecimalsDescription(decimalDialog.GetDecimals());
                mModel = decimalOperation.Execute(mModel, decimalDescription);
                RecreateTable();

                if (IsRecording)
                {
                    OperationsTracker.AddStep(OperationConstants.CHANGE_DECIMALS, decimalDescription);
                }
            }
        }

        private void PivotCW()
        {
            SetForUndo();
            var p = new Paxiom.Operations.Pivot();
            mModel = p.PivotCW(mModel);
            RecreateTable();

            if (IsRecording)
            {
                OperationsTracker.AddStep(OperationConstants.PIVOT_CW, null);
            }
        }

        private void PivotCCW()
        {
            SetForUndo();
            var p = new Paxiom.Operations.Pivot();
            mModel = p.PivotCCW(mModel);
            RecreateTable();

            if (IsRecording)
            {
                OperationsTracker.AddStep(OperationConstants.PIVOT_CCW, null);
            }
        }

        private void Pivot()
        {
            using (var pivotDialog = new PivotDialog())
            {
                pivotDialog.SelectedModel = mModel;

                if (pivotDialog.ShowDialog() != DialogResult.OK) return;

                var pd = pivotDialog.GetPivotDescriptions();
                if (pd == null) return;

                SetForUndo();
                var p = new Paxiom.Operations.Pivot();
                mModel = p.Execute(mModel, pd);
                RecreateTable();

                if (IsRecording)
                {
                    OperationsTracker.AddStep(OperationConstants.PIVOT, pd);
                }
            }
        }



        private void OverlayWithTable()
        {
            string filePath = string.Empty;
            string folderPath = GetSelectedFolderPath("OVERLAY");

            if (folderPath.Length > 0)
            {
                using (var dlg = new SelectPxFileDialog())
                {
                    dlg.Action = "OVERLAY";
                    dlg.FolderPath = folderPath;
                    dlg.CurrentModel = mModel;
                    dlg.Text = Lang.GetLocalizedString("OverlayTableDialogText");
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        // Open dialog to select dbFile
                        filePath = dlg.SelectedFilePath;
                        // MessageBox.Show(filePath)

                        using (var overlayDialog = new OverlayWithTableDialog())
                        {
                            if (overlayDialog.ShowDialog() == DialogResult.OK)
                            {
                                var overlayTableOperation = new OverlayWithTable();
                                var overlayTableDescription = new OverlayWithTableDescription
                                {
                                    OverlayModel = OpenTableWithSelectAll(filePath),
                                    OverlayVariable = overlayDialog.OverlayVariable,
                                    OverlayCode1 = overlayDialog.OverlayCode1,
                                    OverlayCode2 = overlayDialog.OverlayCode2,
                                    OverlayValue1 = overlayDialog.OverlayValue1,
                                    OverlayValue2 = overlayDialog.OverlayValue2
                                };

                                SetForUndo();
                                mModel = overlayTableOperation.Execute(mModel, overlayTableDescription);
                                RecreateTable();
                            }
                        }
                    }
                }
            }
        }

        private void LinkWithTable()
        {
            string filePath = string.Empty;
            string folderPath = GetSelectedFolderPath("LINK");

            if (folderPath.Length > 0)
            {
                using (var dlg = new SelectPxFileDialog())
                {
                    dlg.Action = "LINK";
                    dlg.CurrentModel = mModel;
                    dlg.FolderPath = folderPath;
                    dlg.Text = Lang.GetLocalizedString("LinkTableText2");
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        // Open dialog to select dbFile
                        filePath = dlg.SelectedFilePath;

                        var linkTableOperation = new LinkWithTable();
                        var linkTableDescription = new LinkWithTableDescription(OpenTableWithSelectAll(filePath));

                        SetForUndo();
                        mModel = linkTableOperation.Execute(mModel, linkTableDescription);
                        RecreateTable();
                    }
                }
            }
        }

        private void ChangeTextCode()
        {
            using (var dlg = new ChangeTextCodeDialog(mModel))
            {
                if (dlg.ShowDialog() != DialogResult.OK) return;
                var operation = new ChangeTextCodePresentation();
                var presDescription = new ChangeTextCodePresentationDescription(dlg.GetSelections());
                SetForUndo();
                mModel = operation.Execute(mModel, presDescription);
                RecreateTable();

                if (IsRecording)
                {
                    OperationsTracker.AddStep(OperationConstants.CHANGE_TEXT_CODE_PRESENTATION, presDescription);
                }
            }
        }

        private void SaveQuery()
        {
            using (var stopDialog = new StopRecordingDialog(this))
            {
                MEFBooter.Container.ComposeParts(stopDialog);
                stopDialog.ShowDialog();
            }
        }

        private PXModel OpenTableWithSelectAll(string filePath)
        {
            var builder = new PXFileBuilder();
            builder.SetPath(filePath);

            try
            {
                Cursor = Cursors.WaitCursor;
                builder.BuildForSelection();

            }
            finally
            {
                Cursor = Cursors.Default;
            }

            var selection = Selection.SelectAll(builder.Model.Meta);
            builder.BuildForPresentation(selection);
            // Return model
            return builder.Model;
        }

        private string GetSelectedFolderPath(string action)
        {
            using (var dlg = new SelectFolderDialog(action))
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    // Open dialog to select dbFile
                    return dlg.SelectedFolderPath;
                }
                return string.Empty;
            }
        }


        /// <summary>
        /// Pops the latest PXmodel and uses it to recreate the table
        /// </summary>
        private void Undo()
        {
            mModel = undoModels.Last();
            if (PercentModel == undoModels.Count)
            {
                miPercent.Enabled = true;
                tsbPercent.Enabled = true;
            }

            undoModels.Remove(mModel);
            RecreateTable();
            tsbUndo.Enabled = undoModels.Count > 0;
            tsbSave.Enabled = tsbUndo.Enabled;
            miSave.Enabled = tsbUndo.Enabled;

            if (IsRecording)
            {
                OperationsTracker.RemoveLastStep();
            }
        }



        /// <summary>
        /// Stacks PXmodels before every operation
        /// </summary>
        /// <param name="GridModel"></param>
        private void SetForUndo()
        {
            tsbUndo.Enabled = true;
            tsbSave.Enabled = true;
            miSave.Enabled = true;
            if (undoModels == null)
            {
                undoModels = new List<PXModel>() { mModel };
            }
            else
            {
                undoModels.Add(mModel);
            }
        }
        #endregion

        #region "Event handlers"

        private void miPivot_Click(object sender, EventArgs e)
        {
            Pivot();
        }

        private void miPivotCW_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            PivotCW();
        }

        private void miPivotCCW_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            PivotCCW();
        }

        private void miDeleteVariable_Click(object sender, EventArgs e)
        {
            DeleteVariable();
        }

        private void miDeleteValue_Click(object sender, EventArgs e)
        {
            DeleteValue();
        }

        private void miChangeValueOrder_Click(object sender, EventArgs e)
        {
            ChangeValueOrder();
        }

        private void miOverlayTable_Click(object sender, EventArgs e)
        {
            OverlayWithTable();
        }

        private void miLinkTable_Click(object sender, EventArgs e)
        {
            LinkWithTable();
        }

        private void miChangeDecimals_Click(object sender, EventArgs e)
        {
            ChangeDecimals();
        }

        private void miChangeTextCode_Click(object sender, EventArgs e)
        {
            ChangeTextCode();
        }

        private void miSum_Click(object sender, EventArgs e)
        {
            Sum();
        }

        private void miAdd_Click(object sender, EventArgs e)
        {
            CalculateValue(SumOperationType.Addition);
        }

        private void miSubstract_Click(object sender, EventArgs e)
        {
            CalculateValue(SumOperationType.Subtraction);
        }

        private void miMultiply_Click(object sender, EventArgs e)
        {
            CalculateValue(SumOperationType.Multiplication);
        }

        private void miDivide_Click(object sender, EventArgs e)
        {
            CalculateValue(SumOperationType.Division);
        }

        private void miPercent_Click(object sender, EventArgs e)
        {
            CalculatePercent(CalculatePerPartType.PerCent);
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        private void tsbUndo_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Undo();
        }

        private void tsbPivotCW_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            PivotCW();
        }

        private void tsbPivot_Click(object sender, EventArgs e)
        {
            Pivot();
        }

        private void tsbPivotCCW_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            PivotCCW();
        }

        private void tsbSum_Click(object sender, EventArgs e)
        {
            Sum();
        }

        private void tsbPercent_Click(object sender, EventArgs e)
        {
            CalculatePercent(CalculatePerPartType.PerCent);
        }

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            CalculateValue(SumOperationType.Addition);
        }

        private void tsbSubstract_Click(object sender, EventArgs e)
        {
            CalculateValue(SumOperationType.Subtraction);
        }

        private void tsbMultiply_Click(object sender, EventArgs e)
        {
            CalculateValue(SumOperationType.Multiplication);
        }

        private void tsbDivide_Click(object sender, EventArgs e)
        {
            CalculateValue(SumOperationType.Division);
        }

        private void tsbStopRecording_Click(object sender, EventArgs e)
        {
            SaveQuery();
        }

        private void miSaveQuery_Click(object sender, EventArgs e)
        {
            SaveQuery();
        }

        private void tsbChangeSelection_Click(object sender, EventArgs e)
        {

        }

        #endregion

        public ToolStrip Toolstrip { get { return toolStrip1; } }
        public StatusStrip Statusstrip { get { return statusStrip1; } }
        public MenuStrip Menustrip { get { return menuStrip1; } }





        public string Title
        {
            get { return Filename; }
        }

        public string Id
        {
            get { return _id; }
        }

        public Control View
        {
            get { return this; }
        }

        public ToolStrip Toolbar
        {
            get { return Toolstrip; }
        }

        public StatusStrip Statusbar
        {
            get { return Statusstrip; }
        }

        void ILocalizable.SwitchLanguage(string language)
        {
            SwitchLanguage();
        }

        public MenuStrip Menubar
        {
            get { return Menustrip; }
        }


        public event EventHandler TitleChanged;


        public void Reload()
        {
        }


        public bool CanClose()
        {
            if (undoModels != null && undoModels.Count > 0)
            {

                if (!string.IsNullOrWhiteSpace(Id))
                {
                    _host.Show(Id);
                }

                var dialog = SaveChangesDialog.Show(this.Filename);

                if (dialog == DialogResult.OK)
                {
                    var saveResult = SaveAs();
                    if (saveResult != System.Windows.Forms.DialogResult.OK)
                    {
                        return false;
                    }
                }
                else if (dialog == DialogResult.Cancel)
                {
                    return false;
                }
            }

            return true;
            
        }

    }

    class MyDataGridView : DataGridView
    {
        /// <summary>
        /// Handle copying by Ctrl-C
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.C))
            {
                DataObject dataObj = this.GetClipboardContent();
                string txt = dataObj.GetText();
                string txt2 = RemoveClipboardFormatting(txt);

                DataObject o2 = new DataObject("Text", txt2);
                if (dataObj != null)
                    Clipboard.SetDataObject(o2);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// Remove formatting added to data cells so values can be calculated with in Excel
        /// </summary>
        /// <param name="inText"></param>
        /// <returns></returns>
        private string RemoveClipboardFormatting(string inText)
        {
            char[] valSep = { '\t' }; 
            string[] rows = inText.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries); // Split rows \r\n
            System.Text.StringBuilder outText = new System.Text.StringBuilder();

            foreach (string row in rows)
            {
                string[] values = row.Split(valSep); // Split values \t

                for (int i = 0; i < values.Length; i++)
                {
                    double val;

                    if (double.TryParse(values[i], out val))
                    {
                        outText.Append(val.ToString());
                    }
                    else
                    {
                        outText.Append(values[i]);
                    }

                    // Don´t add new column after the last value
                    if (i < (values.Length - 1))
                    {
                        outText.Append('\t');
                    }
                }

                outText.Append(Environment.NewLine.ToCharArray());
            }

            return outText.ToString();
        }


    }
}