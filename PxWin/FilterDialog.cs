using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PCAxis.Paxiom;

namespace PCAxis.Desktop
{
    public partial class FilterDialog : Form
    {
        private PCAxis.Paxiom.Variable _variable;
        private List<string> _valueCodes = new List<string>();
        // The column we are currently using for sorting.
        private ColumnHeader SortingColumn = null;
       
        /// <summary>
        /// List of value codes from the selected filter
        /// </summary>
        public List<string> ValueCodes 
        {
            get
            { 
                return _valueCodes;
            }
        }

        public FilterDialog()
        {
            InitializeComponent();
            SetLocalizedTexts();
            ResizeFilterControls();
            InitActionButton();
            InitFilters();
        }

        public PCAxis.Paxiom.Variable Variable
        {
            get
            {
                return _variable;
            }
            set
            {
                _variable = value;
                InitializeDialog();
            }
        }

        /// <summary>
        /// Selection tab
        /// Initialize list of avaliable filters
        /// </summary>
        private void InitFilters()
        {
            lvFilters.Columns.Clear();
            lvFilters.Items.Clear();

            lvFilters.Columns.Add(Lang.GetLocalizedString(Lang.GetLocalizedString("FilterDialogTabSelectFilternameColumn")), 300);
            lvFilters.Columns.Add(Lang.GetLocalizedString(Lang.GetLocalizedString("FilterDialogTabSelectFiltertypeColumn")), 150);
            
            
            List<VariableFilter> lst = new List<VariableFilter>();

            lst = VariableFilterHelper.GetAllFilterWithoutValues();
            
            //// TODO: Get from helper...

            //VariableFilter filter = new VariableFilter();
            //filter.FilterName = "Mitt filter";
            //filter.GlobalFilter = false;
            //filter.Path = @"C:\Users\scbmino\AppData\Roaming\scb\pxwin\variableFilter\år.xml";
            //filter.ValueCodes = new List<string>();
            //lst.Add(filter);

            //filter = new VariableFilter();
            //filter.FilterName = "Ditt filter";
            //filter.GlobalFilter = true;
            //filter.Path = @"C:\Users\scbmino\AppData\Roaming\scb\pxwin\variableFilter\test.xml";
            //filter.ValueCodes = new List<string>();
            //lst.Add(filter);

            foreach (VariableFilter f in lst)
            {
                ListViewItem itm = lvFilters.Items.Add(f.FilterName);
                itm.Tag = f;

                string filterType = f.GlobalFilter ? Lang.GetLocalizedString("FilterDialogTabSelectSharedFilter") : Lang.GetLocalizedString("FilterDialogTabSelectPersonalFilter");
                itm.SubItems.Add(filterType);
            }
        }

        private void InitializeDialog()
        {
            // --- Select filter tab ---
            btnRemove.Enabled = false;

            //// --- Create filter tab ---
            //lstValues.Items.Clear();
            //((ListBox)lstValues).DataSource = _variable.Values;
            //((ListBox)lstValues).DisplayMember = "Text";
            //((ListBox)lstValues).ValueMember = "Code";

            foreach (Value value in _variable.Values)
            {
                ListViewItem itm = lvValues.Items.Add(value.Text);
                itm.Tag = value;
            }

            lblSaveError.Text = "";
        }

        private void SetLocalizedTexts()
        {
            this.Text = Lang.GetLocalizedString("FilterDialogTitle");
            btnCancel.Text = Lang.GetLocalizedString("FilterDialogCancel");
            tabSelect.Text = Lang.GetLocalizedString("FilterDialogTabSelect");

            // -- Select filter tab --
            lblSelectDescription.Text = Lang.GetLocalizedString("FilterDialogTabSelectDescription");
            toolTipRemove.SetToolTip(btnRemove, Lang.GetLocalizedString("FilterDialogTabSelectDeleteFilter"));
            
            // --- Create filter tab ---
            tabCreate.Text = Lang.GetLocalizedString("FilterDialogTabCreate");
            lblValues.Text = Lang.GetLocalizedString("FilterDialogTabCreateValues");
            lblCreateDescription.Text = Lang.GetLocalizedString("FilterDialogTabCreateDescription");
            btnImport.Text = Lang.GetLocalizedString("FilterDialogTabCreateImport");
            lblName.Text = Lang.GetLocalizedString("FilterDialogTabCreateName");
        }

