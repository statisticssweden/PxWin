using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PCAxis.Desktop.Properties;
using PCAxis.Menu;
using PCAxis.Menu.Implementations;
using PCAxis.Paxiom;
using System.Xml;
using System.Xml.Linq;
using System.Globalization;
using System.Threading;
using WeifenLuo.WinFormsUI.Docking;
using System.ComponentModel.Composition;
using PX.Plugin.Interfaces.Attributes;
using PX.Plugin.Interfaces;
using PX.Desktop.Interfaces;

namespace PCAxis.Desktop
{
    [Export]
    public partial class OpenTableForm : ToolWindowContent, IToolWindow, ILocalizable
    {
        #region "Open dialog events"

        /// <summary>
        /// Event that is fired when the "Open table" dialog is activated
        /// </summary>
        public event EventHandler OpenDialogActivated;

        protected virtual void OnOpenDialogActivated(EventArgs e)
        {
            EventHandler handler = OpenDialogActivated;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Argument class for the OpenPXTable event
        /// </summary>
        public class OpenPXTableEventArgs : System.EventArgs
        {
            /// <summary>
            /// Selected database
            /// </summary>
            public DatabaseInfo DbInfo;

            /// <summary>
            /// Builder object
            /// </summary>
            public IPXModelBuilder Builder;

            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="builder"></param>
            public OpenPXTableEventArgs(DatabaseInfo dbInfo, IPXModelBuilder builder)
            {
                DbInfo = dbInfo;
                Builder = builder;
            }
        }

        public delegate void OpenPXTableEventHandler(object sender, OpenPXTableEventArgs e);

        /// <summary>
        /// Event that is fired when a table shall be opened
        /// </summary>
        public event OpenPXTableEventHandler OpenPXTable;
       

        protected virtual void OnOpenPXTable(OpenPXTableEventArgs e)
        {
            OpenPXTableEventHandler handler = OpenPXTable;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion


        [Import]
        IHost _host;

        /// <summary>
        /// Imports all plugins that implements IDataSource
        /// </summary>
        [ImportMany(AllowRecomposition = true)]
        private IEnumerable<Lazy<IDataSource, IDataSourceMetadata>> _dataSources;


        #region "Private fields"

        private DatabaseInfo _selectedDatabase;
        private DatabaseInfo _databaseToCache = null;
        private string _databaseToCacheLanguage;
        private PCAxis.Paxiom.IPXModelBuilder _selectedTable;
        private PxMenuBase _menu;
        
        #endregion

        public OpenTableForm()
        {
            InitializeComponent();;
            lstDatabases.HideSelection = false;
            lstTables.HideSelection = false;
            AddDatabases();
            HideOnClose = true;
            DockAreas = DockAreas.Document | DockAreas.Float;
            Tool = this;
        }

        private void OpenTableForm_Load(object sender, EventArgs e)
        {
            btnOpen.Enabled = false;
        }

        private void ChangeLanguageOnColumns()
        {
            this.chName.Text = Lang.GetLocalizedString("Name");
            if (lstTables.Columns.ContainsKey("colPeriod"))
            {
                lstTables.Columns[lstTables.Columns.IndexOfKey("colPeriod")].Text = Lang.GetLocalizedString("OpenTablePeriodColumn");
            }
            if (lstTables.Columns.ContainsKey("colCategory"))
            {
                lstTables.Columns[lstTables.Columns.IndexOfKey("colCategory")].Text = Lang.GetLocalizedString("Category");
            }
        }

        /// <summary>
        /// Change language texts for database context menu
        /// </summary>
        private void ChangeLanguageOnContextMenu()
        {
            this.dbContextMenuStrip.Items["tsRefresh"].Text = Lang.GetLocalizedString("OpenTableRefrehDatabaseCache");
        }

        /// <summary>
        /// Add available databases to list
        /// </summary>
        private void AddDatabases()
        {
            DatabaseInfo di;
            ListViewItem item;
            
            // Open table from file system

            foreach (KeyValuePair<string, DatabaseInfo> kvp in DatabaseRepository.Current.Databases)
            {
                di = kvp.Value;
                item = new ListViewItem(di.Name);
                item.Tag = di;
                if (di.Type.Equals("CNMM", StringComparison.OrdinalIgnoreCase))
                {
                    item.ImageIndex = 2;
                    item.StateImageIndex = 2;
                }
                else if (di.Type.Equals("PXAPI", StringComparison.OrdinalIgnoreCase))
                {
                    item.ImageIndex = 3;
                    item.StateImageIndex = 3;
                }
                else
                {
                    if (di.Id == "FileSystem")
                    {
                        item.ImageIndex = 0;
                        item.StateImageIndex = 0;
                    }
                    else
                    {
                        item.ImageIndex = 1;
                        item.StateImageIndex = 1;
                    }
                }
                lstDatabases.Items.Add(item);
            }

            lstDatabases.Select();            
        }

        public void SelectDatabase(string databaseId)
        {
            foreach (ListViewItem item in lstDatabases.Items)
            {
                DatabaseInfo info = (DatabaseInfo)item.Tag;
                if (databaseId.Equals(info.Id))
                {
                    item.Selected = true;
                    SelectDatabase(info);
                }
            }
        }

        private void SelectDatabase()
        {
            ListViewItem item;
            if (lstDatabases.SelectedItems.Count != 0)
            {
                item = lstDatabases.SelectedItems[0];

                SelectDatabase((DatabaseInfo)item.Tag);
            }
        }

        private void SelectDatabase(DatabaseInfo info)
        {
            _selectedDatabase = info;
            if (_selectedDatabase.Id == "FileSystem")
            {
                _selectedDatabase = null;
                _selectedTable = null;
                _databaseToCache = null;
                _databaseToCacheLanguage = "";

                tvDatabase.Nodes.Clear();
                ClearTableList();

                using (OpenFileDialog openDialog = new OpenFileDialog())
                {
                    openDialog.Filter = "PC-Axis Files (.px)|*.px";
                    openDialog.FilterIndex = 0;

                    if (openDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        _selectedTable = new PCAxis.Paxiom.PXFileBuilder();
                        _selectedTable.SetPath(openDialog.FileName);

                        OnOpenPXTable(new OpenPXTableEventArgs(info, _selectedTable));

                        return;
                    }
                }
            }
            else
            {
                InitDatabase(GetMenu(null));
                tvDatabase.Select();
            }

        }

        private void InitDatabase(PxMenuBase menu)
        {
            TreeNode node;
            string dbLang;

            if (menu == null)
            {
                return;
            }

            _menu = menu;

            // 1. Save loaded database to cache
            CacheDatabaseMenu();

            // 2. Clear tree view
            tvDatabase.Nodes.Clear();
            ClearTableList();

            // 3. Try to load tree view from cache if possible
            dbLang = GetDbLanguage(_selectedDatabase, Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName);
            
            if (!DatabaseCache.Current.Get(_selectedDatabase.Id, dbLang, tvDatabase))
            {
                // If tree view could not be loaded from cache load it the ordinary way...
                if (_menu.CurrentItem is Menu.PxMenuItem)
                {
                    Item curr = _menu.CurrentItem;
                    foreach (Item itm in ((Menu.PxMenuItem)_menu.CurrentItem).SubItems)
                    {
                        if (itm is PxMenuItem)
                        {
                            node = tvDatabase.Nodes.Add(itm.Text);
                            node.Tag = itm.ID;
                            AddDummyNode(node);
                        }
                    }
                }
            }

            _databaseToCache = _selectedDatabase;
            _databaseToCacheLanguage = dbLang;
        }


        /// <summary>
        /// Cache the currently selected database menu
        /// </summary>
        private void CacheDatabaseMenu()
        {
            if (_databaseToCache != null)
            {
                DatabaseCache.Current.Add(_databaseToCache.Id, _databaseToCacheLanguage, tvDatabase);
            }
        }

        private void AddDummyNode(TreeNode node)
        {
            TreeNode node2;
            node2 = node.Nodes.Add("---");
            node2.Tag = new ItemSelection("dummy","dummy");
        }


        private PxMenuBase GetMenu(ItemSelection node)
        {

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                var dsc = _dataSources.FirstOrDefault(s => s.Metadata.SourceType == _selectedDatabase.Type);


                if (dsc == null)
                {
                    throw new Exception("No dataprovider for " + _selectedDatabase.Type);
                }

                var ds = dsc.Value;
                string dbLang = GetDbLanguage(_selectedDatabase, Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName);

                PxMenuBase menu = null; 

                if (node == null)
                {
                    menu = ds.CreateMenu(_selectedDatabase, "", "", dbLang);
                }
                else
                { 
                    menu = ds.CreateMenu(_selectedDatabase, node.Menu, node.Selection, dbLang);
                }

                return menu;

            }
            catch (Exception)
            {

                MessageBox.Show("Could not open database " + _selectedDatabase.Name, "Error");
                return null;
            }
            finally
            {
                Cursor.Current = Cursors.Arrow;
            }

        }


        /// <summary>
        /// Get database language
        /// </summary>
        /// <param name="dbi">DatabaseInfo object</param>
        /// <param name="preferredLanguage">Preffered language if possible for the selected database</param>
        /// <returns>Language to use for the database</returns>
        private string GetDbLanguage(DatabaseInfo dbi, string preferredLanguage)
        {
            // Set database language
            if ((preferredLanguage == dbi.DefaultLanguage) || (dbi.Languages.Contains(preferredLanguage)))
            {
                return preferredLanguage;
            }
            else
            {
                return dbi.DefaultLanguage;
            }
        }


        private void tvDatabase_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            TreeNode node;
            node = e.Node;

            if (node.Nodes.Count == 1 && IsDummyNode(node.Nodes[0]))
            {
                // Remove dummy node
                node.Nodes.Clear(); 

                // Get sub nodes for this node
                ItemSelection item = (ItemSelection)node.Tag;
                _menu = GetMenu(item);

                foreach (Item itm in ((Menu.PxMenuItem)_menu.CurrentItem).SubItems)
                {
                    if (itm is PxMenuItem)
                    {
                        AddChildNode(node, itm);
                    }
                }

                // Select node to display tables if it has no child nodes
                if (node.Nodes.Count == 0)
                {
                    tvDatabase.SelectedNode = node;
                }

            }
        }


