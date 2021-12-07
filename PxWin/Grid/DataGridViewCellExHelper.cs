using System.Drawing;
using System.Windows.Forms;

namespace PCAxis.Desktop.Grid
{
    static class DataGridViewCellExHelper
    {
        public static Rectangle GetSpannedCellClipBounds<TCell>(
            TCell ownerCell,
            Rectangle cellBounds,
            bool singleVerticalBorderAdded,
            bool singleHorizontalBorderAdded)
            where TCell : DataGridViewCell, ISpannedCell
        {
            var dataGridView = ownerCell.DataGridView;
            var clipBounds = cellBounds;
            //Setting X (skip invisible columns).
            for (int columnIndex = ownerCell.ColumnIndex; columnIndex < ownerCell.ColumnIndex + ownerCell.ColumnSpan; columnIndex++)
            {
                DataGridViewColumn column = dataGridView.Columns[columnIndex];
                if (!column.Visible)
                    continue;
                if (column.Frozen
                    || columnIndex > dataGridView.FirstDisplayedScrollingColumnIndex)
                {
                    break;
                }
                if (columnIndex == dataGridView.FirstDisplayedScrollingColumnIndex)
                {
                    clipBounds.Width -= dataGridView.FirstDisplayedScrollingColumnHiddenWidth;
                    if (dataGridView.RightToLeft != RightToLeft.Yes)
                    {
                        clipBounds.X += dataGridView.FirstDisplayedScrollingColumnHiddenWidth;
                    }
                    break;
                }
                clipBounds.Width -= column.Width;
                if (dataGridView.RightToLeft != RightToLeft.Yes)
                {
                    clipBounds.X += column.Width;
                }
            }

            //Setting Y.
            for (int rowIndex = ownerCell.RowIndex; rowIndex < ownerCell.RowIndex + ownerCell.RowSpan; rowIndex++)
            {
                DataGridViewRow row = dataGridView.Rows[rowIndex];
                if (!row.Visible)
                    continue;
                if (row.Frozen || rowIndex >= dataGridView.FirstDisplayedScrollingRowIndex)
                {
                    break;
                }
                clipBounds.Y += row.Height;
                clipBounds.Height -= row.Height;
            }

            // exclude borders.
            if (dataGridView.BorderStyle != BorderStyle.None)
            {
                var clientRectangle = dataGridView.ClientRectangle;
                clientRectangle.Width--;
                clientRectangle.Height--;
                if (dataGridView.RightToLeft == RightToLeft.Yes)
                {
                    clientRectangle.X++;
                    clientRectangle.Y++;
                }
                clipBounds.Intersect(clientRectangle);
            }
            return clipBounds;
        }

