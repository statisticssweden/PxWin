using PCAxis.Paxiom;
using PX.Plugin.Interfaces;
using PX.Plugin.Interfaces.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCAxis.Desktop
{
    [Export]
    public class MEFPlumber
    {

        [ImportMany(AllowRecomposition = true)]
        private IEnumerable<Lazy<Func<IPXModelStreamSerializer>, ISerializerMetadata>> _saveAsFormats;
        [ImportMany(AllowRecomposition = true)]
        private IEnumerable<Lazy<IDataSource, IDataSourceMetadata>> _dataSources;

        public void RegisterSavedQueryDependencies()
        {
            foreach (var serializer in _saveAsFormats)
            {
                SavedQueryResult.AddSerializer(serializer.Value, serializer.Metadata);
            }

            foreach (var datasource in _dataSources)
            {
                SavedQueryResult.AddDatasource(datasource.Metadata.SourceType, datasource.Value);
            }
        }
    }
}
