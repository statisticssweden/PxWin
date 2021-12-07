using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PX.Plugin.Interfaces
{
    /// <summary>
    /// Interface for a PX database
    /// </summary>
    public interface IDatabaseInfo
    {
        /// <summary>
        /// Db id
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Db name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Get database related information
        /// </summary>
        /// <param name="parameter">Paramaeter id</param>
        /// <returns>Value</returns>
        string GetValue(string parameter);
    }
}
