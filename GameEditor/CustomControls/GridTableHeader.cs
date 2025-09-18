using GameEditor.Misc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameEditor.CustomControls
{
    public partial class GridTableHeader : AbstractPaintedControl
    {
        private const int CELL_PADX = GridTable.SizeCalculator.CELL_PADX;
        private const int CELL_PADY = GridTable.SizeCalculator.CELL_PADY;

        private GridTable.ITableDataSource? tableDataSource;
        private GridTable.SizeCalculator sizeInfo;

        public GridTableHeader() {
            InitializeComponent();
            SetDoubleBuffered();

            tableDataSource = null;
            sizeInfo = new GridTable.SizeCalculator();
        }

        public Color InactiveBackColor { get; set; }
        public int HorizontalPosition { get; set; }

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
                pe.Graphics.Clear(InactiveBackColor);
                using Pen pen = new(ForeColor);
                pe.Graphics.DrawRectangle(pen, 0, 0, 150, ClientSize.Height-1);
                pe.Graphics.DrawLine(pen, 50, 0, 50, ClientSize.Height-1);
                pe.Graphics.DrawLine(pen, 100, 0, 100, ClientSize.Height-1);
                return;
            }

            if (TableDataSource == null) {
                pe.Graphics.Clear(InactiveBackColor);
                return;
            }

            if (! sizeInfo.SizeCalculated) {
                sizeInfo.Calculate(pe.Graphics, Font, TableDataSource, 1);
                Height = sizeInfo.Height;
                Invalidate();
                return;
            }

            pe.Graphics.Clear(InactiveBackColor);
            if (sizeInfo.RowHeight == 0 || sizeInfo.ColumnPositions.Count == 0) {
                return;
            }

            using Pen forePen = new(ForeColor);
            //using Pen debugPen = new(Color.Green);
            using SolidBrush textBrush = new(ForeColor);
            using SolidBrush backBrush = new(BackColor);

            pe.Graphics.Clear(InactiveBackColor);
            pe.Graphics.TranslateTransform(-HorizontalPosition, 0);
            pe.Graphics.FillRectangle(backBrush, 0, 0, sizeInfo.Width, sizeInfo.Height);

            // header
            string[] header = TableDataSource.GetHeader();
            bool[] fatCols = TableDataSource.GetFatColumns();
            for (int c = 0; c < sizeInfo.ColumnPositions.Count; c++) {
                int x = sizeInfo.ColumnPositions[c];
                if (c < header.Length) {
                    int x2 = sizeInfo.ColumnPositions[c+1];
                    Rectangle textBox = new Rectangle(
                        x + 2*CELL_PADX, 1 + 4*CELL_PADY,
                        x2 - x - 2*CELL_PADX, sizeInfo.RowHeight - 4*CELL_PADY - 2
                    );
                    pe.Graphics.DrawString(header[c], Font, textBrush, textBox, sizeInfo.DrawStringFormat);
                }
                pe.Graphics.DrawLine(forePen, x, 0, x, sizeInfo.Height - 1);
                if (c < fatCols.Length && fatCols[c]) {
                    pe.Graphics.DrawLine(forePen, x+1, 0, x+1, ClientSize.Height - 1);
                }
                //pe.Graphics.DrawRectangle(debugPen, textBox);
            }

            // horizontal lines
            pe.Graphics.DrawLine(forePen, 0, 0, sizeInfo.Width-1, 0);
            pe.Graphics.DrawLine(forePen, 0, sizeInfo.Height-2, sizeInfo.Width-1, sizeInfo.Height-2);
            pe.Graphics.DrawLine(forePen, 0, sizeInfo.Height-1, sizeInfo.Width-1, sizeInfo.Height-1);
            pe.Graphics.TranslateTransform(HorizontalPosition, 0);
        }

    }
}
