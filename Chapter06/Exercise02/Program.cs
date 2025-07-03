using System;

namespace Exercise02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Square square = new(80.00);
            Console.WriteLine($"Площадь квадрата - {square.Area}");
            Console.WriteLine(new string('-',40));
            Circle circle = new(50.00);
            Console.WriteLine($"Площадь круга - {circle.Area}");
            Console.WriteLine(new string('-', 40));
            Rectangle rectangle = new(50.00,35.00);
            Console.WriteLine($"Площадь прямоугольника - {rectangle.Area}");
            Console.WriteLine(new string('-', 40));
        }
    }
}
