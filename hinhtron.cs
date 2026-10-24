using System;

namespace Input
{
    class Program
    {
        static void Main()
        {
        Console.Write("Nhap ban kinh hinh tron: ");
            double r = double.Parse(Console.ReadLine());
            double S = Math.PI * r * r;
            Console.WriteLine($"Dien tich hinh tron la: {S}");
            double C = 2 * Math.PI * r;
            Console.WriteLine($"Chu vi hinh tron la: {C}");
        }
    }
}
