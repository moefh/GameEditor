using GameEditor.CustomControls;
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

namespace GameEditor.SpriteAnimationEditor
{
    public partial class SpriteAnimationLoopPropertiesDialog : Form
    {
        private struct SelectedFrame(int index)
        {
            public int Index = index;

            public override readonly string ToString() {
                return (Index >= 0) ? Index.ToString() : "(none)";
            }
        }

        private readonly List<SelectedFrame> headFrames = [];
        private readonly List<SelectedFrame> footFrames = [];

        public SpriteAnimationLoopPropertiesDialog(SpriteAnimationLoop loop) {
            InitializeComponent();
            for (int i = 0; i < loop.NumFrames; i++) {
                headFrames.Add(new SelectedFrame(loop.Indices[i].HeadIndex));
                footFrames.Add(new SelectedFrame(loop.Indices[i].FootIndex));
            }
            listBoxHeadFrames.DataSource = headFrames;
            listBoxFootFrames.DataSource = footFrames;

            checkEnableFoot.Checked = loop.Indices.Any((SpriteAnimationLoop.Frame frame) => frame.FootIndex >= 0);

            numSelectedFrames.Minimum = 1;
            numSelectedFrames.Maximum = 50;
            numSelectedFrames.Value = loop.NumFrames;

            allFramesListView.Sprite = loop.Animation.Sprite;
            allFramesListView.Frames = MakeLoopFrameRange(loop.Animation.Sprite.NumFrames);
            allFramesListView.DisplayFoot = false;
            allFramesListView.SelectedIndex = 0;

            selFramesListView.Sprite = loop.Animation.Sprite;
            selFramesListView.DisplayFoot = checkEnableFoot.Checked;
            selFramesListView.FootOverlap = loop.Animation.FootOverlap;
            selFramesListView.SelectedIndex = 0;
            UpdateSelectedFrames();
        }

        public IEnumerable<SpriteAnimationLoop.Frame> SelectedFrames {
            get {
                return Enumerable.Range(0, headFrames.Count).Select((int i) => {
                    int footIndex = checkEnableFoot.Checked ? footFrames[i].Index : -1;
                    return new SpriteAnimationLoop.Frame(headFrames[i].Index, footIndex);
                });
            }
        }

        private List<SpriteFrameListView.Frame> MakeLoopFrameRange(int num) {
            List<SpriteFrameListView.Frame> ret = [];
            for (int i = 0; i < num; i++) {
                ret.Add(new SpriteFrameListView.Frame(i, i));
            }
            return ret;
        }

        private void AdjustListBoxScroll(ListBox listBox) {
            int numVisibleItems = listBox.ClientSize.Height / listBox.ItemHeight;
            listBox.TopIndex = int.Max(0, listBox.SelectedIndex - numVisibleItems/2);
        }

        private void UpdateSelectedFrames() {
            int numFrames = (int)numSelectedFrames.Value;

            // fix frame indices
            if (numFrames > headFrames.Count) headFrames.AddRange(Enumerable.Repeat(new SelectedFrame(-1), numFrames - headFrames.Count));
            if (numFrames > footFrames.Count) footFrames.AddRange(Enumerable.Repeat(new SelectedFrame(-1), numFrames - footFrames.Count));
            if (numFrames < headFrames.Count) headFrames.RemoveRange(numFrames, headFrames.Count - numFrames);
            if (numFrames < footFrames.Count) footFrames.RemoveRange(numFrames, footFrames.Count - numFrames);

            // fix list boxes
            listBoxHeadFrames.DataSource = null;
            listBoxHeadFrames.DataSource = headFrames;
            listBoxFootFrames.DataSource = null;
            listBoxFootFrames.DataSource = footFrames;
            AdjustListBoxScroll(listBoxHeadFrames);
            AdjustListBoxScroll(listBoxFootFrames);

            // fix selected frames view
            List<SpriteFrameListView.Frame> frames = selFramesListView.Frames ?? [];
            for (int i = 0; i < numFrames; i++) {
                SpriteFrameListView.Frame frame = new SpriteFrameListView.Frame(headFrames[i].Index, footFrames[i].Index);
                if (frames.Count <= i) {
                    frames.Add(frame);
                } else {
                    frames[i] = frame;
                }
            }
            if (frames.Count > numFrames) frames.RemoveRange(numFrames, frames.Count - numFrames);
            selFramesListView.Frames = frames;
        }

        private void numSelectedFrames_ValueChanged(object sender, EventArgs e) {
            UpdateSelectedFrames();
        }

        private void checkEnableFoot_CheckedChanged(object sender, EventArgs e) {
            if (!checkEnableFoot.Enabled) return;
            selFramesListView.DisplayFoot = checkEnableFoot.Checked;
        }

        private void btnOK_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void EraseSelectedListBoxItemFrame(ListBox listBox) {
            int index = listBox.SelectedIndex;
            if (listBox == listBoxHeadFrames) {
                if (index < 0 || index >= headFrames.Count) return;
                headFrames[index] = new SelectedFrame(-1);
            } else if (listBox == listBoxFootFrames) {
                if (index < 0 || index >= footFrames.Count) return;
                footFrames[index] = new SelectedFrame(-1);
            }
            UpdateSelectedFrames();
        }

        private void eraseHeadFrameToolStripMenuItem_Click(object sender, EventArgs e) {
            EraseSelectedListBoxItemFrame(listBoxHeadFrames);
        }

        private void eraseFootFrameToolStripMenuItem_Click(object sender, EventArgs e) {
            EraseSelectedListBoxItemFrame(listBoxFootFrames);
        }

        // ==========================================================================
        // DRAG & DROP
        // ==========================================================================

        private void listBox_DragEnter(object sender, DragEventArgs e) {
            if (e.Data != null && e.Data.GetDataPresent(typeof(SpriteFrameListView.Frame))) {
                e.Effect = e.AllowedEffect & DragDropEffects.Copy;
            }
        }

        private void listBox_DragOver(object sender, DragEventArgs e) {
            if (sender is not ListBox listBox) return;
            int index = listBox.IndexFromPoint(listBox.PointToClient(new Point(e.X, e.Y)));
            if (index >= 0 && index < listBox.Items.Count) {
                listBox.SelectedIndex = index;
            }
        }

        private void listBox_DragDrop(object sender, DragEventArgs e) {
            if (sender is not ListBox listBox) return;
            if (e.Data == null) return;
            SpriteFrameListView.Frame? frame = (SpriteFrameListView.Frame?)e.Data.GetData(typeof(SpriteFrameListView.Frame));
            if (frame == null) return;
            int dropAtIndex = listBox.SelectedIndex;
            if (listBox == listBoxHeadFrames) {
                if (dropAtIndex < 0 || dropAtIndex >= headFrames.Count) return;
                headFrames[dropAtIndex] = new SelectedFrame(frame.Value.HeadIndex);
            } else if (listBox == listBoxFootFrames) {
                if (dropAtIndex < 0 || dropAtIndex >= footFrames.Count) return;
                footFrames[dropAtIndex] = new SelectedFrame(frame.Value.FootIndex);
                checkEnableFoot.Checked = frame.Value.FootIndex >= 0;
            }
            UpdateSelectedFrames();
        }

    }
}
