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
    public partial class MultiLineLabel : AbstractPaintedControl
    {
        private bool growing;

        public MultiLineLabel() {
            InitializeComponent();
        }

        private void ResizeLabel() {
            if (growing) return;
            try {
                growing = true;
                TextRenderer.MeasureText(Text, Font, ClientSize, TextFormatFlags.WordBreak);
            } finally {
                growing = false;
            }
        }

        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);
            TextRenderer.DrawText(e.Graphics, Text, Font, ClientRectangle, ForeColor, TextFormatFlags.WordBreak);
        }

        protected override void OnTextChanged(EventArgs e) {
            base.OnTextChanged(e);
            ResizeLabel();
            Invalidate();
        }

        protected override void OnFontChanged(EventArgs e) {
            base.OnFontChanged(e);
            ResizeLabel();
            Invalidate();
        }

        protected override void OnSizeChanged(EventArgs e) {
            base.OnSizeChanged(e);
            ResizeLabel();
            Invalidate();
        }
    }
}
