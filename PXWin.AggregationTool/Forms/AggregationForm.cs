using IniParser;
using IniParser.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PX.Desktop.Interfaces;
using System.ComponentModel.Composition;

namespace PXWin.AggregationTool
{
    public partial class AggregationForm : UserControl, IToolWindow, ILocalizable, IToolbar, IStatusbar
    {
        [Import]
        private IHost _host;
        
        private List<Value> _values;
        private List<Value> _usedValues = new List<Value>();
        private List<GroupedValue> _groupedValues; //= new List<GroupedValue>();
        private Dictionary<String, ValueInfo> _dictUsedValues = new Dictionary<string, ValueInfo>();
        private string _editGroupCode;
        private string _valusetInAggFile = "";
        private string _valusetInVsFile = "";
        private string _vsFilename;
        private string _aggFilename;
        private bool _dirty = false; // Has any value been changed?
        private string _loadedName = "";
        private string _loadedMap = "";
        private bool _saveClicked = false;

        public AggregationForm()
        {
            _groupedValues = new List<GroupedValue>();
            InitializeComponent();
            lnkVsFile.Visible = false;
            grbAggregation.Enabled = false;
            btnGroupUp.Enabled = false;
            btnGroupDown.Enabled = false;
            btnGroupDelete.Enabled = false;
            btnNewGroup.Enabled = false;
            tsbSave.Enabled = false;
            Rectangle r = pnlEditButtons.Bounds;
            pnlEditButtons.SetBounds(10, r.Y, r.Width, r.Height);
            ShowGroupInfo(false);

        }

        /// <summary>
        /// Path to the aggregation (.agg) file
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Path to the associated valueset (.vs) file
        /// </summary>
        public string Valueset { get; set; }
        public ToolStrip Toolstrip { get { return toolStrip1; } }
        public StatusStrip Statusstrip { get { return statusStrip1; } }

