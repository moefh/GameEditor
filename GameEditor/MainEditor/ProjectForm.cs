using GameEditor.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.MainEditor
{
    /**
     * This should be an abstract class, but Visual Studio gets really
     * annoyed about it for some reason. It also doesn't like if we
     * don't have a default constructor.
     */
    public class ProjectForm : Form
    {
        protected string? propName;

        public ProjectForm(string propName) {
            this.propName = propName;
        }

        public ProjectForm() {}  // to keep VS happy

        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
            if (propName != null) Util.LoadWindowPosition(this, propName);
        }

        protected override void OnFormClosing(FormClosingEventArgs e) {
            base.OnFormClosing(e);
            if (propName != null) {
                Util.SaveWindowPosition(this, propName);
            }
        }

    }
}
