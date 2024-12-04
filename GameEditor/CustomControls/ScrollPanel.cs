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

namespace GameEditor.CustomControls
{
    public partial class ScrollPanel : Panel
    {
        public ScrollPanel() {
            InitializeComponent();
        }

        protected override Point ScrollToControl(Control activeControl) {
            // We'll refuse to scroll to any control, because apparently
            // the base panel always uses this to scroll to the first
            // control when activating (e.g. when the form receives focus)
            return AutoScrollPosition;
        }
    }
}
