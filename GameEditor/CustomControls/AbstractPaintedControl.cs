using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.CustomControls
{
    public abstract class AbstractPaintedControl : Control
    {
        protected void SetDoubleBuffered() {
            SetStyle(ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint | ControlStyles.SupportsTransparentBackColor,
                true);
            if (SystemInformation.TerminalServerSession)
                return;
            System.Reflection.PropertyInfo? prop = typeof(Control).GetProperty(
                "DoubleBuffered",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            prop?.SetValue(this, true, null);
        }

        protected override void OnResize(EventArgs e) {
            base.OnResize(e);
            Invalidate();
        }
    }
}
