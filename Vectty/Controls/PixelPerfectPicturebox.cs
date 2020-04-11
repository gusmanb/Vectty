﻿using System;
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
    public partial class PixelPerfectPicturebox : PictureBox
    {
        bool pp = true;
        public bool PixelPerfect { get { return pp; } set { pp = value; this.Invalidate(); } }
        public PixelPerfectPicturebox()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            if (pp)
            {
                pe.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                pe.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
            }
            base.OnPaint(pe);
        }
    }
}
