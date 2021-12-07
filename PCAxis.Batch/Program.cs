using PCAxis.Desktop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCAxis.Desktop.SavedQuery;
using System.Globalization;
using System.Configuration;
using System.Threading;
using System.ComponentModel.Composition.Hosting;

namespace PCAxis.Batch
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new StartOptions();
            if (CommandLine.Parser.Default.ParseArguments(args, options))
            {


                MEFBooter.Container.GetExportedValue<MEFPlumber>().RegisterSavedQueryDependencies();

                //Set the defaultLanguage
                CultureInfo culture = new CultureInfo(string.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("defaultLanguage")) ? "en" : ConfigurationManager.AppSettings.Get("defaultLanguage"));
                Thread.CurrentThread.CurrentCulture = culture;

                Console.WriteLine("Running PX-Batch");

                if (!(options.IsPxsqFile || options.IsPxtFile))
                {
                    //TODO localize error
                    Console.WriteLine("Either PXT och PXSQ file must be supplied");
                    return;
                }
                if (options.IsPxsqFile)
                {
                    if (string.IsNullOrEmpty(options.OutputFormat) || string.IsNullOrEmpty(options.OutputPath))
                    {
                        Console.WriteLine("Output and format must be specified");
                        return;
                    }

                    SavedQueryResult sqr = SavedQueryResult.Create(options.Files[0]);

                    sqr.Save(options.OutputPath, options.OutputFormat);

                }
                else if (options.IsPxtFile)
                {
                    var batch = new BatchQuery();
                    batch.AddFromPxt(options.Files[0]);
                    Console.WriteLine("Start running");
                    batch.Run((file, ok, error) =>
                    {
                        Console.Write(file);
                        if (ok)
                        {
                            Console.WriteLine("...OK");
                        }
                        else
                        {
                            Console.WriteLine("...Error");
                        }
                    }, outputPath: options.OutputPath, format: options.OutputFormat);
                }
            }
        }


    }
}
