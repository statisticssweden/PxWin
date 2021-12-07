using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PXWin.AggregationTool
{
    public class AggToolUtils
    {
        public static bool ValueIsChanged(string val1, string val2)
        {
            if (string.IsNullOrWhiteSpace(val1) && string.IsNullOrWhiteSpace(val2))
            {
                return false;
            }

            return (string.Compare(val1, val2) != 0);
        }
    }
}
