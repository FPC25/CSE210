using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello World! This is the Fractions Project.");

        List<Fraction> numbers = new List<Fraction> { new Fraction(), new Fraction(5), new Fraction(3, 4), new Fraction(1, 3) };

        foreach (Fraction number in numbers)
        {
            Console.WriteLine(number.GetFractionString());
            Console.WriteLine(number.GetDecimalValue());
        }
    }
}