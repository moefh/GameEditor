using GameEditor.Misc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.CustomControls
{
    [Flags]
    public enum RenderFlags {
        Foreground = 1<<0,
        Background = 1<<1,
        Collision = 1<<2,
        Effects = 1<<3,
        Grid = 1<<4,
        Screen = 1<<5,
        Transparent = 1<<6,
    }

    public enum PaintTool {
        Pen,
        ColorPicker,
        RectSelect,
        FloodFill,
    }

    public abstract class AbstractPaintedControl : Control
    {
        private class SelfDisposer(Action disposeAction) : IComponent
        {
            public ISite? Site { get; set; }
            public event EventHandler? Disposed;
            public Action DisposeAction = disposeAction;

            public void Dispose() {
                DisposeAction();
                Disposed?.Invoke(this, EventArgs.Empty);
            }
        }

        protected static void RunAfter(int ms, Action action) {
            Task.Delay(ms).ContinueWith(
                (task) => { action(); },
                TaskScheduler.FromCurrentSynchronizationContext()
            );
        }  

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

        protected virtual void SelfDispose() {
        }

        protected void RegisterSelfDispose(IContainer? container) {
            container?.Add(new SelfDisposer(SelfDispose));
        }

    }
}
