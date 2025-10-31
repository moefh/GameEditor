using GameEditor.GameData;
using GameEditor.MainEditor;
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
    public abstract class AbstractRoomItem {
        [Browsable(false)]
        public RoomDataItem RoomItem { get; }

        [Browsable(false)]
        public RoomData Room { get { return RoomItem.Room; } }

        [Category(".Information")]
        public abstract string Type { get; }

        public AbstractRoomItem(RoomDataItem room) {
            RoomItem = room;
        }
    }

    public class MapRoomItem : AbstractRoomItem {

        public override string Type { get { return "Map"; } }

        [Browsable(false)]
        public int RoomMapId { get; }

        [Browsable(false)]
        public MapData? Map { get { return Room.GetMap(RoomMapId)?.MapData; } }

        [Category("Basic")]
        [DisplayName("Map")]
        public string MapName { get { return Map?.Name ?? "(invalid)"; } }

        [Category("Basic")]
        public Point Position {
            get { return Room.GetMap(RoomMapId)?.Position ?? Point.Empty; }
            set { Room.GetMap(RoomMapId)?.SetPosition(value); }
        }

        public MapRoomItem(RoomDataItem room, int roomMapId) : base(room) {
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

        [Category("Basic")]
        [DisplayName("SpriteAnim")]
        public SpriteAnimProperty SpriteAnimProp { get; set; }

        [Category("Basic")]
        public string Name {
            get { return Room.GetEntity(RoomEntityId)?.Name ?? ""; }
            set { Room.GetEntity(RoomEntityId)?.SetName(value); }
        }

        [Category("Basic")]
        public Point Position {
            get { return Room.GetEntity(RoomEntityId)?.Position ?? Point.Empty; }
            set { Room.GetEntity(RoomEntityId)?.SetPosition(value); }
        }

        [Category("Extra")]
        public int Data0 {
            get { return Room.GetEntity(RoomEntityId)?.Data[0] ?? 0; }
            set { Room.GetEntity(RoomEntityId)?.SetData(0, value); }
        }

        [Category("Extra")]
        public int Data1 {
            get { return Room.GetEntity(RoomEntityId)?.Data[1] ?? 0; }
            set { Room.GetEntity(RoomEntityId)?.SetData(1, value); }
        }

        [Category("Extra")]
        public int Data2 {
            get { return Room.GetEntity(RoomEntityId)?.Data[2] ?? 0; }
            set { Room.GetEntity(RoomEntityId)?.SetData(2, value); }
        }

        [Category("Extra")]
        public int Data3 {
            get { return Room.GetEntity(RoomEntityId)?.Data[3] ?? 0; }
            set { Room.GetEntity(RoomEntityId)?.SetData(3, value); }
        }

        public EntityRoomItem(RoomDataItem room, int roomEntityId) : base(room) {
            RoomEntityId = roomEntityId;
            SpriteAnimProp = new SpriteAnimProperty(room, roomEntityId);
        }

        [Editor(typeof(SpriteAnimPropertyEditor), typeof(UITypeEditor))]
        public class SpriteAnimProperty(RoomDataItem room, int entityId) {
            private readonly RoomDataItem room = room;
            private readonly int entityId = entityId;
            public ProjectDataItem Project { get { return room.Project; } }
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

        [Category("Basic")]
        public string Name {
            get { return Room.GetTrigger(RoomTriggerId)?.Name ?? ""; }
            set { Room.GetTrigger(RoomTriggerId)?.SetName(value); }
        }

        [Category("Basic")]
        public Point Position {
            get { return Room.GetTrigger(RoomTriggerId)?.Position ?? Point.Empty; }
            set { Room.GetTrigger(RoomTriggerId)?.SetPosition(value); }
        }

        [Category("Basic")]
        public Size Size {
            get { return Room.GetTrigger(RoomTriggerId)?.Size ?? Size.Empty; }
            set { Room.GetTrigger(RoomTriggerId)?.SetSize(value); }
        }

        [Category("Extra")]
        public int Data0 {
            get { return Room.GetTrigger(RoomTriggerId)?.Data[0] ?? 0; }
            set { Room.GetTrigger(RoomTriggerId)?.SetData(0, value); }
        }

        [Category("Extra")]
        public int Data1 {
            get { return Room.GetTrigger(RoomTriggerId)?.Data[1] ?? 0; }
            set { Room.GetTrigger(RoomTriggerId)?.SetData(1, value); }
        }

        [Category("Extra")]
        public int Data2 {
            get { return Room.GetTrigger(RoomTriggerId)?.Data[2] ?? 0; }
            set { Room.GetTrigger(RoomTriggerId)?.SetData(2, value); }
        }

        [Category("Extra")]
        public int Data3 {
            get { return Room.GetTrigger(RoomTriggerId)?.Data[3] ?? 0; }
            set { Room.GetTrigger(RoomTriggerId)?.SetData(3, value); }
        }


        public TriggerRoomItem(RoomDataItem room, int roomTriggerId) : base(room) {
            RoomTriggerId = roomTriggerId;
        }
    }

}