        private void tvDatabase_AfterSelect(object sender, TreeViewEventArgs e)
        {
            bool first = true;
            bool loadNodes = false;

            TreeNode node = e.Node;
            if (node.Nodes.Count == 1 && IsDummyNode(node.Nodes[0]))
            {
                // Remove dummy node
                node.Nodes.Clear();
                loadNodes = true;
            }

            _menu = GetMenu((ItemSelection)e.Node.Tag);
            
            lstTables.ShowItemToolTips = true;
            
            ClearTableList();
            
            foreach (Item item in ((PxMenuItem)_menu.CurrentItem).SubItems)
            {
               
                if (item is TableLink)
                {
                    
                    TableLink lnk = (TableLink)item;
                    StringBuilder categoryText = new StringBuilder();
                    StringBuilder periodText = new StringBuilder();
                   
                    ListViewItem lItem ;

                    //Fetch table category
                    AddTableCategory(lnk, categoryText);
                    //Fetch table category
                    AddTablePeriod(lnk, periodText);

                    //Link text
                    lItem = lstTables.Items.Add(lnk.Text);
                    
                    if (first)
                    {
                        lItem.Selected = true;
                        first = false;
                    }
                    lItem.SubItems.Add(periodText.ToString());
                    lItem.SubItems.Add(categoryText.ToString());

                    lItem.SubItems.Add(lnk.ID.Selection);
                    if (lnk.LastUpdated > lnk.Published)
                    {
                        lItem.ImageIndex = 0;
                    }
                    lItem.Tag = lnk;
                }
                else if (item is PxMenuItem && loadNodes)
                {
                    AddChildNode(node, item);
                }
            }
            
        }

