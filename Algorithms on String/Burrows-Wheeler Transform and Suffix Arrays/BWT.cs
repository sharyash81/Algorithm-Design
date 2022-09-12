using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace AlgorihmsOnString_W2Q2
{
    public class W2Q2
    {
        public static void Main()
        {
            string bwt = Console.ReadLine();
            int n = bwt.Length;
            string sorted_bwt = new string(bwt.OrderBy(x => x).ToArray());
            Dictionary<char, int> a = new Dictionary<char, int>();
            Dictionary<char, int> b = new Dictionary<char, int>();
            Dictionary<Tuple<char, int>, Tuple<char, int>> dict = new Dictionary<Tuple<char, int>, Tuple<char, int>>();
            int[] countBwt = new int[n];
            int[] countSBwt = new int[n];
            for (int i = 0; i < n; i++)
            {
                if (a.ContainsKey(bwt[i])) countBwt[i] = ++a[bwt[i]];
                else
                {
                    a.Add(bwt[i], 1);
                    countBwt[i] = 1;
                }
                if (b.ContainsKey(sorted_bwt[i])) countSBwt[i] = ++b[sorted_bwt[i]];
                else
                {
                    b.Add(sorted_bwt[i], 1);
                    countSBwt[i] = 1;
                }
                dict.Add(new Tuple<char, int>(sorted_bwt[i], countSBwt[i]), new Tuple<char,int>(bwt[i], countBwt[i]));
            }
            Tuple<char, int> vt = new Tuple<char,int>('$', 1);
            List<char> ans = new List<char>();
            while (ans.Count != n)
            {
                ans.Add(dict[vt].Item1);
                vt = dict[vt];
            }
            ans.Reverse();
            ans.Add('$');
            Console.WriteLine( new string(ans.ToArray()).Substring(1));
        }
    }
}