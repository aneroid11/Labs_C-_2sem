// Рассчитать максимальную степень двойки, на которую делится произведение 
// подряд идущих чисел от a до b (числа целые 64-битные без знака).

using System;

namespace Task3
{
  class Program
  {
    private static UInt64 CalculateDividingPowerOfTwo(UInt64 number) 
    {
      UInt64 pow = 1, exponent = 0;
      
      while (pow <= number) 
      {
        try
        {
          checked
          {
            pow *= 2;
          }
        }
        catch
        {
          break;
        }
        
        exponent++;
        
        if (number % pow != 0) 
        {
          exponent--;
          break;
        }
      }
      
      return exponent;
    }
    
    private static UInt64 CalculateDividingPowerOfTwoFromAToB(UInt64 a, UInt64 b) {
      UInt64 exponent = 0;
      
      for (UInt64 num = a; num <= b;) 
      {
        UInt64 currentExp = 0;
        
        currentExp = CalculateDividingPowerOfTwo(num);
       
        exponent += currentExp;
        
        try
        {
          checked
          {
            num++;
          }
        }
        catch
        {
          break;
        }
      }
      
      return exponent;
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
        Console.Write("Enter b (> a): ");
        string strB = Console.ReadLine();
        error = !UInt64.TryParse(strB, out b) || (b <= a);
      } while (error);
      
      Console.WriteLine("a = " + a + ", b = " + b);
      
      UInt64 exponent = CalculateDividingPowerOfTwoFromAToB(a, b);
      Console.WriteLine("The maximum power of two that divides the product: 2^" + exponent);
      
      return 0;
    }
  }
}