        public static Rectangle GetSpannedCellBoundsFromChildCellBounds<TCell>(
            TCell childCell,
            Rectangle childCellBounds,
            bool singleVerticalBorderAdded,
            bool singleHorizontalBorderAdded)
            where TCell : DataGridViewCell, ISpannedCell
        {
            var dataGridView = childCell.DataGridView;
            var ownerCell = childCell.OwnerCell as TCell ?? childCell;
            var spannedCellBounds = childCellBounds;
            //
            int firstVisibleColumnIndex = GetFirstVisibleColumnIndex(dataGridView, ownerCell.ColumnIndex,
                                                                     ownerCell.ColumnSpan);
            if (dataGridView.Columns[firstVisibleColumnIndex].Frozen)
            {
                spannedCellBounds.X = dataGridView.GetColumnDisplayRectangle(firstVisibleColumnIndex, false).X;
            }
            else
            {
                int dx = 0;
                for(int i = firstVisibleColumnIndex; i < childCell.ColumnIndex; i++)
                {
                    DataGridViewColumn column = dataGridView.Columns[i];
                    if (!column.Visible) continue;
                    dx += column.Width;
                }
                spannedCellBounds.X = dataGridView.RightToLeft == RightToLeft.Yes
                                          ? spannedCellBounds.X + dx
                                          : spannedCellBounds.X - dx;
            }
            //
            var firstVisibleRowIndex = GetFirstVisibleRowIndex(dataGridView, ownerCell.RowIndex, ownerCell.RowSpan);
            if (dataGridView.Rows[firstVisibleRowIndex].Frozen)
            {
                spannedCellBounds.Y = dataGridView.GetRowDisplayRectangle(firstVisibleRowIndex, false).Y;
            }
            else
            {
                int dy = 0;
                for (int i = firstVisibleRowIndex; i < childCell.RowIndex; i++ )
                {
                    DataGridViewRow row = dataGridView.Rows[i];
                    if (!row.Visible) continue;
                    dy += row.Height;
                }
                spannedCellBounds.Y -= dy;
            }
            //
            int spannedCellWidth = 0;
            for(int i = ownerCell.ColumnIndex; i < ownerCell.ColumnIndex + ownerCell.ColumnSpan; i++)
            {
                DataGridViewColumn column = dataGridView.Columns[i];
                if (!column.Visible) continue;
                spannedCellWidth += column.Width;
            }

            if (dataGridView.RightToLeft == RightToLeft.Yes)
            {
                spannedCellBounds.X = spannedCellBounds.Right - spannedCellWidth;
            }
            spannedCellBounds.Width = spannedCellWidth;
            //
            int spannedCellHieght = 0;
            for (int i = ownerCell.RowIndex; i < ownerCell.RowIndex + ownerCell.RowSpan; i++)
            {
                DataGridViewRow row = dataGridView.Rows[i];
                if (!row.Visible) continue;
                spannedCellHieght += row.Height;
            }
            spannedCellBounds.Height = spannedCellHieght;

            if (singleVerticalBorderAdded && InFirstDisplayedColumn(ownerCell))
            {
                spannedCellBounds.Width++;
                if (dataGridView.RightToLeft != RightToLeft.Yes)
                {
                    if (childCell.ColumnIndex != dataGridView.FirstDisplayedScrollingColumnIndex)
                    {
                        spannedCellBounds.X--;
                    }
                }
                else
                {
                    if (childCell.ColumnIndex == dataGridView.FirstDisplayedScrollingColumnIndex)
                    {
                        spannedCellBounds.X--;
                    }
                }
            }
            if (singleHorizontalBorderAdded && InFirstDisplayedRow(ownerCell))
            {
                spannedCellBounds.Height++;
                if (childCell.RowIndex != dataGridView.FirstDisplayedScrollingRowIndex)
                {
                    spannedCellBounds.Y--;
                }
            }
            return spannedCellBounds;
        }

        public static DataGridViewAdvancedBorderStyle AdjustCellBorderStyle<TCell>(TCell cell)
            where TCell : DataGridViewCell, ISpannedCell
        {
            var dataGridViewAdvancedBorderStylePlaceholder = new DataGridViewAdvancedBorderStyle();
            var dataGridView = cell.DataGridView;
            return cell.AdjustCellBorderStyle(
                dataGridView.AdvancedCellBorderStyle, 
                dataGridViewAdvancedBorderStylePlaceholder,
                DataGridViewTextBoxCellExHelper.SingleVerticalBorderAdded(dataGridView),
                DataGridViewTextBoxCellExHelper.SingleHorizontalBorderAdded(dataGridView),
                InFirstDisplayedColumn(cell),
                InFirstDisplayedRow(cell)); 
        }

        public static bool InFirstDisplayedColumn<TCell>(TCell cell)
            where TCell : DataGridViewCell, ISpannedCell
        {
            var dataGridView = cell.DataGridView;
            return dataGridView.FirstDisplayedScrollingColumnIndex >= cell.ColumnIndex
                   && dataGridView.FirstDisplayedScrollingColumnIndex < cell.ColumnIndex + cell.ColumnSpan;
        }

        public static bool InFirstDisplayedRow<TCell>(TCell cell)
            where TCell : DataGridViewCell, ISpannedCell
        {
            var dataGridView = cell.DataGridView;
            return dataGridView.FirstDisplayedScrollingRowIndex >= cell.RowIndex
                   && dataGridView.FirstDisplayedScrollingRowIndex < cell.RowIndex + cell.RowSpan;
        }


        #region Private Methods
		
        private static int GetFirstVisibleColumnIndex(DataGridView dataGridView, int startIndex, int span)
        {
            for (int i = startIndex; i < startIndex + span; i++)
            {
                if (dataGridView.Columns[i].Visible)
                {
                    return i;
                }
            }
            return -1;
        }

        private static int GetFirstVisibleRowIndex(DataGridView dataGridView, int startIndex, int span)
        {
            for (int i = startIndex; i < startIndex + span; i++)
            {
                if (dataGridView.Rows[i].Visible)
                {
                    return i;
                }
            }
            return -1;
        }

	    #endregion    
    }
}