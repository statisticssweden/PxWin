using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PCAxis.Paxiom;
using PCAxis.Query;
using PCAxis.Desktop.SavedQuery;
using System.Threading.Tasks;
using PCAxis.Sql.Pxs;
using PX.Plugin.Interfaces.Attributes;

namespace PCAxis.Desktop
{
    [Export()]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class BatchForm : Form
    {
        /// <summary>
        /// List av all available serializers
        /// </summary>
        [ImportMany(AllowRecomposition = true)]
        private IEnumerable<Lazy<Func<IPXModelStreamSerializer>, ISerializerMetadata>> _saveAsFormats;
        private const char DELIMITER = '|';
        private BatchQuery _batchQuery;

        public BatchForm()
        {
            InitializeComponent();
            SetLanguage();
            _batchQuery = new BatchQuery();
        }

        private void SetLanguage()
        {
            Text = Lang.GetLocalizedString("Batch");
            lblSaveToBat.Text = Lang.GetLocalizedString("BatchSaveToBatLabel");
            btnOpenSq.Text = Lang.GetLocalizedString("Open");
            btnSaveToPxt.Text = Lang.GetLocalizedString("BatchSaveToPxtButton");
            btnSaveToBat.Text = Lang.GetLocalizedString("BatchSaveToBatButton");
            lblDestinationFolder.Text = Lang.GetLocalizedString("BatchDestinationFolder");
            lblFileFormats.Text = Lang.GetLocalizedString("BatchFileFormat");
            btnRun.Text = Lang.GetLocalizedString("BatchRun");
            btnClose.Text = Lang.GetLocalizedString("BatchClose");
            btnBrowse.Text = Lang.GetLocalizedString("SavedQueryBrowse");
            btnBrowsePxt.Text = Lang.GetLocalizedString("SavedQueryBrowse");
            lblOpen.Text = Lang.GetLocalizedString("BatchOpen");
            lblAction.Text = Lang.GetLocalizedString("BatchAction");

            this.toolTipRemove.SetToolTip(btnRemove, Lang.GetLocalizedString("BatchRemove"));

            openPxtDialog.Filter = Lang.GetLocalizedString("BatchSavedQueryList") + " (*.pxt)|*.pxt"; //New Translate SCBMASV
            openPxtDialog.FileName = "";
            saveBatDialog.Filter = Lang.GetLocalizedString("BatchBatFile") + " (*.bat)|*.bat"; //New Translate SCBMASV

            tabPage2.Text = Lang.GetLocalizedString("BatchTabCreateBatFile"); //New Translate SCBMASV
            tabPage1.Text = Lang.GetLocalizedString("BatchTabBatch"); //New Translate SCBMASV
            openSqDialog.Filter = Lang.GetLocalizedString("BatchSavedQueryDialog") + " (*.pxsq, *.pxt)|*.pxsq; *.pxt"; //New Translate SCBMASV

        }


        private void UpdateGUIValues()
        {
            //var strFormat = _batchQuery.Format;
            if (!string.IsNullOrEmpty(_batchQuery.Format))
            {
            //    //comboFormat.SelectedIndex = SaveHandler.GetFileFormatTexts().IndexOf(_batchQuery.Format);
                for (int i = 0; i < comboFormat.Items.Count; i++)
                {
                    FileFormat f = (FileFormat)comboFormat.Items[i];
                    if (f.Value.Equals(_batchQuery.Format))
                    {
                        comboFormat.SelectedIndex = i;
                        break;
                    }
                }
            }

            if (!string.IsNullOrEmpty(_batchQuery.OutputPath) || string.IsNullOrWhiteSpace(tbOutputDirectory.Text))
            {
                tbOutputDirectory.Text = _batchQuery.OutputPath;
            }

            ClearListView();
            
            foreach (var file in _batchQuery.PxsqFiles)
            {
                ListViewItem itm;
                Button btn = new Button();

                itm = lvSavedQueries.Items.Add(file);
                itm.SubItems.Add(Lang.GetLocalizedString("BatchNotRun"));
            }

            btnRemove.Enabled = lvSavedQueries.Items.Count > 0;

            if ((lvSavedQueries.Items.Count > 0) && (tbOutputDirectory.Text.Length > 0))
            {
                btnRun.Enabled = true;
                btnSaveToPxt.Enabled = true;
            }
            else
            {
                btnRun.Enabled = false;
                btnSaveToPxt.Enabled = false;
            }
        }
        
        private void UpdateObjectValues()
        {
            _batchQuery.OutputPath = tbOutputDirectory.Text;
            _batchQuery.Format = (comboFormat.SelectedItem as FileFormat).Value;
        }

        private void ClearListView()
        {
            for (var i = 0; i < lvSavedQueries.Items.Count; i++)
            {
                lvSavedQueries.Items[i].SubItems.Clear();
            }
            lvSavedQueries.Items.Clear();
        }

        public void InitializeControlsOnForm()
        {
            InitializeControls();
        }

        private void InitializeControls()
        {
            comboFormat.DisplayMember = "Text";
            comboFormat.ValueMember = "Value";
            
            var fileTypes = (from f in _saveAsFormats
                                  select (Lang.GetLocalizedString(f.Metadata.Id) + DELIMITER + f.Metadata.Id)).ToArray();


            foreach (var item in fileTypes)
            {
                comboFormat.Items.Add((new FileFormat() { Text = item.Split(DELIMITER)[0], Value = item.Split(DELIMITER)[1] }));
            }
            
            comboFormat.SelectedIndex = 0;

            lvSavedQueries.Columns.Add(Lang.GetLocalizedString(Lang.GetLocalizedString("BatchSavedQueries")), 370);
            lvSavedQueries.Columns.Add(Lang.GetLocalizedString(Lang.GetLocalizedString("BatchStatus")), 120);

            btnRun.Enabled = false;
            btnSaveToPxt.Enabled = false;
            btnRemove.Enabled = false;
            btnSaveToBat.Enabled = false;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                tbOutputDirectory.Text = folderBrowserDialog.SelectedPath;
                UpdateGUIValues();
            }
        }

