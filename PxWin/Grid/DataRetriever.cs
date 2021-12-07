using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCAxis.Paxiom;
using System.Data;

namespace PCAxis.Desktop.Grid
{
    public interface IDataPageRetriever
    {
        DataTable SupplyPageOfData(int lowerPageBoundary, int rowsPerPage);
    }

    public class DataRetriever : IDataPageRetriever
    {
        //private string tableName;
        //private SqlCommand command;
        private PXModel _model;
        private PCAxis.Paxiom.DataFormatter _dataFormatter;

        public DataRetriever(PXModel model)
        {
            _model = model;
            _dataFormatter = new DataFormatter(model);

            DataTable table = new DataTable();
            for (int col = 0; col < _model.Data.MatrixColumnCount; col++)
            {
                table.Columns.Add(new DataColumn(col.ToString()));
            }

            columnsValue = table.Columns;
      
        }

        private int rowCountValue = -1;

        public int RowCount
        {
            get
            {
                // Return the existing value if it has already been determined.
                if (rowCountValue != -1)
                {
                    return rowCountValue;
                }

                rowCountValue = _model.Data.MatrixRowCount;
                return rowCountValue;
            }
        }

        private DataColumnCollection columnsValue;

        public DataColumnCollection Columns
        {
            get
            {
                // Return the existing value if it has already been determined.
                if (columnsValue != null)
                {
                    return columnsValue;
                }

                //// Retrieve the column information from the database.
                //command.CommandText = "SELECT * FROM " + tableName;
                //SqlDataAdapter adapter = new SqlDataAdapter();
                //adapter.SelectCommand = command;
                //DataTable table = new DataTable();
                //table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                //adapter.FillSchema(table, SchemaType.Source);

                DataTable table = new DataTable();
                for (int col = 0; col < _model.Data.MatrixColumnCount; col++)
                {
                    table.Columns.Add(new DataColumn(col.ToString()));
                }

                columnsValue = table.Columns;
                return columnsValue;
            }
        }

        //private string commaSeparatedListOfColumnNamesValue = null;

        //private string CommaSeparatedListOfColumnNames
        //{
        //    get
        //    {
        //        // Return the existing value if it has already been determined.
        //        if (commaSeparatedListOfColumnNamesValue != null)
        //        {
        //            return commaSeparatedListOfColumnNamesValue;
        //        }

        //        // Store a list of column names for use in the
        //        // SupplyPageOfData method.
        //        System.Text.StringBuilder commaSeparatedColumnNames =
        //            new System.Text.StringBuilder();
        //        bool firstColumn = true;
        //        foreach (DataColumn column in Columns)
        //        {
        //            if (!firstColumn)
        //            {
        //                commaSeparatedColumnNames.Append(", ");
        //            }
        //            commaSeparatedColumnNames.Append(column.ColumnName);
        //            firstColumn = false;
        //        }

        //        commaSeparatedListOfColumnNamesValue =
        //            commaSeparatedColumnNames.ToString();
        //        return commaSeparatedListOfColumnNamesValue;
        //    }
        //}

        // Declare variables to be reused by the SupplyPageOfData method.
        private string columnToSortBy;

        public DataTable SupplyPageOfData(int lowerPageBoundary, int rowsPerPage)
        {
            //// Store the name of the ID column. This column must contain unique 
            //// values so the SQL below will work properly.
            //if (columnToSortBy == null)
            //{
            //    columnToSortBy = this.Columns[0].ColumnName;
            //}

            //if (!this.Columns[columnToSortBy].Unique)
            //{
            //    throw new InvalidOperationException(String.Format(
            //        "Column {0} must contain unique values.", columnToSortBy));
            //}

            //// Retrieve the specified number of rows from the database, starting
            //// with the row specified by the lowerPageBoundary parameter.
            //command.CommandText = "Select Top " + rowsPerPage + " " +
            //    CommaSeparatedListOfColumnNames + " From " + tableName +
            //    " WHERE " + columnToSortBy + " NOT IN (SELECT TOP " +
            //    lowerPageBoundary + " " + columnToSortBy + " From " +
            //    tableName + " Order By " + columnToSortBy +
            //    ") Order By " + columnToSortBy;
            //adapter.SelectCommand = command;

            DataTable table = new DataTable();
            for (int col = 0; col < _model.Data.MatrixColumnCount; col++)
            {
                table.Columns.Add(new DataColumn(col.ToString()));
            }

            for (int row = lowerPageBoundary; row < lowerPageBoundary + rowsPerPage; row++)
            {
                if (row < _model.Data.MatrixRowCount)
                {
                    DataRow dr = table.NewRow();

                    for (int col = 0; col < columnsValue.Count; col++)
                    {
                        dr[col] = _dataFormatter.ReadElement(row, col);
                    }
                    table.Rows.Add(dr);
                }
                else
                {
                    DataRow dr = table.NewRow();

                    for (int col = 0; col < columnsValue.Count; col++)
                    {
                        dr[col] = 0;
                    }
                    table.Rows.Add(dr);
                }
            }
            //table.Locale = System.Globalization.CultureInfo.InvariantCulture;
            //adapter.Fill(table);
            return table;
        }

    }
}
