using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.Misc
{
    public class CursorUtil
    {
        private static Cursor? fillCursor;

        public static Cursor FillCursor {
            get {
                fillCursor ??= CreateCursor(Properties.Resources.FillCursor, 2, 25);
                return fillCursor;
            }
        }

        // ================================================================
        // IMPORT FUNCTIONS FROM USER32.DLL
        // ================================================================

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr CreateIconIndirect([In] ref ICONINFO iconInfo);

        [StructLayout(LayoutKind.Sequential)]
        private struct ICONINFO
        {
            public bool fIcon;
            public int xHotspot;
            public int yHotspot;
            public IntPtr hbmMask;
            public IntPtr hbmColor;
        }

        public static Cursor CreateCursor(Bitmap bmp, int xHotSpot, int yHotSpot)
        {
            ICONINFO iconInfo = new ICONINFO();
            iconInfo.fIcon = false;
            iconInfo.xHotspot = xHotSpot;
            iconInfo.yHotspot = yHotSpot;
            iconInfo.hbmMask = bmp.GetHbitmap();
            iconInfo.hbmColor = bmp.GetHbitmap();

            IntPtr ptr = CreateIconIndirect(ref iconInfo);
            return new Cursor(ptr);
        }

    }
}
