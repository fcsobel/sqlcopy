using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

namespace Test.SqlCopy
{
    /// <summary>
    /// DataGridView column for displaying progress
    /// </summary>
    public class DataGridViewProgressColumn : DataGridViewColumn
    {
        public DataGridViewProgressColumn()
        {
            this.CellTemplate = new DataGridViewProgressCell();
        }
    }

    /// <summary>
    /// DataGridView column-Cell
    /// set a value between 0-100 to set the progress-bar
    /// </summary>
    public class DataGridViewProgressCell : DataGridViewImageCell
    {
        public DataGridViewProgressCell()
        {
            this.ValueType = typeof(int);
        }
        public override Type FormattedValueType
        {
            get
            {
                return typeof(string);
            }
        }
       
        /// <summary>
        /// paint
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="clipBounds"></param>
        /// <param name="cellBounds"></param>
        /// <param name="rowIndex"></param>
        /// <param name="cellState"></param>
        /// <param name="value"></param>
        /// <param name="formattedValue"></param>
        /// <param name="errorText"></param>
        /// <param name="cellStyle"></param>
        /// <param name="advancedBorderStyle"></param>
        /// <param name="paintParts"></param>
        protected override void Paint(System.Drawing.Graphics graphics, System.Drawing.Rectangle clipBounds, System.Drawing.Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            int progressVal = value == null ? 0 : Convert.ToInt32(value);
            float percentage = ((float)progressVal / 100.0f); // Need to convert to float before division; otherwise C# returns int which is 0 for anything but 100%.
            Brush backColorBrush = new SolidBrush(cellStyle.BackColor);
            Brush foreColorBrush = new SolidBrush(cellStyle.ForeColor);

            // Draws the cell grid
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, (paintParts & ~DataGridViewPaintParts.ContentForeground));

            const int margin = 2;
              
            int height = cellBounds.Bottom - cellBounds.Top - (margin * 2);
            int width = cellBounds.Right - cellBounds.Left - (margin * 2);
            //simply draw a rect with reduced size depending on percentage complete
            graphics.FillRectangle(Brushes.LightGreen, cellBounds.X + margin, cellBounds.Y + margin, width * percentage, height);
            graphics.DrawString(string.Format("{0}%", progressVal), base.DataGridView.Font, foreColorBrush, cellBounds, new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center});
        }
    }
}
