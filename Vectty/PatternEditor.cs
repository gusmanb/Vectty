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

        public PatternEditor()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            DoubleBuffered = true;

            InitializeComponent();
            Console.WriteLine(drawArea.DisplayRectangle.ToString());
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

            drawArea.Invalidate();
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

            Panel newPnl = new Panel();
            newPnl.Size = new Size(32, 32);
            newPnl.BackgroundImageLayout = ImageLayout.Tile;
            newPnl.BackgroundImage = pixels;
            fpPatterns.Controls.Add(newPnl);

            pixels = new Bitmap(8, 8, PixelFormat.Format32bppPArgb);
            Graphics g = Graphics.FromImage(pixels);
            g.Clear(Color.Transparent);
            g.Dispose();

            drawArea.Image = pixels;

            drawArea.Invalidate();

        }
    }
}
