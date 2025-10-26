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
    }

    public class MapRoomItem : AbstractRoomItem {

        public override string Type { get { return "Map"; } }

        [Browsable(false)]
        public int RoomMapId { get; }

        [Browsable(false)]
        public MapData? Map { get { return Room.GetMap(RoomMapId)?.MapData; } }

        [Category(".Information")]
        [DisplayName("Map")]
        public string MapName { get { return Map?.Name ?? "(invalid)"; } }

        [Category("Map")]
        [DisplayName("Position.X")]
        public int X {
            get { return Room.GetMap(RoomMapId)?.X ?? 0; }
            set { Room.GetMap(RoomMapId)?.SetX(value); }
        }

        [Category("Map")]
        [DisplayName("Position.X")]
        public int Y {
            get { return Room.GetMap(RoomMapId)?.Y ?? 0; }
            set { Room.GetMap(RoomMapId)?.SetY(value); }
        }

        public MapRoomItem(RoomDataItem room, string rootNodeId, int roomMapId)
                : base(room, rootNodeId) {
            RoomMapId = roomMapId;
        }
    }

    public class EntityRoomItem : AbstractRoomItem {

        public override string Type { get { return "Entity"; } }

        [Browsable(false)]
        public int RoomEntityId { get; }

        [Browsable(false)]
        public SpriteAnimation? SpriteAnim {
            get { return Room.GetEntity(RoomEntityId)?.SpriteAnim ?? null; }
        }

        [Category("Entity")]
        [DisplayName("SpriteAnim")]
        public SpriteAnimProperty SpriteAnimProp { get; set; }

        [Category("Entity")]
        public string Name {
            get { return Room.GetEntity(RoomEntityId)?.Name ?? ""; }
            set { Room.GetEntity(RoomEntityId)?.SetName(value); }
        }

        [Category("Entity")]
        [DisplayName("Position.X")]
        public int X {
            get { return Room.GetEntity(RoomEntityId)?.X ?? 0; }
            set { Room.GetEntity(RoomEntityId)?.SetX(value); }
        }

        [Category("Entity")]
        [DisplayName("Position.Y")]
        public int Y {
            get { return Room.GetEntity(RoomEntityId)?.Y ?? 0; }
            set { Room.GetEntity(RoomEntityId)?.SetY(value); }
        }

        public EntityRoomItem(RoomDataItem room, string rootNodeId, int roomEntityId)
                : base(room, rootNodeId) {
            RoomEntityId = roomEntityId;
            SpriteAnimProp = new SpriteAnimProperty(room, roomEntityId);
        }

        [Editor(typeof(SpriteAnimPropertyEditor), typeof(UITypeEditor))]
        public class SpriteAnimProperty(RoomDataItem room, int entityId) {
            private readonly RoomDataItem room = room;
            private readonly int entityId = entityId;
            public ProjectData Project { get { return room.Project; } }
            public SpriteAnimation? SprAnim {
                get { return room.Room.GetEntity(entityId)?.SpriteAnim; }
                set {
                    if (value == null) return;
                    room.Room.GetEntity(entityId)?.SetSpriteAnim(value);
                    room.Editor?.Redraw();
                }
            }
            public override string ToString() {
                return SprAnim?.Name ?? "(invalid)";
            }
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

    public class TriggerRoomItem : AbstractRoomItem {

        public override string Type { get { return "Trigger"; } }

        [Browsable(false)]
        public int RoomTriggerId { get; }

        [Category("Trigger")]
        public string Name {
            get { return Room.GetTrigger(RoomTriggerId)?.Name ?? ""; }
            set { Room.GetEntity(RoomTriggerId)?.SetName(value); }
        }

        [Category("Trigger")]
        [DisplayName("Position.X")]
        public int X {
            get { return Room.GetTrigger(RoomTriggerId)?.X ?? 0; }
            set { Room.GetTrigger(RoomTriggerId)?.SetX(value); }
        }

        [Category("Trigger")]
        [DisplayName("Position.Y")]
        public int Y {
            get { return Room.GetTrigger(RoomTriggerId)?.Y ?? 0; }
            set { Room.GetTrigger(RoomTriggerId)?.SetY(value); }
        }

        [Category("Trigger")]
        [DisplayName("Size.Width")]
        public int Width {
            get { return Room.GetTrigger(RoomTriggerId)?.Width ?? 0; }
            set { Room.GetTrigger(RoomTriggerId)?.SetWidth(value); }
        }

        [Category("Trigger")]
        [DisplayName("Size.Height")]
        public int Height {
            get { return Room.GetTrigger(RoomTriggerId)?.Height ?? 0; }
            set { Room.GetTrigger(RoomTriggerId)?.SetHeight(value); }
        }

        public TriggerRoomItem(RoomDataItem room, string rootNodeId, int roomTriggerId)
                : base(room, rootNodeId) {
            RoomTriggerId = roomTriggerId;
        }
    }

}
