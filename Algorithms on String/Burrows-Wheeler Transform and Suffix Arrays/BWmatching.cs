using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace AlgorithmsOnString_W2Q3
{
    public class W2Q3
    {
        public static void Main()
        {
            string text = Console.ReadLine();
            int n = int.Parse(Console.ReadLine());
            string[] patterns = Console.ReadLine().Split();
            int lenght = text.Length;
            Dictionary<char, int> FirstOccurrence = new Dictionary<char, int>();
            Dictionary<char, int[]> Count = new Dictionary<char, int[]>();
            List<long> ans = new List<long>();
            char[] lastColumn = text.ToCharArray();
            char[] firstColumn = lastColumn.OrderBy(x => x).ToArray();
            for (int i = 0; i < lenght; i++)
            {
                if (!FirstOccurrence.ContainsKey(firstColumn[i])) FirstOccurrence.Add(firstColumn[i], i);
            }
            foreach (var key in FirstOccurrence.Keys)
            {
                Count.Add(key, new int[lenght + 1]);
            }
            foreach (var key in Count.Keys)
            {
                Count[key][0] = 0;
                int counter = 0;
                for (int i = 1; i < lenght + 1; i++)
                {
                    if (lastColumn[i - 1] == key) counter++;
                    Count[key][i] = counter;
                }
            }
            for (int i = 0; i < n; i++)
            {
                string pattern = patterns[i];
                int top = 0;
                int bottom = lenght - 1;
                bool empty = true;
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
                        empty = false;
                        ans.Add(bottom - top + 1);
                        break;
                    }
                }
                if (empty) ans.Add(0);
            }
            for (int i = 0; i < ans.Count(); i++)
            {
                Console.Write(ans[i] + " ");
            }
        }
    }
}