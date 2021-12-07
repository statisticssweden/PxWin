using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PCAxis.Paxiom;

namespace PCAxis.Desktop.UserControls
{
    public partial class TextCodeControl : UserControl
    {
        public Variable Variable { get; set; }

        public TextCodeControl(Variable variable)
        {
            Variable = variable;
            InitializeComponent();
            InitControls();
        }

        private void InitControls()
        {          
            gbVariable.Text = Variable.Name;
            rbText.Text = Lang.GetLocalizedString("ChangeText");
            rbCode.Text = Lang.GetLocalizedString("ChangeCode");
            rbCodeText.Text = Lang.GetLocalizedString("ChangeTextAndCode");
            rbText.AutoCheck = rbCode.AutoCheck = rbCodeText.AutoCheck = true;
        }

        public KeyValuePair<string, HeaderPresentationType> GetSelection()
        {
            HeaderPresentationType headerType;
            if (rbText.Checked)
            {
                headerType = HeaderPresentationType.Text;
            }
            else if (rbCode.Checked)
            {
                headerType = HeaderPresentationType.Code;
            }
            else if(rbCodeText.Checked)
            {
                headerType = HeaderPresentationType.CodeAndText;
            }
            else
            {
                return new KeyValuePair<string, HeaderPresentationType>();
            }

            return new KeyValuePair<string, HeaderPresentationType>(Variable.Code, headerType);
        }
    }
}
