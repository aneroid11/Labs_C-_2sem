// В заданной строке поменять порядок слов на обратный (слова разделены пробелами).

using System;
using System.Collections.Generic;
using System.Text;

namespace Task1 
{
  class Program 
  {
    public static List<StringBuilder> StringToWords(StringBuilder str) 
    {
      str.Append(' ');
      List<StringBuilder> words = new List<StringBuilder>(0);
      bool insideWord = false, prevInsideWord = false;
      StringBuilder currentWord = new StringBuilder("");
      
      for (int i = 0; i < str.Length; i++) 
      {
        char ch = str[i];
        
        if (Char.IsWhiteSpace(ch)) 
        {
          insideWord = false;
        } 
        else 
        {
          insideWord = true;
          currentWord.Append(ch);
        }
        
        if (prevInsideWord && !insideWord) {
          words.Add(currentWord);
          currentWord = new StringBuilder("");
        }
        
        prevInsideWord = insideWord;
      }
      
      return words;
    }
    
    public static int Main(string[] args)
    {
      Console.Write("Enter a string: ");
      StringBuilder str = new StringBuilder(Console.ReadLine());
      List<StringBuilder> words = StringToWords(str);
      
      StringBuilder reversed = new StringBuilder("");
      
      for (int i = words.Count - 1; i >= 0; i--) {
        reversed.Append(words[i] + " ");
      }
      
      Console.WriteLine("Words in reversed order: ");
      Console.WriteLine(reversed);
      return 0;
    }
  }
}
