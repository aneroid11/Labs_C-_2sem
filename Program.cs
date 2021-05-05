using System;
using System.Collections.Generic;

namespace lab7
{ 
    class MainClass
    {
        public static int Main(string[] args)
        {
            List<Rational> rationals = new List<Rational>();

            Console.WriteLine("Enter rational numbers in one of those formats:");
            Console.WriteLine("a/b");
            Console.WriteLine("(a/b)");
            Console.WriteLine("a divided by b");
            Console.WriteLine("Enter - to stop");
            string str = Console.ReadLine();
            Rational rational;

            while (str != "-")
            {
                if (!Rational.TryParse(str, out rational))
                {
                    Console.WriteLine("Invalid input. Please try again:");
                }
                else
                {
                    rationals.Add(rational);
                }

                str = Console.ReadLine();
            }

            Console.WriteLine("You entered: ");
            foreach (Rational r in rationals)
            {
                Console.WriteLine(r.ToString("B"));
            }

            rationals.Sort();

            Console.WriteLine("Sorted rational numbers:");
            Rational sum = new Rational(0, 1);

            foreach (Rational r in rationals)
            {
                Console.WriteLine(r.ToString("S"));
                sum += r;
            }

            Console.WriteLine("Sum of all numbers: " + (double)sum);
            return 0;
        }
    }
}
