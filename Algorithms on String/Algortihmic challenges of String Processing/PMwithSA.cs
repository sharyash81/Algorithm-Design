using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsOnString_W4Q3
{
    public class W4Q3
    {
        public static void Main()
        {
            string text = Console.ReadLine();
            int n = int.Parse(Console.ReadLine());
            string [] patterns = Console.ReadLine().Split();
            long[] order = SF(text);
            int length = text.Length;
            char[] bw_trans = new char[length];
            for (int i = 0; i < length; i++)
            {
                bw_trans[i] = text[(int)(order[i] - 1 + length) % length];
            }
            text = new string(bw_trans);
            Dictionary<char, int> FirstOccurrence = new Dictionary<char, int>();
            Dictionary<char, int[]> Count = new Dictionary<char, int[]>();
            List<long> ans = new List<long>();
            char[] lastColumn = text.ToCharArray();
            char[] firstColumn = lastColumn.OrderBy(x => x).ToArray();
            for (int i = 0; i < length; i++)
            {
                if (!FirstOccurrence.ContainsKey(firstColumn[i])) FirstOccurrence.Add(firstColumn[i], i);
            }
            foreach (var key in FirstOccurrence.Keys)
            {
                Count.Add(key, new int[length + 1]);
            }
            foreach (var key in Count.Keys)
            {
                Count[key][0] = 0;
                int counter = 0;
                for (int i = 1; i < length + 1; i++)
                {
                    if (lastColumn[i - 1] == key) counter++;
                    Count[key][i] = counter;
                }
            }
            for (int i = 0; i < n; i++)
            {
                string pattern = patterns[i];
                int top = 0;
                int bottom = length - 1;
                while (top <= bottom)
                {
                    if (pattern.Length != 0)
                    {
                        char symbol = pattern[pattern.Length - 1];
                        pattern = pattern.Remove(pattern.Length - 1);
                        if (!FirstOccurrence.ContainsKey(symbol)) break;
                        top = FirstOccurrence[symbol] + Count[symbol][top];
                        bottom = FirstOccurrence[symbol] + Count[symbol][bottom + 1] - 1;
                    }
                    else
                    {
                        for (int j = top; j <= bottom; j++) if (!ans.Contains(order[j])) ans.Add(order[j]);
                        break;
                    }
                }
            }

            for (int i = 0; i < ans.Count(); i++)
            {
                Console.Write(ans[i] + " ");
            }

        }
        public static long[] SF(string text)
        {
            long[] order = SortCharacters(text);
            long[] _class = ComputeCharClasses(text, order);
            long L = 1;
            while (L < text.Length)
            {
                order = SortDoubled(text, L, ref order, ref _class);
                _class = UpdateClasses(ref order, ref _class, L);
                L *= 2;
            }
            return order;
        }
        public static long[] SortCharacters(string S)
        {
            int n = S.Length;
            long[] order = new long[n];
            long[] count = new long[256];
            for (int i = 0; i < n; i++) count[S[i]]++;
            for (int j = 1; j < 256; j++) count[j] += count[j - 1];
            for (int i = n - 1; i >= 0; i--)
            {
                char c = S[i];
                count[c]--;
                order[count[c]] = i;
            }
            return order;
        }
        public static long[] ComputeCharClasses(string S, long[] order)
        {
            int n = S.Length;
            long[] _classes = new long[n];
            _classes[order[0]] = 0;
            for (int i = 1; i < n; i++)
            {
                if (S[(int)order[i]] != S[(int)order[i - 1]]) _classes[order[i]] = _classes[order[i - 1]] + 1;
                else _classes[order[i]] = _classes[order[i - 1]];
            }
            return _classes;
        }
        public static long[] SortDoubled(string S, long L, ref long[] order, ref long[] _class)
        {
            int n = S.Length;
            long[] count = new long[n];
            long[] newOrder = new long[n];
            for (int i = 0; i < n; i++) count[_class[i]]++;
            for (int i = 1; i < n; i++) count[i] += count[i - 1];
            for (int i = n - 1; i >= 0; i--)
            {
                long start = (order[i] - L + n) % n;
                long cl = _class[(int)start];
                count[cl]--;
                newOrder[count[cl]] = start;
            }
            return newOrder;
        }
        public static long[] UpdateClasses(ref long[] newOrder, ref long[] _class, long L)
        {
            int n = newOrder.Length;
            long[] newClass = new long[n];
            newClass[newOrder[0]] = 0;
            for (int i = 1; i < n; i++)
            {
                long cur = newOrder[i];
                long prev = newOrder[i - 1];
                long mid = (cur + L) % n;
                long midPrev = (prev + L) % n;
                if (_class[cur] != _class[prev] || _class[mid] != _class[midPrev]) newClass[cur] = newClass[prev] + 1;
                else newClass[cur] = newClass[prev];
            }
            return newClass;
        }
    }
}