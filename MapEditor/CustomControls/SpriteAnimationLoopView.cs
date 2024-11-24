using GameEditor.GameData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameEditor.CustomControls
{
    public partial class SpriteAnimationLoopView : AbstractPaintedControl
    {
        private const int MARGIN = 3;

        private struct RenderInfo(int width, int height, int numDisplayFrames)
        {
            public int Width { get; set; } = width;
            public int Height { get; set; } = height;
            public int NumDisplayFrames { get; set; } = numDisplayFrames;
        }

        private SpriteAnimationLoop? spriteLoop;
        private int selLoopIndex;

        public event EventHandler? SelectedLoopIndexChanged;

        public SpriteAnimationLoopView() {
            InitializeComponent();
            SetDoubleBuffered();
        }

        public int SelectedLoopIndex { get { return selLoopIndex; } set { selLoopIndex = value; Invalidate(); } }

        public SpriteAnimationLoop? Loop {
            get { return spriteLoop; }
            set { spriteLoop = value; selLoopIndex = 0; Invalidate(); }
        }

        private RenderInfo GetRenderInfo(Sprite spr) {
            int height = ClientSize.Height - 2*MARGIN;
            int width = (int) (height * spr.Width / (double) spr.Height);
            int numDisplayFrames = ClientSize.Width / (width + MARGIN);
            return new RenderInfo(width, height, numDisplayFrames);
        }

        private int GetLoopIndexAtPosition(int pos, int numDisplayFrames) {
            if (Loop == null) return 0;
            return (SelectedLoopIndex + pos - (numDisplayFrames-1)/2 + numDisplayFrames*Loop.NumFrames) % Loop.NumFrames;
        }

        protected override void OnPaint(PaintEventArgs pe) {
            base.OnPaint(pe);
            if (Util.DesignMode) { ImageUtil.DrawEmptyControl(pe.Graphics, ClientSize); return; }
            if (Loop == null) return;

            ImageUtil.SetupTileGraphics(pe.Graphics);
            Sprite spr = Loop.Animation.Sprite;

            RenderInfo r = GetRenderInfo(spr);
            for (int i = 0; i < r.NumDisplayFrames; i++) {
                int frame = Loop.Frame(GetLoopIndexAtPosition(i, r.NumDisplayFrames));
                spr.DrawFrameAt(pe.Graphics, frame, MARGIN + (r.Width + MARGIN) * i, MARGIN, r.Width, r.Height, true);
                if (i ==  (r.NumDisplayFrames-1)/2) {
                    for (int j = 0; j < MARGIN-1; j++) {
                        pe.Graphics.DrawRectangle(Pens.Black, MARGIN + (r.Width + MARGIN) * i-j, MARGIN-j, r.Width+2*j, r.Height+2*j);
                    }
                }
            }
        }

        protected override void OnMouseClick(MouseEventArgs e) {
            base.OnMouseClick(e);
            if (Util.DesignMode) return;
            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right) Focus();
            if (e.Button != MouseButtons.Left) return;
            if (Loop == null) return;

            RenderInfo r = GetRenderInfo(Loop.Animation.Sprite);
            SelectedLoopIndex = GetLoopIndexAtPosition((e.X - MARGIN) / (r.Width + MARGIN), r.NumDisplayFrames);
            SelectedLoopIndexChanged?.Invoke(this, EventArgs.Empty);
        }

    }
}
