using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Primitives;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using PCAxis.Desktop.OperationDialogs;
using PCAxis.Desktop.Serializers;
using PCAxis.Excel;
using PCAxis.Paxiom;
using PCAxis.Query;
using PCAxis.Sql.DbConfig;
using PCAxis.Paxiom.Operations;
using WeifenLuo.WinFormsUI.Docking;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using PX.Plugin.Interfaces.Attributes;
using PX.Desktop.Interfaces;
using PX.Desktop.Interfaces.Attributes;
using PX.Plugin.Interfaces;
using PCAxis.Desktop.Events;

namespace PCAxis.Desktop
{
    [Export(typeof(IHost))]
    public partial class MainForm : Form, IHost
    {
        private bool SelectedChildIsTable()
        {
            if (ActiveMdiChild == null)
            {
                return false;
            }

            return ActiveMdiChild is GridModelForm;
        }

        [Import]
        private OpenTableForm _openDialog;

        [ImportMany(typeof(IToolsMenu))]
        private List<IToolsMenu> _toolsMenu;

        [Import]
        public ILanguageService Language
        {
            get;
            set;
        }

        [ImportMany(AllowRecomposition = true)]
        private IEnumerable<Lazy<Func<string,Dictionary<string,string>, IToolWindow>, IFileAssociationMetadata>> _fileAssociations;

        //[ImportMany(AllowRecomposition = true)]
        //private IEnumerable<Lazy<Func<IPXModelStreamSerializer>, ISerializerMetadata>> _saveAsFormats;
        //[ImportMany(AllowRecomposition = true)]
        //private IEnumerable<Lazy<IDataSource, IDataSourceMetadata>> _dataSources;

        public MainForm()
        {
            //Compose();
            InitializeComponent();
            

            DatabaseRepository.Current.GetDatabase("");

            SetRoundingRule();
            PCAxis.Paxiom.GroupRegistry.GetRegistry().Strict = UseStrictAggregationCheck();
            PCAxis.Paxiom.GroupRegistry.GetRegistry().LoadGroupingsAsync();

            DragEnter += MainForm_DragEnter;
            DragDrop += MainForm_DragDrop;
        }


        /// <summary>
        /// Sets the rounding rule that should be used by PxWin
        /// </summary>
        private void SetRoundingRule()
        {
            string roundingRule = string.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("roundingRule")) ? "RoundUp" : ConfigurationManager.AppSettings.Get("roundingRule");

            if (roundingRule.ToLower().Equals("bankersrounding"))
            {
                PCAxis.Paxiom.Settings.Numbers.RoundingRule = MidpointRounding.ToEven;
            }
            else
            {
                PCAxis.Paxiom.Settings.Numbers.RoundingRule = MidpointRounding.AwayFromZero;
            }
        }
        
        /// <summary>
        /// Checks if strict aggregation check shall be used or not
        /// </summary>
        /// <returns></returns>
        private bool UseStrictAggregationCheck()
        {
            string strStrict = System.Configuration.ConfigurationManager.AppSettings.Get("strictAggregationCheck");
            bool strict;
            if (strStrict == null)
            {
                return false;
            }

            if (!bool.TryParse(strStrict, out strict))
            {
                return false;
            }
            else
            {
                return strict;
            }
        }

