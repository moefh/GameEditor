using GameEditor.GameData;
using GameEditor.Misc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;

namespace GameEditor.RoomEditor
{
    public abstract class AbstractRoomItem
    {
        [Browsable(false)]
        public RoomDataItem RoomItem { get; }

        [Browsable(false)]
        public RoomData Room { get { return RoomItem.Room; } }

        [Browsable(false)]
        public string RootNodeId { get; }

        [Category(".Information")]
        public abstract string Type { get; }

        public AbstractRoomItem(RoomDataItem room, string rootNodeId) {
            RoomItem = room;
            RootNodeId = rootNodeId;
        }

        public virtual bool Validate(string property, object? oldValue) {
            return true;
        }
    }

    public class MapRoomItem : AbstractRoomItem {

        public override string Type { get { return "Map"; } }

        [Browsable(false)]
        public int RoomMapIndex { get; }

        [Browsable(false)]
        public MapData Map { get { return Room.Maps[RoomMapIndex].MapData; } }

        [Category(".Information")]
        [DisplayName("Map")]
        public string MapName { get { return Room.Maps[RoomMapIndex].MapData.Name; } }

        [Category("Map")]
        public int X {
            get { return Room.Maps[RoomMapIndex].X; }
            set { Room.SetMapX(RoomMapIndex, value); }
        }

        [Category("Map")]
        public int Y {
            get { return Room.Maps[RoomMapIndex].Y; }
            set { Room.SetMapY(RoomMapIndex, value); }
        }

        public MapRoomItem(RoomDataItem room, string rootNodeId, int roomMapIndex)
                : base(room, rootNodeId) {
            RoomMapIndex = roomMapIndex;
        }

        public override bool Validate(string property, object? oldValue) {
            switch (property) {
            case "X":
                if (Room.Maps[RoomMapIndex].X < 0) {
                    MessageBox.Show("Error: value must not be negative", $"Invalid valie for {property}", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (oldValue is int oldX) X = oldX;
                    return false;
                }
                return true;

            case "Y":
                if (Room.Maps[RoomMapIndex].Y < 0) {
                    MessageBox.Show("Error: value must not be negative", $"Invalid valie for {property}", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (oldValue is int oldY) Y = oldY;
                    return false;
                }
                return true;

            default:
                return true;
            }
        }
    }

    public class EntityRoomItem : AbstractRoomItem {

        public override string Type { get { return "Entity"; } }

        [Browsable(false)]
        public int RoomEntityIndex { get; }

        [Browsable(false)]
        public SpriteAnimation SpriteAnim {
            get { return Room.Entities[RoomEntityIndex].SpriteAnim; }
        }

        [Category("Entity")]
        [DisplayName("Anim")]
        public SpriteAnimProperty SpriteAnimProp { get; set; }

        [Category("Entity")]
        public string Name {
            get { return Room.Entities[RoomEntityIndex].Name; }
            set { Room.SetEntityName(RoomEntityIndex, value); }
        }

        [Category("Entity")]
        public int X {
            get { return Room.Entities[RoomEntityIndex].X; }
            set { Room.SetEntityX(RoomEntityIndex, value); }
        }

        [Category("Entity")]
        public int Y {
            get { return Room.Entities[RoomEntityIndex].Y; }
            set { Room.SetEntityY(RoomEntityIndex, value); }
        }

        public EntityRoomItem(RoomDataItem room, string rootNodeId, int roomEntityIndex)
                : base(room, rootNodeId) {
            RoomEntityIndex = roomEntityIndex;
            SpriteAnimProp = new SpriteAnimProperty(room, roomEntityIndex);
        }

        public override bool Validate(string property, object? oldValue) {
            switch (property) {
            case "Name":
                if (Room.Entities[RoomEntityIndex].Name == "") {
                    MessageBox.Show("Error: value must not be empty", $"Invalid valie for {property}", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (oldValue is string oldName) Name = oldName;
                    return false;
                }
                RoomItem.Editor?.RefreshAsset();
                return true;

            default:
                return true;
            }
        }

        [Editor(typeof(SpriteAnimPropertyEditor), typeof(UITypeEditor))]
        public class SpriteAnimProperty(RoomDataItem room, int roomEntityIndex) {
            private readonly RoomDataItem room = room;
            private readonly int roomEntityIndex = roomEntityIndex;
            public ProjectData Project { get { return room.Project; } }
            public SpriteAnimation SprAnim {
                get { return room.Room.Entities[roomEntityIndex].SpriteAnim; }
                set {
                    room.Room.SetEntitySpriteAnim(roomEntityIndex, value);
                    room.Editor?.Redraw();
                }
            }
            public override string ToString() { return SprAnim.Name; }
        }

        public class SpriteAnimPropertyEditor : UITypeEditor {
            public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext? ctx) {
                return UITypeEditorEditStyle.Modal;
            }

            public override object? EditValue(ITypeDescriptorContext? ctx, IServiceProvider provider, object? value) {
                IWindowsFormsEditorService? svc = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
                SpriteAnimProperty? prop = value as SpriteAnimProperty;
                if (svc != null && prop != null) {
                    using SpriteAnimationPickerDialog dlg = new SpriteAnimationPickerDialog();
                    dlg.SpriteAnimation = prop.SprAnim;
                    dlg.AvailableSpriteAnimations = prop.Project.SpriteAnimationList;
                    if (svc.ShowDialog(dlg) == DialogResult.OK) {
                        prop.SprAnim = dlg.SpriteAnimation;
                    }
                }
                return value;
            }
        }

    }

}
