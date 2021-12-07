using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCAxis.Desktop.Grid
{
    /// <summary>
    /// Used to store metadata about the table
    /// </summary>
    /// <remarks></remarks>
    public class ColumnAndRowMeta
    {
        private int _rows;
        /// <summary>
        /// Gets or set the number of rows in the table
        /// </summary>
        /// <value>Number of rows in the table</value>
        /// <returns>The number of rows in the table</returns>
        /// <remarks></remarks>
        public int Rows
        {
            get { return _rows; }
        }

        private int _columns;
        /// <summary>
        /// Gets  the number of columns in the table
        /// </summary>
        /// <value>Number of columns in the table</value>
        /// <returns>The number of columns in the table</returns>
        /// <remarks></remarks>
        public int Columns
        {
            get { return _columns; }
        }


        private int _rowOffset;
        /// <summary>
        /// Gets  the number of rows that contains headers
        /// </summary>
        /// <value>Number of rows that contains headers</value>
        /// <returns>The number of rows that contains headers</returns>
        /// <remarks></remarks>
        public int RowOffset
        {
            get { return _rowOffset; }
        }

        private int _columnOffset;
        /// <summary>
        /// Gets the number of columns that contains headers
        /// </summary>
        /// <value>Number of columns that contains headers</value>
        /// <returns>The number of columns that contains headers</returns>
        /// <remarks></remarks>
        public int ColumnOffset
        {
            get { return _columnOffset; }
        }
        private bool _useHierarchy;
        /// <summary>
        /// Gets if the table uses hierarchy
        /// </summary>
        /// <value>If the table uses hierarchy</value>
        /// <returns>If the table uses hierarchy</returns>
        /// <remarks></remarks>
        public bool UseHierarchy
        {
            get { return _useHierarchy; }
        }

        /// <summary>
        /// Initialize a new <see cref="ColumnAndRowMeta" />
        /// </summary>
        /// <param name="rows">The number of rows</param>
        /// <param name="columns">The number of columns</param>
        /// <param name="columnOffset">The number of columns that contains headers</param>
        /// <param name="rowOffset">The number of rows that contains headers</param>
        /// <param name="useHierarchy">Whether the table uses hierarchy or not</param>
        /// <remarks></remarks>
        public ColumnAndRowMeta(int rows, int columns, int columnOffset, int rowOffset, bool useHierarchy)
        {
            this._columns = columns;
            this._rows = rows;
            this._columnOffset = columnOffset;
            this._rowOffset = rowOffset;
            this._useHierarchy = useHierarchy;
        }
    }
}
