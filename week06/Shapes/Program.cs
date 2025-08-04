using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello World! This is the Shapes Project.");

        Square s = new Square(15.2, "blue");
        Circle c = new Circle(7.5, "red");
        Rectangle r = new Rectangle(10.0, 5.0, "green");


        List<Shape> shapes = new List<Shape>() { s, c, r };

        foreach (Shape shape in shapes)
        {
            Console.WriteLine($"The {shape.GetColor()} shape has an area of {shape.GetArea():F2} u^2");
        }
    }
}