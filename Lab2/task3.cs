// Рассчитать максимальную степень двойки, на которую делится произведение 
// подряд идущих чисел от a до b (числа целые 64-битные без знака).

using System;

namespace Task3
{
  class Program
  {
    private static UInt64 CalculateDividingPowerOfFact(UInt64 b)
    {
      UInt64 exponent = 0;
      
      //Результатом будет [b/2] + [b/4] + [b/8] + ... 
      while (b > 0)
      {
        b /= 2;
        exponent += b;
      }
      
      return exponent;
    }
    
    public static int Main(string[] args)
    {
      UInt64 a = 0, b = 0;
      bool error = false;
      
      do
      {
        Console.Write("Enter a (> 0): ");
        string strA = Console.ReadLine();
        error = !UInt64.TryParse(strA, out a) || (a <= 0);
      } while (error);
      
      do
      {
        Console.Write("Enter b (> a): ");
        string strB = Console.ReadLine();
        error = !UInt64.TryParse(strB, out b) || (b <= a);
      } while (error);
      
      Console.WriteLine("a = " + a + ", b = " + b);
      
      UInt64 exponentA = CalculateDividingPowerOfFact(a - 1);
      UInt64 exponentB = CalculateDividingPowerOfFact(b);
      
      // Так как b! / (a - 1)! = искомому произведению, то искомая степень равна exponentB - exponentA
      Console.WriteLine("The maximum power of two that divides the product from a to b: 2^" + (exponentB - exponentA));
      
      return 0;
    }
  }
}
