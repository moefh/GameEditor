using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.Misc
{
    public static class ConfigUtil
    {
        public static Color TilePickerLeftColor {
            get { return GetColor("TilePickerLeftColor"); }
            set { SetColor("TilePickerLeftColor", value); }
        }

        public static Color TilePickerRightColor {
            get { return GetColor("TilePickerRightColor"); }
            set { SetColor("TilePickerRightColor", value); }
        }

        public static Color MapEditorGridColor {
            get { return GetColor("MapEditorGridColor"); }
            set { SetColor("MapEditorGridColor", value); }
        }

        public static Color TileEditorGridColor {
            get { return GetColor("TileEditorGridColor"); }
            set { SetColor("TileEditorGridColor", value); }
        }

        public static Color SpriteEditorGridColor {
            get { return GetColor("SpriteEditorGridColor"); }
            set { SetColor("SpriteEditorGridColor", value); }
        }

        public static Color SpriteEditorCollisionColor {
            get { return GetColor("SpriteEditorCollisionColor"); }
            set { SetColor("SpriteEditorCollisionColor", value); }
        }

        public static Color GetColor(string name) {
            try {
                Color? color = (Color?)Properties.Settings.Default[name];
                if (color != null) return color.Value;
            } catch (Exception ex) {
                Util.Log($"WARNING: exception reading color '{name}':\n{ex}");
            }
            return Color.FromArgb(0,0,0);
        }

        public static void SetColor(string name, Color color) {
            try {
                Properties.Settings.Default[name] = color;
                Properties.Settings.Default.Save();
            } catch (Exception ex) {
                Util.Log($"WARNING: exception writing color '{name}':\n{ex}");
            }
        }
        
    }
}
