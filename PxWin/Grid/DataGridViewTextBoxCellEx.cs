using System.Drawing;
using System.Windows.Forms;

namespace PCAxis.Desktop.Grid
{
    public class DataGridViewTextBoxCellEx: DataGridViewTextBoxCell, ISpannedCell
    {
        #region Fields
        private int m_ColumnSpan = 1;
        private int m_RowSpan = 1;
        private DataGridViewTextBoxCellEx m_OwnerCell;
        #endregion

        #region Properties
        
        public int ColumnSpan
        {
            get { return m_ColumnSpan; }
            set
            {
                if (DataGridView == null || m_OwnerCell != null)
                    return;
                if (value < 1 || ColumnIndex + value - 1 >= DataGridView.ColumnCount)
                    throw new System.ArgumentOutOfRangeException("value");
                if (m_ColumnSpan != value)
                    SetSpan(value, m_RowSpan);
            }
        }
        
        public int RowSpan
        {
            get { return m_RowSpan; }
            set
            {
                if (DataGridView == null || m_OwnerCell != null)
                    return;
                if (value < 1 || RowIndex + value - 1 >= DataGridView.RowCount)
                    throw new System.ArgumentOutOfRangeException("value");
                if (m_RowSpan != value)
                    SetSpan(m_ColumnSpan, value);
            }
        }
        
        public DataGridViewCell OwnerCell
        {
            get { return m_OwnerCell; }
            private set { m_OwnerCell = value as DataGridViewTextBoxCellEx; }
        }
        
        public override bool ReadOnly
        {
            get
            {
                return base.ReadOnly;
            }
            set
            {
                base.ReadOnly = value;

                if (m_OwnerCell == null
                    && (m_ColumnSpan > 1 || m_RowSpan > 1)
                    && DataGridView != null)
                {
                    for (int col = ColumnIndex; col < ColumnIndex + m_ColumnSpan; col++)
                        for (int row = RowIndex; row < RowIndex + m_RowSpan; row++ )
                            if (col != ColumnIndex || row != RowIndex)
                            {
                                DataGridView[col, row].ReadOnly = value;
                            }
                }
            }
        }
        
        #endregion

        #region Painting.
        
        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            if (m_OwnerCell != null && m_OwnerCell.DataGridView == null)
                m_OwnerCell = null; //owner cell was removed.

            if (DataGridView == null
                || (m_OwnerCell == null && m_ColumnSpan == 1 && m_RowSpan == 1))
            {
                base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle,
                        paintParts);
                return;
            }

            var ownerCell = this;
            var columnIndex = ColumnIndex;
            var columnSpan = m_ColumnSpan;
            var rowSpan = m_RowSpan;
            if (m_OwnerCell != null)
            {
                ownerCell = m_OwnerCell;
                columnIndex = m_OwnerCell.ColumnIndex;
                rowIndex = m_OwnerCell.RowIndex;
                columnSpan = m_OwnerCell.ColumnSpan;
                rowSpan = m_OwnerCell.RowSpan;
                value = m_OwnerCell.GetValue(rowIndex);
                errorText = m_OwnerCell.GetErrorText(rowIndex);
                cellState = m_OwnerCell.State;
                cellStyle = m_OwnerCell.GetInheritedStyle(null, rowIndex, true);
                formattedValue = m_OwnerCell.GetFormattedValue(value,
                    rowIndex, ref cellStyle, null, null, DataGridViewDataErrorContexts.Display);
            }
            if (CellsRegionContainsSelectedCell(columnIndex, rowIndex, columnSpan, rowSpan))
                cellState |= DataGridViewElementStates.Selected;

