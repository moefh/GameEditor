using GameEditor.GameData;
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
    public partial class PropFontDisplay : AbstractPaintedControl
    {
        public const int MARGIN_WIDTH = 5;
        public const int MARGIN_HEIGHT = 5;

        private PropFontData? propFontData;

        public PropFontDisplay() {
            InitializeComponent();
            SetDoubleBuffered();
        }

        public PropFontData? PropFontData {
            get { return propFontData; }
            set { propFontData = value; Invalidate(); }
        }

        private int GetDisplayZoom() {
            if (PropFontData == null) return 0;
            return (ClientSize.Height - 2*MARGIN_HEIGHT) / PropFontData.Height;
        }

        protected override void OnPaint(PaintEventArgs pe) {
            base.OnPaint(pe);
            if (Util.DesignMode) { ImageUtil.DrawEmptyControl(pe.Graphics, ClientSize); return; }
            if (PropFontData == null) return;
            int zoom = GetDisplayZoom();
            if (zoom <= 0) return;

            ImageUtil.SetupTileGraphics(pe.Graphics);
            pe.Graphics.Clear(Color.White);
            int zoomedHeight = zoom * PropFontData.Height;
            int x = MARGIN_WIDTH;
            for (int i = 0; i < Text.Length; i++) {
                byte c = (byte) ((Text[i] & 0xff) - 0x20);
                if (c > PropFontData.NUM_CHARS) c = 127 - PropFontData.FIRST_CHAR;
                int zoomedWidth = zoom * PropFontData.CharWidth[c];
                PropFontData.DrawCharAt(pe.Graphics, c,
                                        x, MARGIN_HEIGHT,
                                        zoomedWidth, zoomedHeight, true);
                x += zoomedWidth + zoom;
            }

        }

        protected override void OnTextChanged(EventArgs e) {
            base.OnTextChanged(e);
            Invalidate();
        }
    }
}
