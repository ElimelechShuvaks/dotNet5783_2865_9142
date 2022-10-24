using System;

namespace Targil0
{
  partial class Program
    {
        static void Main(string[] args)
        {
            Welcome2865();
            Welcome9142();
            Console.ReadKey();
        }
        static partial void Welcome9142();
        private static void Welcome2865()
        {
            Console.Write("Enter your name: ");
            string n = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", n);
        }
    }
}