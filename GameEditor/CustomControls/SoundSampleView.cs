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
    public partial class SoundSampleView : AbstractPaintedControl
    {
        private sbyte[]? samples;

        public SoundSampleView() {
            InitializeComponent();
            SetDoubleBuffered();
        }

        public sbyte[]? Samples {
            get { return samples; }
            set { samples = value; Invalidate(); }
        }

        protected override void OnPaint(PaintEventArgs pe) {
            base.OnPaint(pe);
            if (Util.DesignMode) { ImageUtil.DrawEmptyControl(pe.Graphics, ClientSize); return; }

            pe.Graphics.Clear(Color.Black);
            if (Samples == null) return;

            int xMax = ClientSize.Width - 10;
            int xBase = 5;
            int yBase = ClientSize.Height / 2;
            int yMax = ClientSize.Height / 2 - 10;
            long step = ((long) Samples.Length << 16) / xMax;
            if (step <= 0) return;
            for (int x = 0; x < xMax; x++) {
                int iStart = (int) ((x * step) >> 16);
                int iNextStart = (int) (((x+1) * step) >> 16) - 1;
                sbyte sample = SoundUtil.GetMaxSampleInRange(Samples, iStart, iNextStart-iStart);
                int y = sample * yMax / 128;
                if (y == 0) y = 1;
                pe.Graphics.DrawLine(Pens.White, x + xBase, yBase - y, x + xBase, yBase);
            }

        }
    }
}
