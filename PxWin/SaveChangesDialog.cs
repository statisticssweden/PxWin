using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PCAxis.Desktop
{
    public partial class SaveChangesDialog : Form
    {
        private SaveChangesDialog(string filename)
        {
            InitializeComponent();

            this.Text = Lang.GetLocalizedString("SaveChangesTitle");
            lblQuestion.Text = string.Format(Lang.GetLocalizedString("SaveChangesQuestion"), filename);
            btnSave.Text = Lang.GetLocalizedString("SaveChangesYes");
            btnDontSave.Text = Lang.GetLocalizedString("SaveChangesNo");
            btnCancel.Text = Lang.GetLocalizedString("SaveChangesCancel");
        }

        public static new DialogResult Show(string filename)
        {
            SaveChangesDialog frm = new SaveChangesDialog(filename);
            DialogResult result = frm.ShowDialog();
            frm.Close();
            return result;
        }

    }
}
