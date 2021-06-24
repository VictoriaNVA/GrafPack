using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;

namespace GrafPack
{
    class Circle : Shapes
    {
        Point point1, point2;
        private GraphicsPath path;
        public Circle(Point point1, Point point2)   // constructor
        {
            this.point1 = point1;
            this.point2 = point2;
        }

        public void DrawCircle(Graphics g, Pen blackPen)
        {
            //Calculate the positions of the base corners of the equilateral triangle
            int radius = (int)Math.Sqrt(Math.Pow(point1.X - point2.X, 2) + (Math.Pow(point1.Y - point2.Y, 2)));
            int width = radius * 2;
            int height = radius * 2;
            Rectangle r = new Rectangle((int)(point1.X - width / 2), (int)(point1.Y - height / 2), (int)width, (int)height);

            g.DrawEllipse(blackPen, r);

            //Create a graphics path for the shape
            path = new GraphicsPath();
            path.AddEllipse((int)(point1.X - width / 2), (int)(point1.Y - height / 2), width, height);
        }

        public GraphicsPath GetPath()
        {
            return path;
        }

    }
}
