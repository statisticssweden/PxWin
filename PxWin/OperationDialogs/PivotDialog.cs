using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PCAxis.Paxiom;
using PCAxis.Paxiom.Operations;

namespace PCAxis.Desktop.OperationDialogs
{
    public partial class PivotDialog : Form
    {
        private PXModel _Model;
        public PivotDialog()
        {
            InitializeComponent();
            SetLanguage();
        }

        private void SetLanguage()
        {
            chStub.Text = Lang.GetLocalizedString("OperationColumnHeaderStub");
            chHeading.Text = Lang.GetLocalizedString("OperationColumnHeaderHeading");
            this.Text = Lang.GetLocalizedString("OperationPivot");
            btnOk.Text = Lang.GetLocalizedString("Ok");
            btnCancel.Text = Lang.GetLocalizedString("Cancel");
        }

        public PXModel SelectedModel {
            get { return _Model; }
            set
            {
                _Model = value;              
                RefreshList();
            } 
        }

        private void RefreshList()
        {
            lvStub.Items.Clear();
            lvHeading.Items.Clear();

            foreach (var stub in SelectedModel.Meta.Stub)
            {
                lvStub.Items.Add(stub.Name);
            }

            foreach (var heading in SelectedModel.Meta.Heading)
            {
                lvHeading.Items.Add(heading.Name);
            }
        }

        public PivotDescription[] GetPivotDescriptions()
        {
            if (SelectedModel == null)
            {
                return null;
            }
            var pd = new PivotDescription[SelectedModel.Meta.Variables.Count];
            var index = 0;

            //Heading
            for (var i = 0; i <= lvHeading.Items.Count - 1; i++)
            {
                pd[index] = new PivotDescription(lvHeading.Items[i].Text, PlacementType.Heading);
                index += 1;
            }

            //Stub
            for (var i = 0; i <= lvStub.Items.Count - 1; i++)
            {
                pd[index] = new PivotDescription(lvStub.Items[i].Text, PlacementType.Stub);
                index += 1;
            }

            return pd;
        }
        #region Events
        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnStubToHeading_Click(object sender, EventArgs e)
        {
            if (lvStub.SelectedItems.Count < 1) return;
            var item = lvStub.SelectedItems[0];            
            lvStub.Items.Remove(item);
            lvHeading.Items.Add(item);
            lvHeading.Focus();

        }

        private void btnHeadingToStub_Click(object sender, EventArgs e)
        {
            if (lvHeading.SelectedItems.Count < 1) return;
            var item = lvHeading.SelectedItems[0];            
            lvHeading.Items.Remove(item);
            lvStub.Items.Add(item);
            lvStub.Focus();
        }
        #endregion

        private void listview_MouseDown(object sender, MouseEventArgs e)
        {
            if (!(sender is ListView)) return;
            
            var lst = (ListView)sender;           
            var item = lst.GetItemAt(e.X, e.Y);
            
            if (item != null)
            {
                item.Selected = true;
                DoDragDrop(item, DragDropEffects.Move);
            }
        }

        private void listview_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("System.Windows.Forms.ListViewItem"))
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void listview_DragDrop(object sender, DragEventArgs e)
        {

            if (!(sender is ListView)) return;
            
            var lstDest = (ListView)sender;

            if (!e.Data.GetDataPresent("System.Windows.Forms.ListViewItem")) return;

            var item = (ListViewItem)e.Data.GetData("System.Windows.Forms.ListViewItem");
            var lstSrc = item.ListView;

                
            var p = lstDest.PointToClient(new Point(e.X, e.Y));
            var hoverItem = lstDest.GetItemAt(p.X, p.Y);

            if (ReferenceEquals(lstSrc, lstDest))
            {
                if (!ReferenceEquals(item, hoverItem))
                {
                    if (hoverItem == null)
                    {
                        lstSrc.Items.Remove(item);
                        lstDest.Items.Add(item);
                    }
                    else
                    {
                        var index = hoverItem.Index;
                        lstSrc.Items.Remove(item);
                        lstDest.Items.Insert(index, item);
                    }
                }
            }
            else
            {
                lstSrc.Items.Remove(item);
                if (hoverItem == null)
                {
                    lstDest.Items.Add(item);
                }
                else
                {
                    lstDest.Items.Insert(hoverItem.Index, item);
                }
            }
        }
    }
}
