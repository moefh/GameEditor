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
    public partial class GridTable : UserControl
    {
        public class SizeCalculator
        {
            public const int CELL_PADX = 2;
            public const int CELL_PADY = 1;

            public StringFormat DrawStringFormat { get; }
            public List<int> ColumnPositions { get; } = [];
            public int RowHeight { get; private set; }
            public int Width { get; private set; }
            public int Height { get; private set; }
            public bool SizeCalculated { get; set; }

            public SizeCalculator() {
                DrawStringFormat = new StringFormat(StringFormat.GenericDefault);
                DrawStringFormat.Trimming = StringTrimming.None;
                DrawStringFormat.FormatFlags =
                    StringFormatFlags.MeasureTrailingSpaces |
                    StringFormatFlags.NoWrap |
                    StringFormatFlags.NoClip |
                    StringFormatFlags.FitBlackBox;
                DrawStringFormat.Alignment = StringAlignment.Center;
                DrawStringFormat.LineAlignment = StringAlignment.Center;
            }

            public void Calculate(Graphics g, Font font, ITableDataSource data, int numRows) {

                // columns
                string[] header = data.GetHeader();
                bool[] fatCols = data.GetFatColumns();
                int w = 0;
                ColumnPositions.Clear();
                ColumnPositions.Add(w);
                for (int c = 0; c < header.Length; c++) {
                    SizeF hSize = g.MeasureString(header[c], font, 1000, DrawStringFormat);
                    w += 1 + 2 * CELL_PADX + (int)Math.Ceiling(hSize.Width);
                    if (c < fatCols.Length && fatCols[c]) w++;
                    ColumnPositions.Add(w);
                }

                // rows
                SizeF rSize = g.MeasureString("A", font, 1000, DrawStringFormat);
                RowHeight = 1 + 2 * CELL_PADY + (int)Math.Ceiling(rSize.Height);
                int h = numRows * RowHeight;

                Width = w + 1;
                Height = h;
                SizeCalculated = true;
            }
        }

        public class CellEventArgs : EventArgs
        {
            public int RowIndex { get; }
            public int ColumnIndex { get; }

            public CellEventArgs(int rowIndex, int columnIndex) {
                RowIndex = rowIndex;
                ColumnIndex = columnIndex;
            }
        }

        public interface ITableDataSource
        {
            public bool[] GetFatColumns();
            public string[] GetHeader();
            public bool IsRowFat(int row);
            public string[] GetRow(int row);
        }

        private ITableDataSource? tableDataSource;
        private Color inactiveBackColor = SystemColors.Control;

        public event EventHandler<CellEventArgs>? CellDoubleClick;

        public GridTable() {
            InitializeComponent();
            gridTableContent.CellDoubleClick += delegate (object? sender, CellEventArgs e) {
                CellDoubleClick?.Invoke(sender, e);
            };
        }

        public Font ContentFont {
            get { return gridTableContent.Font; }
            set { gridTableContent.Font = value; gridTableContent.RequestSizeCalculation(); }
        }

        public Font HeaderFont {
            get { return gridTableHeader.Font; }
            set {
                gridTableHeader.Font = value;
                gridTableHeader.RequestSizeCalculation();
                gridTableContent.HeaderFont = value;
                gridTableContent.RequestSizeCalculation();
            }
        }

        public Color InactiveBackColor {
            get { return inactiveBackColor; }
            set {
                inactiveBackColor = value;
                contentPanel.BackColor = value;
                gridTableHeader.InactiveBackColor = value;
                gridTableHeader.Invalidate();
            }
        }

        public Color ContentBackColor {
            get { return gridTableContent.BackColor; }
            set { gridTableContent.BackColor = value; }
        }

        public Color HeaderBackColor {
            get { return gridTableHeader.BackColor; }
            set { gridTableHeader.BackColor = value; }
        }

        public ITableDataSource? TableDataSource {
            get { return tableDataSource; }
            set {
                tableDataSource = value;
                gridTableHeader.TableDataSource = tableDataSource;
                gridTableContent.TableDataSource = tableDataSource;
            }
        }

        public int NumRows {
            get { return gridTableContent.NumRows; }
            set { gridTableContent.NumRows = value; }
        }

        private void contentPanel_Scroll(object sender, ScrollEventArgs e) {
            if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll) {
                gridTableHeader.HorizontalPosition = e.NewValue;
                gridTableHeader.Invalidate();
            }
        }

        internal void ForceRefresh() {
            gridTableContent.Invalidate();
        }
    }
}
