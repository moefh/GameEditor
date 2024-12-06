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
    public partial class SpriteAnimationEditor : AbstractPaintedControl
    {
        public const uint RENDER_GRID = 1u<<0;
        public const uint RENDER_TRANSPARENT = 1u<<1;

        private SpriteAnimation? anim;

        public event EventHandler? ImageChanged;
        public event EventHandler? SelectedColorsChanged;

        public SpriteAnimationEditor() {
            InitializeComponent();
            SetDoubleBuffered();
        }

        public bool ReadOnly { get; set; }
        public Color ForePen { get; set; }
        public Color BackPen { get; set; }
        public Color GridColor { get; set; }
        public uint RenderFlags { get; set; }
        public int SelectedIndex { get; set; }
        public int SelectedLoop { get; set; }

        public SpriteAnimation? Animation {
            get { return anim; }
            set { anim = value; SelectedLoop = 0; SelectedIndex = 0; }
        }

        protected override void OnPaint(PaintEventArgs pe) {
            base.OnPaint(pe);
            ImageUtil.DrawEmptyControl(pe.Graphics, ClientSize);
            if (Util.DesignMode) return;
        }
    }
}
