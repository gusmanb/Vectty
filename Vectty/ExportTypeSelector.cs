using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vectty
{
    public partial class ExportTypeSelector : Form
    {
        public string FileName { get; set; }
        public SpeccyDrawExportMode Mode { get; set; }
        public string Identifier { get; set; }
        public string Address { get; set; }
        public bool IncludeCode { get; set; }
        public bool IncludePatterns { get; set; }

        public ExportTypeSelector()
        {
            InitializeComponent();
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            int parsed;

            if (string.IsNullOrWhiteSpace(txtPath.Text))
            {
                MessageBox.Show("Specify file name", "Export");
                return;
            }

            if (rbSinclair.Checked)
            {
                if (string.IsNullOrWhiteSpace(txtAddr.Text) || !int.TryParse(txtAddr.Text, out parsed))
                {
                    MessageBox.Show("Specify starting address for the data", "Export");
                    return;
                }

                Mode = SpeccyDrawExportMode.SinclairBasic;
            }
            else if (rbBoriel.Checked)
            {
                if (string.IsNullOrWhiteSpace(txtIdent.Text))
                {
                    MessageBox.Show("Specify data identifier", "Export");
                    return;
                }

                Mode = SpeccyDrawExportMode.BorielBasic;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(txtAddr.Text) || !int.TryParse(txtAddr.Text, System.Globalization.NumberStyles.AllowHexSpecifier, System.Globalization.CultureInfo.InvariantCulture, out parsed))
                {
                    MessageBox.Show("Specify starting address for the data", "Export");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtIdent.Text))
                {
                    MessageBox.Show("Specify data identifier", "Export");
                    return;
                }

                Mode = SpeccyDrawExportMode.Assembler;
            }

            FileName = txtPath.Text;
            Identifier = txtIdent.Text;
            Address = txtAddr.Text;
            IncludeCode = ckInclude.Checked;
            IncludePatterns = ckPatterns.Checked;
            DialogResult = DialogResult.OK;

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSelectPath_Click(object sender, EventArgs e)
        {
            using (var dlg = new SaveFileDialog { Filter = "Basic files (*.bas)|*.bas|Assembler files (*.asm)|*.asm|All files|*.*" })
            {
                if (dlg.ShowDialog() != DialogResult.OK)
                    return;

                txtPath.Text = dlg.FileName;
            }
        }

        private void rbAssembler_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAssembler.Checked)
            {
                ckInclude.Enabled = false;
                txtAddr.Enabled = true;
                txtIdent.Enabled = true;
            }
        }

        private void rbBoriel_CheckedChanged(object sender, EventArgs e)
        {
            if (rbBoriel.Checked)
            {
                ckInclude.Enabled = true;
                txtAddr.Enabled = false;
                txtIdent.Enabled = true;
            }
        }

        private void rbSinclair_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSinclair.Checked)
            {
                ckInclude.Enabled = true;
                txtAddr.Enabled = true;
                txtIdent.Enabled = false;
            }
        }
    }
}
