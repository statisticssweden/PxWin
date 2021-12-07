using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PCAxis.Paxiom;
using System.Windows.Forms;
using System.Data;

namespace PCAxis.Desktop.Grid
{
    /// <summary>
    /// Helper class for building grid presentation for Paxiom model
    /// </summary>
     public class GridHelper
    {
        /// <summary>
        /// Paxiom model for table to display
        /// </summary>
        private PXModel mModel;

        /// <summary>
        /// Holds table metadata
        /// </summary>
        public ColumnAndRowMeta tableMeta;
        private Dictionary<String, int> _hierarchyLevelMap;
        private DataTable headingTable = new DataTable();
        //private DataTable rowHeadingTable = new DataTable();
        /// <summary>
        /// Matrix containg all value-names in the stub
        /// </summary>
        private string[,] stubHeadingMatrix;
        private Cache memoryCache;

        /// <summary>
        /// Create grid from Paxiom model
        /// </summary>
        /// <param name="model"></param>
        public void CreateGrid(DataGridView grid, PXModel model)
        {
            mModel = model;
            
            // Disable resizing because of performance reasons. Tables with many columns will load extremly slow otherwise
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            //grid.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            grid.ColumnHeadersVisible = false;
            grid.RowHeadersVisible = false;
            grid.AllowUserToResizeColumns = true;

            //Calculate the tablemeta need to create the table
            tableMeta = CalculateRowAndColumnMeta();

            //Create matrix for stub heading
            stubHeadingMatrix = new string[tableMeta.Rows, tableMeta.ColumnOffset];
            //for (int i = 0; i < tableMeta.ColumnOffset; i++)
            //{
            //    for (int j = 0; j < tableMeta.RowOffset; j++)
            //    {
            //        stubHeadingMatrix[j, i] = "xxxxxxxxxxxx";
            //    }
            //}

            DataGridViewTextBoxColumnEx[] dgvc = new DataGridViewTextBoxColumnEx[tableMeta.Columns];
            for (int c = 0; c < tableMeta.Columns; c++)
            {
                DataGridViewTextBoxColumnEx col = new DataGridViewTextBoxColumnEx();
                col.FillWeight = 0.1f;
                dgvc[c] = col;
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                int widthCol = col.Width;
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                col.Width = widthCol;
                //col.MinimumWidth = 100;
                //col.Resizable = DataGridViewTriState.True;
            }

            grid.Columns.AddRange(dgvc);
            
            //Create all the tables headers
            CreateHeadingTable();
            CreateHeading(tableMeta, grid);
            grid.RowCount = tableMeta.Rows;
            //CreateRowHeadingTable();
            CreateStubHeadingMatrix();
            //CreateRowHeadings(grid);
            ApplyHeadingStyle(grid);

            //No selected row at start
            grid.Rows[0].Selected = false;

            DataRetriever retriever =
                new DataRetriever(model);
            memoryCache = new Cache(retriever, 16);
        }

        /// <summary>
        /// Get value for the specified cell
        /// </summary>
        /// <param name="rowIndex">row index</param>
        /// <param name="columnIndex">column index</param>
        /// <returns>Cell value</returns>
        public string GetCellValue(int rowIndex, int columnIndex)
        {
            if (columnIndex < tableMeta.ColumnOffset)
            {
                if (rowIndex > tableMeta.RowOffset - 1)
                {
                    //return rowHeadingTable.Rows[rowIndex - tableMeta.RowOffset][columnIndex].ToString();
                    return stubHeadingMatrix[rowIndex - tableMeta.RowOffset, columnIndex];
                }
                else
                {
                    return "";
                }
            }
            else if (rowIndex < tableMeta.RowOffset)
            {
                return headingTable.Rows[rowIndex][columnIndex].ToString();
            }
            else
            {
                if (memoryCache != null)
                {
                    return memoryCache.RetrieveElement(rowIndex - tableMeta.RowOffset, columnIndex - tableMeta.ColumnOffset);
                }
                //if (mModel != null && mModel.Data != null)
                //{
                //    return mModel.Data.ReadElement(rowIndex - tableMeta.RowOffset, columnIndex - tableMeta.ColumnOffset);
                //}
                else
                {
                    return "";
                }
            }
        }


