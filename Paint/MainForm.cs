using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint
{
    public partial class MainForm : Form
    {
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

        Bitmap bm;
        Graphics g;
        bool paint = false;
        Point px, py;
        Pen p = new Pen(System.Drawing.Color.Black, 1);
        int index;

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
        }
            picture.Refresh();

        }

        private void picture_MouseUp(object sender, MouseEventArgs e)
        {
          
            paint = false;
        }

     

        private void picture_MouseDown(object sender, MouseEventArgs e)
        {
            
            paint = true;
            py = e.Location;
        }

        private void Pancel_Click(object sender, EventArgs e)
        {
            index = 1; 
        }









    }
}