        public bool LoadData(string filename, string vsfilename)
        {
            LoadVsData(vsfilename);
            LoadAggData(filename);
            
            if (CheckValuesetAssociation() == false)
            {
                if (MessageBox.Show(_host.Language.GetString("toolAggIllegalVsAssociationMessage"), _host.Language.GetString("toolAggIllegalVsAssociationCaption"), MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    RemoveAggFileFromVsFile(filename);
                }
                return false;
            }

            UpdateGroupList();
            GetUsedValuesInformation();
            CheckForDuplicateAndIllegalValues();
            UpdateNumberOfItems();
            Title = System.IO.Path.GetFileName(filename);

            return true;
        }

        /// <summary>
        ///  Verifies that the "Valuset" in the .agg file has the same value as "Name" in the .vs file
        /// </summary>
        /// <returns></returns>
        private bool CheckValuesetAssociation()
        {
            if (!string.IsNullOrWhiteSpace(_valusetInAggFile) && !string.IsNullOrWhiteSpace(_valusetInVsFile))
            {
                return (string.Compare(_valusetInAggFile, _valusetInVsFile) == 0);
            }

            return true;
        }

        public void LoadAggData(string filename)
        {
            _aggFilename = filename;
            int counter;
            var parser = new FileIniDataParser();

            IniParser.Model.IniData data;

            try
            {
                data = parser.ReadFile(filename);
            }
            catch (Exception ex)
            {
                MessageBox.Show(_host.Language.GetString("toolAggIllegalFileMessage") + "\n\n" + ex.Message, _host.Language.GetString("toolAggIllegalFileCaption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (data["Aggreg"] != null)
            {
                if (data["Aggreg"]["Valueset"] != null)
                {
                    _valusetInAggFile = data["Aggreg"]["Valueset"];
                }

                if (data["Aggreg"]["Name"] != null)
                {
                    txtName.Text = data["Aggreg"]["Name"];
                    _loadedName = data["Aggreg"]["Name"];
                }
                if (data["Aggreg"]["Map"] != null)
                {
                    txtMap.Text = data["Aggreg"]["Map"];
                    _loadedMap = data["Aggreg"]["Map"]; 
                }

                counter = 1;
                _groupedValues = new List<GroupedValue>();
                while (!string.IsNullOrEmpty(data["Aggreg"][counter.ToString()]))
                {
                    GroupedValue group = new GroupedValue();
                    group.Code = data["Aggreg"][counter.ToString()];
                    _groupedValues.Add(group);
                    counter = counter + 1;
                }
            }

            if (data["Aggtext"] != null)
            {
                counter = 1;
                for (int i = 0; i < _groupedValues.Count; i++)
                {
                    if (!string.IsNullOrEmpty(data["Aggtext"][counter.ToString()]))
                    {
                        _groupedValues[i].Text = data["Aggtext"][counter.ToString()];
                    }
                    counter = counter + 1;
                }
            }

            foreach (GroupedValue group in _groupedValues)
            {
                counter = 1;
                if (data[group.Code] != null)
                {
                    while (!string.IsNullOrEmpty(data[group.Code][counter.ToString()]))
                    {
                        Value value = _values.Find(v => v.Code == data[group.Code][counter.ToString()]);

                        if (value != null)
                        {
                            group.Values.Add(value);

                            foreach (ListViewItem item in lstAvailable.Items)
                            {
                                Value val = (Value)item.Tag;

                                if (val.Code == value.Code)
                                {
                                    item.Remove();
                                }
                            }
                        }
                        else
                        {
                            // Illegal value that are not in the valueset!
                            value = new Value();
                            value.Code = data[group.Code][counter.ToString()];
                            value.Text = _host.Language.GetString("toolAggIllegalValue");
                            value.Illegal = true;
                            group.Values.Add(value);
                        }
                        
                        counter = counter + 1;
                    }
                }

            }

            sslFileName.Text = filename;

        }
        
        public void LoadVsData(string vsfilename)
        {
            _vsFilename = vsfilename;
            var parser = new FileIniDataParser();
                        
            IniParser.Model.IniData data;

            try
            {
                data = parser.ReadFile(vsfilename);
            }
            catch (Exception ex)
            {
                MessageBox.Show(_host.Language.GetString("toolAggIllegalFileMessage") + "\n\n" + ex.Message, _host.Language.GetString("toolAggIllegalFileCaption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (data["Descr"] != null)
            {
                _valusetInVsFile = data["Descr"]["Name"];
                txtVsName.Text = _valusetInVsFile;
            }
            if (data["Domain"] != null)
            {
                txtDomain.Text = data["Domain"]["1"];
            }

            int counter = 1;
            _values = new List<Value>();
            
            if (data["Valuecode"] != null)
            {
                while (!string.IsNullOrEmpty(data["Valuecode"][counter.ToString()]))
                {
                    _values.Add(new Value() { Code = data["Valuecode"][counter.ToString()], Text = data["Valuetext"][counter.ToString()] });
                    counter++;
                }
            }

            var items = from value in _values select new ListViewItem(new string[] { value.Code, value.Text }) { Tag = value };
            lstAvailable.Items.Clear();
            lstAvailable.Items.AddRange(items.ToArray());

            Valueset = vsfilename;

            lnkVsFile.Text = System.IO.Path.GetFileName(vsfilename);
            lnkVsFile.Visible = true;
            lnkVsFile.LinkClicked += VsFile_LinkClicked;
            System.Windows.Forms.ToolTip tooltipVs = new System.Windows.Forms.ToolTip();
            tooltipVs.SetToolTip(lnkVsFile, vsfilename);
            btnBrowseVsFile.Visible = false;
            btnNewGroup.Enabled = true;
            grbAggregation.Enabled = true;
            tsbSave.Enabled = true;
        }

        private void btnNewGroup_Click(object sender, EventArgs e)
        {
            ShowGroupEdit(false);
        }

        private void btnSaveGroup_Click(object sender, EventArgs e)
        {
            if (txtGroupCode.Text.Length == 0)
            {
                MessageBox.Show(_host.Language.GetString("toolAggEnterGroupCodeMessage"), _host.Language.GetString("toolAggEnterGroupCodeCaption"));
                return;
            }
            if (txtGroupText.Text.Length == 0)
            {
                MessageBox.Show(_host.Language.GetString("toolAggEnterGroupNameMessage"), _host.Language.GetString("toolAggEnterGroupNameCaption"));
                return;
            }

            string groupCode;
            bool isEdit = _editGroupCode.Length > 0;

            if (isEdit)
            {
                groupCode = _editGroupCode;
            }
            else
            {
                groupCode = txtGroupCode.Text;
            }

            var group = _groupedValues.Find(g => g.Code == groupCode);

            if (group == null)
            {
                group = new GroupedValue();
            }
            else
            {
                if (!isEdit)
                {
                    // This group aready exists
                    MessageBox.Show(_host.Language.GetString("toolAggExistingGroupMessage"), _host.Language.GetString("toolAggExistingGroupCaption"));
                    return;
                }
            }

            group.Code = txtGroupCode.Text;
            group.Text = txtGroupText.Text;

            group.Values.Clear();
            foreach (ListViewItem item in lstGroup.Items)
            {
                group.Values.Add(item.Tag as Value);
            }

            if (!isEdit)
            {
                _groupedValues.Add(group);
            }
            GetUsedValuesInformation();
            UpdateGroupList();
            DisplaySelectedGroup();
            _dirty = true;
        }

        private void UpdateGroupList()
        {
            lstGrouped.Items.Clear();
            foreach (GroupedValue group in _groupedValues)
            {
                lstGrouped.Items.Add(new ListViewItem(new string[] { group.Code, group.Text, string.Join(",", (group.Values.Select(v => v.Code).ToArray())) }) { Tag = group });
            }
        }

        private void lstGrouped_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplaySelectedGroup();
        }


        private void DisplaySelectedGroup()
        {
            lstGroup.Items.Clear();
            if (lstGrouped.SelectedItems.Count > 0)
            {
                GroupedValue value = lstGrouped.SelectedItems[0].Tag as GroupedValue;

                txtGroupCode.Text = value.Code;
                txtGroupText.Text = value.Text;

                lstGroup.BeginUpdate();
                foreach (var v in value.Values)
                {
                    lstGroup.Items.Insert(0, new ListViewItem(new string[] { v.Code, v.Text }) { Tag = v, Checked = true });
                }
                lstGroup.EndUpdate();

                ShowGroupInfo(true);
                btnGroupUp.Enabled = true;
                btnGroupDown.Enabled = true;
                btnGroupDelete.Enabled = true;
            }
            else
            {
                btnGroupUp.Enabled = false;
                btnGroupDown.Enabled = false;
                btnGroupDelete.Enabled = false;
                txtGroupCode.Text = "";
                txtGroupText.Text = "";
                ShowGroupInfo(false);
            }

            CheckForDuplicateAndIllegalValues();
            UpdateNumberOfItems();
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        public DialogResult SaveAs()
        {
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.AddExtension = true;
                dlg.Filter = "Aggregation-file (*.agg)|*.agg";
                dlg.DefaultExt = "agg";
                if (!string.IsNullOrWhiteSpace(Path))
                {
                    dlg.FileName = System.IO.Path.GetFileName(Path);
                }
                else
                {
                    dlg.FileName = txtName.Text;
                }

                var res = dlg.ShowDialog();
                if (res != DialogResult.OK) return res;


                bool bNew = Path == null;

                res = SaveAggFile(dlg.FileName);
                if (res != DialogResult.OK) return res;

                if (bNew && (System.IO.File.Exists(Valueset)))
                {
                    AddAggFileToVsFile(System.IO.Path.GetFileName(dlg.FileName));
                }

                return DialogResult.OK;
            }
        }

        /// <summary>
        /// Add the aggregation (.agg) file to list of aggregation files in the associated valueset (.vs) file
        /// </summary>
        /// <param name="filename">Filename of the aggregation (.agg) file to add in the valueset (.vs) file</param>
        private void AddAggFileToVsFile(string filename)
        {
            var parser = new FileIniDataParser();

            IniParser.Model.IniData data;

            try
            {
                data = parser.ReadFile(Valueset);
            }
            catch (Exception ex)
            {
                return;
            }

            if (data.Sections["Aggreg"] == null)
            {
                data.Sections.AddSection("Aggreg");
            }

            SectionData secData = data.Sections.GetSectionData("Aggreg");
            int KeyNr = secData.Keys.Count + 1;
            AddSectionValue(secData, KeyNr.ToString(), filename);

            var config = new IniParser.Model.Configuration.DefaultIniParserConfiguration();
            config.AssigmentSpacer = "";
            var formatter = new IniParser.Model.Formatting.DefaultIniDataFormatter(config);

            using (var stream = new System.IO.StreamWriter(Valueset, false, System.Text.Encoding.Default))
            {
                parser.WriteData(stream, data, formatter);
            }

        }

        /// <summary>
        /// Remove the aggregation (.agg) file to list of aggregation files in the associated valueset (.vs) file
        /// </summary>
        /// <param name="filename">Filename of the aggregation (.agg) file to remove in the valueset (.vs) file</param>
        private void RemoveAggFileFromVsFile(string filename)
        {
            filename = System.IO.Path.GetFileName(filename);
            var parser = new FileIniDataParser();

            IniParser.Model.IniData data;

            try
            {
                data = parser.ReadFile(Valueset);
            }
            catch (Exception ex)
            {
                return;
            }

            if (data.Sections["Aggreg"] == null)
            {
                return;
            }

            SectionData secData = data.Sections.GetSectionData("Aggreg");

            int counter = 1;
            while (secData.Keys[counter.ToString()] != null)
            {
                if (string.Compare(secData.Keys[counter.ToString()], filename) == 0)
                {
                    secData.Keys.RemoveKey(counter.ToString());

                    // Remove all the following items in the section
                    List<KeyData> lst = new List<KeyData>();
                    int i = counter + 1; 
                    while (secData.Keys[i.ToString()] != null)
                    {
                        KeyData key = new KeyData(counter.ToString());
                        key.Value = secData.Keys[i.ToString()];
                        lst.Add(key);
                        secData.Keys.RemoveKey(i.ToString());
                        counter = counter + 1;
                        i = counter + 1;
                    }
                    
                    // Add the following items again in the section with the right key
                    for (int j = lst.Count - 1; j >= 0; j--)
                    {
                        AddSectionValue(secData, lst[j].KeyName, lst[j].Value);
                    }

                    break;
                }
                counter = counter + 1;
            }

            var config = new IniParser.Model.Configuration.DefaultIniParserConfiguration();
            config.AssigmentSpacer = "";
            var formatter = new IniParser.Model.Formatting.DefaultIniDataFormatter(config);

            using (var stream = new System.IO.StreamWriter(Valueset, false, System.Text.Encoding.Default))
            {
                parser.WriteData(stream, data, formatter);
            }

        }



        private DialogResult SaveAggFile(string filename)
        {
            if (txtName.Text.Length == 0)
            {
                MessageBox.Show(_host.Language.GetString("toolAggEnterAggregationNameMessage"), _host.Language.GetString("toolAggEnterAggregationNameCaption"));
                return DialogResult.Cancel;
            }

            IniData data = new IniParser.Model.IniData();
            
            // --- Aggreg section ---
            SectionData aggregSection = new IniParser.Model.SectionData("Aggreg");

            // Name
            AddSectionValue(aggregSection, "Name", txtName.Text);
            _loadedName = txtName.Text;

            // Map
            if (txtMap.Text.Length > 0)
            {
                AddSectionValue(aggregSection, "Map", txtMap.Text);
            }
            _loadedMap = txtMap.Text;

            // Valueset
            AddSectionValue(aggregSection, "Valueset", txtVsName.Text);

            int i = 1;
            foreach (GroupedValue group in _groupedValues)
            {
                AddSectionValue(aggregSection, i.ToString(), group.Code);
                i = i + 1;
            }

            data.Sections.Add(aggregSection);


            // --- Aggtext section ---
            SectionData aggtextSection = new IniParser.Model.SectionData("Aggtext");

            i = 1;
            foreach (GroupedValue group in _groupedValues)
            {
                AddSectionValue(aggtextSection, i.ToString(), group.Text);
                i = i + 1;
            }

            data.Sections.Add(aggtextSection);

            // --- Group sections ---
            foreach (GroupedValue group in _groupedValues)
            {
                SectionData groupSection = new IniParser.Model.SectionData(group.Code);

                i = 1;
                foreach (Value value in group.Values)
                {
                    AddSectionValue(groupSection, i.ToString(), value.Code);
                    i = i + 1;
                }

                data.Sections.Add(groupSection);
            }
            
            var parser = new FileIniDataParser();
            var config = new IniParser.Model.Configuration.DefaultIniParserConfiguration ();
            config.AssigmentSpacer = "";
            var formatter = new IniParser.Model.Formatting.DefaultIniDataFormatter(config);

            using (var stream = new System.IO.StreamWriter(filename, false, System.Text.Encoding.Default))
            {
                parser.WriteData(stream, data, formatter);
            }
            
            //parser.WriteFile(filename, data, System.Text.Encoding.Default);
            sslFileName.Text = filename;
            Title = System.IO.Path.GetFileName(filename);
            Path = filename;
            _dirty = false;

            return DialogResult.OK;
        }

        private void AddSectionValue(SectionData section, string name, string value)
        {
            KeyData key = new KeyData(name);
            key.Value = value;
            section.Keys.AddKey(key);
        }

        private void btnBrowseVsFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openDialog = new OpenFileDialog())
            {
                openDialog.Filter = "Valueset-files (*.vs)|*.vs";
                openDialog.FilterIndex = 0;

                if (openDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    LoadVsData(openDialog.FileName);
                    _dirty = true;
                    return;
                }
            }
        }

        private void btnAddValues_Click(object sender, EventArgs e)
        {
            lstAvailable.BeginUpdate();
            lstGroup.BeginUpdate();
            foreach (ListViewItem item in lstAvailable.SelectedItems)
            {
                Value value = (Value)item.Tag;
                lstGroup.Items.Add(new ListViewItem(new string[] { value.Code, value.Text }) { Tag = value });
                if (!chkOverlappingValues.Checked)
                {
                    item.Remove();
                }
            }

            UpdateNumberOfItems();
            lstGroup.EndUpdate();
            lstAvailable.EndUpdate();
        }

        private void btnRemoveValues_Click(object sender, EventArgs e)
        {
            lstAvailable.BeginUpdate();
            lstGroup.BeginUpdate();
            foreach (ListViewItem item in lstGroup.SelectedItems)
            {
                Value value = (Value)item.Tag;
                RemoveValueAndMakeAvailable(value);
                item.Remove();
            }

            UpdateNumberOfItems();
            lstGroup.EndUpdate();
            lstAvailable.EndUpdate();
        }

        /// <summary>
        /// Return value to list of available values
        /// </summary>
        /// <param name="value"></param>
        private void RemoveValueAndMakeAvailable(Value value)
        {
            if (value.Illegal)
            {
                // Illegal values shall not be added to the available list
                return;
            }

            if (chkOverlappingValues.Checked)
            {
                // All values are already in the available list...
                return;
            }

            if (_dictUsedValues.ContainsKey(value.Code) && _dictUsedValues[value.Code].Groups.Count > 1)
            {
                // The value is still used in another group so it shouldn´t be made available
                return;
            }

            lstAvailable.Items.Add(new ListViewItem(new string[] { value.Code, value.Text }) { Tag = value });
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lstGrouped.SelectedItems.Count > 0)
            {
                GroupedValue value = lstGrouped.SelectedItems[0].Tag as GroupedValue;

                txtGroupCode.Text = value.Code;
                txtGroupText.Text = value.Text;
                lstGroup.Items.Clear();

                foreach (var v in value.Values)
                {
                    lstGroup.Items.Insert(0, new ListViewItem(new string[] { v.Code, v.Text }) { Tag = v, Checked = true });
                }
            }

            ShowGroupEdit(true);
        }

        private void ShowGroupInfo(bool enabled)
        {
            btnRemoveValues.Enabled = false;
            btnAddValues.Enabled = false;
            grbGroupInfo.Enabled = enabled;
            pnlEditButtons.Visible = false;
            txtGroupCode.Enabled = false;
            txtGroupText.Enabled = false;

            if (!enabled)
            {
                lstGroup.Items.Clear();
                pnlInfoButtons.Visible = false;
            }
            else
            {
                pnlInfoButtons.Visible = true;
            }
        }

        private void ShowGroupEdit(bool edit)
        {
            grbGroupInfo.Enabled = true;
            grpAvailableValues.Enabled = true;
            btnRemoveValues.Enabled = true;
            btnAddValues.Enabled = true;
            pnlInfoButtons.Visible = false;
            pnlEditButtons.Visible = true;
            txtGroupCode.Enabled = true;
            txtGroupText.Enabled = true;

            if (edit)
            {
                _editGroupCode = txtGroupCode.Text;
            }
            else
            {
                _editGroupCode = "";
                txtGroupCode.Text = "";
                txtGroupText.Text = "";
                lstGroup.Items.Clear();
            }

            CheckForDuplicateAndIllegalValues();
            UpdateNumberOfItems();
        }

        private void btnCancelEdit_Click(object sender, EventArgs e)
        {
            UpdateAvailableValues();
            DisplaySelectedGroup();
        }

        private void btnGroupDelete_Click(object sender, EventArgs e)
        {
            GroupedValue group = GetSelectedGroup();

            if (group != null)
            {
                if (MessageBox.Show(_host.Language.GetString("toolAggDeleteGroupQuestion"), _host.Language.GetString("toolAggDeleteGroupCaption"), MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    // release selected values
                    foreach (Value value in group.Values)
                    {
                        RemoveValueAndMakeAvailable(value);
                    }

                    _groupedValues.Remove(group);
                    GetUsedValuesInformation();
                    UpdateNumberOfItems();
                    UpdateGroupList();
                    DisplaySelectedGroup();
                }
            }
        }

        private void btnGroupUp_Click(object sender, EventArgs e)
        {
            GroupedValue group = GetSelectedGroup();

            if (group != null)
            {
                int index = _groupedValues.IndexOf(group);

                if (index == 0)
                {
                    return;
                }

                _groupedValues.RemoveAt(index);
                _groupedValues.Insert(index -1, group);
                UpdateGroupList();
                lstGrouped.Items[index - 1].Selected = true;
                lstGrouped.Items[index - 1].Focused = true;
                lstGrouped.Select();
                DisplaySelectedGroup();
                _dirty = true;
            }

        }

        private void btnGroupDown_Click(object sender, EventArgs e)
        {
            GroupedValue group = GetSelectedGroup();

            if (group != null)
            {
                int index = _groupedValues.IndexOf(group);

                if (index == _groupedValues.Count - 1)
                {
                    return;
                }

                _groupedValues.RemoveAt(index);
                _groupedValues.Insert(index + 1, group);
                UpdateGroupList();
                lstGrouped.Items[index + 1].Selected = true;
                lstGrouped.Items[index + 1].Focused = true;
                lstGrouped.Select();
                DisplaySelectedGroup();
                _dirty = true;
            }
        }

        private GroupedValue GetSelectedGroup()
        {
            GroupedValue group = null;

            if (lstGrouped.SelectedItems.Count > 0)
            {
                group = lstGrouped.SelectedItems[0].Tag as GroupedValue;
            }

            return group;
        }

        private void chkOverlappingValues_CheckedChanged(object sender, EventArgs e)
        {
            UpdateAvailableValues();
        }

        private void UpdateAvailableValues()
        {
            lstAvailable.BeginUpdate();
            lstAvailable.Items.Clear();


            if (chkOverlappingValues.Checked)
            {
                ClearDuplicates();

                // Overlapping values allowed - Display all values
                var items = from value in _values select new ListViewItem(new string[] { value.Code, value.Text }) { Tag = value };
                lstAvailable.Items.AddRange(items.ToArray());
            }
            else
            {
                GetUsedValuesInformation();
                CheckForDuplicateAndIllegalValues();

                // Display all values that are not used before
                var available = _values.Where(p => !_usedValues.Any(p2 => p2 == p));
                var items = from value in available select new ListViewItem(new string[] { value.Code, value.Text }) { Tag = value };
                lstAvailable.Items.AddRange(items.ToArray());
            }

            UpdateNumberOfItems();
            lstAvailable.EndUpdate();
        }

        private void GetUsedValuesInformation()
        {
            _usedValues = new List<Value>();
            // Verify that there are no duplicate values
            _dictUsedValues = new Dictionary<string, ValueInfo>();

            foreach (GroupedValue group in _groupedValues)
            {
                foreach (Value value in group.Values)
                {
                    if (!_usedValues.Contains(value))
                    {
                        _usedValues.Add(value);
                    }

                    if (_dictUsedValues.ContainsKey(value.Code))
                    {
                        _dictUsedValues[value.Code].Groups.Add(group);
                    }
                    else
                    {
                        ValueInfo vi = new ValueInfo() { Value = value };
                        vi.Groups.Add(group);
                        _dictUsedValues.Add(value.Code, vi);
                    }
                }
            }
        }

        private void CheckForDuplicateAndIllegalValues()
        {
            lblMessOverlappingOrIllegalValues.Visible = false;
            
            if (!chkOverlappingValues.Checked)
            {
                // Check groups
                foreach (ListViewItem itm in lstGrouped.Items)
                {
                    GroupedValue group = itm.Tag as GroupedValue;

                    foreach (Value value in group.Values)
                    {
                        if ((_dictUsedValues.ContainsKey(value.Code) && _dictUsedValues[value.Code].Groups.Count > 1) || (value.Illegal))
                        {
                            // The value exists in more than one group - mark it as an duplicate
                            itm.BackColor = Color.LightPink;
                            lblMessOverlappingOrIllegalValues.Visible = true;
                            break;
                        }
                    }
                }

                // Check group
                foreach (ListViewItem itm in lstGroup.Items)
                {
                    Value value = itm.Tag as Value;

                    if ((_dictUsedValues.ContainsKey(value.Code) && _dictUsedValues[value.Code].Groups.Count > 1) || (value.Illegal))
                    {
                        // The value exists in more than one group - mark it as an duplicate                        
                        itm.BackColor = Color.LightPink;
                        lblMessOverlappingOrIllegalValues.Visible = true;
                    }
                }
            }
        }

        private void ClearDuplicates()
        {
            lblMessOverlappingOrIllegalValues.Visible = false;
            foreach (ListViewItem itm in lstGrouped.Items)
            {
                itm.BackColor = Color.White;
            }
            foreach (ListViewItem itm in lstGroup.Items)
            {
                itm.BackColor = Color.White;
            }
        }

        private void UpdateNumberOfItems()
        {
            grpAvailableValues.Text = _host.Language.GetString("toolAggAggregationAvailableBox") + " (" + lstAvailable.Items.Count.ToString() + ")";
            grpGroups.Text = _host.Language.GetString("toolAggGroupsBox") + " (" + lstGrouped.Items.Count.ToString() + ")";
            grbGroupInfo.Text = _host.Language.GetString("toolAggGroupInfoBox") + " (" + lstGroup.Items.Count.ToString() + ")";
        }

        public class Value
        {
            public string Code { get; set; }
            public string Text { get; set; }
            public bool Illegal { get; set; }
        }

        public class GroupedValue : Value
        {
            private List<Value> _values;
            public List<Value> Values { get { return _values; } }

            public GroupedValue()
            {
                _values = new List<Value>();
            }
        }

        private class ValueInfo
        {
            private List<GroupedValue> _groups;

            public ValueInfo()
            {
                _groups = new List<GroupedValue>();
            }

            public Value Value { get; set; }
            public List<GroupedValue> Groups { get { return _groups; } }
        }

        private void grbGroupInfo_SizeChanged(object sender, EventArgs e)
        {
            PositionButtons();
        }

        private void PositionButtons()
        {
            int y = this.Height - (splitContainer1.Panel2.Height / 2);
            Rectangle ba = btnAddValues.Bounds;

            y = y - ba.Height - 5;

            btnAddValues.SetBounds(ba.X, y, ba.Width, ba.Height);
            btnRemoveValues.SetBounds(ba.X, y + 5 + ba.Height, ba.Width, ba.Height);
        }


        private string _title;
        public string Title
        {
            get { return _title; }
            set 
            {
                if ( string.Compare(_title,  value) != 0)
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

        public void SwitchLanguage(string language)
        {
            if (string.IsNullOrEmpty(Path))
            {
                Title = _host.Language.GetString("toolAggNewAggregation");
            }

            grbAggregation.Text = _host.Language.GetString("toolAggAggregationBox");
            lblName.Text = _host.Language.GetString("toolAggAggregationName");
            lblMap.Text = _host.Language.GetString("toolAggAggregationMap");
            chkOverlappingValues.Text = _host.Language.GetString("toolAggAllowOverlappingValues");
            grpGroups.Text = _host.Language.GetString("toolAggGroupsBox");
            lstGrouped.Columns[0].Text = _host.Language.GetString("toolAggGroupsListCode");
            lstGrouped.Columns[1].Text = _host.Language.GetString("toolAggGroupsListText");
            lstGrouped.Columns[2].Text = _host.Language.GetString("toolAggGroupsListIncludedValues");
            btnGroupUp.Text = _host.Language.GetString("toolAggGroupUp");
            btnGroupDown.Text = _host.Language.GetString("toolAggGroupDown");
            btnGroupDelete.Text = _host.Language.GetString("toolAggGroupDelete");
            btnNewGroup.Text = _host.Language.GetString("toolAggGroupNew");
            grbGroupInfo.Text = _host.Language.GetString("toolAggGroupInfoBox");
            btnSaveGroup.Text = _host.Language.GetString("toolAggGroupSave");
            btnCancelEdit.Text = _host.Language.GetString("toolAggGroupCancelEdit");
            btnEditGroup.Text = _host.Language.GetString("toolAggGroupEdit");
            lbCode.Text = _host.Language.GetString("toolAggGroupCode");
            lbText.Text = _host.Language.GetString("toolAggGroupText");
            lstGroup.Columns[0].Text = _host.Language.GetString("toolAggGroupValueListCode");
            lstGroup.Columns[1].Text = _host.Language.GetString("toolAggGroupValueListText");
            grpValueset.Text = _host.Language.GetString("toolAggAggregationValuesetBox");
            lbVsName.Text = _host.Language.GetString("toolAggAggregationValuesetName");
            lbVsDomain.Text = _host.Language.GetString("toolAggAggregationValuesetDomain");
            lblVsFile.Text = _host.Language.GetString("toolAggAggregationValuesetFile");
            btnBrowseVsFile.Text = _host.Language.GetString("toolAggAggregationValuesetFileBrowse");
            grpAvailableValues.Text = _host.Language.GetString("toolAggAggregationAvailableBox");
            lstAvailable.Columns[0].Text = _host.Language.GetString("toolAggAggregationAvailableCode");
            lstAvailable.Columns[1].Text = _host.Language.GetString("toolAggAggregationAvailableText");
            tsbSave.Text = _host.Language.GetString("toolAggAggregationSaveFile");
            lblMessOverlappingOrIllegalValues.Text = _host.Language.GetString("toolAggMessOverlappingOrIllegalValues");
        }

        private void AggregationForm_Load(object sender, EventArgs e)
        {
            
            SwitchLanguage(_host.Language.CurrentLanguage);
            if (_vsFilename != null && Id != null)
            {
                _host.RegisterFileNotifier(_vsFilename, Id);
            }
            if (_aggFilename != null && Id != null)
            {
                _host.RegisterFileNotifier(_aggFilename, Id);
            }

            UpdateNumberOfItems();
        }

        public StatusStrip Statusbar
        {
            get { return statusStrip1; }
        }

        

        public ToolStrip Toolbar
        {
            get { return toolStrip1; }
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

        private void VsFile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (sender is LinkLabel)
            {
                if ((!string.IsNullOrWhiteSpace(Valueset)) && (System.IO.File.Exists(Valueset)))
                {
                    _host.Show(Valueset);
                }
            }

        }

        public void Reload()
        {
            if (_saveClicked)
            {
                _saveClicked = false;
                return;
            }

            if (MessageBox.Show(string.Format(_host.Language.GetString("toolAggAggFileChangedMessage"), System.IO.Path.GetFileName(Path)), _host.Language.GetString("toolAggFileChangedCaption"), MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                LoadData(_aggFilename, _vsFilename);
            }
        }


        public bool CanClose()
        {
            if (AggToolUtils.ValueIsChanged(_loadedName, txtName.Text) || AggToolUtils.ValueIsChanged(_loadedMap, txtMap.Text))
            {
                _dirty = true;
            }

            if (_dirty)
            {
                if (!string.IsNullOrWhiteSpace(Path))
                {
                    _host.Show(Path);
                }

                SaveChangesDialog dialog = new SaveChangesDialog(System.IO.Path.GetFileName(_aggFilename));
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
