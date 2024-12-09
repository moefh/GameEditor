using GameEditor.GameData;
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
        private List<IAssetProblem> savedProblems = [];

        public CheckerWindow(ProjectData proj) : base(proj, "ValidatorWindow") {
            InitializeComponent();
        }

        public void ClearResults() {
            savedProblems.Clear();
            txtLog.Text = "";
        }

        private void toolStripBtnRunValidator_Click(object sender, EventArgs e) {
            RunCheck();
        }

        // ===================================================================
        // === CHECK
        // ===================================================================

        public void RunCheck() {
            if (Project == null) return;
            ProjectInspector inspector = new ProjectInspector(Project);
            inspector.Run();
            savedProblems = inspector.GetProblems();
            txtLog.Text = inspector.GetReport();
        }

        private void toolStripBtnOpenProblems_Click(object sender, EventArgs e) {
            if (Project == null || MdiParent == null) return;
            foreach (IAssetProblem p in savedProblems) {
                p.Asset.ShowEditor(Project, MdiParent);
            }
        }
    }
}
