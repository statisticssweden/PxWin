using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using CommandLine;

namespace PCAxis.Desktop
{
    public class StartOptions
    {
        [Option('d', "database", Required = false, HelpText = "The database")]
        public string Database { get; set; }

        [Option('t', "table", Required = false, HelpText = "The table")]
        public string Table { get; set; }

        [Option('o', "output", Required = false, HelpText = "The output path")]
        public string OutputPath { get; set; }

        [Option('f', "format", Required = false, HelpText = "The output format")]
        public string OutputFormat { get; set; }

        [ValueList(typeof(List<string>), MaximumElements = 1)]
        public IList<string> Files { get; set; }

        public bool IsPxFile 
        {
            get
            {
                if (Files.Count > 0 && Files[0].EndsWith(".px", StringComparison.InvariantCultureIgnoreCase)) 
                {
                    return true;
                }
                return false;
            }
        }

        public bool IsPxsqFile
        {
            get
            {
                if (Files.Count > 0 && Files[0].EndsWith(".pxsq", StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
                return false;
            }
        }

        public bool IsPxtFile
        {
            get
            {
                if (Files.Count > 0 && Files[0].EndsWith(".pxt", StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
                return false;
            }
        }

        public StartOptions()
        {
            Files = new List<string>();
        }
    }
}
