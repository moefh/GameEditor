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
        private int MARGIN_WIDTH = 5;
        private int MARGIN_HEIGHT = 5;

        private sbyte[]? samples;
        private int selectedMarker;
        private int[] markers = [];
        private Color[] markerColors = [];

        public event EventHandler? MarkerChanged;

        public SoundSampleView() {
            InitializeComponent();
            SetDoubleBuffered();
        }

        public sbyte[]? Samples {
            get { return samples; }
            set { samples = value; Invalidate(); }
        }

        public int SelectedMarker {
            get { return selectedMarker; }
            set { selectedMarker = value; Invalidate(); }
        }

        public Color[] MarkerColor {
            get { return markerColors; }
        }

        public int[] Marker {
            get { return markers; }
        }

        public int NumMarkers {
            get { return markers.Length; }
            set {
                int[] m = new int[value];
                Array.Copy(markers, m, int.Min(m.Length, markers.Length));
                markers = m;

                Color[] mc = new Color[value];
                Array.Copy(markerColors, mc, int.Min(mc.Length, markerColors.Length));
                if (mc.Length > markerColors.Length) {
                    Array.Fill(mc, Color.FromArgb(0,255,255), markerColors.Length, mc.Length-markerColors.Length);
                }
                markerColors = mc;
            }
        }

        protected override void OnPaint(PaintEventArgs pe) {
            base.OnPaint(pe);
            if (Util.DesignMode) { ImageUtil.DrawEmptyControl(pe.Graphics, ClientSize); return; }

            pe.Graphics.Clear(Color.Black);
            if (Samples == null) return;

            int[] markerX = [..markers.Select((marker) => marker * (ClientSize.Width - 2*MARGIN_WIDTH) / Samples.Length)];
            int xMax = ClientSize.Width - 2 * MARGIN_WIDTH + 1;
            int yZero = ClientSize.Height / 2;
            int yMax = ClientSize.Height / 2 - MARGIN_HEIGHT;
            long step = ((long) Samples.Length << 16) / (xMax - 1);
            if (step <= 0) return;
            for (int x = 0; x < xMax; x++) {
                int iStart = (int) ((x * step) >> 16);
                int iNextStart = (int) (((x+1) * step) >> 16) - 1;
                if (iNextStart == iStart) iNextStart = iStart + 1;
                sbyte sample = SoundUtil.GetMaxSampleInRange(Samples, iStart, iNextStart-iStart);
                int y = sample * yMax / 128;
                if (y == 0) y = 1;
                for (int m = 0; m < markers.Length; m++) {
                    if (markerX[m] != x) continue;
                    using Pen p = new Pen(markerColors[m]);
                    pe.Graphics.DrawLine(p, x + MARGIN_WIDTH, MARGIN_HEIGHT, x + MARGIN_WIDTH, ClientSize.Height-MARGIN_HEIGHT);
                }
                pe.Graphics.DrawLine(Pens.White, x + MARGIN_WIDTH, yZero - y, x + MARGIN_WIDTH, yZero);
            }
        }

        private void SetMarkerFromMouse(int mouseX) {
            if (SelectedMarker < 0 || SelectedMarker >= markers.Length) return;
            if (Samples == null) return;
            markers[SelectedMarker] = (mouseX - MARGIN_WIDTH) * Samples.Length / ClientSize.Width;
            MarkerChanged?.Invoke(this, EventArgs.Empty);
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e) {
            base.OnMouseDown(e);
            if (Enabled && e.Button == MouseButtons.Left) {
                SetMarkerFromMouse(e.X);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e) {
            base.OnMouseMove(e);
            if (Enabled && e.Button == MouseButtons.Left) {
                SetMarkerFromMouse(e.X);
            }
        }
    }
}
