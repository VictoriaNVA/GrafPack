using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing.Drawing2D;

namespace GrafPack
{
    public partial class GrafPack : Form
    {
        //Global variables of all sorts
        private Pen blackpen = new Pen(Color.Black, 2);
        private Point previous = Point.Empty;
        private Point one = Point.Empty;
        private Point two = Point.Empty;
        private GraphicsPath selectedPath;
        private List<GraphicsPath> paths = new List<GraphicsPath>();
        private bool squareStatus = false;
        private bool triangleStatus = false;
        private bool circleStatus = false;
        private bool selectStatus = false;
        private bool moveShape = false;
        private bool rotateShape = false;
        private bool scaleShape = false;
        private bool deleteShape = false;
        private float angle;
        private float scaleX;
        private float scaleY;
        private int clicknumber = 0;
        //Submenu for shape options
        private ContextMenu options = new ContextMenu();
        private MenuItem move = new MenuItem();
        private MenuItem rotate = new MenuItem();
        private MenuItem scale = new MenuItem();
        private MenuItem delete = new MenuItem();
       
       

        public GrafPack()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.White;

            //Setting up menu and menu items paired with mosue clicks
            MainMenu mainMenu = new MainMenu();
            MenuItem createItem = new MenuItem();
            MenuItem selectItem = new MenuItem();
            MenuItem squareItem = new MenuItem();
            MenuItem triangleItem = new MenuItem();
            MenuItem circleItem = new MenuItem();

            createItem.Text = "&Create";
            squareItem.Text = "&Square";
            triangleItem.Text = "&Triangle";
            circleItem.Text = "&Circle";
            selectItem.Text = "&Select";

            mainMenu.MenuItems.Add(createItem);
            mainMenu.MenuItems.Add(selectItem);
            createItem.MenuItems.Add(squareItem);
            createItem.MenuItems.Add(triangleItem);
            createItem.MenuItems.Add(circleItem);

            selectItem.Click += new System.EventHandler(this.Select);
            squareItem.Click += new System.EventHandler(this.CreateSquare);
            triangleItem.Click += new System.EventHandler(this.CreateTriangle);
            circleItem.Click += new System.EventHandler(this.CreateCircle);
            //Submenu options and click events
            move.Click += new System.EventHandler(this.MoveShape);
            rotate.Click += new System.EventHandler(this.Rotate);
            scale.Click += new System.EventHandler(this.Scale);
            delete.Click += new System.EventHandler(this.Delete);

            this.Menu = mainMenu;
            this.MouseClick += mouseClick;
        }

        //Menu & Submenu options methods 
        private void CreateSquare(object sender, EventArgs e)
        {
            squareStatus = true;
            MessageBox.Show("Click OK and then click once each at two locations to create a square");
        }

        private void CreateTriangle(object sender, EventArgs e)
        {
            triangleStatus = true;
            MessageBox.Show("Click OK and then click once each at two locations to create an equilateral triangle");
        }
        private void CreateCircle(object sender, EventArgs e)
        {
            circleStatus = true;
            MessageBox.Show("Click OK and then click once each at two locations to create a circle based on radius");
        }

        private void Select(object sender, EventArgs e)
        {
            selectStatus = true;
            MessageBox.Show("You selected the Select option...");     
        }

        private void MoveShape(object sender, EventArgs e)
        {
            moveShape = true;
        }

        private void Rotate(object sender, EventArgs e)
        {
            angle = float.Parse(Prompt.ShowDialog("Please insert rotation angle (degrees)", "Rotate"));
            rotateShape = true;
        }

        private void Scale(object sender, EventArgs e)
        {

            scaleX = float.Parse(Prompt.ShowDialog("Please insert x value (i.e.: 1.2, 1.5, 1.7, 2)", "Scale"));
            scaleY = float.Parse(Prompt.ShowDialog("Please insert y value (i.e.: 1.2, 1.5, 1.7, 2)", "Scale"));
            scaleShape = true;
        }

        private void Delete(object sender, EventArgs e)
        {
            deleteShape = true;
        }

