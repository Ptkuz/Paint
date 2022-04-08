using System;
using System.Drawing;
using System.Windows.Forms;
using ClassLibrary;
using System.Collections.Generic;


namespace Paint
{
    public partial class MainForm : Form
    {


        Bitmap bm;
        Bitmap bmLast;
        Graphics g;
        Graphics gLast;
        bool paint = false;
        Point px, py;


        ColorDialog cd = new ColorDialog();
        Color new_color;
       
        int index;
        bool minus = false;
        static int tickness=1;
        int x, y, width, height, cX, cY;
        Pen p = new Pen(System.Drawing.Color.Black, tickness);
        Pen eracer = new Pen(System.Drawing.Color.White, tickness);
        Figure figure;


        public MainForm()
        {

            InitializeComponent();
            this.Width = 1900;
            this.Height = 850;
            bm = new Bitmap(picture.Width, picture.Height);
            g = Graphics.FromImage(bm);
            g.Clear(System.Drawing.Color.White);
            picture.Image = bm;
        }


        private void MainForm_Load(object sender, EventArgs e)
        {


        }



        private void picture_MouseMove(object sender, MouseEventArgs e)
        {
            if (paint)
            {
                if (index == 1)
                {
                    px = e.Location;
                    g.DrawLine(p, px, py);
                    py = px;

                }

                if (index == 5) 
                {
                    px = e.Location;
                    g.DrawLine (eracer, px, py);
                    py = px;
                }

            }
            



            picture.Refresh();

           


        }

        

        private void picture_MouseUp(object sender, MouseEventArgs e)
        {
            
            paint = false;

            x = e.X;
            y = e.Y;
            width = x - cX;
            height = y - cY;
            

            if (index == 4)
            {
                figure = new Ellipse(width, height);

                g.DrawEllipse(p, cX, cY, width, height);
                Information_label.Text = figure.ToString();

            }


            if (index == 3)
            {

                py = e.Location;
                g.DrawLine(p, px, py);
            }

            if (index == 2)
            {
                if (width < 0 || height < 0)
                {
                    width = cX - x;
                    height = cY - y;
                    minus = true;
                }

                figure = new ClassLibrary.Rectangle(width, height);
                if (minus)
                {
                   
                    g.DrawRectangle(p, x, y, width, height);
                   
                }
                else
                    g.DrawRectangle(p, cX, cY, width, height);

                Information_label.Text = figure.ToString();
                minus = false;

                



            }


        }

        private void thickness_bar_Scroll(object sender, EventArgs e)
        {
            thickness_bar.SetRange(1,100);
            tickness = thickness_bar.Value;
            p.Width = thickness_bar.Value;
            eracer.Width = thickness_bar.Value;
        }

       

        private void picture_MouseDown(object sender, MouseEventArgs e)
        {


            paint = true;
            py = e.Location;

            if (index == 4 || index == 2)
            {

                cX = e.X;
                cY = e.Y;
            }
            if (index == 3)
            {
                px = e.Location;
            }
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            g.Clear(System.Drawing.Color.White);
            picture.Image = bm;
            index = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void Color_Click(object sender, EventArgs e)
        {
            cd.ShowDialog();
            new_color = cd.Color;
            pictur_color.BackColor = cd.Color;
            p.Color = cd.Color;
        }

        private void picture_Paint(object sender, PaintEventArgs e)
        {

           
        }

        

        private void Rect_Click(object sender, EventArgs e)
        {
            index = 2;
        }

        private void Line_Click(object sender, EventArgs e)
        {
            index = 3;
        }

        private void Circle_Click(object sender, EventArgs e)
        {
            index = 4;
        }

        private void picture_MouseClick(object sender, MouseEventArgs e)
        {
            if (index==7) 
            {
                Point p = set_point(picture, e.Location);
                FillMethod(bm, p.X, p.Y, new_color);

                
            }
        }

        private void Fill_Click(object sender, EventArgs e)
        {
            index = 7;
        }

        private void Pancel_Click(object sender, EventArgs e)
        {
            index = 1;
        }

        private void Eraser_Click(object sender, EventArgs e)
        {
            index = 5;
        }

        static Point set_point(PictureBox pb, Point pt) 
        {
            float pX = 1f * pb.Image.Width / pb.Width;
            float pY = 1f * pb.Image.Height / pb.Height;
            return new Point((int)(pt.X*pX), (int)(pt.Y*pY));
        }
        private void pic_color_MouseClick(object sender, MouseEventArgs e)
        {
            Point point = set_point(pic_color, e.Location);
            pictur_color.BackColor = ((Bitmap)pic_color.Image).GetPixel(point.X,point.Y);
            new_color = pictur_color.BackColor;
            p.Color = pictur_color.BackColor;
        }


        private void validate(Bitmap bm, Stack<Point> sp, int x, int y, Color old_color, Color new_color) 
        { 
            Color cx = bm.GetPixel(x, y);
            if (cx == old_color) 
            {
                sp.Push(new Point(x,y));
                bm.SetPixel(x, y, new_color);
            
            }
        
        }

        public void FillMethod(Bitmap bm, int x, int y, Color new_clr) 
        { 
            Color old_color = bm.GetPixel(x, y);
            Stack<Point> pixel = new Stack<Point>();
            pixel.Push(new Point(x,y));
            bm.SetPixel(x,y,new_clr);
            if (old_color == new_clr) return;
            while (pixel.Count>0) 
            { 
                Point p = (Point)pixel.Pop();
                if (p.X>0 && p.Y>0 && p.X<bm.Width-1&& p.Y<bm.Height-1) 
                {
                    validate(bm, pixel, p.X - 1, p.Y, old_color, new_clr);
                    validate(bm, pixel, p.X, p.Y-1, old_color, new_clr);
                    validate(bm, pixel, p.X+1, p.Y, old_color, new_clr);
                    validate(bm, pixel, p.X, p.Y+1, old_color, new_clr); 

                }
            }
        }
    }
}
