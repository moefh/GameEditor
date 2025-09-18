namespace GameEditor.CustomControls
{
    partial class GridTable
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            gridTableHeader = new GridTableHeader();
            contentPanel = new Panel();
            gridTableContent = new GridTableContent();
            contentPanel.SuspendLayout();
            SuspendLayout();
            // 
            // gridTableHeader
            // 
            gridTableHeader.Dock = DockStyle.Top;
            gridTableHeader.InactiveBackColor = SystemColors.Control;
            gridTableHeader.Location = new Point(0, 0);
            gridTableHeader.Name = "gridTableHeader";
            gridTableHeader.Size = new Size(299, 23);
            gridTableHeader.TabIndex = 0;
            gridTableHeader.TableDataSource = null;
            gridTableHeader.Text = "gridTableHeader1";
            // 
            // contentPanel
            // 
            contentPanel.AutoScroll = true;
            contentPanel.Controls.Add(gridTableContent);
            contentPanel.Dock = DockStyle.Fill;
            contentPanel.Location = new Point(0, 23);
            contentPanel.Name = "contentPanel";
            contentPanel.Size = new Size(299, 205);
            contentPanel.TabIndex = 1;
            contentPanel.Scroll += contentPanel_Scroll;
            // 
            // gridTableContent
            // 
            gridTableContent.BackColor = Color.White;
            gridTableContent.HeaderFont = null;
            gridTableContent.Location = new Point(0, 0);
            gridTableContent.Name = "gridTableContent";
            gridTableContent.NumRows = 0;
            gridTableContent.Size = new Size(151, 25);
            gridTableContent.TabIndex = 0;
            gridTableContent.TableDataSource = null;
            gridTableContent.Text = "gridTableContent1";
            // 
            // GridTable
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(contentPanel);
            Controls.Add(gridTableHeader);
            Name = "GridTable";
            Size = new Size(299, 228);
            contentPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GridTableHeader gridTableHeader;
        private Panel contentPanel;
        private GridTableContent gridTableContent;
    }
}
