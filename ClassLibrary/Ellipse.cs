using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Ellipse : Figure
    {
        public override int Width { get; set; }
        public override int Height { get; set; }

        public Ellipse(int width, int height)
        {
            Width = width;
            Height = height;

        }



        public override double Perimetr() => Math.PI*((Width/2)*(Height/2));


        public override double Area() => Math.PI * Width * Height;

        public override string ToString()
        {
            string result = string.Format("Длина созданного элипа: {0} \nПлощадь созданного элипса: {1}", Perimetr(), Area());
            return result;

        }


    }
}