        private void btnOpenSq_Click(object sender, EventArgs e)
        {
            
            if (openSqDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (var file in openSqDialog.FileNames)
                {
                    if (Path.GetExtension(file).Equals(".pxt"))
                    {
                        _batchQuery.AddFromPxt(file);
                    }
                    else //.pxsq
                    {
                        _batchQuery.PxsqFiles.Add(file);
                        //var sq = SavedQueryManager.Current.Load(file);
                        //var lvItem = new ListViewItem(Path.GetFileNameWithoutExtension(file)) { Tag = sq, Checked = true };
                        //lvSavedQueries.Items.Add(lvItem).SubItems.Add(new ListViewItem.ListViewSubItem(lvItem, "Ej körd"));
                    }
                }
                UpdateGUIValues();
                comboFormat.Enabled = true;
                btnBrowse.Enabled = true;
            }                
        }

        private async void btnRun_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbOutputDirectory.Text))
            {
                MessageBox.Show(Lang.GetLocalizedString("BatchMissingDestinationText"), Lang.GetLocalizedString("BatchMissingDestination"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            try
            {
                btnRun.Enabled = false;
                for (var i = 0; i < lvSavedQueries.Items.Count; i++)
                {
                     lvSavedQueries.Items[i].SubItems[1].Text = Lang.GetLocalizedString("BatchWorking");//.RemoveAt(1);                                      
                }

                UpdateObjectValues();
                var result = await RunBatchAsync();

                btnRun.Enabled = true;
                
            }
            catch (Exception ex)
            {
                
            }
            
        }

        private async Task<bool> RunBatchAsync()
        {
            try
            {
                await Task.Run(() => {_batchQuery.Run(UpdateRunStatus); });
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void UpdateRunStatus(string pxsqFile, bool isOk, string errorMessage)
        {
            Action del = delegate
            {
                UpdateListView(pxsqFile, isOk, errorMessage);
            };
            Invoke(del);
        }

        private void UpdateListView(string pxsqFile, bool isOk, string errorMessage)
        {
            for (int i = 0; i < lvSavedQueries.Items.Count; i++)
            {
                if (pxsqFile.Equals(lvSavedQueries.Items[i].SubItems[0].Text, StringComparison.InvariantCultureIgnoreCase)) 
                {
                    lvSavedQueries.Items[i].SubItems[1].Text = isOk ? Lang.GetLocalizedString("Ok") : Lang.GetLocalizedString("BatchNotOk");
                }
            }
        }

        private void btnSaveToPxt_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbOutputDirectory.Text))
            {
                MessageBox.Show(Lang.GetLocalizedString("BatchMissingDestinationText"), Lang.GetLocalizedString("BatchMissingDestination"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            SaveFileDialog dlgSave = new SaveFileDialog
            {
                Filter = "PX batch file (*.pxt)| *.pxt",
                FilterIndex = 1,
                RestoreDirectory = true
            };

            if (dlgSave.ShowDialog() == DialogResult.OK)
            {
                _batchQuery.OutputPath = tbOutputDirectory.Text;
                _batchQuery.Format = (comboFormat.SelectedItem as FileFormat).Value;
                _batchQuery.Save(dlgSave.FileName);
            }
        }       

        private void btnRemoveAll_Click(object sender, EventArgs e)
        {
            _batchQuery.PxsqFiles.Clear();
            UpdateGUIValues();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lvSavedQueries.Items)
            {
                if (item.Selected)
                {
                    var hej = _batchQuery.PxsqFiles.Remove(item.Text);
                }
            }
            UpdateGUIValues();
        }


        #region "Create BAT tab"

        private void btnBrowsePxt_Click(object sender, EventArgs e)
        {
            openPxtDialog.Multiselect = false;

            if (openPxtDialog.ShowDialog() == DialogResult.OK)
            {
                if (openPxtDialog.FileName.Length > 0)
                {
                    tbBrowsePxt.Text = openPxtDialog.FileName;
                    btnSaveToBat.Enabled = true;
                }
            }                
        }

        private void btnSaveToBat_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            string batchPath = System.Configuration.ConfigurationManager.AppSettings.Get("batchApplicationPath");

            if (string.IsNullOrWhiteSpace(batchPath))
            {
                MessageBox.Show("Batch application path has not been set", "Configuration needed");
                return;
            }
            
            if ((saveBatDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) && (saveBatDialog.FileName != "") && (tbBrowsePxt.Text != ""))
            {
                sb.Append(batchPath);
                sb.Append(" ");
                sb.Append(tbBrowsePxt.Text);
                System.IO.File.WriteAllText(saveBatDialog.FileName, sb.ToString());
            }
        }

        #endregion
    }

    public class FileFormat
    {
        public string Text { get; set; }
        public string Value { get; set; }
    }
}
