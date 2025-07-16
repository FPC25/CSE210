using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello World! This is the Fractions Project.");

        Fraction number1 = new Fraction();
        Fraction number2 = new Fraction(5);
        Fraction number3 = new Fraction(3, 4);
        Fraction number4 = new Fraction(1, 3);

        List<Fraction> numbers = new List<Fraction> { number1, number2, number3, number4 };

        foreach (Fraction number in numbers)
        {
            Console.WriteLine(number.GetFractionString());
            Console.WriteLine(number.GetDecimalValue());
        }
    }
}