        public void Init(StartOptions options) 
        {
            MEFBooter.Container.GetExportedValue<MEFPlumber>().RegisterSavedQueryDependencies();
            
            if (options.IsPxFile)
            {
                try
                {
                    var builder = new PCAxis.Paxiom.PXFileBuilder();
                    builder.SetPath(options.Files[0]);
                    SelectValues(DatabaseRepository.Current.GetDatabase(null), builder);
                }
                catch (PXException ex)
                {
                    MessageBox.Show(ex.Message);                    
                }
                
            }
            else if (options.IsPxsqFile)
            { 
                //TODO Run PXSQ file workflow
                LoadSavedQuery(options.Files[0]);
            }
            else if (options.IsPxtFile)
            {
                //TODO Open batch dialog and populat information from the pxt file.
            }
            if (!(string.IsNullOrEmpty(options.Database) || string.IsNullOrEmpty(options.Table)))
            { 
                //TODO select the right table and database
                DatabaseInfo dbInfo = DatabaseRepository.Current.GetDatabase(options.Database);
                SelectValues(dbInfo, dbInfo.CreateBuilder(options.Table));
            }
            else if (!string.IsNullOrEmpty(options.Database))
            { 
                //TODO select the right database
                _openDialog.SelectDatabase(options.Database);
            }

            _openDialog.OpenPXTable += OpenPXTable;

            _openDialog.Text = Lang.GetLocalizedString("MenuOpenTable");

            _openDialog.Show(dockPanel);

            tslVersion.Text = AboutDialog.Version;

            RegenerateLanguages();
        }



 

        /// <summary>
        /// A PX table shall be opened
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenPXTable(object sender, OpenPXTableEventArgs e)
        {
            SelectValues(e.DbInfo, e.Builder, e.PreviousTableQuery);
        }

        /// <summary>
        /// Select variables and values
        /// </summary>
        /// <param name="builder"></param>
        private void SelectValues(DatabaseInfo dbInfo, IPXModelBuilder builder, TableQuery previousTableQuery = null)
        {
            if (!HasAccessRights(dbInfo, builder))
            {
                MessageBox.Show(Lang.GetLocalizedString("AccessDeniedText"), Lang.GetLocalizedString("AccessDeniedHeading"));
                return;
            }

            PCAxis.Paxiom.Selection[] selection = null;
            bool ok = false;

            Cursor.Current = Cursors.WaitCursor;
            //builder.SetPreferredLanguage(Thread.CurrentThread.CurrentCulture.ToString());
            builder.SetPreferredLanguage(LanguageHelper.GetTableLanguage(dbInfo));
            if (!builder.BuildForSelection())
            {
                MessageBox.Show("Error when building model", "Builder error");
                return;
            }

            if (builder.Errors.Count > 0)
            {
                ShowBuilderErrors(builder.Errors, Lang.GetLocalizedString("FatalErrors"));
                return;
            }

            if (builder.Warnings.Count > 0)
            {
                ShowBuilderErrors(builder.Warnings, Lang.GetLocalizedString("Warnings"));
            }

            if (builder.Model.Meta.AutoOpen)
            {
                selection = PCAxis.Paxiom.Selection.SelectAll(builder.Model.Meta);
                ok = true;
            }
            else
            {
                SelectValuesDialog dlg = new SelectValuesDialog(previousTableQuery);
                dlg.Builder = builder;

                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    selection = dlg.GetSelection();
                    ok = true;
                }
            }

