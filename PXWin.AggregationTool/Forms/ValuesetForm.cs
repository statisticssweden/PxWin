using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IniParser;
using PX.Desktop.Interfaces;
using System.ComponentModel.Composition;
using PCAxis.Paxiom;
using IniParser;
using IniParser.Model;

namespace PXWin.AggregationTool
{
    public partial class ValuesetForm : UserControl, IToolWindow, ILocalizable, IToolbar, IStatusbar
    {
        [Import]
        private IHost _host;

        private bool _saveClicked = false;
        private bool _gridLoaded = false;
        private bool _switchingLanguage = false;
        private bool _dirty = false; // Has any value been changed?
        private string _loadedName = "";
        private string _loadedPrestext = "";
        private string _loadedDomain = "";
        private List<DataGridViewRow> _selectedRows = new List<DataGridViewRow>();

        public ValuesetForm()
        {
            InitializeComponent();
            txtType.Text = "V";
        }

        public string Path { get; set; }
        public ToolStrip Toolstrip { get { return toolStrip1; } }
        public StatusStrip Statusstrip { get { return statusStrip1; } }

        #region Import valuecode and valuetext
        private void tsbImport_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openDialog = new OpenFileDialog())
            {
                openDialog.Filter = "Valueset-file (*.vs)|*.vs|PX-file (*.px)|*.px|csv (*.csv)|*.csv";
                openDialog.FilterIndex = 0;

                if (openDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (openDialog.FileName.Length > 0)
                    {
                        statusStrip1.Text = System.IO.Path.GetFileName(openDialog.FileName);

                        switch (System.IO.Path.GetExtension(System.IO.Path.GetFileName(openDialog.FileName)))
                        {
                            case ".csv":
                                ImportFromCsv(openDialog.FileName);
                                CheckForDublicateCodes();
                                return;
                            case  ".px":
                                ImportFromPx(openDialog.FileName);
                                CheckForDublicateCodes();
                                return;
                            default:
                                ImportFromVs(openDialog.FileName);
                                CheckForDublicateCodes();
                                return;
                        }
                    }
                }
            }
            
        }

        /// <summary>
        /// Import valuecode and valuetext from .vs file
        /// </summary>
        /// <param name="filename">Name of the file that contains  valuescodes and valuetext </param>
        private void ImportFromVs(string filename)
        {
            var parser = new FileIniDataParser();
            //gridValues.Rows.Clear();
            //pnlAggFiles.Controls.Clear();

            try
            {
                var data = parser.ReadFile(filename);

                int counter = 1;
                if (data["Valuecode"] != null)
                {
                    while (!string.IsNullOrEmpty(data["Valuecode"][counter.ToString()]))
                    {
                        var row = (DataGridViewRow)gridValues.Rows[0].Clone();
                        row.Cells[0].Value = data["Valuecode"][counter.ToString()];
                        if (data["Valuetext"] != null)
                        {
                            row.Cells[1].Value = data["Valuetext"][counter.ToString()];
                        }
                        gridValues.Rows.Add(row);
                        counter++;
                    }
                }
                else 
                {
                    MessageBox.Show(_host.Language.GetString("toolValueSetCodesMissingMessage"),
                    _host.Language.GetString("toolValueSetErrorCaption"));
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(_host.Language.GetString("toolValueSetIllegalFileMessage") + "\n\n" + @"""" + ex.Message + @"""", _host.Language.GetString("toolValueSetIllegalFileCaption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }        
        /// <summary>
        /// Import valuecode and valuetext from .px file
        /// </summary>
        /// <param name="fileName">Name of the file that contains  valuescodes and valuetext</param>

        private void ImportFromPx(string fileName)
        {

            PXFileBuilder _builder;
            string _variable;
            var pxFileValuesDialog = new PxFileValuesDialog();
            _host.ComposeParts(pxFileValuesDialog);
            pxFileValuesDialog.StartPosition = FormStartPosition.CenterParent;
            pxFileValuesDialog.FileName = fileName;

            if (pxFileValuesDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _builder = pxFileValuesDialog.Builder;
                    _variable = pxFileValuesDialog.ChoosenVariable;
                    foreach (var variable in _builder.Model.Meta.Variables)
                    {
                        if (variable.Name == _variable)
                        {
                            foreach (var value in variable.Values)
                            {
                                DataGridViewRow row = (DataGridViewRow)gridValues.Rows[0].Clone();
                                row.Cells[0].Value = value.Code;
                                row.Cells[1].Value = value;
                                gridValues.Rows.Add(row);
                            }
                            break;

                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(_host.Language.GetString("toolValueSetIllegalFileMessage") + "\n\n" + @"""" + ex.Message + @"""", _host.Language.GetString("toolValueSetIllegalFileCaption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

            }
        }         
           

        
        /// <summary>
        /// Import valuecode and valuetext from .csv file
        /// </summary>
        /// <param name="fileName">Name of the file that contains  valuescodes and valuetext</param>
        private void ImportFromCsv(string fileName)
        {
            var dialogDelimiterCsvDialog = new DelimiterCsvDialog(System.IO.Path.GetFileName(fileName));
            _host.ComposeParts(dialogDelimiterCsvDialog);

            dialogDelimiterCsvDialog.StartPosition = FormStartPosition.CenterParent;
            if (dialogDelimiterCsvDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {

                    using (var sr = new StreamReader(fileName, System.Text.Encoding.Default))
                    {
                        string s;
                        int counter = 1;
                        while ((s = sr.ReadLine()) != null)
                        {
                            var textRow = s.Split((char)dialogDelimiterCsvDialog.Delimiter);
                            DataGridViewRow row = (DataGridViewRow)gridValues.Rows[0].Clone();
                            row.DefaultCellStyle.BackColor = Color.White;
                            string codeValue = textRow[0];
                            string textValue = textRow[1];

                            //Remove " if it´s exist in start and end of the string
                            if (codeValue.TrimStart().StartsWith("\"") && codeValue.TrimEnd().EndsWith("\""))
                            {
                                codeValue = codeValue.Trim('"');
                            }
                            if (textValue.TrimStart().StartsWith("\"") && textValue.TrimEnd().EndsWith("\""))
                            {
                                textValue = textValue.Trim('"');
                            }
                            row.Cells[0].Value =  codeValue;
                            row.Cells[1].Value = textValue;
 
                            gridValues.Rows.Add(row);
                                
                            counter++;
                               
                        }
                    }
                        
                }
                catch (Exception ex)
                {
                    MessageBox.Show(_host.Language.GetString("toolValueSetErrorReadingCSVfileMessage") + "\n\n" + @"""" + ex.Message + @"""", _host.Language.GetString("toolValueSetIllegalFileCaption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            return;
        }
        #endregion
        /// <summary>
        /// Load valuesetfile .vs into the grid
        /// </summary>
        /// <param name="filename">Name of the valuesetfile that the user wants to open</param>
        public void LoadData(string filename)
        {
            var parser = new FileIniDataParser();
            gridValues.Rows.Clear();
            pnlAggFiles.Controls.Clear();

            try
            {
                var data = parser.ReadFile(filename);
                //fill the txt-fileds from the file.
                txtName.Text = data["Descr"]["Name"];
                _loadedName = data["Descr"]["Name"];

                txtPrestext.Text = data["Descr"]["Prestext"];
                _loadedPrestext = data["Descr"]["Prestext"];

                txtDomain.Text = data["Domain"]["1"];
                _loadedDomain = data["Domain"]["1"];

                txtType.Text = data["Descr"]["Type"];

                int counter = 1;
                if (data["Valuecode"] != null)
                {
                    while (!string.IsNullOrEmpty(data["Valuecode"][counter.ToString()]))
                    {
                        var row = (DataGridViewRow)gridValues.Rows[0].Clone();
                        row.Cells[0].Value = data["Valuecode"][counter.ToString()];
                        if (data["Valuetext"] != null)
                        {
                            row.Cells[1].Value = data["Valuetext"][counter.ToString()];
                        }
                        gridValues.Rows.Add(row);
                        counter++;
                    }
                }
                else
                {

                    MessageBox.Show(_host.Language.GetString("toolValueSetCodesMissingMessage"),
                    _host.Language.GetString("toolValueSetErrorCaption"));
                        return;
                }
                //Create links to aggregation files
                LinkLabel lnk;
                counter = 1;
                if (data["Aggreg"] != null)
                {
                    while (!string.IsNullOrEmpty(data["Aggreg"][counter.ToString()]))
                    {
                        lnk = new LinkLabel();
                        lnk.Width = 400;
                        lnk.Text = data["Aggreg"][counter.ToString()];
                        //TODO: fel kastas här. Path är null. agg filer inte i samma katalog
                        if (!System.IO.File.Exists(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Path), lnk.Text)))
                        {
                            lnk.Enabled = false;
                        }

                        pnlAggFiles.Controls.Add(lnk);
                        lnk.LinkClicked += AggFile_LinkClicked;
                        counter++;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(_host.Language.GetString("toolValueSetIllegalFileMessage") + "\n\n" + @"""" + ex.Message + @"""", _host.Language.GetString("toolValueSetIllegalFileCaption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            sslFileName.Text = filename;
            Title = System.IO.Path.GetFileName(filename);
            btnNewAggregation.Enabled = true;
            _gridLoaded = true;
        }

        //Save value set to file
        private void tsbSave_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        public DialogResult SaveAs()
        {
            if (CheckForDublicateCodes())
            {
                MessageBox.Show(_host.Language.GetString("toolValueSetDublicateCodesMessage"),
                                _host.Language.GetString("toolValueSetDublicateCodesCaption"));
                return DialogResult.Cancel;
            }
            if (txtName.Text.Length == 0)
            {
                MessageBox.Show(_host.Language.GetString("toolValueSetEnterValueSetNameMessage"),
                                _host.Language.GetString("toolValueSetEnterValueSetCaption"));
                return DialogResult.Cancel;
            }
            if (txtDomain.Text.Length == 0)
            {
                MessageBox.Show(_host.Language.GetString("toolValueSetEnterValueSetDomainMessage"),
                                _host.Language.GetString("toolValueSetEnterValueSetCaption"));
                return DialogResult.Cancel;
            }

            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.AddExtension = true;
                dlg.FileName = txtName.Text;
                dlg.Filter = "Valueset-file (*.vs)|*.vs";

                var res = dlg.ShowDialog();
                if (res != DialogResult.OK) return res;

                SaveVsFile(dlg.FileName);
                _saveClicked = true;

                return DialogResult.OK;
            }
        }

        private void SaveVsFile(string filename)
        {
            if (txtName.Text != System.IO.Path.GetFileNameWithoutExtension(filename))
            {
                if (MessageBox.Show(_host.Language.GetString("toolValueSetFileNameAndNameMessage"),
                                    _host.Language.GetString("toolValueSetFileNameCaption"),
                                    MessageBoxButtons.OKCancel) != DialogResult.OK)
                {
                    return;
                }

            }
            try
            {
                var data = new IniParser.Model.IniData();

                //Description section
                var valueSetSelection = new IniParser.Model.SectionData("Descr");


                // Name
                AddSectionValue(valueSetSelection, "Name", txtName.Text);
                _loadedName = txtName.Text;

                // Prestext
                if (txtPrestext.Text != "")
                {
                    AddSectionValue(valueSetSelection, "Prestext", txtPrestext.Text);
                }
                _loadedPrestext = txtPrestext.Text;

                //Type
                if (txtType.Text != "")
                {
                    AddSectionValue(valueSetSelection, "Type", txtType.Text);
                }

                data.Sections.Add(valueSetSelection);

                //Aggregation fils section
                SectionData valueSetAggregationSelection = new IniParser.Model.SectionData("Aggreg");

                List<LinkLabel> aggFiles = pnlAggFiles.Controls.OfType<LinkLabel>().ToList();

                int i = 1;
                foreach (var item in aggFiles)
                {
                    AddSectionValue(valueSetAggregationSelection, i.ToString(), item.Text);
                    i++;
                }

                data.Sections.Add(valueSetAggregationSelection);

                //Domain section
                var valueSetDomainSelection = new IniParser.Model.SectionData("Domain");

                //Domain
                AddSectionValue(valueSetDomainSelection, "1", txtDomain.Text);
                _loadedDomain = txtDomain.Text;

                data.Sections.Add(valueSetDomainSelection);

                //Value code section
                var valueCodeSection = new IniParser.Model.SectionData("Valuecode");

                i = 1;
                for (int y = 0; y < gridValues.Rows.Count - 1; y++)
                {
                    var k = gridValues.Rows[y].Cells[0].Value;
                    AddSectionValue(valueCodeSection, i.ToString(), gridValues.Rows[y].Cells[0].Value.ToString());
                    i++;
                }

                data.Sections.Add(valueCodeSection);

                //Value text section
                var valueTextSection = new IniParser.Model.SectionData("Valuetext");

                i = 1;
                for (int y = 0; y < gridValues.Rows.Count - 1; y++)
                {
                    if (gridValues.Rows[y].Cells[1].Value != null)
                    {
                        AddSectionValue(valueTextSection, i.ToString(), gridValues.Rows[y].Cells[1].Value.ToString());
                        i++;
                    }

                }

                data.Sections.Add(valueTextSection);

                var parser = new FileIniDataParser();
                var config = new IniParser.Model.Configuration.IniParserConfiguration { AssigmentSpacer = "" };
                var formatter = new IniParser.Model.Formatting.DefaultIniDataFormatter(config);

                using (var stream = new System.IO.StreamWriter(filename, false, System.Text.Encoding.Default))
                {
                    parser.WriteData(stream, data, formatter);
                }

                //parser.WriteFile(filename, data, System.Text.Encoding.Default);
                sslFileName.Text = filename;
                Title = System.IO.Path.GetFileName(filename);
                Path = filename;
                btnNewAggregation.Enabled = true;
                _dirty = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(_host.Language.GetString("toolValueSetSaveFileMessage") + "\n\n" + @"""" + ex.Message + @"""", _host.Language.GetString("toolValueSetSaveFileCaption") , MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void AddSectionValue(SectionData section, string name, string value)
        {
            KeyData key = new KeyData(name);
            key.Value = value;
            section.Keys.AddKey(key);
        }



        //Class used for checking for dublicates in data grid
        private class Duplicates
        {
            public string Code { get; set; }
            public DataGridViewRow rowObject { get; set; }
        }


        /// <summary>
        /// Check if code allredy exist in datagrid.
        /// </summary>
        private bool CheckForDublicateCodes()
        {
            lblContainsDuplicates.Visible = false;
            bool exist = false;
            int countItems = 0;

            var okCodes = new Dictionary<string, DataGridViewRow>();
            var duplicatesCodes = new List<Duplicates>();
            try
            { 
                for (int i = 0; i < gridValues.Rows.Count; i++)
                {
                    if (gridValues.Rows[i].Cells[0].Value != null && gridValues.Rows[i].Cells[0].Value.ToString() != String.Empty)
                    {
                        gridValues.Rows[i].DefaultCellStyle.BackColor = Color.White;

                        if (!okCodes.ContainsKey(gridValues.Rows[i].Cells[0].Value.ToString().ToLower()))
                            okCodes.Add(gridValues.Rows[i].Cells[0].Value.ToString(), gridValues.Rows[i]);
                        else
                        {
                            duplicatesCodes.Add(new Duplicates() { Code = gridValues.Rows[i].Cells[0].Value.ToString(), rowObject = gridValues.Rows[i] });
                        }
                    }

                }

                foreach (var item in duplicatesCodes)
                {
                    item.rowObject.DefaultCellStyle.BackColor = Color.LightPink; 

                    if (okCodes.ContainsKey(item.Code))
                    {
                        okCodes[item.Code].DefaultCellStyle.BackColor = Color.LightPink;
                    }
                }            
            }
            catch (Exception ex)
            {
                MessageBox.Show(_host.Language.GetString("toolValueSetErrorDublicateCodesMessage") + "\n\n" + ex.Message, _host.Language.GetString("toolValueSetErrorCaption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }

            if (duplicatesCodes.Count > 0)
            {
                lblContainsDuplicates.Visible = true;
            }

            return duplicatesCodes.Count > 0;

        }


        private Rectangle dragBoxFromMouseDown;
        private int rowIndexFromMouseDown;
        private int rowIndexOfItemUnderMouseToDrop;
        private bool doTheCheckForDublicateCodes = true;

        /// <summary>
        /// If a row is deleted check if a duplicate value is removed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridValues_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if (doTheCheckForDublicateCodes)
                CheckForDublicateCodes();

            if (_gridLoaded && !_switchingLanguage) 
            {
                _dirty = true;
            }
        }
        /// <summary>
        /// If a row is deleted check if a duplicate value is removed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridValues_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (gridValues.Rows[0].Cells[0].Value != null)
            {
                if (doTheCheckForDublicateCodes)
                    CheckForDublicateCodes();
            }

            if (_gridLoaded && !_switchingLanguage)
            {
                _dirty = true;
            }
        }

        private void gridValues_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                // If the mouse moves outside the rectangle, start the drag.
                if (dragBoxFromMouseDown != Rectangle.Empty &&
                !dragBoxFromMouseDown.Contains(e.X, e.Y))
                {
                    // Proceed with the drag and drop, passing in the list item.                    
                    DragDropEffects dropEffect = gridValues.DoDragDrop(
                          gridValues.Rows[rowIndexFromMouseDown],
                          DragDropEffects.Move);
                }
            }
        }

        private void gridValues_MouseDown(object sender, MouseEventArgs e)
        {
            // Get the index of the item the mouse is below.
            rowIndexFromMouseDown = gridValues.HitTest(e.X, e.Y).RowIndex;

            if (rowIndexFromMouseDown != -1)
            {
                // Remember the point where the mouse down occurred. 
                // The DragSize indicates the size that the mouse can move 
                // before a drag event should be started.                
                Size dragSize = SystemInformation.DragSize;

                // Create a rectangle using the DragSize, with the mouse position being
                // at the center of the rectangle.
                dragBoxFromMouseDown = new Rectangle(
                          new Point(
                            e.X - (dragSize.Width / 2),
                            e.Y - (dragSize.Height / 2)),
                      dragSize);
            }
            else
                // Reset the rectangle if the mouse is not over an item in the ListBox.
                dragBoxFromMouseDown = Rectangle.Empty;

        }

        private void gridValues_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void gridValues_DragDrop(object sender, DragEventArgs e)
        {
            // The mouse locations are relative to the screen, so they must be 
            // converted to client coordinates.
            Point clientPoint = gridValues.PointToClient(new Point(e.X, e.Y));

            // Get the row index of the item the mouse is below. 
            rowIndexOfItemUnderMouseToDrop = gridValues.HitTest(clientPoint.X, clientPoint.Y).RowIndex;

            // If the drag operation was a move then remove and insert the row.
            if (e.Effect == DragDropEffects.Move)
            {
                var rowToMove = e.Data.GetData(typeof(DataGridViewRow)) as DataGridViewRow;
                gridValues.Rows.RemoveAt(rowIndexFromMouseDown);
                gridValues.Rows.Insert(rowIndexOfItemUnderMouseToDrop, rowToMove);

            }
        }


        private int _ctxRowIndex =  -1;

        private void gridValues_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right  )
            {
                _ctxRowIndex = e.RowIndex;
                
                tsCut.Enabled = gridValues.Rows[_ctxRowIndex].Selected;
                tsPaste.Enabled = _selectedRows.Count > 0;
                if (gridValues.Rows[_ctxRowIndex].ReadOnly )
                {
                    tsPaste.Enabled = false;
                }
                //It shall not be possible to paste in a row on the last editable row in the grid
                //or cut this last row out
                if (_ctxRowIndex == gridValues.Rows.Count - 1)
                {
                    tsPaste.Enabled = false;
                    tsCut.Enabled = false;
                }
            }
        }

        private void tsInsertEmpty_Click(object sender, EventArgs e)
        {
            if (_ctxRowIndex == -1)
            {
                return;
            }

            gridValues.Rows.Insert(_ctxRowIndex, "", "");
        }

        private void tsCut_Click(object sender, EventArgs e)
        {
            // Remove any already cut rows that have not been pasted
            foreach (DataGridViewRow row in _selectedRows)
            {
                EnableRow(row);
            }
            _selectedRows.Clear();

            foreach (DataGridViewRow row in gridValues.Rows)
            {
                if (row.Selected)
                {
                    DisableRow(row);
                    _selectedRows.Add(row);
                }
            }
            gridValues.ClearSelection();
            
        }

        private void DisableRow(DataGridViewRow row)
        {
            row.ReadOnly = true;
            row.Selected = false;
            foreach (DataGridViewCell cell in row.Cells)
            {
                cell.Style.ForeColor = Color.LightGray;
            }
        }
        private void EnableRow(DataGridViewRow row)
        {
            row.ReadOnly = false;
            foreach (DataGridViewCell cell in row.Cells)
            {
                cell.Style.ForeColor = Color.Black;
            }
            
        }

        private void tsPaste_Click(object sender, EventArgs e)
        {
            doTheCheckForDublicateCodes = false;
            if (_ctxRowIndex == -1)
            {
                return;
            }

            foreach (DataGridViewRow row in _selectedRows)
            {
                gridValues.Rows.RemoveAt(row.Index);
                gridValues.Rows.Insert(_ctxRowIndex, row);
                
                EnableRow(row);
                row.Selected = true;
            }

            _selectedRows.Clear();

            gridValues.CurrentCell.Selected = false;
            
            CheckForDublicateCodes();
            doTheCheckForDublicateCodes = true;
            
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                if (string.Compare(_title, value) != 0)
                {
                    _title = value;
                    OnTitleChanged(EventArgs.Empty);
                }
            }
        }

        public string Id
        {
            get { return Path; }
        }

        public Control View
        {
            get { return this; }
        }

        public event EventHandler TitleChanged;

        protected virtual void OnTitleChanged(EventArgs e)
        {
            EventHandler handler = TitleChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public void SwitchLanguage(string language)
        {
            _switchingLanguage = true;

            if (string.IsNullOrEmpty(Path))
            {
                Title = _host.Language.GetString("toolAggNewValueset");
            }
            tsbSave.Text = _host.Language.GetString("toolValueSetSaveFile"); // "Save valueset file";
            tsbImport.Text = _host.Language.GetString("toolValueSetImportFile"); //"Import values from file";
            lblName.Text = _host.Language.GetString("toolValueSetName"); ;
            lblPrestext.Text = _host.Language.GetString("toolValueSetPrestext"); 
            lblDomain.Text = _host.Language.GetString("toolValueSetDomain"); 
            lblType.Text = _host.Language.GetString("toolValueSetType");
            this.tabPage1.Text = _host.Language.GetString("toolValueSetTabValues");
            this.tabPage2.Text = _host.Language.GetString("toolValueSetTabAggFiles");
            this.Code.HeaderText = _host.Language.GetString("toolValueSetColumnCode");
            this.Text.HeaderText = _host.Language.GetString("toolValueSetColumnText");
            btnNewAggregation.Text = _host.Language.GetString("toolValueSetCreateNewAggregationFile");
            lblContainsDuplicates.Text = _host.Language.GetString("toolValueSetMessDublicateCodes");

            _switchingLanguage = false;
        }

        public ToolStrip Toolbar
        {
            get { return toolStrip1; }
        }

        public StatusStrip Statusbar
        {
            get { return statusStrip1; }
        }

        private void ValuesetForm_Load(object sender, EventArgs e)
        {
            SwitchLanguage(_host.Language.CurrentLanguage);
            if (Path != null)
            {
                _host.RegisterFileNotifier(Path, Id);
            }
        }

        private void btnNewAggregation_Click(object sender, EventArgs e)
        {
            var dlg = new AggregationForm();
            dlg.LoadVsData(Path);
            _host.ComposeParts(dlg);
            _host.Show(dlg);
        }

        private void AggFile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (sender is LinkLabel)
            {
                var label = sender as LinkLabel;
                var param = new Dictionary<string, string>();
                param.Add("Valueset", Path);
                _host.Show(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Path), label.Text), param);
            }
        }

        public void Reload()
        {
            if (_saveClicked)
            {
                _saveClicked = false;
                return;
            }

            if (MessageBox.Show(string.Format(_host.Language.GetString("toolAggFileChangedMessage"), System.IO.Path.GetFileName(Path)), _host.Language.GetString("toolAggFileChangedCaption"), MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                _gridLoaded = false;
                LoadData(Path);
            }
        }


        public bool CanClose()
        {
            if (AggToolUtils.ValueIsChanged(_loadedName, txtName.Text) || AggToolUtils.ValueIsChanged(_loadedPrestext, txtPrestext.Text) || AggToolUtils.ValueIsChanged(_loadedDomain, txtDomain.Text))
            {
                _dirty = true;
            }

            if (_dirty)
            {
                if (!string.IsNullOrEmpty(Path))
                {
                    _host.Show(Path);
                }

                SaveChangesDialog dialog = new SaveChangesDialog(System.IO.Path.GetFileName(Path));
                _host.ComposeParts(dialog);

                DialogResult res = dialog.ShowDialog();

                if (res == DialogResult.OK)
                {
                    var saveResult = SaveAs();
                    if (saveResult != System.Windows.Forms.DialogResult.OK)
                    {
                        return false;
                    }
                }
                else if (res == DialogResult.Cancel)
                {
                    return false;
                }
            }

            return true;
        }


    }
}
