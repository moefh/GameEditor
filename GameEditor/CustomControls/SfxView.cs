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
    public partial class SfxView : AbstractPaintedControl
    {
        protected SfxData? sfx;

        public SfxView() {
            InitializeComponent();
            SetDoubleBuffered();
        }

        public SfxData? Sfx {
            get { return sfx; }
            set { sfx = value; Invalidate(); }
        }

        protected override void OnPaint(PaintEventArgs pe) {
            base.OnPaint(pe);
            if (Util.DesignMode) { ImageUtil.DrawEmptyControl(pe.Graphics, ClientSize); return; }
            if (Sfx == null) return;

            pe.Graphics.Clear(Color.Black);

            int xMax = ClientSize.Width - 10;
            int xBase = 5;
            int yBase = ClientSize.Height / 2;
            int yMax = ClientSize.Height / 4;
            long step = ((long) Sfx.NumSamples << 16) / xMax;
            if (step <= 0) return;
            for (int x = 0; x < xMax; x++) {
                byte sample = Sfx.GetSample((int) ((x * step) >> 16));
                int y = (sample - 128) * yMax / 128;
                pe.Graphics.DrawLine(Pens.White, x + xBase, yBase - y, x + xBase, yBase);
            }
        }
    }
}