            if (ok)
            {
                Cursor.Current = Cursors.WaitCursor;
                TableQuery query = new TableQuery(builder.Model, selection);
                if (!builder.BuildForPresentation(selection))
                {
                    MessageBox.Show("Builder error", "Error");
                    return;
                }

                builder.Model.Meta.Prune();             
                OpenTableForm(builder.Model, query, dbInfo, builder.Path);
            }
        }

        public GridModelForm OpenTableForm(PXModel model, TableQuery query, DatabaseInfo dbInfo, string caption)
        {
            GridModelForm frm = CreateGridModelForm();//new GridModelForm();

            frm.DbInfo = dbInfo;
            frm.Text = caption;
            frm.SetModel(model);
            frm.TableQuery = query;

            frm.OpenPXTable += OpenPXTable;

            //DockContent content = new DockContent();
            //content.DockAreas = DockAreas.Document | DockAreas.Float;
            //content.DockStateChanged += content_DockStateChanged;

            //TODO: Form closing
            //content.FormClosing += content_FormClosing;
            //content.Controls.Add(frm);
            //frm.Dock = DockStyle.Fill;
            //content.Text = frm.Filename;
            //content.Show(dockPanel);

            frm.Show(dockPanel);

            return frm;
        }

        private GridModelForm CreateGridModelForm()
        {
            var frm = new GridModelForm();
            ComposeParts(frm);

            return frm;
        }

        //void content_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //    if (sender is DockContent)
        //    {
        //        DockContent content = (DockContent)sender;

        //        if (content.Controls.Count > 0 && content.Controls[0] is GridModelForm)
        //        {
        //            GridModelForm frm = (GridModelForm)content.Controls[0];
        //            if (!frm.Closing())
        //            {
        //                e.Cancel = true;
        //            }
        //        }
        //    }
        //}

        //private void content_DockStateChanged(object sender, EventArgs e)
        //{
        //    if (sender is DockContent)
        //    {
        //        DockContent content = (DockContent)sender;

        //        if (content.Controls.Count > 0 && content.Controls[0] is GridModelForm)
        //        {
        //            GridModelForm frm = (GridModelForm)content.Controls[0];

        //            if (content.DockState == DockState.Document)
        //            {
        //                frm.Toolstrip.Visible = false;
        //                frm.Statusstrip.Visible = false;
        //            }
        //            else
        //            {
        //                frm.Toolstrip.Visible = true;
        //                frm.Statusstrip.Visible = true;
        //            }
        //        }
        //    }
        //}

        /// <summary>
        /// Verify that the user has access rights to the selected table
        /// </summary>
        /// <param name="selectedDb">DatabaseInfo object for the selected database</param>
        /// <param name="selectedTable">IPXModelBuilder object for the selected table</param>
        /// <returns>True if the user may access the table, else false</returns>
        private bool HasAccessRights(DatabaseInfo selectedDb, IPXModelBuilder selectedTable)
        {
            if (selectedDb != null)
            {
                string menu = selectedDb.GetParam(DatabaseInfo.PATH);
                string selection = selectedTable.Path;

                if (selectedDb.Type == "CNMM")
                {
                    selection = selection.Substring(selection.IndexOf(":") + 1);
                }

                return AuthorizationUtil.IsAuthorized(selectedDb.Id, menu, selection);
              
            }
            return true;
        }
        
        private void SwitchLanguage(CultureInfo culture)
        {
            Thread.CurrentThread.CurrentCulture = culture;
            string language = Lang.CurrentLanguage();

            this.Text = Lang.GetLocalizedString("MainTitle");
           
            fileToolStripMenuItem.Text = Lang.GetLocalizedString("MenuFile");
            toolsToolStripMenuItem.Text = Lang.GetLocalizedString("MenuTools");
            languageToolStripMenuItem.Text = Lang.GetLocalizedString("MenuLanguage");
            helpToolStripMenuItem.Text = Lang.GetLocalizedString("MenuHelp");
            miOpen.Text = Lang.GetLocalizedString("MenuOpen");
            exitToolStripMenuItem.Text = Lang.GetLocalizedString("MenuExit");
            aboutToolStripMenuItem.Text = Lang.GetLocalizedString("MenuAbout");
            tsbOpen.Text = Lang.GetLocalizedString("MenuOpen");

            //if (_openDialog != null)
            //{
            //    _openDialog.SwitchLanguage(culture);
            //}
            
            //Saved query
            miRunSavedQuery.Text = Lang.GetLocalizedString("MenuRunSavedQuery");
            miBatch.Text = Lang.GetLocalizedString("MenuBatch");
            openSavedQueryDialog.Filter = Lang.GetLocalizedString("MenuRunSavedQueryDialog") + " (*.pxsq)|*.pxsq"; //Translate scbmasv

            foreach (DockContent child in this.MdiChildren)
            {
                if (child.Controls[0] is GridModelForm)
                {
                    ((GridModelForm)child.Controls[0]).SwitchLanguage();
                }

                if (child is ToolWindowContent)
                {
                    ((ToolWindowContent)child).SwitchLanguage(language);
                }
            }

            foreach (var menu in _toolsMenu)
            {
                if (menu is ILocalizable)
                {
                    ((ILocalizable)menu).SwitchLanguage(language);
                }
            }

        }


        #region Events
        
        private void tsbOpen_Click(object sender, EventArgs e)
        {
            OpenTable();
        }
     
        void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy : DragDropEffects.None;
        }

        void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop)) return;

            var filePaths = (string[])(e.Data.GetData(DataFormats.FileDrop));
            foreach (var fileLoc in filePaths)
            {
                if (File.Exists(fileLoc) && fileLoc.EndsWith("px", StringComparison.OrdinalIgnoreCase))
                {
                    DatabaseInfo dbInfo = DatabaseRepository.Current.GetDatabase(null);
                    var builder = new PXFileBuilder();
                    builder.SetPath(fileLoc);
                    SelectValues(dbInfo, builder);
                }
            }
        }


        private void miBatch_Click(object sender, EventArgs e)
        {
            var batchFrm = new BatchForm();
            ComposeParts(batchFrm);
            batchFrm.InitializeControlsOnForm();
            batchFrm.ShowDialog();

        }

        private void miRunSavedQuery_Click(object sender, EventArgs e)
        {
            
            //openSavedQueryDialog.Filter += " (*.pxsq)|*.pxsq"; //Flyttad till swichLanguage
            openSavedQueryDialog.FileName = ""; //"savedQuery"; TODO: Ska verkligen savedQuery stå i fildialogen??
            if (openSavedQueryDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                LoadSavedQuery(openSavedQueryDialog.FileName);
            }
        }

        private bool LoadSavedQuery(string filename)
        {
            try
            {

                Cursor.Current = Cursors.WaitCursor;

                SavedQueryResult sqr = SavedQueryResult.Create(filename);
                
                // Get DatabaseInfo from repository
                DatabaseInfo dbInfo = DatabaseRepository.Current.GetDatabase(sqr.SavedQuery.Sources.First().DatabaseId);

                GridModelForm frm = OpenTableForm(sqr.Model, sqr.TableQuery, dbInfo, filename);
                frm.OperationsTracker = new OperationsTracker(sqr.SavedQuery.Workflow.ToArray());
                frm.OperationsTracker.IsTimeDependent = sqr.SavedQuery.TimeDependent;
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error when running saved query", "Error");
                return false;
            }
        }

        public void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region Operation events
     


        private void OpenTable()
        {
            _openDialog.Show();
            //_openDialog.Activate();
            _openDialog.Select();
        }

        #endregion

 
        private static void ShowBuilderErrors(ICollection<BuilderMessage> errors, string title)
        {
            var sb = new StringBuilder();

            foreach (BuilderMessage builderMessage in errors)
            {
                if (builderMessage.Params != null)
                {
                    // ReSharper disable once AssignNullToNotNullAttribute
                    sb.Append(string.Format(Lang.GetLocalizedString(builderMessage.Code), builderMessage.Params));
                }
                else
                {
                    var test = Lang.GetLocalizedString(builderMessage.Code);
                    sb.Append(test);
                }
                sb.Append(Environment.NewLine);
            }

            MessageBox.Show(sb.ToString(), title);
            errors.Clear();
        }
        
        /// <summary>
        /// Language is selected in language dropdown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void languageToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            int index = 0;
            foreach (ToolStripDropDownItem item in languageToolStripMenuItem.DropDownItems)
            {
                if (item.Equals(e.ClickedItem))
                {
                    SwitchLanguage((CultureInfo)item.Tag);
                    ((ToolStripMenuItem)languageToolStripMenuItem.DropDownItems[index]).Checked = true;
                }
                else
                {
                    ((ToolStripMenuItem)languageToolStripMenuItem.DropDownItems[index]).Checked = false;
                }

                index += 1;
            }
        }

        /// <summary>
        /// Regenerate the language dropdown list
        /// </summary>
        private void RegenerateLanguages()
        {
            List<string> currentLanguages = LanguageHelper.GetLanguages();
            languageToolStripMenuItem.DropDownItems.Clear();
            ToolStripMenuItem n = new ToolStripMenuItem();
            List<ToolStripMenuItem> langset = new List<ToolStripMenuItem>();
            int index = 0; 
            foreach (string lang in currentLanguages)
            {
                n.Text = lang;
                langset.Add(n);
                CultureInfo culture = new CultureInfo(n.Text);
                ToolStripItem itm = languageToolStripMenuItem.DropDownItems.Add(culture.NativeName);
                itm.Tag = culture;

                if (LanguageHelper.IsDefaultLanguage(n.Text))
                {
                    itm.Select();
                    SwitchLanguage(culture);
                    ((ToolStripMenuItem)languageToolStripMenuItem.DropDownItems[index]).Checked = true;
                }

                index += 1;
            }

            languageToolStripMenuItem.DropDown.AllowDrop = true;
        }





        //#region Encryption

        ////ConfigPath

        //static void ToggleConfigEncryption(string exeConfigName)
        //{
        //    // Takes the file name without the
        //    // .config extension.

        //    try
        //    {
        //        // Open the configuration file and retrieve 
        //        // the connectionStrings section.
        //        Configuration config = ConfigurationManager.OpenExeConfiguration(exeConfigName);

        //        ConnectionStringsSection section = config.GetSection("connectionStrings") as ConnectionStringsSection;

        //        if (section.SectionInformation.IsProtected)
        //        {
        //            // Remove encryption.
        //            section.SectionInformation.UnprotectSection();
        //        }
        //        else
        //        {
        //            // Encrypt the section.
        //            section.SectionInformation.ProtectSection("DataProtectionConfigurationProvider");
        //        }
        //        // Save the current configuration.
        //        config.SaveAs(exeConfigName, ConfigurationSaveMode.Minimal);

        //        Console.WriteLine("Protected={0}", section.SectionInformation.IsProtected);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //}

        //#endregion


        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutDialog dlg = new AboutDialog();

            dlg.ShowDialog();
        }

        ///// <summary>
        ///// Merges child window ToolStrips
        ///// </summary>
        //private void MergeChildToolStrip()
        //{
        //    ToolStripManager.RevertMerge(menuStrip1);
        //    ToolStripManager.RevertMerge(toolStrip1);
        //    ToolStripManager.RevertMerge(statusStrip1);

        //    if (dockPanel.ActiveDocument == null)
        //    {
        //        return;
        //    }

        //    DockContent content = (DockContent)dockPanel.ActiveDocument;

        //    if (content.Controls[0] is GridModelForm)
        //    {
        //        ToolStripManager.Merge(((GridModelForm)content.Controls[0]).Menustrip, menuStrip1);
        //        ToolStripManager.Merge(((GridModelForm)content.Controls[0]).Toolstrip, toolStrip1);
        //        ToolStripManager.Merge(((GridModelForm)content.Controls[0]).Statusstrip, statusStrip1);
        //    }
        //        // TODO: Change check to plugin interface
        //    else if (content.Controls[0] is PXWin.AggregationTool.ValuesetForm)
        //    {
        //        ToolStripManager.Merge(((PXWin.AggregationTool.ValuesetForm)content.Controls[0]).Toolstrip, toolStrip1);
        //        ToolStripManager.Merge(((PXWin.AggregationTool.ValuesetForm)content.Controls[0]).Statusstrip, statusStrip1);
        //    }
        //    else if (content.Controls[0] is PXWin.AggregationTool.AggregationForm)
        //    {
        //        ToolStripManager.Merge(((PXWin.AggregationTool.AggregationForm)content.Controls[0]).Toolstrip, toolStrip1);
        //        ToolStripManager.Merge(((PXWin.AggregationTool.AggregationForm)content.Controls[0]).Statusstrip, statusStrip1);
        //    }
        //}

        private void dockPanel_ActiveDocumentChanged(object sender, EventArgs e)
        {
            MergeChildStrips();
        }

        public PX.Plugin.Interfaces.IDatabaseRegistry DbRegistry
        {
            get { throw new NotImplementedException(); }
        }

        public void Show(IToolWindow view)
        {
            var wnd = new ToolWindowContent(view) { Text = view.Title };
            wnd.Show(dockPanel);
        }

        public bool Show(string path, Dictionary<string, string> param)
        {
            string ext = System.IO.Path.GetExtension(path);
            if (ext.StartsWith(".")) ext = ext.Substring(1);

            var association = _fileAssociations.FirstOrDefault(a => string.Compare(a.Metadata.Extension, ext, StringComparison.InvariantCultureIgnoreCase) == 0);

            if (association == null)
            {
                
                return false;
            }

            if (association.Metadata.AlwaysNew)
            {
                var wnd = association.Value(path, param);
            }
            else
            {

                var cdoc = FindWindow(path);
                if (cdoc != null)
                {
                    cdoc.Show(dockPanel);
                    return true;
                }
                
                var frm = association.Value(path, param);

                //Chacks that the form can be created
                if (frm == null) {

                    if (dockPanel.ActiveDocument != null)
                    {
                        ((ToolWindowContent)dockPanel.ActiveDocument).RefreshState();
                    }

                    return false;
                }

                var twnd = new ToolWindowContent(frm);
                twnd.TabText = frm.Title;
                twnd.Show(dockPanel);
                return true;
            }

            return true;
        }

        public bool Show(string path)
        {
            return Show(path, new Dictionary<string, string>());
        }

        private void MergeChildStrips()
        {
            ToolStripManager.RevertMerge(menuStrip1);
            ToolStripManager.RevertMerge(toolStrip1);
            ToolStripManager.RevertMerge(statusStrip1);

            if (dockPanel.ActiveDocument == null)
            {
                return;
            }

            ToolWindowContent wnd = (ToolWindowContent)dockPanel.ActiveDocument;

            if (wnd.HasToolStrip) ToolStripManager.Merge(wnd.ToolStrip, toolStrip1);
            if (wnd.HasStatusStrip) ToolStripManager.Merge(wnd.StatusStrip, statusStrip1);
            if (wnd.HasMenuStrip) ToolStripManager.Merge(wnd.MenuStrip, menuStrip1);

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            AddToolsMenuItems();
        }

        private void AddToolsMenuItems()
        {
            foreach (var tmenu in _toolsMenu)
            {
                toolsToolStripMenuItem.DropDownItems.AddRange(tmenu.MenuItems);
            }
        }


        public void ComposeParts(object obj)
        {
            MEFBooter.Container.ComposeParts(obj);
        }


        public void RegisterFileNotifier(string path, string callerId)
        {

            ToolWindowContent callerWnd = FindWindow(callerId);

            if (callerWnd == null) return;

            callerWnd.AddFileWatcher(path);

            
        }

        private ToolWindowContent FindWindow(string callerId)
        {
            foreach (var doc in dockPanel.Documents)
            {
                if (doc is ToolWindowContent)
                {
                    var cdoc = doc as ToolWindowContent;
                    if (string.Compare(cdoc.Tool.Id, callerId, StringComparison.InvariantCultureIgnoreCase) == 0)
                    {
                        return cdoc;
                    }
                }
            }
            return null;
        }

        private void tmrPoller_Tick(object sender, EventArgs e)
        {
            if (dockPanel.ActiveDocument != null) 
            {
                var doc = dockPanel.ActiveDocument as ToolWindowContent;

                doc.PollWork();
            }
        }


        //public void RegisterFileNotifier(string path, string callerId)
        //{
        //    ToolWindowContent callerWnd = FindWindow(callerId);

        //    if (callerWnd == null) return;

        //    callerWnd.AddFileWatcher(path, SetToolAsChanged);
        //}

        //private void SetToolAsChanged(string id)
        //{
        //    ToolWindowContent callerWnd = FindWindow(id);

        //    if (callerWnd == null) return;

        //    callerWnd.FileChanged = true;
        //}
    }
}
