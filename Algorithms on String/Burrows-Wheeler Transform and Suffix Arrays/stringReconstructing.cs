using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsOnString_W2Q1
{
    public class W2Q1
    {
        public static void Main()
        {
            string text = Console.ReadLine();
            int n = text.Length;
            char[] s = text.ToArray();
            char[][] BW_MATRIX = new char[n][];
            BW_MATRIX[0] = new char[n];
            for (int i = 0; i < n; i++)
            {
                BW_MATRIX[0][i] = s[i];
            }
            for (int i = 1; i < n; i++)
            {
                BW_MATRIX[i] = new char[n];
                string s1 = text.Substring(n - i) + text.Substring(0, n - i);
                for (int j = 0; j < n; j++)
                {
                    BW_MATRIX[i][j] = s1[j];
                }
            }
            BW_MATRIX = BW_MATRIX.OrderBy(x => new string(x)).ToArray();
            List<char> result = new List<char>();
            foreach (var array in BW_MATRIX) result.Add(array[n - 1]);
            Console.WriteLine( new string(result.ToArray()));
        }
    }
}