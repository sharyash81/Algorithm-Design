using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
namespace AlgorithmsOnString_W4Q1
{
    public class W4Q1
    {
        public static void Main()
        {
            string pattern = Console.ReadLine();
            string text = Console.ReadLine();
            int n = pattern.Length;
            List<char> txt = pattern.ToList();
            txt.Add('$');
            txt.AddRange(text.ToList());
            long[] s = ComputePrefixFunction(txt);
            List<long> result = new List<long>();
            for (int i = n + 1; i < s.Length; i++)
            {
                if (s[i] == n) result.Add(i - 2 * n);
            }
            for (int i = 0; i < result.Count() ;i++)
            {
                Console.Write(result[i] + " ");   
            }
        }
        public static long[] ComputePrefixFunction(List<char> P)
        {
            int n = P.Count;
            long[] s = new long[n];
            s[0] = 0;
            long border = 0;
            for (int i = 1; i < n; i++)
            {
                while (border > 0 && P[i] != P[(int)border])
                {
                    border = s[border - 1];
                }
                if (P[i] == P[(int)border]) border++;
                else border = 0;
                s[i] = border;
            }
            return s;
        }
    }
}