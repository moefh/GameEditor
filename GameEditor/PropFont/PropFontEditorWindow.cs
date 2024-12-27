using GameEditor.FontEditor;
using GameEditor.GameData;
using GameEditor.MainEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameEditor.PropFont
{
    public partial class PropFontEditorWindow : ProjectAssetEditorForm
    {
        private PropFontDataItem propFontItem;

        public PropFontEditorWindow(PropFontDataItem propFontItem) : base(propFontItem, "PropFontEditor") {
            this.propFontItem = propFontItem;
            InitializeComponent();

            SetupAssetControls(lblDataSize);
        }

        public PropFontData PropFontData {
            get { return propFontItem.PropFont; }
        }

        protected override void FixFormTitle() {
            Text = $"{PropFontData.Name} [{PropFontData.Height}] - Proportional Font";
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e) {

        }
    }
}
