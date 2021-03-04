// Рассчитать максимальную степень двойки, на которую делится произведение 
// подряд идущих чисел от a до b (числа целые 64-битные без знака).

using System;

namespace Task3
{
  class Program
  {
    private static UInt64 MultFromAToB(UInt64 a, UInt64 b)
    {
      UInt64 mult = 1;
      
      for (UInt64 i = a; i <= b;) 
      {
        checked
        {
          mult *= i;
          i++;
        }
      }
      
      return mult;
    }
    
    private static UInt64 CalculatePowerOfTwo(UInt64 a) {
      UInt64 pow = 1;
      UInt64 powMax = pow;
      
      while (pow <= a) {
        if (a % pow == 0)
        {
          powMax = pow;
        }
        else
        {
          break;
        }
        
        pow *= 2;
      }
      
      return powMax;
    }
    
    public static int Main(string[] args)
    {
      UInt64 a = 0, b = 0;
      bool error = false;
      
      do
      {
        Console.Write("Enter a: ");
        string strA = Console.ReadLine();
        error = !UInt64.TryParse(strA, out a);
      } while (error);
      
      do
      {
        Console.Write("Enter b (>= a): ");
        string strB = Console.ReadLine();
        error = !UInt64.TryParse(strB, out b) || (b < a);
      } while (error);
      
      Console.WriteLine("a = " + a + ", b = " + b);
      
      try
      {
        UInt64 product = MultFromAToB(a, b);
        Console.WriteLine("The product of the numbers from a to b: " + product);
        UInt64 pow = CalculatePowerOfTwo(product);
        Console.WriteLine("The maximum power of two that divides the product: " + pow);
      }
      catch (OverflowException)
      {
        Console.WriteLine("Cannot calculate the product of the numbers from a to b: overflow");
      }
      
      return 0;
    }
  }
}