        private void btnAction_Click(object sender, EventArgs e)
        {
            if (tabControlFilter.SelectedIndex == 0)
            {
                UseFilter();
            }
            else
            {
                SaveFilter();
            }
        }

        private void UseFilter()
        {
            if (lvFilters.SelectedItems.Count != 1)
            {
                return;
            }

            VariableFilter filter = (VariableFilter)lvFilters.SelectedItems[0].Tag;
            filter.GetValueCodes();

            _valueCodes.AddRange(filter.ValueCodes);
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void SaveFilter()
        {
            if (!CheckSaveFilter())
            {
                return;
            }

            List<string> values = new List<string>();

            foreach (ListViewItem itm in lvValues.CheckedItems)
            {
                Value value = (Value)itm.Tag;
                values.Add(value.Code);
            }

            string filter = txtName.Text;

            VariableFilterHelper.SaveFilter(values, _variable.Domain, filter);

            foreach (ListViewItem listItem in lvValues.Items) 
            {  
                listItem.Checked = false;  
            }  

            txtName.Text = "";

            InitFilters();
            tabControlFilter.SelectedTab = tabSelect;
            FocusFilter(filter);
            
        }


        /// <summary>
        /// Select tab
        /// Selects and focuses the filter with the specified name in the list of filters
        /// </summary>
        /// <param name="name"></param>
        private void FocusFilter(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return;
            }

            ListViewItem itm = lvFilters.Items.Cast<ListViewItem>().Where(i => i.Text.Equals(name)).Single();

            if (itm != null)
            {
                itm.Selected = true;
                itm.EnsureVisible();
            }
        }

        private bool CheckSaveFilter()
        {
            if (lvValues.CheckedItems.Count == 0)
            {
                ShowError(Lang.GetLocalizedString("FilterDialogTabCreateErrorNoValues"));
                return false;
            }

            if (txtName.Text.Length == 0)
            {
                ShowError(Lang.GetLocalizedString("FilterDialogTabCreateErrorNoName"));
                return false;
            }

            if (VariableFilterHelper.FilterExists(txtName.Text))
            {
                if (MessageBox.Show(Lang.GetLocalizedString("FilterDialogTabCreateFilterExistsText"), Lang.GetLocalizedString("FilterDialogTabCreateFilterExistsCaption"), MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                {
                    return false;
                }
            }
            
            return true;
        }

        private void ShowError(string error)
        {
            lblSaveError.Text = error;
        }

        private void FilterDialog_Resize(object sender, EventArgs e)
        {
            ResizeFilterControls();
        }

        private void ResizeFilterControls()
        {
            lblCreateDescription.MaximumSize = new Size(this.Width - 60, 40);
        }

        private void InitActionButton()
        {
            if (tabControlFilter.SelectedIndex == 0)
            {
                btnAction.Text = Lang.GetLocalizedString("FilterDialogTabSelectUseFilter");
            }
            else
            {
                btnAction.Text = Lang.GetLocalizedString("FilterDialogTabCreateSave");
            }
        }

        private void tabControlFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitActionButton();
        }

        private void lstValues_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            ClearSaveError();
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            ClearSaveError();
        }

        private void ClearSaveError()
        {
            lblSaveError.Text = "";
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openDialog = new OpenFileDialog())
            {
                openDialog.Filter = "Filter-file (*.TXT)|*.txt";
                openDialog.FilterIndex = 0;

                if (openDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    try
                    {
                        List<string> codes = VariableFilterHelper.ImportValues(openDialog.FileName);
                        for (int i = 0; i < lvValues.Items.Count; i++)
                        {
                            Value value = (Value)lvValues.Items[i].Tag;
                            if (codes.Contains(value.Code))
                            {
                                //lvValues.SetItemChecked(i, true);
                                lvValues.Items[i].Checked = true;
                            }
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Error importing file");
                    }
                }
            }
        }

