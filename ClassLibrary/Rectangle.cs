


namespace ClassLibrary
{
    public class Rectangle : Figure
    {

        public override int Width { get; set; }
        public override int Height { get; set; }

        public Rectangle(int width, int height)
        {
            Width = width;
            Height = height;

        }



        public override double Perimetr() => 2 * (Width + Height);


        public override double Area() => Width * Height;

        public override string ToString()
        {
            string result = string.Format("Перимертр созданного прямоугольника: {0} \nПлощадь созданного прямоугольника: {1}", Perimetr(), Area());
            return result;
                
        }



    }
}
