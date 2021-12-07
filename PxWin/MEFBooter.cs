using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;

namespace PCAxis.Desktop
{
    public class MEFBooter
    {
        public static CompositionContainer Container { get; set; }

        static MEFBooter()
        {
            Compose();
        }

        private static void Compose()
        {
            var catalog = new AggregateCatalog();
            var assembly = typeof(MainForm).Assembly;
            catalog.Catalogs.Add(new AssemblyCatalog(assembly));

            var path = System.IO.Path.GetDirectoryName(assembly.Location);
            path = System.IO.Path.Combine(path, "plugins");

            if (System.IO.Directory.Exists(path))
            {
                catalog.Catalogs.Add(new DirectoryCatalog(path));
            }
            Container = new CompositionContainer(catalog);
        }
    }
}