        /// <summary>
        /// Selection tab
        /// Occurs when the user selects a filter in the list of available filters
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvFilters_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvFilters.SelectedItems.Count < 1)
            {
                return;
            }

            VariableFilter filter = (VariableFilter)lvFilters.SelectedItems[0].Tag; // Only one filter can be selected

            btnRemove.Enabled = !filter.GlobalFilter;
        }

        /// <summary>
        /// Selection tab
        /// Delete filter button clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lvFilters.SelectedItems.Count < 1)
            {
                return;
            }

            VariableFilter filter = (VariableFilter)lvFilters.SelectedItems[0].Tag; // Only one filter can be selected
            if (MessageBox.Show(Lang.GetLocalizedString("FilterDialogTabSelectDeleteFilterText"), Lang.GetLocalizedString("FilterDialogTabSelectDeleteFilterCaption"), MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                VariableFilterHelper.DeleteVariableFilterFile(filter);
                InitFilters();
            }

        }

        private void lvFilters_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Get the new sorting column.
            ColumnHeader new_sorting_column = lvFilters.Columns[e.Column];

            // Figure out the new sorting order.
            System.Windows.Forms.SortOrder sort_order;
            if (SortingColumn == null)
            {
                // New column. Sort ascending.
                sort_order = SortOrder.Ascending;
            }
            else
            {
                // See if this is the same column.
                if (new_sorting_column == SortingColumn)
                {
                    // Same column. Switch the sort order.
                    if (SortingColumn.Text.StartsWith("> "))
                    {
                        sort_order = SortOrder.Descending;
                    }
                    else
                    {
                        sort_order = SortOrder.Ascending;
                    }
                }
                else
                {
                    // New column. Sort ascending.
                    sort_order = SortOrder.Ascending;
                }

                // Remove the old sort indicator.
                SortingColumn.Text = SortingColumn.Text.Substring(2);
            }

            // Display the new sort order.
            SortingColumn = new_sorting_column;
            if (sort_order == SortOrder.Ascending)
            {
                SortingColumn.Text = "> " + SortingColumn.Text;
            }
            else
            {
                SortingColumn.Text = "< " + SortingColumn.Text;
            }

            // Create a comparer.
            lvFilters.ListViewItemSorter =
                new ListViewComparer(e.Column, sort_order);

            // Sort.
            lvFilters.Sort();

        }

    }




    // Compares two ListView items based on a selected column.
    public class ListViewComparer : System.Collections.IComparer
    {
        private int ColumnNumber;
        private SortOrder SortOrder;

        public ListViewComparer(int column_number,
            SortOrder sort_order)
        {
            ColumnNumber = column_number;
            SortOrder = sort_order;
        }

        // Compare two ListViewItems.
        public int Compare(object object_x, object object_y)
        {
            // Get the objects as ListViewItems.
            ListViewItem item_x = object_x as ListViewItem;
            ListViewItem item_y = object_y as ListViewItem;

            // Get the corresponding sub-item values.
            string string_x;
            if (item_x.SubItems.Count <= ColumnNumber)
            {
                string_x = "";
            }
            else
            {
                string_x = item_x.SubItems[ColumnNumber].Text;
            }

            string string_y;
            if (item_y.SubItems.Count <= ColumnNumber)
            {
                string_y = "";
            }
            else
            {
                string_y = item_y.SubItems[ColumnNumber].Text;
            }

            // Compare them.
            int result;
            double double_x, double_y;
            if (double.TryParse(string_x, out double_x) &&
                double.TryParse(string_y, out double_y))
            {
                // Treat as a number.
                result = double_x.CompareTo(double_y);
            }
            else
            {
                DateTime date_x, date_y;
                if (DateTime.TryParse(string_x, out date_x) &&
                    DateTime.TryParse(string_y, out date_y))
                {
                    // Treat as a date.
                    result = date_x.CompareTo(date_y);
                }
                else
                {
                    // Treat as a string.
                    result = string_x.CompareTo(string_y);
                }
            }

            // Return the correct result depending on whether
            // we're sorting ascending or descending.
            if (SortOrder == SortOrder.Ascending)
            {
                return result;
            }
            else
            {
                return -result;
            }
        }
    }
}
