using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace PCAxis.Desktop.SavedQuery
{
    public class BatchQuery
    {
        HashSet<string> _files = new HashSet<string>();

        /// <summary>
        /// List of PXSQ files
        /// </summary>
        public HashSet<string> PxsqFiles { get {return _files;}  }

        /// <summary>
        /// The output path 
        /// </summary>
        public string OutputPath { get; set; }

        /// <summary>
        /// The output format
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// Reads a PXT file and adds the information into the collections
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public int AddFromPxt(string filename)
        {
            XDocument doc = XDocument.Load(filename);
             
            if (doc.Root.Attribute("output") != null) { OutputPath = doc.Root.Attribute("output").Value; }
            if (doc.Root.Attribute("format") != null) { Format = doc.Root.Attribute("format").Value; }

            var x = from xnode in doc.Element("px-batch").Elements("saved-queries").Elements("saved-query")
                    select xnode.Value;

            int count = 0;
            foreach (var file in x)
            {
                if (PxsqFiles.Add(file)) { count++; }
            }

            return count;
        }

       

        /// <summary>
        /// Saves the data as a PXT file
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public bool Save(string filename)
        {
            XDocument doc =
                new XDocument(
                      new XDeclaration("1.0", Encoding.UTF8.HeaderName, String.Empty),
                      new XElement("px-batch", new XAttribute("output", OutputPath == null ? "" : OutputPath), new XAttribute("format", Format == null ? "" : Format),
                            new XElement("saved-queries", 
                                  PxsqFiles.Select(file => new XElement("saved-query", file))
                            )
                      )
                );
            doc.Save(filename);

            return true;
        }

        /// <summary>
        /// Runs all the saved querys
        /// </summary>
        /// <param name="outputPath"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public bool Run(Action<string, bool, string> resultCallback, string outputPath = null, string format = null)
        {
            string output = outputPath == null ? OutputPath : outputPath;
            string fmt = format == null ? Format : format;

            foreach (var pxsq in PxsqFiles)
            {
                try
                {
                    var result = SavedQueryResult.Create(pxsq);
                    if (result.Save(output, fmt))
                    {
                        resultCallback(pxsq, true, null);
                    }
                    else
                    {
                        resultCallback(pxsq, false, null);
                    }
                    result.Model.Dispose();
                }
                catch (Exception ex)
                {
                    resultCallback(pxsq, false, ex.Message);
                }
                
            }

            return true;
        }

    }
}
