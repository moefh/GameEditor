using GameEditor.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.MainEditor
{
    /**
     * This is the base class for all project MDI child windows. It implements
     * basic facilities for saving/loading the window position from the settings.
     *
     * This should be an abstract class, but Visual Studio gets really
     * annoyed about it for some reason. It also doesn't like if we
     * don't have a default constructor.
     */
    public class BaseProjectForm : Form
    {
        protected string? propName;

        public BaseProjectForm(string propName) {
            this.propName = propName;
        }

        public BaseProjectForm() {}  // to keep VS happy

        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
            if (propName != null) {
                Util.LoadWindowPosition(this, propName);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e) {
            base.OnFormClosing(e);
            if (propName != null) {
                Util.SaveWindowPosition(this, propName);
            }
        }

    }
}
