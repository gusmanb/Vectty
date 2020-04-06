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
        public ExportTypeSelector()
        {
            InitializeComponent();
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtPath.Text))
            {
                MessageBox.Show("Specify file name");
                return;
            }

            if (rbSinclair.Checked)
                Mode = SpeccyDrawExportMode.SinclairBasic;
            else if (rbBoriel.Checked)
                Mode = SpeccyDrawExportMode.BorielBasic;
            else
                Mode = SpeccyDrawExportMode.Assembler;

            FileName = txtPath.Text;

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
    }
}