        //This method detects mouse clicks for the Create menu options.
        private void mouseClick(object sender, MouseEventArgs e)
        {
            //Brush & graphics object
            SolidBrush brush = new SolidBrush(Color.CornflowerBlue);
            Graphics g = this.CreateGraphics();


            //Draw shape according to option selected
            if (e.Button == MouseButtons.Left)
            {
                if (squareStatus)
                {
                    if (clicknumber == 0)
                    {
                        one = new Point(e.X, e.Y);
                        clicknumber = 1;
                    }
                    else
                    {
                        two = new Point(e.X, e.Y);
                        clicknumber = 0;
                        squareStatus = false;
                        //Create square object & draw it to the screen
                        Square aShape = new Square(one, two);
                        aShape.Draw(g, blackpen);
                        //Get square's Graphics Path, fill and add to list of paths (shapes)
                        var path = aShape.GetPath();
                        g.FillPath(brush, path);    
                        paths.Add(path);

                    }
                }

                if (triangleStatus)
                {
                    if (clicknumber == 0)
                    {
                        one = new Point(e.X, e.Y);
                        clicknumber = 1;
                    }
                    else
                    {
                        two = new Point(e.X, e.Y);
                        clicknumber = 0;
                        triangleStatus = false;
                        //Create triangle object & draw it to the screen
                        EqTriangle eqTriangle = new EqTriangle(one, two);
                        eqTriangle.DrawEquilateral(g, blackpen);
                        //Get triangle's Graphics Path, fill and add to list of paths (shapes)
                        var path = eqTriangle.GetPath();
                        g.FillPath(brush, path);
                        paths.Add(path);

                    }
                }

                if (circleStatus)
                {
                    if (clicknumber == 0)
                    {
                        one = new Point(e.X, e.Y);
                        clicknumber = 1;
                    }
                    else
                    {
                        two = new Point(e.X, e.Y);
                        clicknumber = 0;
                        circleStatus = false;
                        //Create circle object & draw it to the screen
                        Circle circle = new Circle(one, two);
                        circle.DrawCircle(g, blackpen);
                        //Get circle's Graphics Path, fill and add to list of paths (shapes)
                        var path = circle.GetPath();
                        g.FillPath(brush, path);
                        paths.Add(path);
                    }
                }
            }

            //Get rid of drawing resources (memory)
            g.Dispose();
            brush.Dispose();
        }

        
        protected override void OnMouseDown(MouseEventArgs e)
        {
            Graphics g = this.CreateGraphics();
            SolidBrush highlight = new SolidBrush(Color.Gold);

            //If the "Select" menu option is activated
            if (selectStatus)
            {
                //Check which shape is clicked on
                for (int i = paths.Count - 1; i >= 0; i--)
                {
                    if (paths[i].IsVisible(e.Location))
                    {
                        selectedPath = paths[i];
                        g.FillPath(highlight, selectedPath);

                        //Context Menu details
                        move.Text = "&Move";
                        rotate.Text = "&Rotate";
                        scale.Text = "&Scale";
                        delete.Text = "&Delete";
                        options.MenuItems.Add(move);
                        options.MenuItems.Add(rotate);
                        options.MenuItems.Add(scale);
                        options.MenuItems.Add(delete);
                        this.ContextMenu = options;
                        previous = e.Location;
                        options.Show(this, MousePosition);

                        //"Reset" the canvas
                        this.Invalidate();
                    }
                    
                }
                selectStatus = false;
            }
            base.OnMouseDown(e);
        }
        