        /// <summary>
        /// Add a child node to an existing tree node
        /// </summary>
        /// <param name="node">The existing tree node</param>
        /// <param name="item">The PxMenuItem object representing the new child node</param>
        private void AddChildNode(TreeNode node, Item item)
        {
            if (!(item is PxMenuItem))
            {
                return;
            }

            TreeNode node2;
            node2 = node.Nodes.Add(item.Text);
            node2.Tag = item.ID;
            AddDummyNode(node2);
        }

        /// <summary>
        /// Checks if the node is a dummy node, meaning that the node has not yet been loaded from database.
        /// </summary>
        /// <param name="node">The node</param>
        /// <returns>True if it is a dummy node, else false</returns>
        private bool IsDummyNode(TreeNode node)
        {
            if (node.Tag == null)
            {
                return false;
            }

            if (!(node.Tag is ItemSelection))
            {
                return false;
            }

            ItemSelection id = (ItemSelection)node.Tag;

            if (id.Menu.Equals("dummy") && id.Selection.Equals("dummy"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
 
        private void ShowAndHideColumn()
        {
            if (_selectedDatabase != null && _selectedDatabase.Type.Equals("CNMM"))
            {
                if (!lstTables.Columns.ContainsKey("colPeriod"))
                {
                    ColumnHeader periodHeader = lstTables.Columns.Add("colPeriod", Lang.GetLocalizedString("OpenTablePeriodColumn"));
                    periodHeader.Width = 100;
                }

                if (!lstTables.Columns.ContainsKey("colCategory"))
                {
                    ColumnHeader categoryHeader = lstTables.Columns.Add("colCategory", Lang.GetLocalizedString("Category"));
                    categoryHeader.Width = 70;
                }

            }
            else
            {
                if (lstTables.Columns.ContainsKey("colPeriod"))
                {
                    lstTables.Columns.RemoveByKey("colPeriod");
                }
                if (lstTables.Columns.ContainsKey("colCategory"))
                {
                    lstTables.Columns.RemoveByKey("colCategory");
                }
            }

            ResizeTableList();

        }

        private void AddTableCategory(PCAxis.Menu.TableLink tabelLink, StringBuilder linkText)
        {
            switch (tabelLink.Category)
            {
                case PresCategory.Internal:
                    linkText.Append(Lang.GetLocalizedString("CategoryInternal"));
                    break;
                case PresCategory.Official:
                    linkText.Append(Lang.GetLocalizedString("CategoryOfficial"));
                    break;
                case PresCategory.Private:
                    linkText.Append(Lang.GetLocalizedString("CategoryPrivate"));
                    break;
                default:
                    linkText.Append(" S ");
                    break;
            }

        }

        private void AddTablePeriod(PCAxis.Menu.TableLink tableLink, StringBuilder periodText)
        {
            if (!string.IsNullOrEmpty(tableLink.StartTime))
            {
                periodText.Append(tableLink.StartTime);
                periodText.Append(" - ");

                if (!string.IsNullOrEmpty(tableLink.EndTime))
                {
                    periodText.Append(tableLink.EndTime);
                }
            }
        }

        private void AddImageList()
        {
            // Create one ImageList objects.
            ImageList imageListSmall = new ImageList();

            imageListSmall.ImageSize = new Size(9,12);


            imageListSmall.Images.Add(Properties.Resources.refresh);
            
            lstTables.SmallImageList = imageListSmall;

        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenTable();
        }

        private void OpenTable()
        {
            Link lnk;

            if (lstTables.SelectedItems.Count < 1)
            {
                return;
            }

            lnk = (Link)lstTables.SelectedItems[0].Tag;

            switch (lnk.Type)
            {
                case LinkType.Table:
                    if (CreateTable((TableLink)lstTables.SelectedItems[0].Tag))
                    {
                        OnOpenPXTable(new OpenPXTableEventArgs(_selectedDatabase, _selectedTable));    
                    }
                    break;
                case LinkType.PX:
                    if (CreateTable((TableLink)lstTables.SelectedItems[0].Tag))
                    {
                        OnOpenPXTable(new OpenPXTableEventArgs(_selectedDatabase, _selectedTable));
                    }
                    break;
                case LinkType.XDF:
                    break;
                case LinkType.PXS:
                    break;
                case LinkType.URL:
                    break;
                default:
                    break;
            }
        }

        private bool CreateTable(TableLink link)
        {

            var dsc = _dataSources.FirstOrDefault(s => s.Metadata.SourceType == _selectedDatabase.Type);

            if (dsc == null)
            {
                return false;
            }

            var ds = dsc.Value;
            var language = GetDbLanguage(_selectedDatabase, Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName);
            _selectedTable = (IPXModelBuilder)ds.CreateBuilder(_selectedDatabase, link.ID.Menu, link.ID.Selection, language);

            return true;
        }

        private void lstDatabases_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) // Return pressed
            {
                SelectDatabase();
            }
        }

        private void ClearTableList()
        {
            lstTables.Items.Clear();
            btnOpen.Enabled = false;
        }


        private void lstTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstTables.SelectedItems.Count > 0)
            {
                btnOpen.Enabled = true;
            }
            else
            {
                btnOpen.Enabled = false;
            }
        }