            var cellBounds2 = DataGridViewCellExHelper.GetSpannedCellBoundsFromChildCellBounds(
                this,
                cellBounds,
                DataGridViewTextBoxCellExHelper.SingleVerticalBorderAdded(DataGridView),
                DataGridViewTextBoxCellExHelper.SingleHorizontalBorderAdded(DataGridView));
            clipBounds = DataGridViewCellExHelper.GetSpannedCellClipBounds(ownerCell, cellBounds2,
                DataGridViewTextBoxCellExHelper.SingleVerticalBorderAdded(DataGridView),
                DataGridViewTextBoxCellExHelper.SingleHorizontalBorderAdded(DataGridView));
            using (var g = DataGridView.CreateGraphics())
            {
                g.SetClip(clipBounds);
                //Paint the content.
                advancedBorderStyle = DataGridViewCellExHelper.AdjustCellBorderStyle(ownerCell);
                ownerCell.NativePaint(g, clipBounds, cellBounds2, rowIndex, cellState,
                    value, formattedValue, errorText,
                    cellStyle, advancedBorderStyle,
                    paintParts & ~DataGridViewPaintParts.Border);
                //Paint the borders.
                if ((paintParts & DataGridViewPaintParts.Border) != DataGridViewPaintParts.None)
                {
                    var leftTopCell = ownerCell;
                    var advancedBorderStyle2 = new DataGridViewAdvancedBorderStyle
                                                   {
                        Left = advancedBorderStyle.Left,
                        Top = advancedBorderStyle.Top,
                        Right = DataGridViewAdvancedCellBorderStyle.None,
                        Bottom = DataGridViewAdvancedCellBorderStyle.None
                    };
                    leftTopCell.PaintBorder(g, clipBounds, cellBounds2, cellStyle, advancedBorderStyle2);

                    var rightBottomCell = DataGridView[columnIndex + columnSpan - 1, rowIndex + rowSpan - 1] as DataGridViewTextBoxCellEx 
                                          ?? this;
                    var advancedBorderStyle3 = new DataGridViewAdvancedBorderStyle
                    {
                        Left = DataGridViewAdvancedCellBorderStyle.None,
                        Top = DataGridViewAdvancedCellBorderStyle.None,
                        Right = advancedBorderStyle.Right,
                        Bottom = advancedBorderStyle.Bottom
                    };
                    rightBottomCell.PaintBorder(g, clipBounds, cellBounds2, cellStyle, advancedBorderStyle3);
                }
            }
        }

        private void NativePaint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
        }

        #endregion
        #region Spanning.

        private void SetSpan(int columnSpan, int rowSpan)
        {
            int prevColumnSpan = m_ColumnSpan;
            int prevRowSpan = m_RowSpan;
            m_ColumnSpan = columnSpan;
            m_RowSpan = rowSpan;

            if (DataGridView != null)
            {
                // clear.
                for (int rowIndex = RowIndex; rowIndex < RowIndex + prevRowSpan; rowIndex++)
                    for (int columnIndex = ColumnIndex; columnIndex < ColumnIndex + prevColumnSpan; columnIndex++)
                    {
                        var cell = DataGridView[columnIndex, rowIndex] as DataGridViewTextBoxCellEx;
                        if (cell != null)
                            cell.OwnerCell = null;
                    }

                // set.
                for (int rowIndex = RowIndex; rowIndex < RowIndex + m_RowSpan; rowIndex++)
                    for (int columnIndex = ColumnIndex; columnIndex < ColumnIndex + m_ColumnSpan; columnIndex++)
                    {
                        var cell = DataGridView[columnIndex, rowIndex] as DataGridViewTextBoxCellEx;
                        if (cell != null && cell != this)
                        {
                            if (cell.ColumnSpan > 1) cell.ColumnSpan = 1;
                            if (cell.RowSpan > 1) cell.RowSpan = 1;
                            cell.OwnerCell = this;
                        }
                    }

                OwnerCell = null;
                DataGridView.Invalidate();
            }
        }

        #endregion

        #region Editing.

        public override Rectangle PositionEditingPanel(Rectangle cellBounds, Rectangle cellClip, DataGridViewCellStyle cellStyle, bool singleVerticalBorderAdded, bool singleHorizontalBorderAdded, bool isFirstDisplayedColumn, bool isFirstDisplayedRow)
        {
            if (m_OwnerCell == null
                && m_ColumnSpan == 1 && m_RowSpan == 1)
            {
                return base.PositionEditingPanel(cellBounds, cellClip, cellStyle, singleVerticalBorderAdded, singleHorizontalBorderAdded, isFirstDisplayedColumn, isFirstDisplayedRow);
            }

            var ownerCell = this;
            if (m_OwnerCell != null)
            {
                var rowIndex = m_OwnerCell.RowIndex;
                cellStyle = m_OwnerCell.GetInheritedStyle(null, rowIndex, true);
                m_OwnerCell.GetFormattedValue(m_OwnerCell.Value, rowIndex, ref cellStyle, null, null, DataGridViewDataErrorContexts.Formatting);
                var editingControl = DataGridView.EditingControl as IDataGridViewEditingControl;
                if (editingControl != null)
                {
                    editingControl.ApplyCellStyleToEditingControl(cellStyle);
                    var editingPanel = DataGridView.EditingControl.Parent;
                    if (editingPanel != null)
                        editingPanel.BackColor = cellStyle.BackColor;
                }
                ownerCell = m_OwnerCell;
            }
            cellBounds = DataGridViewCellExHelper.GetSpannedCellBoundsFromChildCellBounds(
                this, 
                cellBounds, 
                singleVerticalBorderAdded, 
                singleHorizontalBorderAdded);
            cellClip = DataGridViewCellExHelper.GetSpannedCellClipBounds(ownerCell, cellBounds, singleVerticalBorderAdded, singleHorizontalBorderAdded);
            return base.PositionEditingPanel(
                 cellBounds, cellClip, cellStyle,
                 singleVerticalBorderAdded, 
                 singleHorizontalBorderAdded,
                 DataGridViewCellExHelper.InFirstDisplayedColumn(ownerCell),
                 DataGridViewCellExHelper.InFirstDisplayedRow(ownerCell));
        }
        
        protected override object GetValue(int rowIndex)
        {
            if (m_OwnerCell != null)
                return m_OwnerCell.GetValue(m_OwnerCell.RowIndex);
            return base.GetValue(rowIndex);
        }
        
        protected override bool SetValue(int rowIndex, object value)
        {
            if (m_OwnerCell != null)
                return m_OwnerCell.SetValue(m_OwnerCell.RowIndex, value);
            return base.SetValue(rowIndex, value);
        }
        
        #endregion

        #region Other overridden

        protected override void OnDataGridViewChanged()
        {
            base.OnDataGridViewChanged();

            if (DataGridView == null)
            {
                m_ColumnSpan = 1;
                m_RowSpan = 1;
            }
        }

        protected override Rectangle BorderWidths(DataGridViewAdvancedBorderStyle advancedBorderStyle)
        {
            if (m_OwnerCell == null
                && m_ColumnSpan == 1 && m_RowSpan == 1)
            {
                return base.BorderWidths(advancedBorderStyle);
            }

            if (m_OwnerCell != null)
                return m_OwnerCell.BorderWidths(advancedBorderStyle);
            
            var leftTop = base.BorderWidths(advancedBorderStyle);
            var rightBottomCell = DataGridView[
                ColumnIndex + ColumnSpan - 1,
                RowIndex + RowSpan - 1] as DataGridViewTextBoxCellEx;
            var rightBottom = rightBottomCell != null
                ? rightBottomCell.NativeBorderWidths(advancedBorderStyle)
                : leftTop;
            return new Rectangle(leftTop.X, leftTop.Y, rightBottom.Width, rightBottom.Height);
        }

        private Rectangle NativeBorderWidths(DataGridViewAdvancedBorderStyle advancedBorderStyle)
        {
            return base.BorderWidths(advancedBorderStyle);
        }

        protected override Size GetPreferredSize(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex, Size constraintSize)
        {
            if (OwnerCell != null) return new Size(0, 0);
            Size size = base.GetPreferredSize(graphics, cellStyle, rowIndex, constraintSize);
            DataGridView grid = DataGridView;
            int width = size.Width;
            for (int col = ColumnIndex + 1; col < ColumnIndex + ColumnSpan; col++)
                width -= grid.Columns[col].Width;
            int height = size.Height;
            for(int row = RowIndex + 1; row < RowIndex + RowSpan; row++)
                height -= grid.Rows[row].Height;
            return new Size(width, height);
        }

        #endregion

        #region Private Methods

        private bool CellsRegionContainsSelectedCell(int columnIndex, int rowIndex, int columnSpan, int rowSpan)
        {
            if (DataGridView == null)
                return false;

            for (int col = columnIndex; col < columnIndex + columnSpan; col++)
                for (int row = rowIndex; row < rowIndex + rowSpan; row++)
                    if (DataGridView[col, row].Selected) return true;
            return false;
        }
        
        #endregion
    }

    public class DataGridViewTextBoxColumnEx : DataGridViewColumn
    {
        #region ctor
        public DataGridViewTextBoxColumnEx()
            : base(new DataGridViewTextBoxCellEx())
        {
        } 
        #endregion
    }
}
