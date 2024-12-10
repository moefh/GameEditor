using GameEditor.Misc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.CustomControls
{
    public abstract class AbstractImageEditor : AbstractPaintedControl
    {
        protected enum MouseAction {
            Down,
            Move,
            Up,
        }

        // properties
        private PaintTool tool;

        // internal stuff
        private Bitmap? selectionBmp;
        private Point selectionOrigin;
        private Point selectionMoveOrigin;
        private Rectangle moveSelectedRectStart;
        private Rectangle selectedRect;
        private bool movingSelection;
        private bool ignoreMouseUntilDown;
        private int selectionAnimationOffset;
        private System.Windows.Forms.Timer? selectionAnimationTimer;

        public event EventHandler? ImageChanged;
        public event EventHandler? SelectedColorsChanged;

        public Color ForePen { get; set; }
        public Color BackPen { get; set; }

        public PaintTool SelectedTool {
            get { return tool; }
            set {
                tool = value;
                Cursor = GetCursorForSelectedTool();
                DropSelection();
                Invalidate();
            }
        }

        protected override void SelfDispose() {
            DropSelection();
            selectionBmp?.Dispose();
            selectionBmp = null;
        }

        private void SelectionAnimationTimer_Tick(object? sender, EventArgs e) {
            if (selectedRect.Width > 0 && selectedRect.Height > 0) {
                selectionAnimationOffset = (selectionAnimationOffset + 1) % 8;
                Invalidate();
            }
        }

        protected virtual Cursor GetCursorForSelectedTool() {
            return SelectedTool switch {
                PaintTool.Pen => Cursors.Arrow,
                PaintTool.RectSelect => Cursors.Cross,
                PaintTool.FloodFill => CursorUtil.FillCursor,
                _ => Cursors.No,
            };
        }

        // ==============================================================================
        // COPY/PASTE
        // ==============================================================================

        public Bitmap? GetSelectionCopy() {
            if (selectionBmp != null) {
                Rectangle r = new Rectangle(0, 0, selectionBmp.Width, selectionBmp.Height);
                return selectionBmp.Clone(r, selectionBmp.PixelFormat);
            }
            return CopyFromImage(0, 0, EditImageWidth, EditImageHeight);
        }

        public void PasteImage(Image img) {
            DropSelection();

            // copy the whole image and make it the current selection
            selectedRect = new Rectangle(0, 0, img.Width, img.Height);
            selectionBmp = new Bitmap(img.Width, img.Height);
            using Graphics g = Graphics.FromImage(selectionBmp);
            g.DrawImage(img, new Point(0,0));

            ImageChanged?.Invoke(this, EventArgs.Empty);
        }

        // ==============================================================================
        // METHODS USED BY DERIVED CLASSES
        // ==============================================================================

        protected void SetupComponents(IContainer? components) {
            RegisterSelfDispose(components);
            if (components != null) {
                selectionAnimationTimer = new System.Windows.Forms.Timer(components);
                selectionAnimationTimer.Tick += SelectionAnimationTimer_Tick;
                selectionAnimationTimer.Interval = 250;
                selectionAnimationTimer.Start();
            }
        }

        protected void DropSelection() {
            if (selectionBmp == null) {
                selectedRect = Rectangle.Empty;
                return;
            }
            DropSelectionBitmap(selectedRect, selectionBmp);
            selectionBmp.Dispose();
            selectionBmp = null;
            selectedRect = Rectangle.Empty;
            ImageChanged?.Invoke(this, EventArgs.Empty);
        }

        protected void LiftSelection() {
            if (selectionBmp != null) return;
            selectionBmp = LiftSelectionBitmap(selectedRect);
            ImageChanged?.Invoke(this, EventArgs.Empty);
        }

        protected void PaintSelectionImage(Graphics g, Rectangle imageRect, int zoom, bool transparent) {
            if (selectionBmp == null) return;
            int x = selectedRect.X * zoom + imageRect.X;
            int y = selectedRect.Y * zoom + imageRect.Y;
            int w = selectionBmp.Width * zoom;
            int h = selectionBmp.Height * zoom;
            if (transparent) {
                g.DrawImage(selectionBmp,
                            new Rectangle(x, y, w, h),
                            0, 0, selectionBmp.Width, selectionBmp.Height,
                            GraphicsUnit.Pixel, ImageUtil.TransparentGreen);
            } else {
                g.DrawImage(selectionBmp,
                            new Rectangle(x, y, w, h),
                            0, 0, selectionBmp.Width, selectionBmp.Height,
                            GraphicsUnit.Pixel);
            }
        }

        protected void PaintSelectionRectangle(Graphics g, Rectangle imageRect, int zoom) {
            if (selectedRect.Width > 0 && selectedRect.Height > 0) {
                int x = selectedRect.X * zoom + imageRect.X;
                int y = selectedRect.Y * zoom + imageRect.Y;
                int w = selectedRect.Width * zoom;
                int h = selectedRect.Height * zoom;
                using Pen pen = new Pen(Color.Black, 3);
                pen.DashPattern = [2,2,2,2];
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
                pen.DashOffset = selectionAnimationOffset;
                g.DrawRectangle(pen, x, y, w, h);
                pen.Color = Color.White;
                pen.DashOffset = selectionAnimationOffset + 2;
                g.DrawRectangle(pen, x, y, w, h);
            }
        }

        // ==============================================================================
        // TOOLS
        // ==============================================================================

        private void ApplyPenTool(MouseEventArgs e, MouseAction action) {
            if (action == MouseAction.Up) return;
            if (e.Button != MouseButtons.Left && e.Button != MouseButtons.Right) return;
            if (! GetImageRenderRect(out int zoom, out Rectangle imageRect)) return;
            if (! imageRect.Contains(e.Location)) return;

            int tx = (e.X - imageRect.X) / zoom;
            int ty = (e.Y - imageRect.Y) / zoom;
            if (tx < 0 || ty < 0 || tx >= EditImageWidth || ty >= EditImageHeight) return;

            Color color = (e.Button == MouseButtons.Left) ? ForePen : BackPen;
            SetImagePixel(tx, ty, color); 
            Invalidate();
            ImageChanged?.Invoke(this, EventArgs.Empty);
        }

        private void ApplyColorPickerTool(MouseEventArgs e, MouseAction action) {
            if (action == MouseAction.Up) return;
            if (e.Button != MouseButtons.Left && e.Button != MouseButtons.Right) return;
            if (! GetImageRenderRect(out int zoom, out Rectangle imageRect)) return;
            if (! imageRect.Contains(e.Location)) return;

            int tx = (e.X - imageRect.X) / zoom;
            int ty = (e.Y - imageRect.Y) / zoom;
            if (tx < 0 || ty < 0 || tx >= EditImageWidth || ty >= EditImageHeight) return;

            Color color = GetImagePixel(tx, ty);
            switch (e.Button) {
            case MouseButtons.Left:  ForePen = color; break;
            case MouseButtons.Right: BackPen = color; break;
            }
            SelectedColorsChanged?.Invoke(this, EventArgs.Empty);
        }

        private void ApplyFloodFillTool(MouseEventArgs e, MouseAction action) {
            if (action != MouseAction.Down) return;
            if (e.Button != MouseButtons.Left && e.Button != MouseButtons.Right) return;
            if (! GetImageRenderRect(out int zoom, out Rectangle imageRect)) return;
            if (! imageRect.Contains(e.Location)) return;

            int tx = (e.X - imageRect.X) / zoom;
            int ty = (e.Y - imageRect.Y) / zoom;
            if (tx < 0 || ty < 0 || tx >= EditImageWidth || ty >= EditImageHeight) return;

            Color color = (e.Button == MouseButtons.Left) ? ForePen : BackPen;
            FloodFillImage(tx, ty, color);
            Invalidate();
            ImageChanged?.Invoke(this, EventArgs.Empty);
        }

        private void ApplyRectSelectTool(MouseEventArgs e, MouseAction action) {
            if (! GetImageRenderRect(out int zoom, out Rectangle imageRect)) return;

            if (e.Button != MouseButtons.Left) return;
            int sx = int.Clamp((e.X + zoom/2 - imageRect.X) / zoom, 0, EditImageWidth);
            int sy = int.Clamp((e.Y + zoom/2 - imageRect.Y) / zoom, 0, EditImageHeight);

            // start new selection
            if (action == MouseAction.Down) {
                DropSelection();
                movingSelection = false;
                selectionOrigin = new Point(sx, sy);
                Invalidate();
                return;
            }

            // create selection from origin to current point
            if (action == MouseAction.Move) {
                int ox = selectionOrigin.X;
                int oy = selectionOrigin.Y;
                if (sx < ox) (sx, ox) = (ox, sx);
                if (sy < oy) (sy, oy) = (oy, sy);
                selectedRect.X = ox;
                selectedRect.Y = oy;
                selectedRect.Width = sx - ox;
                selectedRect.Height = sy - oy;
                Invalidate();
                return;
            }
        }

        // ==============================================================================
        // EVENT HANDLERS
        // ==============================================================================

        private bool HandleSelectionMovement(MouseEventArgs e, MouseAction action) {
            // In this method we return true to indicate that the mouse event was consumed
            // and so the normal tools shouldn't get to process it.

            if (! GetImageRenderRect(out int zoom, out Rectangle imageRect)) return true;
            if (ignoreMouseUntilDown && action != MouseAction.Down) {
                Cursor = Cursors.Default;
                return true;
            }
            ignoreMouseUntilDown = false;

            Rectangle selection = new Rectangle(
                selectedRect.X * zoom + imageRect.X,
                selectedRect.Y * zoom + imageRect.Y,
                selectedRect.Width * zoom,
                selectedRect.Height * zoom
            );
            bool activeSelection = selection.Width > 0 && selection.Height > 0;

            // set cursor
            if (selection.Contains(e.Location)) {
                Cursor = CursorUtil.MoveCursor;
            } else if (SelectedTool != PaintTool.RectSelect && activeSelection) {
                Cursor = CursorUtil.DropCursor;
            } else if (SelectedTool == PaintTool.RectSelect || imageRect.Contains(e.Location)) {
                Cursor = GetCursorForSelectedTool();
            } else {
                Cursor = Cursors.No;
            }

            if (! activeSelection) {
                // no active selection, nothing to do
                return false;
            }

            if (action == MouseAction.Down) {
                if (selection.Contains(e.Location)) {
                    // start moving selection
                    movingSelection = true;
                    selectionMoveOrigin = e.Location;
                    moveSelectedRectStart = selectedRect;
                    return true;
                }

                // drop current selection
                DropSelection();
                Invalidate();

                // For the selection tool, we'll let the tool immediatelly start creating
                // another selection, so we let the code run (by returning false).
                // For any other tool, in addition to stopping the tool from handling this
                // mouse down (by returning true), we'll also ignore the mouse until the next
                // mouse down event.
                if (SelectedTool != PaintTool.RectSelect) {
                    ignoreMouseUntilDown = true;
                    return true;
                }
                return false;
            }

            // move the current selection
            if (e.Button == MouseButtons.Left && action == MouseAction.Move && movingSelection) {
                int dx = (e.X - selectionMoveOrigin.X) / zoom;
                int dy = (e.Y - selectionMoveOrigin.Y) / zoom;
                selectedRect.X = moveSelectedRectStart.X + dx;
                selectedRect.Y = moveSelectedRectStart.Y + dy;
                Invalidate();
                return true;
            }

            // lift the selection from the image
            if (action == MouseAction.Up && selectionBmp == null) {
                LiftSelection();
                movingSelection = false;
                Invalidate();
                return true;
            }

            // We can't let tools (other than selection) run while there's an active selection
            return SelectedTool != PaintTool.RectSelect;
        }

        protected void RunMouseEvent(MouseEventArgs e, MouseAction action) {
            if (Util.DesignMode) return;

            if (HandleSelectionMovement(e, action)) {
                return;
            }

            if (SelectedTool != PaintTool.RectSelect && (ModifierKeys & Keys.Modifiers) == Keys.Control) {
                ApplyColorPickerTool(e, action);
            } else {
                switch (SelectedTool) {
                case PaintTool.Pen: ApplyPenTool(e, action); break;
                case PaintTool.ColorPicker: ApplyColorPickerTool(e, action); break;
                case PaintTool.FloodFill: ApplyFloodFillTool(e, action); break; 
                case PaintTool.RectSelect: ApplyRectSelectTool(e, action); break;
                }
            }
        }

        protected override void OnMouseDown(MouseEventArgs e) {
            base.OnMouseDown(e);
            RunMouseEvent(e, MouseAction.Down);
        }

        protected override void OnMouseMove(MouseEventArgs e) {
            base.OnMouseMove(e);
            RunMouseEvent(e, MouseAction.Move);
        }

        protected override void OnMouseUp(MouseEventArgs e) {
            base.OnMouseUp(e);
            RunMouseEvent(e, MouseAction.Up);
        }

        // ==============================================================================
        // ABSTRACT STUFF
        // ==============================================================================

        protected abstract int EditImageWidth { get; }
        protected abstract int EditImageHeight { get; }
        protected abstract bool GetImageRenderRect(out int zoom, out Rectangle rect);
        protected abstract void DropSelectionBitmap(Rectangle selectedRect, Bitmap selectionBmp);
        protected abstract Bitmap? LiftSelectionBitmap(Rectangle selectedRect);
        protected abstract Color GetImagePixel(int x, int y);
        protected abstract void SetImagePixel(int x, int y, Color color);
        protected abstract void FloodFillImage(int x, int y, Color color);
        protected abstract Bitmap? CopyFromImage(int x, int y, int w, int h);

    }
}
