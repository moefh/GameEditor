using GameEditor.GameData;
using GameEditor.Misc;
using GameEditor.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameEditor.MainEditor
{
    public partial class ProjectPropertiesDialog : Form
    {
        private readonly string[] vgaSyncBitsList = [
            "0x00 (00)",
            "0x40 (10)",
            "0x80 (10)",
            "0xc0 (11)"
        ];

        public ProjectPropertiesDialog() {
            InitializeComponent();
            comboVgaSyncBits.Items.AddRange(vgaSyncBitsList);
        }

        public byte VgaSyncBits {
            get { return (byte)((comboVgaSyncBits.SelectedIndex & 0x03) << 6); }
            set { comboVgaSyncBits.SelectedIndex = value >> 6; }
        }

        public string IdentifierPrefix {
            get { return txtIdentifierPrefix.Text; }
            set { txtIdentifierPrefix.Text = value; }
        }

        private void btnOK_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnExportHeader_Click(object sender, EventArgs e) {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Title = "Save Header File";
            dlg.Filter = "C header files (*.h)|*.h|All files|*.*";
            dlg.RestoreDirectory = true;
            if (dlg.ShowDialog() != DialogResult.OK) return;
            string content = Regex.Replace(Resources.game_data, """\${PREFIX}""", delegate(Match m) {
                return IdentifierPrefix;
            });
            content.ReplaceLineEndings("\n");
            try {
                File.WriteAllBytes(dlg.FileName, Encoding.UTF8.GetBytes(content));
            } catch (Exception ex) {
                Util.ShowError(ex, $"Error writing {dlg.FileName}", "Error Exporting Header");
                return;
            }
            MessageBox.Show("Created header file with struct declarations.", "Header Exported",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
