namespace GameEditor.MainEditor
{
    partial class CheckerWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CheckerWindow));
            txtLog = new TextBox();
            toolStrip = new ToolStrip();
            toolStripBtnRunValidator = new ToolStripButton();
            toolStripBtnOpenProblems = new ToolStripButton();
            toolStrip.SuspendLayout();
            SuspendLayout();
            // 
            // txtLog
            // 
            txtLog.Dock = DockStyle.Fill;
            txtLog.Font = new Font("Consolas", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtLog.Location = new Point(0, 26);
            txtLog.Multiline = true;
            txtLog.Name = "txtLog";
            txtLog.ReadOnly = true;
            txtLog.ScrollBars = ScrollBars.Vertical;
            txtLog.Size = new Size(418, 176);
            txtLog.TabIndex = 3;
            txtLog.WordWrap = false;
            // 
            // toolStrip
            // 
            toolStrip.Items.AddRange(new ToolStripItem[] { toolStripBtnRunValidator, toolStripBtnOpenProblems });
            toolStrip.Location = new Point(0, 0);
            toolStrip.Name = "toolStrip";
            toolStrip.Size = new Size(418, 26);
            toolStrip.TabIndex = 2;
            toolStrip.Text = "toolStrip1";
            // 
            // toolStripBtnRunValidator
            // 
            toolStripBtnRunValidator.Image = (Image)resources.GetObject("toolStripBtnRunValidator.Image");
            toolStripBtnRunValidator.ImageTransparentColor = Color.Magenta;
            toolStripBtnRunValidator.Name = "toolStripBtnRunValidator";
            toolStripBtnRunValidator.Size = new Size(94, 23);
            toolStripBtnRunValidator.Text = "Run Check";
            toolStripBtnRunValidator.ToolTipText = "Clear Log";
            toolStripBtnRunValidator.Click += toolStripBtnRunValidator_Click;
            // 
            // toolStripBtnOpenProblems
            // 
            toolStripBtnOpenProblems.Alignment = ToolStripItemAlignment.Right;
            toolStripBtnOpenProblems.Image = (Image)resources.GetObject("toolStripBtnOpenProblems.Image");
            toolStripBtnOpenProblems.ImageTransparentColor = Color.Magenta;
            toolStripBtnOpenProblems.Name = "toolStripBtnOpenProblems";
            toolStripBtnOpenProblems.Size = new Size(124, 23);
            toolStripBtnOpenProblems.Text = "Open Problems";
            toolStripBtnOpenProblems.Click += toolStripBtnOpenProblems_Click;
            // 
            // CheckerWindow
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(418, 202);
            Controls.Add(txtLog);
            Controls.Add(toolStrip);
            MinimizeBox = false;
            Name = "CheckerWindow";
            StartPosition = FormStartPosition.Manual;
            Text = "Project Checker";
            toolStrip.ResumeLayout(false);
            toolStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtLog;
        private ToolStrip toolStrip;
        private ToolStripButton toolStripBtnRunValidator;
        private ToolStripButton toolStripBtnOpenProblems;
    }
}