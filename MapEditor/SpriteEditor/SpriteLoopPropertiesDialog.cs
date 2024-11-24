using GameEditor.GameData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameEditor.SpriteEditor
{
    public partial class SpriteLoopPropertiesDialog : Form
    {
        private class SpriteFrame(IList<int> selected, int frame)
        {
            public int Frame { get; } = frame;

            public override string ToString() {
                foreach (int f in selected) {
                    if (f == Frame) {
                        return $"{Frame} (selected)";
                    }
                }
                return Frame.ToString();
            }
        }

        public SpriteLoopPropertiesDialog(SpriteAnimationLoop loop) {
            InitializeComponent();
            LoopName = loop.Name;
            spriteViewer.Sprite = loop.Animation.Sprite;

            SelectedFrames = [.. Enumerable.Range(0, loop.NumFrames).Select((i) => loop.Frame(i))];
            AllFrames = [.. Enumerable.Range(0, loop.Animation.Sprite.NumFrames).Select((i) => new SpriteFrame(SelectedFrames, i))];
            listBoxAllFrames.DataSource = AllFrames;
            listBoxSelectedFrames.DataSource = SelectedFrames;
        }

        public string LoopName {
            get { return txtName.Text; }
            set { txtName.Text = value; }
        }

        private List<SpriteFrame> AllFrames { get; set; }

        public BindingList<int> SelectedFrames { get; set; }

        private void RefreshFrameList() {
            listBoxAllFrames.DataSource = null;
            listBoxAllFrames.DataSource = AllFrames;
        }

        private void btnOK_Click(object sender, EventArgs e) {
            if (SelectedFrames.Count <= 0) {
                MessageBox.Show("At least one frame must be selected.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void listBoxSelectedFrames_SelectedValueChanged(object sender, EventArgs e) {
            if (listBoxSelectedFrames.SelectedValue is int frame) {
                listBoxAllFrames.SelectedIndex = frame;
            }
        }

        private void listBoxAllFrames_SelectedValueChanged(object sender, EventArgs e) {
            if (listBoxAllFrames.SelectedValue is SpriteFrame frame) {
                spriteViewer.SelectedFrame = frame.Frame;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e) {
            if (listBoxAllFrames.SelectedValue is SpriteFrame frame) {
                SelectedFrames.Add(frame.Frame);
                RefreshFrameList();
            }
        }

        private void btnRemove_Click(object sender, EventArgs e) {
            int index = listBoxSelectedFrames.SelectedIndex;
            if (index >= 0 && index < SelectedFrames.Count) {
                SelectedFrames.RemoveAt(index);
                RefreshFrameList();
            }
        }
    }
}
