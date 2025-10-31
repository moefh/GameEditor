using GameEditor.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.MainEditor
{
    /**
     * This class implements a form that refuses to close: when requested to close,
     * it will hide itself.
     * 
     * This should be an abstract class, but Visual Studio gets really
     * annoyed about it for some reason. It also doesn't like if we
     * don't have a default constructor.
     */
    public class ProjectUnclosableForm : BaseProjectForm
    {
        public ProjectUnclosableForm(ProjectDataItem proj, string propName) : base(proj, propName) {
        }

        public ProjectUnclosableForm() {}  // to keep VS happy

        protected override void OnFormClosing(FormClosingEventArgs e) {
            base.OnFormClosing(e);
            if (e.CloseReason == CloseReason.UserClosing) {
                e.Cancel = true;
                Hide();
            }
        }
    }
}
