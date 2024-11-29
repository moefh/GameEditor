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
    public partial class VolumeControl : AbstractPaintedControl
    {
        private const int KNOB_WIDTH = 19;
        private const int KNOB_HEIGHT = 19;
        private const int TRACK_MARGIN = 0;

        private int val;
        private int dragXOffset;
        private int dragInitialValue;
        private bool dragging;

        public event EventHandler? ValueChanged;

        public VolumeControl() {
            InitializeComponent();
            SetDoubleBuffered();
            MinValue = 0;
            MaxValue = 100;
            val = 30;
        }

        public int MaxValue { get; set; }
        public int MinValue { get; set; }

        public int Value {
            get {
                return val;
            }
            set {
                if (value >= MinValue && value <= MaxValue) {
                    val = value;
                    NotifyValueChanged();
                }
            }
        }

        private void NotifyValueChanged() {
            ValueChanged?.Invoke(this, EventArgs.Empty);
        }

        protected override void OnPaint(PaintEventArgs pe) {
            base.OnPaint(pe);
            int x1 = ClientRectangle.X + KNOB_WIDTH/2 + TRACK_MARGIN;
            int x2 = ClientRectangle.X + ClientRectangle.Width - 1 - KNOB_WIDTH/2 - TRACK_MARGIN;
            int y = ClientRectangle.Height / 2;

            // track
            pe.Graphics.DrawLine(Pens.DarkGray,  x1, y - 2, x2, y - 2);
            pe.Graphics.DrawLine(Pens.DarkGray,  x1, y - 1, x2, y - 1);
            pe.Graphics.DrawLine(Pens.LightGray, x1, y + 1, x2, y + 1);
            pe.Graphics.DrawLine(Pens.LightGray, x1, y + 2, x2, y + 2);
            pe.Graphics.DrawLine(Pens.DarkGray,  x1 + 0, y - 2, x1 + 0, y + 1);
            pe.Graphics.DrawLine(Pens.DarkGray,  x1 + 1, y - 1, x1 + 1, y + 0);
            pe.Graphics.DrawLine(Pens.LightGray, x2 - 0, y - 1, x2 - 0, y + 2);
            pe.Graphics.DrawLine(Pens.LightGray, x2 - 1, y - 0, x2 - 1, y + 1);

            // knob
            int knobX = x1 + (x2 - x1) * (Value - MinValue) / (MaxValue - MinValue);
            int xkn0 = knobX - KNOB_WIDTH/2;
            int xkn1 = knobX - KNOB_WIDTH/2 + 1;
            int xkp1 = knobX + KNOB_WIDTH/2 - 1;
            int xkp0 = knobX + KNOB_WIDTH/2;
            int ykn0 = y - KNOB_HEIGHT/2;
            int ykn1 = y - KNOB_HEIGHT/2 + 1;
            int ykp1 = y + KNOB_HEIGHT/2 - 1;
            int ykp0 = y + KNOB_HEIGHT/2;
            pe.Graphics.DrawLine(Pens.Black, xkn0, ykn1, xkn0, ykp1);
            pe.Graphics.DrawLine(Pens.Black, xkn1, ykn0, xkp1, ykn0);
            pe.Graphics.DrawLine(Pens.Black, xkp0, ykn1, xkp0, ykp1);
            pe.Graphics.DrawLine(Pens.Black, xkn1, ykp0, xkp1, ykp0);
            if (dragging) {
                pe.Graphics.FillRectangle(Brushes.DarkGray, xkn1, ykn1, KNOB_WIDTH-2, KNOB_HEIGHT-2);
            } else {
                pe.Graphics.FillRectangle(Brushes.LightGray, xkn1, ykn1, KNOB_WIDTH-2, KNOB_HEIGHT-2);
            }
            for (int i = 2; i < KNOB_HEIGHT-2; i += 3) {
                pe.Graphics.DrawLine(Pens.Black, xkn1+2, ykn1+i, xkp1-2, ykn1+i);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e) {
            base.OnMouseDown(e);
            if (e.Button != MouseButtons.Left) return;
            int x1 = ClientRectangle.X + KNOB_WIDTH/2 + TRACK_MARGIN;
            int x2 = ClientRectangle.X + ClientRectangle.Width - 1 - KNOB_WIDTH/2 - TRACK_MARGIN;
            int knobX = x1 + (x2 - x1) * (Value - MinValue) / (MaxValue - MinValue);
            int knobY = ClientRectangle.Height / 2;
            int knobXMin = knobX - KNOB_WIDTH/2;
            int knobXMax = knobX + KNOB_WIDTH/2;
            int knobYMin = knobY - KNOB_HEIGHT/2;
            int knobYMax = knobY + KNOB_HEIGHT/2;
            if (e.X >= knobXMin && e.X <= knobXMax && e.Y >= knobYMin && e.Y <= knobYMax) {
                Capture = true;
                dragging = true;
                dragInitialValue = Value;
                dragXOffset = e.X - knobX;
                Refresh();
            }
        }

        protected override void OnMouseMove(MouseEventArgs e) {
            base.OnMouseMove(e);
            if (e.Button != MouseButtons.Left || ! dragging) return;
            int x1 = ClientRectangle.X + KNOB_WIDTH/2 + TRACK_MARGIN;
            int x2 = ClientRectangle.X + ClientRectangle.Width - 1 - KNOB_WIDTH/2 - TRACK_MARGIN;
            int newKnobX = e.X - dragXOffset;

            val = int.Clamp(MinValue + (newKnobX - x1) * (MaxValue - MinValue) / (x2 - x1), MinValue, MaxValue);
            Refresh();
        }

        protected override void OnMouseUp(MouseEventArgs e) {
            if (dragging) {
                Capture = false;
                dragging = false;
                Refresh();
                if (val != dragInitialValue) {
                    NotifyValueChanged();
                }
            }
        }

    }
}
