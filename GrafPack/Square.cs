using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace GrafPack
{
    class Square : Shapes
    {
        //This class contains the specific details for a square defined in terms of opposite corners
        Point keyPt, oppPt;      // These points identify opposite corners of the square
        double xDiff, yDiff, xMid, yMid;   // Range and mid points of x & y  
        private GraphicsPath path;
        public Square(Point keyPt, Point oppPt)   // Constructor
        {
            this.keyPt = keyPt;
            this.oppPt = oppPt;
        }

        public void Draw(Graphics g, Pen blackPen)
        {
            // This method draws the square by calculating the positions of the other 2 corners

            // Calculate ranges and mid points
            xDiff = oppPt.X - keyPt.X;
            yDiff = oppPt.Y - keyPt.Y;
            xMid = (oppPt.X + keyPt.X) / 2;
            yMid = (oppPt.Y + keyPt.Y) / 2;

            // Draw square
            g.DrawLine(blackPen, (int)keyPt.X, (int)keyPt.Y, (int)(xMid + yDiff / 2), (int)(yMid - xDiff / 2));
            g.DrawLine(blackPen, (int)(xMid + yDiff / 2), (int)(yMid - xDiff / 2), (int)oppPt.X, (int)oppPt.Y);
            g.DrawLine(blackPen, (int)oppPt.X, (int)oppPt.Y, (int)(xMid - yDiff / 2), (int)(yMid + xDiff / 2));
            g.DrawLine(blackPen, (int)(xMid - yDiff / 2), (int)(yMid + xDiff / 2), (int)keyPt.X, (int)keyPt.Y);
            //Create a graphics path for the shape
            path = new GraphicsPath();
            path.AddLine((int)keyPt.X, (int)keyPt.Y, (int)(xMid + yDiff / 2), (int)(yMid - xDiff / 2));
            path.AddLine((int)(xMid + yDiff / 2), (int)(yMid - xDiff / 2), (int)oppPt.X, (int)oppPt.Y);
            path.AddLine((int)oppPt.X, (int)oppPt.Y, (int)(xMid - yDiff / 2), (int)(yMid + xDiff / 2));
            path.AddLine((int)(xMid - yDiff / 2), (int)(yMid + xDiff / 2), (int)keyPt.X, (int)keyPt.Y);
        }

        public GraphicsPath GetPath()
        {
            return path;
        }
    }
}
