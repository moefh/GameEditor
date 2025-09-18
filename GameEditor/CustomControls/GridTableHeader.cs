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

            string[] header = TableDataSource.GetHeader();

            pe.Graphics.Clear(InactiveBackColor);
            pe.Graphics.TranslateTransform(-HorizontalPosition, 0);
            pe.Graphics.FillRectangle(backBrush, 0, 0, sizeInfo.Width, sizeInfo.Height);

            // header
            for (int i = 0; i < sizeInfo.ColumnPositions.Count; i++) {
                if (i < header.Length) {
                    Rectangle textBox = new Rectangle(
                        sizeInfo.ColumnPositions[i] + 2*CELL_PADX,
                        1 + 4*CELL_PADY,
                        sizeInfo.ColumnPositions[i+1] - sizeInfo.ColumnPositions[i] - 2*CELL_PADX,
                        sizeInfo.RowHeight - 4*CELL_PADY - 2
                    );
                    pe.Graphics.DrawString(header[i], Font, textBrush, textBox, sizeInfo.DrawStringFormat);
                }
                pe.Graphics.DrawLine(forePen, sizeInfo.ColumnPositions[i], 0, sizeInfo.ColumnPositions[i], sizeInfo.Height - 1);
                //pe.Graphics.DrawRectangle(debugPen, textBox);
            }

            // horizontal lines
            pe.Graphics.DrawLine(forePen, 0, 0, sizeInfo.Width-1, 0);
            pe.Graphics.DrawLine(forePen, 0, sizeInfo.Height-1, sizeInfo.Width-1, sizeInfo.Height-1);
            pe.Graphics.TranslateTransform(HorizontalPosition, 0);
        }

    }
}
