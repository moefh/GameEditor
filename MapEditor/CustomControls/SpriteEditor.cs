using GameEditor.GameData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameEditor.CustomControls
{
    public partial class SpriteEditor : AbstractPaintedControl
    {
        public const uint RENDER_GRID = 1<<0;

        private SpriteLoop? spriteLoop;
        private int selLoopIndex;
        private uint renderFlags;

        public SpriteEditor() {
            InitializeComponent();
            SetDoubleBuffered();
        }

        public uint RenderFlags { get { return renderFlags; } set { renderFlags = value; Invalidate(); } }
        public int SelectedLoopIndex { get { return selLoopIndex; } set { selLoopIndex = value; Invalidate(); } }
        public bool ReadOnly { get; set; }

        public SpriteLoop? Loop {
            get { return spriteLoop; }
            set { spriteLoop = value; selLoopIndex = 0; Invalidate(); }
        }

        private bool GetSpriteRenderRect(out int zoom, out Rectangle rect) {
            int winWidth = ClientSize.Width;
            int winHeight = ClientSize.Height;
            if (Loop == null || winWidth <= 0 || winHeight <= 0) {
                zoom = 0;
                rect = Rectangle.Empty;
                return false;
            }

            Sprite spr = Loop.Sprite;
            double winAspect = (double) winWidth / winHeight;
            double sprAspect = (double) spr.Width / spr.Height;

            if (sprAspect < winAspect) {
                zoom = ClientSize.Height / (spr.Height + 1);
            } else {
                zoom = ClientSize.Width / (spr.Width + 1);
            }
            int w = zoom * spr.Width;
            int h = zoom * spr.Height;
            rect = new Rectangle((winWidth - w) / 2, (winHeight - h) / 2, w, h);
            return true;
        }

        protected override void OnPaint(PaintEventArgs pe) {
            base.OnPaint(pe);
            ImageUtil.DrawEmptyControl(pe.Graphics, ClientSize);
            if (Util.DesignMode) return;
            if (Loop == null) return;
            if (! GetSpriteRenderRect(out int zoom, out Rectangle sprRect)) return;

            ImageUtil.SetupTileGraphics(pe.Graphics);
            Loop.Sprite.DrawFrameAt(pe.Graphics, Loop.Frame(SelectedLoopIndex),
                sprRect.X, sprRect.Y, sprRect.Width, sprRect.Height, true);

            if ((RenderFlags & RENDER_GRID) != 0) {
                for (int ty = 0; ty < Loop.Sprite.Height + 1; ty++) {
                    int y = ty * zoom;
                    pe.Graphics.DrawLine(Pens.Black, sprRect.X, y + sprRect.Y, sprRect.X + sprRect.Width, y + sprRect.Y);
                }
                for (int tx = 0; tx < Loop.Sprite.Width + 1; tx++) {
                    int x = tx * zoom;
                    pe.Graphics.DrawLine(Pens.Black, x + sprRect.X, sprRect.Y, x + sprRect.X, sprRect.Y + sprRect.Height);
                }
            }
        }
    }
}

