using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PCAxis.Paxiom;
using PCAxis.Query;
using System.ComponentModel.Composition;
using PX.Plugin.Interfaces;
using PX.Plugin.Interfaces.Attributes;
using System.IO;
using Newtonsoft.Json;

namespace PCAxis.Desktop
{
    public partial class StopRecordingDialog : Form
    {

        /// <summary>
        /// Imports all plugins that implements IDataSource
        /// </summary>
        [ImportMany(AllowRecomposition = true)]
        private IEnumerable<Lazy<IDataSource, IDataSourceMetadata>> _dataSources;

        private GridModelForm _modelForm;

        public StopRecordingDialog(GridModelForm frm)
        {
            _modelForm = frm;
            InitializeComponent();
            InitializeStepsListView();
            SetLanguage();
            SetTimeOptions();
        }

        private void SetLanguage()
        {
            Text = Lang.GetLocalizedString("SavedQueryStop");
            btnSave.Text = Lang.GetLocalizedString("MenuSave");
            btnCancel.Text = Lang.GetLocalizedString("Cancel");
            rbFixed.Text = Lang.GetLocalizedString("SavedQueryIntervalFixed");
            rbRolling.Text = Lang.GetLocalizedString("SavedQueryIntervalRolling");
            rbSame.Text = Lang.GetLocalizedString("SavedQueryIntervalSame");
            lblAlert.Text = Lang.GetLocalizedString("SavedQueryTimeDependentAlert");
        }

        /// <summary>
        /// If an operation has changed the time variable (or if no time variable exists) it will not 
        /// be possible to select fixed or rolling starting time
        /// </summary>
        private void SetTimeOptions()
        {
            var tvar = _modelForm.mModel.Meta.Variables.FirstOrDefault(v => v.IsTime);

            if (_modelForm.OperationsTracker.IsTimeDependent || tvar == null)
            {
                rbFixed.Enabled = false;
                rbRolling.Enabled = false;
                rbSame.Checked = true;
                ShowAlert(true);
            }
            else
            {
                ShowAlert(false);
            }
        }

        private void InitializeStepsListView()
        {
            var steps = _modelForm.OperationsTracker.GetSteps();          

            lvSteps.View = View.Details;
            lvSteps.Columns.Add(Lang.GetLocalizedString("Step"), 110);
            lvSteps.Columns.Add(Lang.GetLocalizedString("Operation"), 180);            

            for (var i = 0; i < steps.Length; i++)
            {
                string subItem = "";

                if (steps[i].Type.Equals("SUM") && steps[i].Params["operation"] != null)
                {
                    subItem = steps[i].Params["operation"].Translate();
                }
                else if (steps[i].Type.Equals("PER_PART") && steps[i].Params["operationType"] != null)
                {
                    subItem = steps[i].Params["operationType"].Translate();
                }
                else
                {
                    subItem = Lang.GetLocalizedString(steps[i].Type);
                }

                lvSteps.Items.Add(Lang.GetLocalizedString("Step") + " " + (i + 1)).SubItems.Add(subItem);
            }

            //for (var i = 0; i < lvSteps.Items.Count; i++)
            //{
            //    lvSteps.Items[i].BackColor = i % 2 == 0 ? Color.LightBlue : Color.White;
            //}           
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                saveFileDialog.Filter = "Saved query (*.pxsq)|*.pxsq";
                saveFileDialog.FileName = "*.pxsq";

                if (saveFileDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                {
                    return;
                }

                string filepath = saveFileDialog.FileName;
                var query = GetQueries();

                var time = string.Empty;
                if (rbFixed.Checked)
                {
                    time = "from";
                }
                else if (rbRolling.Checked)
                {
                    time = "top";
                }
                else
                {
                    time = "item";
                }

                var tvar = _modelForm.mModel.Meta.Variables.FirstOrDefault(v => v.IsTime);
                //No defined time variable in the model, skip time filter 
                if (tvar != null)
                {

                    var qq = query.Quieries.FirstOrDefault(q => q.Code == tvar.Code);
                    if (qq == null)
                    {
                        qq = new PCAxis.Query.Query();
                        qq.Code = tvar.Code;
                        qq.Selection = new PCAxis.Query.QuerySelection();
                        qq.Selection.Filter = "all";
                        qq.Selection.Values = new string[] { "*" };
                        query.Quieries.Add(qq);
                    }

                    if (time == "item")
                    {
                        //Do Nothing
                    }
                    else if (time == "top")
                    {
                        qq.Selection.Filter = "top";
                        qq.Selection.Values = new string[] { tvar.Values.Count.ToString() };
                    }
                    else if (time == "from")
                    {
                        qq.Selection.Filter = "from";
                        var minValue = tvar.Values[0];
                        for (int i = 1; i < tvar.Values.Count; i++)
                        {
                            if (string.Compare(minValue.TimeValue, tvar.Values[i].TimeValue) > 0)
                            {
                                minValue = tvar.Values[i];
                            }
                        }
                        qq.Selection.Values = new string[] { minValue.Code };
                    }
                }

                PCAxis.Query.SavedQuery sq = new PCAxis.Query.SavedQuery();
                sq.Sources.Add(query);
                sq.Output = new Output();
                sq.Output.Type = "SCREEN";
                sq.TimeDependent = _modelForm.OperationsTracker.IsTimeDependent;
                sq.Workflow.AddRange(_modelForm.OperationsTracker.GetSteps());

                
                SaveSavedQuery(filepath, sq);

                DialogResult = System.Windows.Forms.DialogResult.OK;

            }
            catch (Exception)
            {
                
                throw;
            }
        }


