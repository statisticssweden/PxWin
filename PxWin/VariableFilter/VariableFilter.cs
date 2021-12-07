using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

namespace PCAxis.Desktop
{
    class VariableFilter
    {
        //Om filtret inte ligger i den personliga mappen GlobalFilter = true
        public bool GlobalFilter { get; set; }
        public string FilterName { get; set; }
        public string Path { get; set; }
        public string Domain { get; set; }
        public List<string> ValueCodes { get; set; }

        public VariableFilter() { }

        //public VariableFilter(bool globalFilter, string filterName, string path, List<string> valueCodes)
        //{
        //    GlobalFilter = globalFilter;
        //    FilterName = filterName;
        //    Path = path;
        //    ValueCodes = valueCodes;
        
        //}

        //public VariableFilter(bool globalFilter, List<string> valueCodes)
        //{
        //    GlobalFilter = globalFilter;
        //    ValueCodes = valueCodes;

        //}

   
        //TODO: fördigställa detta
        /// <summary>
        /// Add a list of valucodes to a variableFilter object
        /// </summary>
        /// <returns>A variablefilter object containing a List of valcodes</returns>
        public void GetValueCodes()
        {
            List<string> valueCode = new List<string>();

            if (! string.IsNullOrEmpty(this.Path) &&  File.Exists(this.Path))
            {
                XmlDocument xdoc = new XmlDocument();
                xdoc.Load(this.Path);

                string xpathDomain = "//domain";
                XmlNode rootDomain = xdoc.SelectSingleNode(xpathDomain);

                this.Domain = rootDomain.InnerText;

                string xpath = "//values";
                XmlNode root = xdoc.SelectSingleNode(xpath);
                foreach (XmlNode node in root.SelectNodes("./value"))
                {
                    valueCode.Add(node.Attributes["code"].Value);

                }
                this.ValueCodes = valueCode;
            }
           
        }
        //egenskap för personlig eller global
        //filter namn
        //sökväg till filterfil
        //en metod get valuecodes 
    }
}
