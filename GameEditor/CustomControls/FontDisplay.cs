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
using System.Windows.Forms.Design;

namespace GameEditor.CustomControls
{
    public partial class FontDisplay : AbstractPaintedControl
    {
        public const int MARGIN_WIDTH = 5;
        public const int MARGIN_HEIGHT = 5;

        private FontData? fontData;

        public FontDisplay() {
            InitializeComponent();
            SetDoubleBuffered();
        }

        public FontData? FontData {
            get { return fontData; }
            set { fontData = value; Invalidate(); }
        }

        private int GetDisplayZoom() {
            if (FontData == null) return 0;
            return (ClientSize.Height - 2*MARGIN_HEIGHT) / FontData.Height;
        }

        protected override void OnPaint(PaintEventArgs pe) {
            base.OnPaint(pe);
            if (Util.DesignMode) { ImageUtil.DrawEmptyControl(pe.Graphics, ClientSize); return; }
            if (FontData == null) return;
            int zoom = GetDisplayZoom();
            if (zoom <= 0) return;

            ImageUtil.SetupTileGraphics(pe.Graphics);
            pe.Graphics.Clear(Color.White);
            int zoomedWidth = zoom * FontData.Width;
            int zoomedHeight = zoom * FontData.Height;
            for (int i = 0; i < Text.Length; i++) {
                byte c = (byte) ((Text[i] & 0xff) - 0x20);
                FontData.DrawCharAt(pe.Graphics, c,
                                    MARGIN_WIDTH + i*FontData.Width*zoom, MARGIN_HEIGHT,
                                    zoomedWidth, zoomedHeight, true);
            }

        }
    }
}