        private static bool SaveSavedQuery(string path, Query.SavedQuery sq)
        {
            try
            {
                //string path = HostingEnvironment.MapPath(@"~/App_Data/queries/" + name);

                using (FileStream fs = File.Open(path, FileMode.Create, FileAccess.ReadWrite, FileShare.Read))
                using (StreamWriter sw = new StreamWriter(fs))
                using (JsonWriter jw = new JsonTextWriter(sw))
                {
                    jw.Formatting = Formatting.Indented;

                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(jw, sq);
                }
            }
            catch (Exception)
            {
                //TODO Logg error
                return false;
            }

            return true;
        }


        ///// <summary>
        ///// Get relative path to table in a mapped PX-file database
        ///// </summary>
        ///// <param name="dbPath">Path to the PX-file database</param>
        ///// <param name="tablePath">Path to the PX-file</param>
        ///// <returns></returns>
        //private string GetRelativePxPath(string dbPath, string tablePath)
        //{
        //    if (string.IsNullOrWhiteSpace(dbPath))
        //    {
        //        return System.IO.Path.GetFullPath(tablePath).Replace("\\", "/");
        //    }
        //    string fullDbPath = System.IO.Path.GetFullPath(dbPath).Replace("\\", "/");
        //    string fullTablePath = System.IO.Path.GetFullPath(tablePath).Replace("\\", "/");

        //    var fullDbParts = fullDbPath.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
        //    var fullTableParts = fullTablePath.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);

        //    int index = 0;

        //    while ((index < fullDbParts.Length) && (fullDbParts[index] == fullTableParts[index]))
        //    {
        //        index++;
        //    }

        //    StringBuilder path = new StringBuilder();

        //    for (int i = index; i < fullTableParts.Length; i++)
        //    {
        //        path.Append("/");
        //        path.Append(fullTableParts[i]);
        //    }

        //    return path.ToString();
        //}


        private PCAxis.Query.TableSource GetQueries()
        {
            PCAxis.Query.TableSource src = new PCAxis.Query.TableSource();

            src.Id = "1";
            src.Default = true;
            src.DatabaseId = _modelForm.DbInfo.Id;
            src.Language = LanguageHelper.GetTableLanguage(_modelForm.DbInfo); //behöver ändras till currentlanguage

            var dsc = _dataSources.FirstOrDefault(s => s.Metadata.SourceType == _modelForm.DbInfo.Type);
            
            if (dsc != null)
            {
                src.Source = dsc.Value.GetSource(_modelForm.DbInfo, _modelForm.mModel, src.Language);
            }

            //if ((_modelForm.DbInfo.Type == "PX"))
            //{
            //    // Mapped PX-database - We only want the relative path to the table
            //    src.Source = GetRelativePxPath(_modelForm.DbInfo.Params[DatabaseInfo.PATH], _modelForm.mModel.Meta.MainTable);
            //}
            //else
            //{
            //    src.Source = _modelForm.mModel.Meta.MainTable; // Unmapped PX -> absolute path to px-file, CNMM -> table id
            //}
            
            src.Type = _modelForm.DbInfo.Type;

            src.SourceIdType = "path";

            src.Quieries.AddRange(
                _modelForm.TableQuery.Query.Select(
                    q => new PCAxis.Query.Query()
                    {
                        Code = q.Code,
                        Selection = new PCAxis.Query.QuerySelection() { Filter = q.Selection.Filter, Values = q.Selection.Values }
                    }).ToArray());

            return src;
        }

        private void ShowAlert(bool show)
        {
            imgAlert.Visible = show;
            lblAlert.Visible = show;
        }
    }
}
