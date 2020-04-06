using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vectty
{
    public static class Exetnsions
    {
        public static double Distance(this Point A, Point B)
        {
            int distX = B.X - A.X;
            int distY = B.Y - A.Y;

            return Math.Sqrt(distX * distX + distY * distY);
        }
    }
}
