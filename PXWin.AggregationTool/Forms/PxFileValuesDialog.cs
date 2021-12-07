using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PCAxis.Paxiom;
using System.ComponentModel.Composition;
using PX.Desktop.Interfaces;

namespace PXWin.AggregationTool
{
    public partial class PxFileValuesDialog : Form
    {
        [Import]
        private IHost _host;

        public PXFileBuilder Builder { get; set; }
        public String ChoosenVariable { get; set; }

        /// <summary>
        /// file name .px
        /// </summary>
        public string FileName { get; set; }

        private void PxFileValuesDialog_Load(object sender, EventArgs e)
        {
            SwitchLanguage(_host.Language.CurrentLanguage);
            LoadFile(FileName);
            
        }

        public void SwitchLanguage(string language)
        {
            lblQuestion.Text = _host.Language.GetString("formPxFileValuesQuestion");
            lbLanguage.Text = _host.Language.GetString("formPxFileValuesLanguage");
            lbVariables.Text = _host.Language.GetString("formPxFileValuesVariables");
            btnCancel.Text = _host.Language.GetString("Cancel");
            btnOk.Text = _host.Language.GetString("Ok");
            this.Text = _host.Language.GetString("formPxFileValuesCaption");
        }

        public PxFileValuesDialog()
        {
            Builder = new PXFileBuilder();
            InitializeComponent();            
            
        }
        public void LoadFile(string fileName)
        {
            lbFilename.Text = @"""" + System.IO.Path.GetFileName(fileName) + @"""";
            ShowLanguageInFile(fileName);
        }

        private void ShowLanguageInFile(string fileName)
        {
            cboLanguage.Items.Clear();

            Builder.SetPath(fileName);

            if (Builder != null)
            {
                try 
                { 
                    Builder.BuildForSelection();

                    //Check if there are more than one languare

                    string[] languagesInFile = Builder.Model.Meta.GetAllLanguages();


                    if (languagesInFile == null)
                    {
                        //Only default language exists - Add it to languagesInFile
                        languagesInFile = new string[1];
                        languagesInFile[0] = Builder.Model.Meta.Language;
                    }

                    string defaultLanguage = Builder.Model.Meta.Language;
                    if (languagesInFile.Length > 0)
                    {
                        foreach (var language in languagesInFile)
                        {
                            cboLanguage.Items.Add(language);
                        }
                        cboLanguage.SelectedIndex = cboLanguage.Items.IndexOf(Builder.Model.Meta.Language);

                        ShowVariablesInFile(defaultLanguage);
                    }
                }
               catch(Exception ex)
               {
                    MessageBox.Show(_host.Language.GetString("toolValueSetIllegalFileMessage") + "\n\n" + @"""" + ex.Message + @"""", _host.Language.GetString("toolValueSetIllegalFileCaption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
               }
            }
            else
            {
                MessageBox.Show(_host.Language.GetString("toolValueSetIllegalFileMessage") + "\n\n" , _host.Language.GetString("toolValueSetIllegalFileCaption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        private void ShowVariablesInFile(string language)
        {
            Builder.Model.Meta.SetLanguage(language);
            PCAxis.Paxiom.Variables variables = Builder.Model.Meta.Variables;
            cboVariables.Items.Clear();
            if (variables != null)
            {
                foreach (var var in variables)
                {
                    cboVariables.Items.Add(var.Name);
                }
                cboVariables.SelectedIndex = 0;
            }

        }

        private void cboLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboLanguage.SelectedItem.ToString() != "" && cboVariables.Items.Count > 0)
                ShowVariablesInFile(cboLanguage.SelectedItem.ToString());
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            ChoosenVariable = cboVariables.SelectedItem.ToString();
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        
    }
}
