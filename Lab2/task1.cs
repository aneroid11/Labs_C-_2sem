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
        
        if (prevInsideWord && !insideWord) 
        {
          words.Add(currentWord);
          currentWord = new StringBuilder();
        }
        
        prevInsideWord = insideWord;
      }
      
      return words;
    }
    
    private static void EraseWord(ref StringBuilder str, int start) 
    {
      for (int i = start; i < str.Length; i++) 
      {
        if (!Char.IsWhiteSpace(str[i])) 
        {
          str.Remove(i, 1);
          i--;
        } 
        else 
        {
          break;
        }
      }
    }
    
    private static void ReplaceWords(ref StringBuilder str, List<StringBuilder> words) 
    {
      int wordCounter = 0;
      bool insideWord = false, prevInsideWord = false;
      
      for (int i = 0; i < str.Length; i++) 
      {
        if (Char.IsWhiteSpace(str[i])) 
        {
          insideWord = false;
        } 
        else 
        {
          insideWord = true;
        }
        
        if (insideWord && !prevInsideWord) 
        {
          EraseWord(ref str, i);
          str.Insert(i, words[wordCounter]);
          wordCounter++;
        }
        
        prevInsideWord = insideWord;
      }
    }
    
    public static int Main(string[] args)
    {
      Console.Write("Enter a string: ");
      StringBuilder str = new StringBuilder(Console.ReadLine());
      List<StringBuilder> words = StringToWords(str);
      
      words.Reverse();
      
      StringBuilder reversed = str;
      ReplaceWords(ref reversed, words);
      
      Console.WriteLine(reversed);
      return 0;
    }
  }
}