        private void tvDatabase_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) // Return pressed
            {
                if (lstTables.Items.Count > 0)
                {
                    lstTables.Select();
                }
            }
        }

        private void lstTables_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) // Return pressed
            {
                OpenTable();
            }
        }

        private void lstTables_DoubleClick(object sender, EventArgs e)
        {
            OpenTable();
        }


        private void OpenTableForm_Activated(object sender, EventArgs e)
        {
            OnOpenDialogActivated(EventArgs.Empty);    
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }


        public string Title
        {
            get { return _host.Language.GetString("MenuOpenTable"); }
        }

        public string Id
        {
            get { return "open.pxwin"; }
        }

        public Control View
        {
            get { return this; }
        }

        public void SwitchLanguage(string language)
        {
            this.Text = Lang.GetLocalizedString("MenuOpenTable");
            this.btnOpen.Text = Lang.GetLocalizedString("Open");

            // File system
            if (lstDatabases.Items[0] != null)
            {
                lstDatabases.Items[0].Text = Lang.GetLocalizedString("OpenTableFileSystem");
            }

            // Check if browse file system (index = 0) is selected
            if (!lstDatabases.SelectedIndices.Contains(0))
            {
                //Reselect database with the new language
                SelectDatabase();
            }
            ChangeLanguageOnColumns();
            ChangeLanguageOnContextMenu();
        }


        public event EventHandler TitleChanged;


        public void Reload()
        {
        }


        public bool CanClose()
        {
            return true;
        }

        private void OpenTableForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CacheDatabaseMenu();
        }

        private void lstDatabases_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                SelectDatabase();
                ShowAndHideColumn();
                AddImageList();
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (!DatabaseCache.Current.CacheEnabled())
                {
                    return;
                }
                ListViewItem itm = lstDatabases.GetItemAt(e.X, e.Y);
                DatabaseInfo dbInfo = (DatabaseInfo)itm.Tag;

                if (!dbInfo.Id.Equals("FileSystem"))
                {
                    this.dbContextMenuStrip.Tag = dbInfo;
                    this.dbContextMenuStrip.Show(this, e.Location);
                }
                ReselectDbInGUI();
            }

        }

        private void ReselectDbInGUI()
        {
            if (_selectedDatabase == null)
            {
                return;
            }

            for (int i=0; i<lstDatabases.Items.Count; i++)
            {
                DatabaseInfo dbInfo = (DatabaseInfo)lstDatabases.Items[i].Tag;
                if (dbInfo.Id.Equals(_selectedDatabase.Id))
                {
                    lstDatabases.Items[i].Selected = true;
                    lstDatabases.Items[i].Focused = true;
                    break;
                }
            }
        }

        private void tsRefresh_Click(object sender, EventArgs e)
        {
            DatabaseInfo dbInfo = (DatabaseInfo)dbContextMenuStrip.Tag;
            string dbLang = GetDbLanguage(dbInfo, Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName);
            DatabaseCache.Current.Refresh(dbInfo.Id, dbLang);

            // Reload if we are refreshing the currently selected database
            if (dbInfo.Id.Equals(_selectedDatabase.Id))
            {
                _databaseToCache = null;
                SelectDatabase();
            }

        }

        private void ResizeTableList()
        {
            int width = lstTables.Width;

            if (lstTables.Columns.ContainsKey("colPeriod"))
            {
                width = width - lstTables.Columns["colPeriod"].Width;
            }

            if (lstTables.Columns.ContainsKey("colCategory"))
            {
                width = width - lstTables.Columns["colCategory"].Width;
            }

            lstTables.Columns[0].Width = width;
        }

        private void OpenTableForm_SizeChanged(object sender, EventArgs e)
        {
            ResizeTableList();
        }
    }

}
