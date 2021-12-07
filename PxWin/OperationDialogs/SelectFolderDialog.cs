using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PCAxis.Desktop.OperationDialogs
{
    public partial class SelectFolderDialog : Form
    {
        public string SelectedFolderPath { get; set; }

        public SelectFolderDialog(string action)
        {
            InitializeComponent();
            Load += SelectFolderDialog_Load;
            SetLanguage(action);
        }

        private void SetLanguage(string action)
        {
            Text = Lang.GetLocalizedString("SelectFolder"+action);
            btnOk.Text = Lang.GetLocalizedString("Ok");
            btnCancel.Text = Lang.GetLocalizedString("Cancel");

        }

        //private bool _useConstant;
        ///// <summary>
        ///// Should we work with a Constant value instead of a table?
        ///// </summary>
        ///// <value></value>
        ///// <returns></returns>
        ///// <remarks></remarks>
        //public bool UseConstant
        //{
        //    get { return _useConstant; }
        //    set { _useConstant = value; }
        //}


        //private bool _showConstantButton;
        //public bool ShowConstantButton
        //{
        //    get { return _showConstantButton; }
        //    set
        //    {
        //        _showConstantButton = value;

        //        buttonConstant.Visible = _showConstantButton;
        //    }
        //}


        private void SelectFolderDialog_Load(Object sender, EventArgs e)
        {
            var drives = DriveInfo.GetDrives();

            foreach (var drive in drives.Where(drive => drive.IsReady))
            {
                comboDrives.Items.Add(drive);
            }

            if (comboDrives.Items.Count > 0)
            {
                comboDrives.SelectedIndex = 0;

                PrepareFolderView((DriveInfo)comboDrives.SelectedItem);
            }
        }

        private void PrepareFolderView(DriveInfo drive)
        {
            treeFolders.Nodes.Clear();
            var @base = new TreeNode(drive.Name) {Tag = drive.Name};
            treeFolders.Nodes.Add(@base);

            BuildSubNode(drive.Name, @base);
            //Dim directories As DirectoryInfo()
            //directories = drive.RootDirectory.GetDirectories()
            //For Each directoryInfo As DirectoryInfo In directories
            //    Dim node As TreeNode = New TreeNode(directoryInfo.Name)
            //    node.Tag = directoryInfo.FullName
            //    base.Nodes.Add(node)
            //    BuildSubNode(CType(node.Tag, String), node)
            //Next

            @base.Expand();
        }

        private void BuildSubNode(string path, TreeNode root)
        {
            root.Nodes.Clear();
            try
            {
                var directories = Directory.GetDirectories(path);
                Array.Sort(directories);
                foreach (var dir in directories)
                {
                    var node = new TreeNode(Path.GetFileName(dir));
                    node.Tag = dir;
                    root.Nodes.Add(node);
                }
            }
            catch (Exception ex)
            {
            }

        }

        private void treeViewFolders_BeforeExpand(Object sender, TreeViewCancelEventArgs e)
        {
            var node = e.Node;

            foreach (TreeNode n in node.Nodes)
            {
                var path = n.Tag.ToString();
                BuildSubNode(path, n);
            }
        }


        private void comboBoxDrive_SelectedIndexChanged(Object sender, EventArgs e)
        {
            PrepareFolderView((DriveInfo)comboDrives.SelectedItem);
        }

        private void btnOK_Click(Object sender, EventArgs e)
        {
            if (treeFolders.SelectedNode == null)
            {
                MessageBox.Show("Select a folder in the treeview");
                return;
            }

            SelectedFolderPath = treeFolders.SelectedNode.Tag.ToString();
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
