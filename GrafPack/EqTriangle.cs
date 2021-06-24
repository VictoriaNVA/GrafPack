using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace GrafPack
{
    class EqTriangle : Shapes
    {

        public PointF point1, point2, point3;
        private GraphicsPath path;
        public EqTriangle(PointF point2, PointF point3)   // constructor
        {
            this.point2 = point2;
            this.point3 = point3;
        }

        public void DrawEquilateral(Graphics g, Pen blackPen)
        {
            //Calculate the positions of the base corners of the equilateral triangle
            //Code adapted from  https://stackoverflow.com/a/23241331/15337490
            double xDiff = point2.X - point3.X;
            double yDiff = point2.Y - point3.Y;
            double length = Math.Sqrt(xDiff * xDiff + yDiff * yDiff);
            double height = Math.Sqrt(3) / 2 * length;
            double xMid = (point2.X + point3.X) / 2;
            double yMid = (point2.Y + point3.Y) / 2;
            double perpDirX = -(yDiff / length);
            double perpDirY = xDiff / length;

            point1 = new PointF((float)(xMid + height * perpDirX), (float)(yMid + height * perpDirY));

            PointF[] triangle = new PointF[] { point1, point2, point3, point1 };
            // draw triangle  
            g.DrawLines(blackPen, triangle);

            //Create a graphics path for the shape
            path = new GraphicsPath();
            path.AddLines(triangle);
        }

        public GraphicsPath GetPath()
        {
            return path;
        }

    }
}
