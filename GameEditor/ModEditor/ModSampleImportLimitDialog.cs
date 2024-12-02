using GameEditor.GameData;
using GameEditor.Misc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GameEditor.ModEditor
{
    public partial class ModSampleImportLimitDialog : Form
    {
        public enum Result
        {
            Proceed,
            ClipToProject,
            ClipToMod,
            ChangeSettings,
            Cancel,
        }

        private const string originalMessage =
            "The selected import settings would result in too many samples being " +
            "imported: {numImportedSamples}.\n\n" +
            "The maximum sample size supported by this editor is {maxProjectSamples}, " +
            "and the maximum allowed in a MOD file is {maxModSamples}.\n\n" +
            "Select one of the options below to continue:";
        private Result userSelection = Result.Cancel;
        private int numImportedSamples;

        public ModSampleImportLimitDialog() {
            InitializeComponent();
        }

        public int NumImportedSamples {
            get { return numImportedSamples; }
            set {
                numImportedSamples = value;
                if (numImportedSamples <= ModData.MAX_SAMPLE_LENGTH) {
                    btnClipToProject.Text = "Continue";
                }
                UpdateMessage();
            }
        }

        public Result UserSelection {
            get { return userSelection; }
        }

        public void UpdateMessage() {
            lblMessage.Text = Regex.Replace(originalMessage, @"{([A-Za-z0-9_]+)}", delegate (Match m) {
                string name = m.Groups[1].ToString();
                return name switch {
                    "\\n" => "\n",
                    "numImportedSamples" => NumImportedSamples.ToString(),
                    "maxProjectSamples" => ModData.MAX_SAMPLE_LENGTH.ToString(),
                    "maxModSamples" => ModFile.MAX_SAMPLE_LENGTH.ToString(),
                    _ => "??",
                };
            });
        }

        private void btnClipToProject_Click(object sender, EventArgs e) {
            if (numImportedSamples <= ModData.MAX_SAMPLE_LENGTH) {
                userSelection = Result.Proceed;
            } else {
                userSelection = Result.ClipToProject;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnClipToMod_Click(object sender, EventArgs e) {
            userSelection = Result.ClipToMod;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnChangeSettings_Click(object sender, EventArgs e) {
            userSelection = Result.ChangeSettings;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
