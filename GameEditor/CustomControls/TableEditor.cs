using GameEditor.Misc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;

namespace GameEditor.CustomControls
{
    public partial class TableEditor : AbstractPaintedControl
    {
        private int CELL_PADX = 2;
        private int CELL_PADY = 1;

        public interface ITableDataSource {
            public string[] GetHeader();
            public string[] GetRow(int row);
        }

        public class CellEventArgs : EventArgs {
            public int RowIndex { get; }
            public int ColumnIndex { get; }

            public CellEventArgs(int rowIndex, int columnIndex) {
                RowIndex = rowIndex;
                ColumnIndex = columnIndex;
            }
        }

        private readonly StringFormat drawStringFormat;

        private int rowHeight;
        private int numRows;
        private ITableDataSource? tableDataSource;
        private readonly List<int> columnPositions = [];
        private bool sizeCalculated = false;

        public event EventHandler<CellEventArgs>? CellDoubleClick;

        public TableEditor() {
            InitializeComponent();
            SetDoubleBuffered();

            drawStringFormat = new StringFormat(StringFormat.GenericDefault);
            drawStringFormat.Trimming = StringTrimming.None;
            drawStringFormat.FormatFlags =
                StringFormatFlags.MeasureTrailingSpaces |
                StringFormatFlags.NoWrap |
                StringFormatFlags.NoClip |
                StringFormatFlags.FitBlackBox;
            drawStringFormat.Alignment = StringAlignment.Center;
            drawStringFormat.LineAlignment = StringAlignment.Center;

            rowHeight = 0;
            numRows = 0;
            tableDataSource = null;
        }

        public Font? HeaderFont { get; set; }

        public int NumRows {
            get { return numRows; }
            set { numRows = value; RequestSizeChange(); }
        }

        public ITableDataSource? TableDataSource {
            get { return tableDataSource; }
            set { tableDataSource = value; Invalidate(); }
        }

        private void RequestSizeChange() {
            sizeCalculated = false;
            Invalidate();
        }

        private void CalculateSize(Graphics g) {
            if (TableDataSource == null) return;

            Font headerFont = HeaderFont ?? Font;

            int w = 0;
            string[] header = TableDataSource.GetHeader();

            // header
            columnPositions.Clear();
            columnPositions.Add(w);
            foreach (string s in header) {
                SizeF hSize = g.MeasureString(s, headerFont, 1000, drawStringFormat);
                w += 1 + 2*CELL_PADX + (int) Math.Ceiling(hSize.Width);
                columnPositions.Add(w);
            }

            // rows
            SizeF rSize = g.MeasureString("A", Font, 1000, drawStringFormat);
            rowHeight = 1 + 2*CELL_PADY + (int) Math.Ceiling(rSize.Height);
            int h = (NumRows + 1) * rowHeight;

            Width = w + 1;
            Height = h + 1;
            sizeCalculated = true;
        }

        protected override void OnPaint(PaintEventArgs pe) {
            base.OnPaint(pe);

            if (TableDataSource == null) {
                pe.Graphics.Clear(BackColor);
                return;
            }

            if (! sizeCalculated) {
                CalculateSize(pe.Graphics);
                Invalidate();
                return;
            }

            pe.Graphics.Clear(BackColor);
            if (rowHeight == 0 || columnPositions.Count == 0) {
                return;
            }

            using Pen forePen = new(ForeColor);
            //using Pen debugPen= new(Color.Green);
            using SolidBrush textBrush = new(ForeColor);

            Font hdrFont = HeaderFont ?? Font;
            string[] header = TableDataSource.GetHeader();

            // header
            for (int i = 0; i < columnPositions.Count-1; i++) {
                Rectangle textBox = new Rectangle(
                    columnPositions[i] + 2*CELL_PADX,
                    1 + 4*CELL_PADY,
                    columnPositions[i+1] - columnPositions[i] - 2*CELL_PADX,
                    rowHeight - 4*CELL_PADY - 2
                );
                pe.Graphics.DrawString(header[i], hdrFont, textBrush, textBox, drawStringFormat);
                pe.Graphics.DrawLine(forePen, columnPositions[i], 0, columnPositions[i], ClientSize.Height - 1);
                //pe.Graphics.DrawRectangle(debugPen, textBox);
            }
            pe.Graphics.DrawLine(forePen,
                columnPositions[columnPositions.Count-1], 0,
                columnPositions[columnPositions.Count-1], ClientSize.Height - 1);

            // rows
            pe.Graphics.DrawLine(forePen, 0, 0, ClientSize.Width-1, 0);
            for (int i = 0; i < numRows; i++) {
                int y =  (i + 1) * rowHeight;
                pe.Graphics.DrawLine(forePen, 0, y, ClientSize.Width-1, y);
                if (TableDataSource != null) {
                    string[] data = TableDataSource.GetRow(i);
                    for (int c = 0; c < columnPositions.Count-1; c++) {
                        Rectangle textBox = new Rectangle(
                            columnPositions[c] + 2*CELL_PADX,
                            y + 1 + 4*CELL_PADY,
                            columnPositions[c+1] - columnPositions[c] - 2*CELL_PADX,
                            rowHeight - 4*CELL_PADY - 2
                        );
                        pe.Graphics.DrawString(data[c], Font, textBrush, textBox);
                        //pe.Graphics.DrawRectangle(debugPen, textBox);
                    }
                }
            }
            int bottom = rowHeight * (NumRows + 1);
            pe.Graphics.DrawLine(forePen, 0, bottom, ClientSize.Width-1, bottom);
        }
        protected override void OnMouseDoubleClick(MouseEventArgs e) {
            base.OnMouseDoubleClick(e);

            if (e.Location.Y < rowHeight) return; // header
            if (e.Location.X < 0 ||
                e.Location.X >= ClientRectangle.Width ||
                e.Location.Y >= ClientRectangle.Height) return;

            int row = (e.Location.Y - rowHeight) / rowHeight;
            for (int col = 0; col < columnPositions.Count-1; col++) {
                if (e.Location.X > columnPositions[col] && e.Location.X < columnPositions[col+1]) {
                    CellDoubleClick?.Invoke(this, new CellEventArgs(row, col));
                }
            }
        }
    }
}
