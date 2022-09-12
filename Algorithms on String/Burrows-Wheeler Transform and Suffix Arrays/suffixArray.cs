using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmOnString_W2Q4
{
    public class W2Q4
    {
        public static void Main()
        {
            string text = Console.ReadLine();
            int n = text.Length;
            List<string> Suffixes = new List<string>(n);
            for (int i = 0; i < n; i++) Suffixes.Add(text.Substring(n - i - 1));
            Suffixes.Sort();
            for (int i = 0; i < n; i++)
            {
                Console.Write(n - Suffixes[i].Length + " ");
            }
            
        }
    }


}