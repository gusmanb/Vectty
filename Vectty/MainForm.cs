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
    public partial class MainForm : Form
    {
        SpeccyDrawControl drawArea;
        Form containerForm;
        public MainForm()
        {
            InitializeComponent();
            cbScale.SelectedIndex = 1;
            cbScale.SelectedIndexChanged += CbScale_SelectedIndexChanged;

            cbBGMode.SelectedIndex = 0;
            cbBGMode.SelectedIndexChanged += cbBGMode_SelectedIndexChanged;

            drawArea = new SpeccyDrawControl();
            drawArea.Scale = 2;
            drawArea.Tool = SpeccyDrawControlTool.Line;
            drawArea.Mode = SpeccyDrawControlMode.Bitmap;
            drawArea.ActiveAttribute.Bright = false;
            drawArea.ActiveAttribute.Ink = ZXClasses.ZXColor.Black;
            drawArea.ActiveAttribute.Paper = ZXClasses.ZXColor.White;
            drawArea.HistoryChanged += drawArea_HistoryChanged;
            drawArea.PolyToolChanged += DrawArea_PolyToolChanged;

            containerForm = new Form();
            containerForm.FormBorderStyle = FormBorderStyle.FixedSingle;
            containerForm.TopLevel = false;
            containerForm.ClientSize = drawArea.Size;
            containerForm.MaximizeBox = false;
            containerForm.MinimizeBox = false;
            containerForm.FormClosing += containerForm_FormClosing;

            containerForm.Controls.Add(drawArea);
            containerForm.Move += ContainerForm_Move;
            drawArea.Location = Point.Empty;
            drawArea.Visible = false;

            windowPanel.Controls.Add(containerForm);
            containerForm.Visible = true;
            drawArea.Visible = true;

        }

        private void DrawArea_PolyToolChanged(object sender, EventArgs e)
        {
            pnlExtra.Visible = drawArea.PolyTool;
        }

        private void ContainerForm_Move(object sender, EventArgs e)
        {
            windowPanel.AutoScroll = true;
        }

        private void containerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }

        private void CbScale_SelectedIndexChanged(object sender, EventArgs e)
        {
            drawArea.Scale = int.Parse(cbScale.SelectedItem.ToString());
            containerForm.ClientSize = drawArea.Size;
        }

        private void btnBlackPaper_Click(object sender, EventArgs e)
        {
            drawArea.ActiveAttribute.Paper = ZXClasses.ZXColor.Black;
            pnlPaper.BackColor = Color.Black;
        }

        private void btnBluePaper_Click(object sender, EventArgs e)
        {
            drawArea.ActiveAttribute.Paper = ZXClasses.ZXColor.Blue;
            pnlPaper.BackColor = Color.Blue;
        }

        private void btnRedPaper_Click(object sender, EventArgs e)
        {
            drawArea.ActiveAttribute.Paper = ZXClasses.ZXColor.Red;
            pnlPaper.BackColor = Color.Red;
        }

        private void btnPinkPaper_Click(object sender, EventArgs e)
        {
            drawArea.ActiveAttribute.Paper = ZXClasses.ZXColor.Magenta;
            pnlPaper.BackColor = Color.Magenta;
        }

        private void btnGreenPaper_Click(object sender, EventArgs e)
        {
            drawArea.ActiveAttribute.Paper = ZXClasses.ZXColor.Green;
            pnlPaper.BackColor = Color.FromArgb(0, 255, 0);
        }

        private void btnCyanPaper_Click(object sender, EventArgs e)
        {
            drawArea.ActiveAttribute.Paper = ZXClasses.ZXColor.Cyan;
            pnlPaper.BackColor = Color.Cyan;
        }

        private void btnYellowPaper_Click(object sender, EventArgs e)
        {
            drawArea.ActiveAttribute.Paper = ZXClasses.ZXColor.Yellow;
            pnlPaper.BackColor = Color.Yellow;
        }

        private void btnWhitePaper_Click(object sender, EventArgs e)
        {
            drawArea.ActiveAttribute.Paper = ZXClasses.ZXColor.White;
            pnlPaper.BackColor = Color.White;
        }

        private void btnBlackInk_Click(object sender, EventArgs e)
        {
            drawArea.ActiveAttribute.Ink = ZXClasses.ZXColor.Black;
            pnlInk.BackColor = Color.Black;
        }

        private void btnBlueInk_Click(object sender, EventArgs e)
        {
            drawArea.ActiveAttribute.Ink = ZXClasses.ZXColor.Blue;
            pnlInk.BackColor = Color.Blue;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            drawArea.ActiveAttribute.Ink = ZXClasses.ZXColor.Red;
            pnlInk.BackColor = Color.Red;
        }

        private void btnPinkInk_Click(object sender, EventArgs e)
        {
            drawArea.ActiveAttribute.Ink = ZXClasses.ZXColor.Magenta;
            pnlInk.BackColor = Color.Magenta;
        }

        private void btnCyanInk_Click(object sender, EventArgs e)
        {
            drawArea.ActiveAttribute.Ink = ZXClasses.ZXColor.Cyan;
            pnlInk.BackColor = Color.Cyan;
        }

        private void btnGreenInk_Click(object sender, EventArgs e)
        {
            drawArea.ActiveAttribute.Ink = ZXClasses.ZXColor.Green;
            pnlInk.BackColor = Color.FromArgb(0, 255, 0);
        }

        private void btnYellowInk_Click(object sender, EventArgs e)
        {
            drawArea.ActiveAttribute.Ink = ZXClasses.ZXColor.Yellow;
            pnlInk.BackColor = Color.Yellow;
        }

        private void btnWhiteInk_Click(object sender, EventArgs e)
        {
            drawArea.ActiveAttribute.Ink = ZXClasses.ZXColor.White;
            pnlInk.BackColor = Color.White;
        }

        private void btnBright_Click(object sender, EventArgs e)
        {
            drawArea.ActiveAttribute.Bright = !drawArea.ActiveAttribute.Bright;
            pnlBright.BackgroundImage = drawArea.ActiveAttribute.Bright ? Vectty.Properties.Resources.BrightOn : Vectty.Properties.Resources.BrightOff;
        }

        private void btnLine_Click(object sender, EventArgs e)
        {
            drawArea.Tool = SpeccyDrawControlTool.Line;
            pnlTool.BackgroundImage = btnLine.Image;
        }

        private void btnCircle_Click(object sender, EventArgs e)
        {
            drawArea.Tool = SpeccyDrawControlTool.Circle;
            pnlTool.BackgroundImage = btnCircle.Image;
        }

        private void btnArc_Click(object sender, EventArgs e)
        {
            drawArea.Tool = SpeccyDrawControlTool.Arc;
            pnlTool.BackgroundImage = btnArc.Image;
        }

        private void btnFill_Click(object sender, EventArgs e)
        {
            drawArea.Tool = SpeccyDrawControlTool.Fill;
            pnlTool.BackgroundImage = btnFill.Image;
        }

        private void btnErase_Click(object sender, EventArgs e)
        {
            drawArea.Tool = SpeccyDrawControlTool.BlockEraser;
            pnlTool.BackgroundImage = btnErase.Image;
        }

        private void btnBrush_Click(object sender, EventArgs e)
        {
            drawArea.Tool = SpeccyDrawControlTool.Brush;
            pnlTool.BackgroundImage = btnBrush.Image;
        }

        private void btnBitmap_Click(object sender, EventArgs e)
        {
            drawArea.Mode = SpeccyDrawControlMode.Bitmap;
            pnlMode.BackgroundImage = btnBitmap.Image;
        }

        private void btnInk_Click(object sender, EventArgs e)
        {
            drawArea.Mode = SpeccyDrawControlMode.Ink;
            pnlMode.BackgroundImage = btnInk.Image;
        }

        private void btnPaper_Click(object sender, EventArgs e)
        {
            drawArea.Mode = SpeccyDrawControlMode.Paper;
            pnlMode.BackgroundImage = btnPaper.Image;
        }

        private void btnInkPaper_Click(object sender, EventArgs e)
        {
            drawArea.Mode = SpeccyDrawControlMode.InkPaper;
            pnlMode.BackgroundImage = btnInkPaper.Image;
        }

        private void btnGrid_Click(object sender, EventArgs e)
        {
            drawArea.Grid = !drawArea.Grid;
            drawArea.Refresh();
        }

        private void btnRect_Click(object sender, EventArgs e)
        {
            drawArea.Tool = SpeccyDrawControlTool.Rectangle;
            pnlTool.BackgroundImage = btnRect.Image;
        }

        private void drawArea_HistoryChanged(object sender, EventArgs e)
        {
            btnUndo.Enabled = drawArea.CanUndo;
            btnRedo.Enabled = drawArea.CanRedo;
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            drawArea.Undo();
        }

        private void btnRedo_Click(object sender, EventArgs e)
        {
            drawArea.Redo();
        }

        private void btnSetInk_Click(object sender, EventArgs e)
        {
            drawArea.FillInk();
        }

        private void btnSetPaper_Click(object sender, EventArgs e)
        {
            drawArea.FillPaper();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (var dlg = new SaveFileDialog { Filter = "Vectty drawing|*.vct" })
            {
                if (dlg.ShowDialog() != DialogResult.OK)
                    return;

                if (drawArea.SaveFile(dlg.FileName))
                    MessageBox.Show("Save file", "File saved");
                else
                    MessageBox.Show("Save file", "Error saving file");
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to clear the drawing?", "New drawing", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            drawArea.Cleanup();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog { Filter = "Vectty drawing|*.vct" })
            {
                if (dlg.ShowDialog() != DialogResult.OK)
                    return;

                drawArea.LoadFile(dlg.FileName);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            using (var dlg = new ExportTypeSelector())
            {
                if (dlg.ShowDialog() != DialogResult.OK)
                    return;

                if (drawArea.ExportFile(dlg.FileName, dlg.Mode))
                    MessageBox.Show("Export", "File exported");
                else
                    MessageBox.Show("Export", "Error exporting file");
            }
        }

        private void btnBG_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                try
                {
                    dlg.Filter = "Bitmaps (*.bmp)|*.bmp|PNG image (*.png)|*.png|JPEG image (*.jpg)|*.jpg";

                    if (dlg.ShowDialog() != DialogResult.OK)
                        return;

                    drawArea.BackgroundImage = Image.FromFile(dlg.FileName);
                }
                catch { MessageBox.Show("Error loading background image", "Error"); }
            }
        }

        private void cbBGMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            drawArea.BackgroundMode = (SpeccyDrawControlBGMode)cbBGMode.SelectedIndex;
        }
    }
}
