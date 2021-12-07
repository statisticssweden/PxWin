using PX.Desktop.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;

namespace PCAxis.Desktop
{
    static class Program
    {

        //[System.Runtime.InteropServices.DllImport("kernel32.dll")]
        //private static extern bool AllocConsole();

        //[System.Runtime.InteropServices.DllImport("kernel32.dll")]
        //private static extern bool AttachConsole(int pid);

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Cleanup PX temporary files that for some reason have not been deleted before
            Paxiom.PXData.RemovePxTempFiles();

            var options = new StartOptions();
            if (!CommandLine.Parser.Default.ParseArguments(args, options))
            {
                //TODO Fix localized string
                MessageBox.Show("Invalid start parameters");
            }
            else 
            {
                MainForm form = MEFBooter.Container.GetExportedValue<IHost>() as MainForm;
                form.Init(options);
                Application.Run(form);
                //Application.Run(new MainForm(options));
            }

        }
    }
}
