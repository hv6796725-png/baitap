using System;

class Program
{
    static void Main()
    {
        double r;
        Console.Write("Nhap ban kinh hinh tron (r): ");
        
        while (!double.TryParse(Console.ReadLine(), out r) || r <= 0)
        {
            Console.Write("Gia tri sai! Vui long nhap lai so lon hon 0: ");
        }

        Console.WriteLine("Chu vi: " + Math.Round(2 * Math.PI * r, 2));
        Console.WriteLine("Dien tich: " + Math.Round(Math.PI * r * r, 2));
    }
}
