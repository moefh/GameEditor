using GameEditor.Misc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameEditor.CustomControls
{
    public partial class GridTableContent : AbstractPaintedControl
    {
        private const int CELL_PADX = GridTable.SizeCalculator.CELL_PADX;
        private const int CELL_PADY = GridTable.SizeCalculator.CELL_PADY;

        private int numRows;
        private GridTable.ITableDataSource? tableDataSource;
        private GridTable.SizeCalculator sizeInfo;

        public event EventHandler<GridTable.CellEventArgs>? CellDoubleClick;

        public GridTableContent() {
            InitializeComponent();
            SetDoubleBuffered();

            numRows = 0;
            tableDataSource = null;
            sizeInfo = new GridTable.SizeCalculator();
        }

        public Font? HeaderFont { get; set; }

        public int NumRows {
            get { return numRows; }
            set { numRows = value; RequestSizeCalculation(); }
        }

        public GridTable.ITableDataSource? TableDataSource {
            get { return tableDataSource; }
            set { tableDataSource = value; RequestSizeCalculation(); }
        }

        public void RequestSizeCalculation() {
            sizeInfo.SizeCalculated = false;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs pe) {
            base.OnPaint(pe);
            if (Util.DesignMode) {
                pe.Graphics.Clear(BackColor);
                using Pen pen = new(ForeColor);
                pe.Graphics.DrawRectangle(pen, 0, 0, ClientSize.Width-1, ClientSize.Height-1);
                pe.Graphics.DrawLine(pen, 50, 0, 50, ClientSize.Height-1);
                pe.Graphics.DrawLine(pen, 100, 0, 100, ClientSize.Height-1);
                return;
            }

            if (TableDataSource == null) {
                pe.Graphics.Clear(BackColor);
                return;
            }

            if (! sizeInfo.SizeCalculated) {
                sizeInfo.Calculate(pe.Graphics, HeaderFont ?? Font, TableDataSource, numRows);
                Width = sizeInfo.Width;
                Height = sizeInfo.Height;
                Invalidate();
                return;
            }

            pe.Graphics.Clear(BackColor);
            if (sizeInfo.RowHeight == 0 || sizeInfo.ColumnPositions.Count == 0) {
                return;
            }

            using Pen forePen = new(ForeColor);
            //using Pen debugPen = new(Color.Green);
            using SolidBrush textBrush = new(ForeColor);

            // column lines
            bool[] fatCols = TableDataSource.GetFatColumns();
            for (int col = 0; col < sizeInfo.ColumnPositions.Count; col++) {
                int x = sizeInfo.ColumnPositions[col];
                pe.Graphics.DrawLine(forePen, x, 0, x, ClientSize.Height - 1);
                if (col < fatCols.Length && fatCols[col]) {
                    pe.Graphics.DrawLine(forePen, x+1, 0, x+1, ClientSize.Height - 1);
                }
            }

            // rows
            for (int i = 0; i < numRows; i++) {
                int y =  i * sizeInfo.RowHeight;
                bool fatRow = false;
                if (TableDataSource != null) {
                    fatRow = TableDataSource.IsRowFat(i);
                    string[] data = TableDataSource.GetRow(i);
                    for (int c = 0; c < sizeInfo.ColumnPositions.Count-1; c++) {
                        Rectangle textBox = new Rectangle(
                            sizeInfo.ColumnPositions[c] + 2*CELL_PADX,
                            y + 1 + 4*CELL_PADY,
                            sizeInfo.ColumnPositions[c+1] - sizeInfo.ColumnPositions[c] - 2*CELL_PADX,
                            sizeInfo.RowHeight - 4*CELL_PADY - 2
                        );
                        pe.Graphics.DrawString(data[c], Font, textBrush, textBox);
                        //pe.Graphics.DrawRectangle(debugPen, textBox);
                    }
                }
                pe.Graphics.DrawLine(forePen, 0, y+sizeInfo.RowHeight-1, ClientSize.Width-1, y+sizeInfo.RowHeight-1);
                if (fatRow) {
                    pe.Graphics.DrawLine(forePen, 0, y+sizeInfo.RowHeight, ClientSize.Width-1, y+sizeInfo.RowHeight);
                }
            }
        }
        protected override void OnMouseDoubleClick(MouseEventArgs e) {
            base.OnMouseDoubleClick(e);

            if (e.Location.X < 0 || e.Location.X >= ClientRectangle.Width ||
                e.Location.Y < 0 || e.Location.Y >= ClientRectangle.Height) return;

            int row = e.Location.Y / sizeInfo.RowHeight;
            for (int col = 0; col < sizeInfo.ColumnPositions.Count-1; col++) {
                if (e.Location.X > sizeInfo.ColumnPositions[col] && e.Location.X < sizeInfo.ColumnPositions[col+1]) {
                    CellDoubleClick?.Invoke(this, new GridTable.CellEventArgs(row, col));
                    return;
                }
            }
        }
    }
}
