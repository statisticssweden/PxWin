using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using PX.Desktop.Interfaces;
using System.IO;
using System.Timers;

namespace PCAxis.Desktop
{
    public partial class ToolWindowContent : DockContent
    {
        public ToolWindowContent()
        {
            InitializeComponent();
            this.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document | WeifenLuo.WinFormsUI.Docking.DockAreas.Float;
            this.Load += ToolWindowContent_Load;
            this.Activated +=ToolWindowContent_Activated;
            this.DockStateChanged += ToolWindowContent_DockStateChanged;
        }

        public ToolWindowContent(IToolWindow tool)
        {
            InitializeComponent();
            this.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document | WeifenLuo.WinFormsUI.Docking.DockAreas.Float;
            this.Load += ToolWindowContent_Load;
            this.Activated += ToolWindowContent_Activated;
            this.DockStateChanged += ToolWindowContent_DockStateChanged;

            var control = tool.View;

            Controls.Add(control);
            control.Dock = DockStyle.Fill;
            Tool = tool;
            Tool.TitleChanged += TitleChangedHander;
        }

        public bool FileChanged { get; set; }
        private string _changedFile;
        private IToolWindow _tool;
        public IToolWindow Tool 
        {
            get {
                return _tool;
            }
            protected set
            {
                _tool = value;
                UpdateTool();
            }
        }

        private void UpdateTool()
        {
            _toolStrip = (_tool is IToolbar);
            _statusStrip = (_tool is IStatusbar); 
            _menuStrip = (_tool is IMenubar);
        }
        private bool _toolStrip;
        public bool HasToolStrip { get { return  _toolStrip; } }
        public ToolStrip ToolStrip
        {
            get
            {
                if (!_toolStrip) return null;
                var tool = _tool as IToolbar;
                return tool.Toolbar;
            }
        }

        private bool _menuStrip;
        public bool HasMenuStrip { get { return _menuStrip; } }
        public MenuStrip MenuStrip
        {
            get
            {
                if (!_menuStrip) return null;
                var tool = _tool as IMenubar;
                return tool.Menubar;
            }
        }

        private bool _statusStrip;
        public bool HasStatusStrip { get { return _statusStrip; } }
        public StatusStrip StatusStrip
        {
            get
            {
                if (!_statusStrip) return null;
                var status = _tool as IStatusbar;
                return status.Statusbar;
            }
        }

        private void TitleChangedHander(object sender, EventArgs e)
        {
            TabText = Tool.Title;
        }

        private void ToolWindowContent_DockStateChanged(object sender, EventArgs e)
        {
            if (DockState == DockState.Document)
            {
                if (_toolStrip) ((IToolbar)_tool).Toolbar.Visible = false;
                if (_statusStrip) ((IStatusbar)_tool).Statusbar.Visible = false;
                if (_menuStrip) ((IMenubar)_tool).Menubar.Visible = false;
            }
            else
            {
                if (_toolStrip) ((IToolbar)_tool).Toolbar.Visible = true;
                if (_statusStrip) ((IStatusbar)_tool).Statusbar.Visible = true;
                if (_menuStrip) ((IMenubar)_tool).Menubar.Visible = true;
            }
        }

        public void SwitchLanguage(string language)
        {
            if (_tool is ILocalizable)
            {
                ((ILocalizable)_tool).SwitchLanguage(language);
                this.TabText = _tool.Title;
            }
        }

        private void ToolWindowContent_Load(object sender, EventArgs e)
        {
            SwitchLanguage(Lang.CurrentLanguage());
        }


        private class FileAction
        {
            public Action<string> NotifyCallback { get; set; }
            public FileSystemWatcher Watcher { get; set; }
            public string Path { get; set; }
            public DateTime? LastChange { get; set; }

            public FileAction()
            {
            }

            void Timer_Elapsed(object sender, ElapsedEventArgs e)
            {
                NotifyCallback(Path);
            }
        }

        private Dictionary<string, FileAction> _watchers = new Dictionary<string, FileAction>();
        public void AddFileWatcher(string path)
        {
            if (_watchers.Keys.Contains(path.ToLower())) return;

            var watcher = new FileSystemWatcher();
            watcher.Path = Path.GetDirectoryName(path);
            watcher.Filter = Path.GetFileName(path);

            watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.Size;

            watcher.Changed += watcher_Changed;

            _watchers.Add(path.ToLower(), new FileAction() { NotifyCallback = FileChangedHandler, Watcher = watcher, Path = path });
            watcher.EnableRaisingEvents = true;
            
        }

        private void FileChangedHandler(string t)
        { 
            FileChanged = true;

            if (this.DockPanel != null && this.DockPanel.ActiveDocument == this)
            {
                RefreshState();
            }
        }

        void watcher_Changed(object sender, FileSystemEventArgs e)
        {
            var path = e.FullPath.ToLower();
            _changedFile = e.FullPath;

            if (_watchers[path] != null)
            {
                _watchers[path].LastChange = DateTime.Now;
            }
        }


        private void ToolWindowContent_Activated(object sender, EventArgs e)
        {
            RefreshState();
        }


        internal void RefreshState()
        {
            if (FileChanged)
            {
                Tool.Reload();

                FileChanged = false;
            }
        }


        public void PollWork()
        {
            foreach (var key in _watchers.Keys)
            {
                if (_watchers[key].LastChange.HasValue)
                {
                    if ((DateTime.Now - _watchers[key].LastChange.Value).TotalMilliseconds > 400)
                    {
                        _watchers[key].LastChange = null;
                        _watchers[key].NotifyCallback(key);
                        
                    }
                }
            }
        }

        private void ToolWindowContent_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!Tool.CanClose())
            {
                e.Cancel = true;
            }
            else
            {
                if (Tool is IDisposable)
                {
                    ((IDisposable)Tool).Dispose();
                }
            }
        }
    }
}

