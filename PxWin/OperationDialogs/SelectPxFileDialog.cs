using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PCAxis.Paxiom;

namespace PCAxis.Desktop.OperationDialogs
{
    public partial class SelectPxFileDialog : Form
    {
        public SelectPxFileDialog()
        {
            InitializeComponent();
            Load += SelectPxFileDialog_Load;
            SetLanguage();
        }

        private void SetLanguage()
        {
            Text = Lang.GetLocalizedString("SelectPxFile");
            btnOk.Text = Lang.GetLocalizedString("Ok");
            btnCancel.Text = Lang.GetLocalizedString("Cancel");
        }

        #region "Property declarations"
        private string _action = string.Empty;
        private string _selectedFile = string.Empty;
        private string _folderPath = string.Empty;

        public string SelectedFilePath
        {
            get { return _selectedFile; }
            set { _selectedFile = value; }
        }
        public string FolderPath
        {
            get { return _folderPath; }
            set { _folderPath = value; }
        }
        public PXModel CurrentModel { get; set; }

        public string Action
        {
            get { return _action; }
            set { _action = value; }
        }
        #endregion
        #region "Constants"
        /// <summary>
        /// Public Constant declations for use with the Action property
        /// </summary>
        /// <remarks></remarks>
        private const string ACTION_OverlayWithTable = "OVERLAY";
        private const string ACTION_LinkWithTable = "LINK";
        #endregion
        private const string ACTION_CalculateWithTable = "CALCULATEWITHTABLE";

        #region "Designer related"
        private void btnOK_Click(Object sender, EventArgs e)
        {
            var fi = (FileInfo)lbPxFiles.SelectedItem;
            SelectedFilePath = fi.FullName;
            DialogResult = DialogResult.OK;
        }

        private void SelectPxFileDialog_Load(Object sender, EventArgs e)
        {
            if (FolderPath.Length == 0)
            {
                MessageBox.Show("Folder path is missing");
                DialogResult = DialogResult.Abort;
            }
            if (CurrentModel == null)
            {
                MessageBox.Show("Current model is missing");
                DialogResult = DialogResult.Abort;
            }

            // Load files
            LoadPxFiles();
        }

        #endregion

        #region "Subs/Functions"
        /// <summary>
        /// Load *.px files that comply to the action parameter
        /// Action = String.Empty --> Load all *.px files
        /// Action = ACTION_LinkWithTable --> Load only those files that are valid for this action
        /// Action = ACTION_OverlayWithTable --> Load only those files that are valid for this action
        /// </summary>
        /// <remarks></remarks>
        private void LoadPxFiles()
        {
            var dirInfo = new DirectoryInfo(FolderPath);
            var files = new List<FileInfo>();
            try
            {
                 files = dirInfo.GetFiles("*.px").ToList();
            }
            catch (UnauthorizedAccessException)
            {
                // Choke the unauthorized exception              
            }
            

            if (Action.Length == 0)
            {
                // Load all *.px files
                foreach (var file in files)
                {
                    lbPxFiles.Items.Add(file);
                }
            }
            else switch (Action)
            {
                case ACTION_LinkWithTable:
                    // Load all *.px files
                    foreach (var file in files)
                    {
                        if (IsModelAvailableForLinkWithTable(CurrentModel, OpenTableWithSelectAll(file.FullName)))
                        {
                            lbPxFiles.Items.Add(file);
                        }
                    }
                    break;
                case ACTION_OverlayWithTable:
                    // Load any *.px file that has the same variable and variable value setup as the current model
                    foreach (var file in files)
                    {
                        if (EnsureSameVariablesAndValues(CurrentModel, OpenTableWithSelectAll(file.FullName)))
                        {
                            lbPxFiles.Items.Add(file);
                        }
                    }
                    break;
                case ACTION_CalculateWithTable:
                    // Load any *.px file that has the same variable and variable value setup as the current model
                    foreach (var file in files)
                    {
                        if (EnsureSameVariablesAndValues(CurrentModel, OpenTableWithSelectAll(file.FullName)))
                        {
                            lbPxFiles.Items.Add(file);
                        }
                    }
                    break;
                default:
                    MessageBox.Show(Lang.GetLocalizedString("ErrorActionIsMissing") + "(" + Action + ")", Lang.GetLocalizedString("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DialogResult = DialogResult.Abort;
                    break;
            }

            if (lbPxFiles.Items.Count > 0)
            {
                lbPxFiles.SelectedIndex = 0;
            }
            else
            {                
                MessageBox.Show(Lang.GetLocalizedString("ErrorNoAvailableFiles") + FolderPath,Lang.GetLocalizedString("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.Abort;
            }
        }

        /// <summary>
        /// Check that the models has the same variables and values.
        /// </summary>
        /// <param name="currentModel"></param>
        /// <param name="checkModel"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private static bool EnsureSameVariablesAndValues(PXModel currentModel, PXModel checkModel)
        {
            try
            {
                // Loop variables in currentModel and verify that they are present in the checkModel
                foreach (var oldVar in currentModel.Meta.Variables)
                {
                    var newVar = checkModel.Meta.Variables.GetByCode(oldVar.Code);
                    if (newVar == null)
                    {
                        return false;
                    }
                    if (oldVar.Values.Count != newVar.Values.Count)
                    {
                        return false;
                    }

                    // Loop currentModel variable values and verify that they are present in checkModel variable values
                    
                    foreach (var oldVal in oldVar.Values)
                    {
                        var newValue = newVar.Values.GetByCode(oldVal.Code);
                        if (newValue == null)
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        private static bool IsModelAvailableForLinkWithTable(PXModel oldModel, PXModel linkModel)
        {
            var numberOfDifferentVariables = 0;

            foreach (var var in oldModel.Meta.Variables)
            {
                var checkVar = linkModel.Meta.Variables.GetByCode(var.Code);

                if (checkVar != null)
                {
                    if (checkVar.Values.Count != var.Values.Count)
                    {
                        numberOfDifferentVariables += 1;
                    }
                    else
                    {
                        foreach (var val in checkVar.Values)
                        {
                            if (var.Values.GetByCode(val.Code) == null)
                            {
                                // Value in model2 variable was not found in model1 variable
                                // this is the changed variable
                                numberOfDifferentVariables += 1;
                                break; // TODO: might not be correct. Was : Exit For
                            }
                        }
                    }
                }
                else
                {
                    // Not the same variable setup
                    return false;
                }
            }

            if (numberOfDifferentVariables < 2)
            {
                // Max one variable can have different values
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Open px file and return model with all variables and values selected
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        /// <remarks></remarks>
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
            builder.Model.Meta.Prune();
            // Return model
            return builder.Model;
        }

        #endregion

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
