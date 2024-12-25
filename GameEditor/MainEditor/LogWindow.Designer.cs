namespace GameEditor.MainEditor
{
    partial class LogWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogWindow));
            toolStrip = new ToolStrip();
            toolStripBtnClear = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            toolStripBtnAlwaysOnTop = new ToolStripButton();
            txtLog = new TextBox();
            toolStrip.SuspendLayout();
            SuspendLayout();
            // 
            // toolStrip
            // 
            toolStrip.Items.AddRange(new ToolStripItem[] { toolStripBtnClear, toolStripSeparator1, toolStripBtnAlwaysOnTop });
            toolStrip.Location = new Point(0, 0);
            toolStrip.Name = "toolStrip";
            toolStrip.Size = new Size(547, 26);
            toolStrip.TabIndex = 0;
            toolStrip.Text = "toolStrip1";
            // 
            // toolStripBtnClear
            // 
            toolStripBtnClear.Image = Properties.Resources.TrashIcon;
            toolStripBtnClear.ImageTransparentColor = Color.Magenta;
            toolStripBtnClear.Name = "toolStripBtnClear";
            toolStripBtnClear.Size = new Size(60, 23);
            toolStripBtnClear.Text = "Clear";
            toolStripBtnClear.ToolTipText = "Clear Log";
            toolStripBtnClear.Click += toolStripBtnClear_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Margin = new Padding(10, 0, 10, 0);
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 26);
            // 
            // toolStripBtnAlwaysOnTop
            // 
            toolStripBtnAlwaysOnTop.CheckOnClick = true;
            toolStripBtnAlwaysOnTop.Image = Properties.Resources.OnTopIcon;
            toolStripBtnAlwaysOnTop.ImageTransparentColor = Color.Magenta;
            toolStripBtnAlwaysOnTop.Name = "toolStripBtnAlwaysOnTop";
            toolStripBtnAlwaysOnTop.Size = new Size(120, 23);
            toolStripBtnAlwaysOnTop.Text = "Always On Top";
            toolStripBtnAlwaysOnTop.ToolTipText = "Stay Always On Top";
            toolStripBtnAlwaysOnTop.Click += toolStripBtnAlwaysOnTop_Click;
            // 
            // txtLog
            // 
            txtLog.Dock = DockStyle.Fill;
            txtLog.Font = new Font("Consolas", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtLog.Location = new Point(0, 26);
            txtLog.Multiline = true;
            txtLog.Name = "txtLog";
            txtLog.ReadOnly = true;
            txtLog.ScrollBars = ScrollBars.Both;
            txtLog.Size = new Size(547, 168);
            txtLog.TabIndex = 1;
            txtLog.WordWrap = false;
            // 
            // LogWindow
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(547, 194);
            Controls.Add(txtLog);
            Controls.Add(toolStrip);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimizeBox = false;
            Name = "LogWindow";
            StartPosition = FormStartPosition.Manual;
            Text = "Log";
            toolStrip.ResumeLayout(false);
            toolStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip toolStrip;
        private ToolStripButton toolStripBtnClear;
        private TextBox txtLog;
        private ToolStripButton toolStripBtnAlwaysOnTop;
        private ToolStripSeparator toolStripSeparator1;
    }
}