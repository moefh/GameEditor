using GameEditor.MapEditor;
using GameEditor.Misc;
using GameEditor.ProjectChecker;
using GameEditor.TilesetEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameEditor.MainEditor
{
    public partial class CheckerWindow : ProjectUnclosableForm
    {
        private ProjectCheckResult? savedResult;

        public CheckerWindow(ProjectDataItem proj) : base(proj, "ValidatorWindow") {
            InitializeComponent();
        }

        public void ClearResults() {
            savedResult = null;
            txtLog.Text = "";
        }

        private void toolStripBtnRunValidator_Click(object sender, EventArgs e) {
            RunCheck();
        }

        // ===================================================================
        // === CHECK
        // ===================================================================

        public void RunCheck() {
            ProjectInspector inspector = new ProjectInspector(Project.ProjectData);
            savedResult = inspector.Run();
            txtLog.Text = savedResult.GetReport();
        }

        private void toolStripBtnOpenProblems_Click(object sender, EventArgs e) {
            if (MdiParent == null || savedResult == null) return;
            foreach (AssetProblem p in savedResult.GetProblemList()) {
                p.Asset.ShowEditor(Project, MdiParent);
            }
        }
    }
}
