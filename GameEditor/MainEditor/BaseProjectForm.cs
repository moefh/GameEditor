using GameEditor.GameData;
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
        private string? propName;
        private ProjectData? project;

        public BaseProjectForm(ProjectData project, string propName) {
            this.project = project;
            this.propName = propName;
        }

        public BaseProjectForm() {}  // to keep VS happy

        public ProjectData Project {
            get { if (project == null) throw new Exception("no project!"); return project; }
            set { project = value; OnProjectChanged(); }
        }

        protected void SetDirty() {
            Project.SetDirty();
        }

        protected virtual void OnProjectChanged() {
        }

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
