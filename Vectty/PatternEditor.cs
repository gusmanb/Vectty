using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vectty
{
    public partial class PatternEditor : Form
    {

        Bitmap pixels;
        Panel selectedPanel;

        public event EventHandler<IndexEventArgs> SelectedPatternChanged;

        public PatternEditor()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            DoubleBuffered = true;

            InitializeComponent();
            pixels = new Bitmap(8, 8, PixelFormat.Format32bppPArgb);
            Graphics g = Graphics.FromImage(pixels);
            g.Clear(Color.Transparent);
            g.Dispose();

            drawArea.Image = pixels;
            drawArea.MouseDown += DrawArea_MouseDown;
            drawArea.MouseMove += DrawArea_MouseMove;
        }

        private void DrawArea_MouseDown(object sender, MouseEventArgs e)
        {
            int x = e.X / 16;
            int y = e.Y / 16;

            if (x < 0 || x > 7 || y < 0 || y > 7)
                return;

            if (e.Button == MouseButtons.Left)
                pixels.SetPixel(e.X / 16, e.Y / 16, Color.Black);
            else if (e.Button == MouseButtons.Right)
                pixels.SetPixel(e.X / 16, e.Y / 16, Color.Transparent);

            drawArea.Invalidate();
        }

        private void DrawArea_MouseMove(object sender, MouseEventArgs e)
        {
            int x = e.X / 16;
            int y = e.Y / 16;

            if (x < 0 || x > 7 || y < 0 || y > 7)
                return;

            if (e.Button == MouseButtons.Left)
                pixels.SetPixel(e.X / 16, e.Y / 16, Color.Black);
            else if (e.Button == MouseButtons.Right)
                pixels.SetPixel(e.X / 16, e.Y / 16, Color.Transparent);

            if(e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
                drawArea.Invalidate();  

        }
        private void drawArea_MouseUp(object sender, MouseEventArgs e)
        {
            if (selectedPanel != null)
            {
                selectedPanel.Invalidate();
                selectedPanel.Tag = BmpToData(selectedPanel.BackgroundImage as Bitmap);
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            for (int buc = 1; buc < 8; buc++)
            {
                int offset = buc * 16;

                e.Graphics.DrawLine(Pens.Black, drawArea.Left, drawArea.Top + offset, drawArea.Right, drawArea.Top + offset);
                e.Graphics.DrawLine(Pens.Black, drawArea.Left + offset, drawArea.Top, drawArea.Left + offset, drawArea.Bottom);
            }

            e.Graphics.DrawRectangle(Pens.Black, drawArea.Left, drawArea.Top, drawArea.Width, drawArea.Height); ;

            Console.WriteLine(drawArea.DisplayRectangle.ToString());

            base.OnPaint(e);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (selectedPanel == null)
            {
                Panel newPnl = new Panel();
                newPnl.Size = new Size(32, 32);
                newPnl.BackgroundImageLayout = ImageLayout.Tile;
                newPnl.BackgroundImage = pixels;
                newPnl.Tag = BmpToData(pixels);
                newPnl.Click += NewPnl_Click;
                fpPatterns.Controls.Add(newPnl);

                pixels = new Bitmap(8, 8, PixelFormat.Format32bppPArgb);
                Graphics g = Graphics.FromImage(pixels);
                g.Clear(Color.Transparent);
                g.Dispose();

                drawArea.Image = pixels;

                drawArea.Invalidate();
            }
            else
            {
                pixels = pixels.Clone() as Bitmap;
                drawArea.Image = pixels;

                selectedPanel.BackColor = SystemColors.Control;
                selectedPanel.BorderStyle = BorderStyle.None;
                
                Panel newPnl = new Panel();
                newPnl.Size = new Size(32, 32);
                newPnl.BackgroundImageLayout = ImageLayout.Tile;
                newPnl.BackgroundImage = pixels;
                newPnl.Tag = BmpToData(pixels);
                newPnl.BackColor = Color.White;
                newPnl.BorderStyle = BorderStyle.FixedSingle;
                newPnl.Click += NewPnl_Click;
                fpPatterns.Controls.Add(newPnl);
                selectedPanel = newPnl;

                drawArea.Invalidate();
            }
        }

        private void NewPnl_Click(object sender, EventArgs e)
        {
            if (selectedPanel != null)
            {
                selectedPanel.BackColor = SystemColors.Control;
                selectedPanel.BorderStyle = BorderStyle.None;
            }

            selectedPanel = sender as Panel;
            selectedPanel.BackColor = Color.White;
            selectedPanel.BorderStyle = BorderStyle.FixedSingle;
            pixels = selectedPanel.BackgroundImage as Bitmap;
            drawArea.Image = pixels;

            drawArea.Invalidate();

            int idx = fpPatterns.Controls.GetChildIndex(selectedPanel);

            if (SelectedPatternChanged != null)
                SelectedPatternChanged(this, new IndexEventArgs { Index = idx });
        }

        public byte[] GetPattern(int Index)
        {
            try
            {
                if (Index < 0)
                    return null;

                return fpPatterns.Controls[Index].Tag as byte[];
            }
            catch { return null; }
        }

        public byte[][] GetAllPatterns()
        {
            List<byte[]> buffer = new List<byte[]>();

            for (int buc = 0; buc < fpPatterns.Controls.Count; buc++)
                buffer.Add(fpPatterns.Controls[buc].Tag as byte[]);

            return buffer.ToArray();
        }

        public void Reset()
        {
            for (int buc = 0; buc < fpPatterns.Controls.Count; buc++)
                fpPatterns.Controls[buc].Dispose();

            fpPatterns.Controls.Clear();
            selectedPanel = null;

            if (pixels != null)
                pixels.Dispose();

            pixels = null;

            pixels = new Bitmap(8, 8, PixelFormat.Format32bppPArgb);
            Graphics g = Graphics.FromImage(pixels);
            g.Clear(Color.Transparent);
            g.Dispose();

            drawArea.Image = pixels;
        }

        public void SetPatterns(byte[][] Patterns, bool AppendPatterns)
        {
            if(!AppendPatterns)
                Reset();

            if (Patterns == null || Patterns.Length == 0)
                return;

            for (int pt = 0; pt < Patterns.Length; pt++)
            {

                Bitmap bmp = new Bitmap(8, 8, PixelFormat.Format32bppPArgb);
                Graphics g = Graphics.FromImage(pixels);
                g.Clear(Color.Transparent);
                g.Dispose();

                for (int y = 0; y < 8; y++)
                {
                    for (int x = 0; x < 8; x++)
                        if ((Patterns[pt][y] & (1 << x)) != 0)
                            bmp.SetPixel(x, y, Color.Black);
                }

                Panel newPnl = new Panel();
                newPnl.Size = new Size(32, 32);
                newPnl.BackgroundImageLayout = ImageLayout.Tile;
                newPnl.BackgroundImage = bmp;
                newPnl.Tag = BmpToData(bmp);
                newPnl.Click += NewPnl_Click;
                fpPatterns.Controls.Add(newPnl);
            }
        }

        byte[] BmpToData(Bitmap Bmp)
        {
            byte[] data = new byte[8];
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    if (Bmp.GetPixel(x, y).A != 0)
                        data[y] = (byte)(data[y] | (1 << x));
                }
            }

            return data;
        }

        private void fpPatterns_Click(object sender, EventArgs e)
        {
            if (selectedPanel != null)
            {
                selectedPanel.BackColor = SystemColors.Control;
                selectedPanel.BorderStyle = BorderStyle.None;
                selectedPanel = null;

                pixels = new Bitmap(8, 8, PixelFormat.Format32bppPArgb);
                Graphics g = Graphics.FromImage(pixels);
                g.Clear(Color.Transparent);
                g.Dispose();

                drawArea.Image = pixels;

                drawArea.Invalidate();

                if (SelectedPatternChanged != null)
                    SelectedPatternChanged(this, new IndexEventArgs { Index = -1 });
            }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            if (selectedPanel == null)
                return;

            int index = fpPatterns.Controls.GetChildIndex(selectedPanel);

            if (index < 1)
                return;

            fpPatterns.Controls.SetChildIndex(selectedPanel, index - 1);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (selectedPanel == null)
                return;

            int index = fpPatterns.Controls.GetChildIndex(selectedPanel);

            if (index >= fpPatterns.Controls.Count)
                return;

            fpPatterns.Controls.SetChildIndex(selectedPanel, index + 1);
        }
    }

    public class IndexEventArgs : EventArgs
    {
        public int Index { get; set; }
    }
}
