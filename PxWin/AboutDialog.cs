using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

namespace PCAxis.Desktop
{
    public partial class AboutDialog : Form
    {
        private Assembly assembly;
            
        public AboutDialog()
        {
            InitializeComponent();

            this.Text = Lang.GetLocalizedString("AboutPXWin");
            btnClose.Text = Lang.GetLocalizedString("Ok");

            assembly = Assembly.GetEntryAssembly();

            if (assembly == null)
            {
                return;
            }

            lblApplication.Text = ProductTitle;
            lblVersion.Text = Version;
            lblCopyright.Text = Copyright;
        }

        /// <summary>
        /// Gets the title property
        /// </summary>
        private string ProductTitle
        {
            get
            {
                return GetAttributeValue<AssemblyTitleAttribute>(a => a.Title,
                       Path.GetFileNameWithoutExtension(assembly.CodeBase));
            }
        }

        /// <summary>
        /// Gets the application's version
        /// </summary>
        public static string Version
        {
            get
            {
                string result = string.Empty;
                Version version = Assembly.GetEntryAssembly().GetName().Version;
                if (version != null)
                    result = version.ToString();
                else
                    result =  "1.0.0.0";

                return Lang.GetLocalizedString("VersionText") + " " + result;
            }
        }

        /// <summary>
        /// Gets the description about the application.
        /// </summary>
        private string Description
        {
            get { return GetAttributeValue<AssemblyDescriptionAttribute>(a => a.Description); }
        }


        /// <summary>
        ///  Gets the product's full name.
        /// </summary>
        private string Product
        {
            get { return GetAttributeValue<AssemblyProductAttribute>(a => a.Product); }
        }

        /// <summary>
        /// Gets the copyright information for the product.
        /// </summary>
        private string Copyright
        {
            get { return GetAttributeValue<AssemblyCopyrightAttribute>(a => a.Copyright); }
        }

        /// <summary>
        /// Gets the company information for the product.
        /// </summary>
        private string Company
        {
            get { return GetAttributeValue<AssemblyCompanyAttribute>(a => a.Company); }
        }


        protected string GetAttributeValue<TAttr>(Func<TAttr, string> resolveFunc, string defaultResult = null) where TAttr : Attribute
        {
            object[] attributes = assembly.GetCustomAttributes(typeof(TAttr), false);
            if (attributes.Length > 0)
                return resolveFunc((TAttr)attributes[0]);
            else
                return defaultResult;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