        /*This method is used for moving shapes with drag & drop approach
         *Using double buffering to decrease shapes flickering.
         *The moving shape trails during drag & drop - could be improved.
         */
        protected override void OnMouseMove(MouseEventArgs e)
        {
            //Back buffered image
            Bitmap backBuffer = new Bitmap(this.Width, this.Height);
            //Graphic objects to front and back buffers
            Graphics g = this.CreateGraphics();
            Graphics g2 = Graphics.FromImage(backBuffer);

            //Brushes for normal filling and "selected" filling
            SolidBrush highlight = new SolidBrush(Color.Gold);
            SolidBrush fill = new SolidBrush(Color.CornflowerBlue);

            //Making sure the other shapes on the screen are showing
            for (int i = paths.Count - 1; i >= 0; i--)
            {
                if (selectedPath == paths[i])
                {
                    paths.RemoveAt(i);
                }
                else
                {
                    g2.FillPath(fill, paths[i]);
                    g.DrawPath(blackpen, paths[i]);
                }
            }
            //If the move option was selected
            if (moveShape)
            {
                //"Translate" the matrix according to cursor's live position
                Matrix move = new Matrix();
                var d = new Point(e.X - previous.X, e.Y - previous.Y);
                move.Translate(d.X, d.Y);
                selectedPath.Transform(move);
                g2.FillPath(highlight, selectedPath);
                g2.DrawPath(blackpen, selectedPath);
                previous = e.Location;
                //Draw to front buffer
                g.DrawImage(backBuffer, 0, 0);
                //This keeps refreshing the screen with the location of the moving shape
                this.Invalidate();
            }

            //Dispose of drawing resources
            g.Dispose();
            g2.Dispose();
            backBuffer.Dispose();
            highlight.Dispose();
            fill.Dispose();

            base.OnMouseMove(e);
        }

        /*This method takes care of all "selected" submenu options.
         *Not sure why but after rotation / scaling, the transformed shape
         *doesn't show until clicking the screen again.
         */
        protected override void OnMouseUp(MouseEventArgs e)
        {
            Graphics g = this.CreateGraphics();
            SolidBrush highlight = new SolidBrush(Color.Gold);
            SolidBrush fill = new SolidBrush(Color.CornflowerBlue);

            for (int i = paths.Count - 1; i >= 0; i--)
            {
                if (selectedPath == paths[i])
                {
                    paths.RemoveAt(i);
                }
                else
                {
                    g.FillPath(fill, paths[i]);
                    g.DrawPath(blackpen, paths[i]);
                }
            }
            //If rotate option was selected
            if (rotateShape)
            {
                //Rotate at user's input angle
                Matrix rotate = new Matrix();
                rotate.RotateAt(angle, previous);
                selectedPath.Transform(rotate);
                //Drawing transformed path
                g.FillPath(highlight, selectedPath);
                g.DrawPath(blackpen, selectedPath);

                //Remove original unmodified path from list
                for (int i = paths.Count - 1; i >= 0; i--)
                {
                    if (selectedPath == paths[i])
                    {
                        paths.RemoveAt(i);
                    }
                }

                paths.Add(selectedPath);
                rotateShape = false;
            }
            //If scale option was selected
            if (scaleShape)
            {
                //Scale up shape according to user's input
                Matrix scale = new Matrix();
                scale.Scale(scaleX, scaleY);
                selectedPath.Transform(scale);
                //Drawing transformed path
                g.FillPath(highlight, selectedPath);
                g.DrawPath(blackpen, selectedPath);

                //Remove original unmodified path from list
                for (int i = paths.Count - 1; i >= 0; i--)
                {
                    if (selectedPath == paths[i])
                    {
                        paths.RemoveAt(i);
                    }
                }

                paths.Add(selectedPath);
                scaleShape = false;
            }

            //If move option was selected
            if (moveShape)
            {
               //Drawing path
               g.FillPath(highlight, selectedPath);
               g.DrawPath(blackpen, selectedPath);

               //Remove original unmodified path from list
               for (int i = paths.Count - 1; i >= 0; i--)
               {
                   if (selectedPath == paths[i])
                   {
                       paths.RemoveAt(i);
                   }
               }

               paths.Add(selectedPath);
               moveShape = false;
            }

            //If delete option was selected
            if (deleteShape)
            {
                //Remove path
                selectedPath.Dispose();
                for (int i = paths.Count - 1; i >= 0; i--)
                {
                    if (selectedPath == paths[i])
                    {
                        paths.RemoveAt(i);
                    }
                }
                deleteShape = false;
            }

            selectedPath = null;

            //Dispose of drawing resources
            g.Dispose();
            highlight.Dispose();
            fill.Dispose();

            base.OnMouseUp(e);
        }
    }

}