        private void CreateHeadingTable()
        {
            //Number of times to add all values for a variable, default to 1 for first header row
            int repetitionsCurrentHeaderLevel = 1;

            //Number of columns that a cell should span, default to 1
            int columnSpan = 1;

            for (int col = 0; col < tableMeta.Columns; col++)
            {
                headingTable.Columns.Add(new DataColumn(col.ToString()));
            }

            DataRow dr = headingTable.NewRow();
            int index = 0;

            ////If we have any variables in the stub create a empty cell at top 
            ////left corner of the table that spans all stub columns
            if (mModel.Meta.Stub.Count > 0)
            {
                for (index = 0; index < tableMeta.ColumnOffset; index++)
                {
                    dr[index] = "";
                }
            }
            ////If no variables in the Header, create a empty column
            if (this.mModel.Meta.Heading.Count <= 0)
            {
                dr[index] = "";
                index++;
            }
            else
            {
                //Otherwise calculate columnspan start value
                columnSpan = tableMeta.Columns - tableMeta.ColumnOffset;
                Value headingValue = default(Value);
                int currColumnSpan = 0;
                int totColumnSpan = 0;
                // loop trough all the variables in the header
                for (int idxHeadingLevel = 0; idxHeadingLevel <= this.mModel.Meta.Heading.Count - 1; idxHeadingLevel++)
                {
                    //Set row to the tableheader section

                    //Set the column span for the header cells for the current row                
                    columnSpan = Convert.ToInt32(columnSpan / mModel.Meta.Heading[idxHeadingLevel].Values.Count);

                    currColumnSpan = columnSpan;
                    totColumnSpan = 0;
                    //Repeat for number of times in repetion, first time only once
                    for (int idxRepetitionCurrentHeadingLevel = 1; idxRepetitionCurrentHeadingLevel <= repetitionsCurrentHeaderLevel; idxRepetitionCurrentHeadingLevel++)
                    {
                        for (int idxHeadingValue = 0; idxHeadingValue <= mModel.Meta.Heading[idxHeadingLevel].Values.Count - 1; idxHeadingValue++)
                        {
                            totColumnSpan += columnSpan;
                            headingValue = mModel.Meta.Heading[idxHeadingLevel].Values[idxHeadingValue];
                            dr[index] = headingValue;
                            for (int i = 0; i < currColumnSpan - 1; i++)
                            {
                                index++;
                                dr[index] = "";
                            }
                            index++;
                        }
                    }
                    //Set repetiton for the next header variable
                    repetitionsCurrentHeaderLevel *= mModel.Meta.Heading[idxHeadingLevel].Values.Count;

                    headingTable.Rows.Add(dr);
                    dr = headingTable.NewRow();

                    index = 0;
                    ////Create Variable Header
                    ////If we have any variables in the stub create a empty cell at top 
                    ////left corner of the table that spans all stub columns
                    if (mModel.Meta.Stub.Count > 0)
                    {
                        for (index = 0; index < tableMeta.ColumnOffset; index++)
                        {
                            dr[index] = "";
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Creates all the header for the table
        /// </summary>
        /// <param name="tableMeta">The <see cref="ColumnAndRowMeta" /> to use to create the headers</param>
        /// <returns>A array of the string of the ids fot the headers, Used to support accessibility for the table</returns>
        /// <remarks></remarks>
        private void CreateHeading(ColumnAndRowMeta tableMeta, DataGridView grid)
        {
            //Number of times to add all values for a variable, default to 1 for first header row
            int repetitionsCurrentHeaderLevel = 1;

            //Number of columns that a cell should span, default to 1
            int columnSpan = 1;

            DataGridViewRow newRow = (DataGridViewRow)grid.RowTemplate.Clone();
            newRow.CreateCells(grid);
            int rowIndex = grid.Rows.Add(newRow);
            newRow = grid.Rows[rowIndex];
            //newRow.Frozen = true;

            int index = 0;
            ////Create Variable Header
            ////If we have any variables in the stub create a empty cell at top 
            ////left corner of the table that spans all stub columns
            if ((mModel.Meta.Stub.Count > 0) && (mModel.Meta.Heading.Count > 0))
            {
                var cell = (DataGridViewTextBoxCellEx)newRow.Cells[index];
                cell.Value = "";
                cell.ColumnSpan = tableMeta.ColumnOffset;
                index = index + tableMeta.ColumnOffset;
            }

            ////If no variables in the Header, create a empty column
            if (this.mModel.Meta.Heading.Count <= 0)
            {
                newRow.Cells[index].Value = "";
                index++;
            }
            else
            {
                //Otherwise calculate columnspan start value
                columnSpan = tableMeta.Columns - tableMeta.ColumnOffset;
                Value headingValue = default(Value);
                int currColumnSpan = 0;
                int totColumnSpan = 0;
                // loop trough all the variables in the header
                for (int idxHeadingLevel = 0; idxHeadingLevel <= this.mModel.Meta.Heading.Count - 1; idxHeadingLevel++)
                {
                    //Set row to the tableheader section
                    //Set the column span for the header cells for the current row                
                    columnSpan = Convert.ToInt32(columnSpan / mModel.Meta.Heading[idxHeadingLevel].Values.Count);
                    currColumnSpan = columnSpan;
                    totColumnSpan = 0;
                    //Repeat for number of times in repetion, first time only once
                    for (int idxRepetitionCurrentHeadingLevel = 1; idxRepetitionCurrentHeadingLevel <= repetitionsCurrentHeaderLevel; idxRepetitionCurrentHeadingLevel++)
                    {
                        for (int idxHeadingValue = 0; idxHeadingValue <= mModel.Meta.Heading[idxHeadingLevel].Values.Count - 1; idxHeadingValue++)
                        {
                            totColumnSpan += columnSpan;
                            headingValue = mModel.Meta.Heading[idxHeadingLevel].Values[idxHeadingValue];
                            var cell = (DataGridViewTextBoxCellEx)newRow.Cells[index];
                            cell.Value = headingValue;
                            cell.ColumnSpan = columnSpan;
                            for (int i = 0; i < currColumnSpan; i++)
                            {
                                index++;
                            }
                        }
                    }

                    //Set repetiton for the next header variable
                    repetitionsCurrentHeaderLevel *= mModel.Meta.Heading[idxHeadingLevel].Values.Count;

                    newRow = (DataGridViewRow)grid.RowTemplate.Clone();
                    newRow.CreateCells(grid);
                    rowIndex = grid.Rows.Add(newRow);

                    if (rowIndex < tableMeta.RowOffset)
                    {
                        newRow = grid.Rows[rowIndex];
                        newRow.Frozen = true;
                        index = 0;
                        ////Create Variable Header
                        ////If we have any variables in the stub create a empty cell at top 
                        ////left corner of the table that spans all stub columns
                        if (mModel.Meta.Stub.Count > 0)
                        {
                            var cell = (DataGridViewTextBoxCellEx)newRow.Cells[index];
                            cell.Value = "";
                            cell.ColumnSpan = tableMeta.ColumnOffset;
                            index = index + tableMeta.ColumnOffset;
                        }
                    }
                }
            }
        }

        private void CreateRowHeadings(DataGridView grid)
        {
            if (mModel.Meta.Stub.Count > 0)
            {
                CreateRowHeading(grid, 0, tableMeta.Rows - tableMeta.RowOffset, tableMeta.RowOffset);
            }

        }


        private void CreateStubHeadingMatrix()
        {
            if (mModel.Meta.Stub.Count > 0)
            {
                int matrixRow = 0;
                CreateStubHeadingMatrixRow(0, tableMeta.Rows - tableMeta.RowOffset, tableMeta.RowOffset, ref matrixRow);
            }
        }

        private void CreateStubHeadingMatrixRow(int index, int rowSpan, int rowIndex, ref int matrixRow)
        {
            //Calculate the rowspan for all the cells to add in this call
            rowSpan = (int)((rowSpan) / mModel.Meta.Stub[index].Values.Count);
            int rowsAdded = 0; // Prevent that too many rows are added for one variable level
            Value val;
            //Loop through all the values in the stub variable
            for (int i = 0; i < mModel.Meta.Stub[index].Values.Count; i++)
            {
                val = mModel.Meta.Stub[index].Values[i];
                stubHeadingMatrix[matrixRow, index] = val.Text;
                rowsAdded = rowsAdded + 1;

                //If there are more stub variables that need to add headers to this row
                if (mModel.Meta.Stub.Count > index + 1)
                {
                    CreateStubHeadingMatrixRow(index + 1, rowSpan, rowIndex, ref matrixRow);
                }
                rowIndex = rowIndex + rowSpan;
                if (rowsAdded < mModel.Meta.Stub[index].Values.Count)
                {
                    matrixRow = matrixRow + 1;
                }
            }
        }

        //private void CreateRowHeadingTable()
        //{
        //    for (int col = 0; col < tableMeta.ColumnOffset; col++)
        //    {
        //        rowHeadingTable.Columns.Add(new DataColumn(col.ToString()));
        //    }

        //    if (mModel.Meta.Stub.Count > 0)
        //    {
        //        CreateRowHeadingTableRow(0, tableMeta.Rows - tableMeta.RowOffset, tableMeta.RowOffset, null);
        //    }

        //}

        //private void CreateRowHeadingTableRow(int index, int rowSpan, int rowIndex, DataRow currentRow)
        //{
        //    //Calculate the rowspan for all the cells to add in this call
        //    rowSpan = (int)((rowSpan) / mModel.Meta.Stub[index].Values.Count);

        //    Value val;
        //    //Loop through all the values in the stub variable
        //    for (int i = 0; i < mModel.Meta.Stub[index].Values.Count; i++)
        //    {
        //        if (currentRow == null)
        //        {
        //            currentRow = rowHeadingTable.NewRow();
        //            rowHeadingTable.Rows.Add(currentRow);
        //        }

        //        val = mModel.Meta.Stub[index].Values[i];
        //        currentRow[index] = val.Text;

        //        //If there are more stub variables that need to add headers to this row
        //        if (mModel.Meta.Stub.Count > index + 1)
        //        {
        //            CreateRowHeadingTableRow(index + 1, rowSpan, rowIndex, currentRow);
        //        }
        //        rowIndex = rowIndex + rowSpan;
        //        currentRow = null;
        //    }
        //}

        private void CreateRowHeading(DataGridView grid, int index, int rowSpan, int rowIndex)
        {
            //Calculate the rowspan for all the cells to add in this call
            rowSpan = (int)((rowSpan) / mModel.Meta.Stub[index].Values.Count);

            Value val;
            //Loop through all the values in the stub variable
            for (int i = 0; i < mModel.Meta.Stub[index].Values.Count; i++)
            {
                val = mModel.Meta.Stub[index].Values[i];
                DataGridViewTextBoxCellEx cell = (DataGridViewTextBoxCellEx)grid.Rows[rowIndex].Cells[index];
                cell.Value = val.Text;
                cell.RowSpan = rowSpan;

                //If there are more stub variables that need to add headers to this row
                if (mModel.Meta.Stub.Count > index + 1)
                {
                    CreateRowHeading(grid, index + 1, rowSpan, rowIndex);
                }
                rowIndex = rowIndex + rowSpan;
            }
        }

        /// <summary>
        /// Style headings.
        /// Freeze Heading rows and Stub columns so they are visible when scrolling the table.
        /// </summary>
        private void ApplyHeadingStyle(DataGridView grid)
        {
            // Freeze stub columns
            if (tableMeta.ColumnOffset > 0)
            {
                for (int i = 0; i < tableMeta.ColumnOffset; i++)
                {
                    grid.Columns[i].Frozen = true;
                    grid.Columns[i].DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(232, 232, 232);
                    grid.Columns[i].DefaultCellStyle.WrapMode = DataGridViewTriState.False;
                }
            }
            // Freeze heading rows
            if (tableMeta.RowOffset > 0)
            {
                for (int i = 0; i < tableMeta.RowOffset; i++)
                {
                    grid.Rows[i].Frozen = true;
                    grid.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(232, 232, 232);
                }
            }
        }

        /// <summary>
        /// Calculate the table meta for the table
        /// Table meta has information about:
        ///  - number of columns in the table
        ///  - number of rows in the table
        ///  - the number of columns that contains headers
        ///  - the number of rows that contains headers
        /// </summary>
        /// <returns>An instance of <see cref="ColumnAndRowMeta" /></returns>
        /// <remarks></remarks>
        private ColumnAndRowMeta CalculateRowAndColumnMeta()
        {
            int columnCount = 1;
            int columnOffset = 0;
            int rowCount = 1;
            int rowOffset = mModel.Meta.Heading.Count;
            int hierarchyCount = 0;
            Variable hierarchyVariable = null;
            bool useHierarchy = false;

            foreach (Variable v in mModel.Meta.Heading)
            {
                columnCount *= v.Values.Count;
            }

            //Layout 2
            rowCount = 1;
            //Calcluate the number of rows when in layout 2
            foreach (Variable v in mModel.Meta.Stub)
            {
                rowCount *= v.Values.Count;
                if (v.Hierarchy.IsHierarchy)
                {
                    hierarchyCount += 1;
                    hierarchyVariable = v;
                }
            }

            columnOffset = mModel.Meta.Stub.Count;

            rowCount += mModel.Meta.Heading.Count;
            columnCount += columnOffset;

            //If only one hierarchy then create a hierarchy
            if (hierarchyCount > 0)
            {
                useHierarchy = true;
                BuildHierarchyMap(hierarchyVariable);
            }

            return new ColumnAndRowMeta(rowCount, columnCount, columnOffset, rowOffset, useHierarchy);
        }

        /// <summary>
        /// Used to create a lookup for all the <see cref="Value" /> in a <see cref="Variable" /> that has a hierarchy
        /// </summary>
        /// <param name="hierarchyVariable">The <see cref="Variable" /> to create the lookup for</param>
        /// <remarks></remarks>
        private void BuildHierarchyMap(Variable hierarchyVariable)
        {
            //Create new dictionary to use for lookup, in contains the id the code of the value and the level in the hierarchy it has
            _hierarchyLevelMap = new Dictionary<string, int>();
            BuildHierarchyLevel(hierarchyVariable.Hierarchy.RootLevel, 1);
        }

        /// <summary>
        /// Adds values to the hierarchy lookup
        /// </summary>
        /// <param name="hierarchy">The <see cref="HierarchyLevel" /> to process</param>
        /// <param name="level">The current level in the hierarchy</param>
        /// <remarks></remarks>
        private void BuildHierarchyLevel(HierarchyLevel hierarchy, int level)
        {
            //Add the current hierarchylevel to the hierarchy lookup
            _hierarchyLevelMap.Add(hierarchy.Code, level);

            foreach (HierarchyLevel hierarchyLevel in hierarchy.Children)
            {
                //Build a new hierarchy level from the children of the current hierarchy level
                BuildHierarchyLevel(hierarchyLevel, level + 1);
            }
        }
    }
}
