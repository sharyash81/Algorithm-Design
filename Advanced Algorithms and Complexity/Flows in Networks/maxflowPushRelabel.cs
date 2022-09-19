using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AAC_W1_Q1
{
    public class Program
    {
        public static long[] heights;
        public static long[,] flow;
        public static long[] excess;
        public static long[,] capacity;
        public static long N;
        public static void Main()
        {
            string[] s = Console.ReadLine().Split();
            long nodeCount = long.Parse(s[0]);
            long edgeCount = long.Parse(s[1]);
            long[][] edges = new long[edgeCount][];
            for (int i = 0; i < edgeCount; i++)
            {
                string[] s1 = Console.ReadLine().Split();
                edges[i] = new long[3] { long.Parse(s1[0])  , long.Parse(s1[1]) , long.Parse(s1[2]) };
            }
            Console.WriteLine(Solve(nodeCount, edgeCount, edges));
        }
        public static long Solve(long nodeCount, long edgeCount, long[][] edges)
        {
            N = nodeCount;
            capacity = new long[N, N];
            for (int i = 0; i < edgeCount; i++)
            {
                capacity[edges[i][0] - 1, edges[i][1] - 1] += edges[i][2];
            }
            heights = new long[N];
            heights[0] = N;
            flow = new long[N, nodeCount];
            excess = new long[N];
            excess[0] = long.MaxValue;
            for (int i = 1; i < N; i++)
            {
                push(0, i);
            }
            List<long> current;
            while ((current = MaxHeight()).Count() > 0)
            {
                foreach (long i in current)
                {
                    bool pushed = false;
                    for (int j = 0; j < N && excess[i] != 0; j++)
                    {
                        if (capacity[i, j] - flow[i, j] > 0 && heights[i] == heights[j] + 1)
                        {
                            push(i, j);
                            pushed = true;
                        }
                    }
                    if (!pushed)
                    {
                        heights[i]++;
                        break;
                    }
                }
            }
            long MAX_FLOW = 0;
            for (int i = 0; i < N; i++) MAX_FLOW += flow[i, N - 1];
            return MAX_FLOW;

        }
        public static void push(long u, long v)
        {
            long delta = excess[u] > capacity[u, v] - flow[u, v] ? capacity[u, v] - flow[u, v] : excess[u];
            flow[u, v] += delta;
            flow[v, u] -= delta;
            excess[u] -= delta;
            excess[v] += delta;
        }
        public static List<long> MaxHeight()
        {
            List<long> maxHeight = new List<long>();
            for (int i = 1; i < N - 1; i++)
            {
                if (excess[i] > 0)
                {
                    if (maxHeight.Count() > 0 && heights[i] > heights[maxHeight[0]]) maxHeight.Clear();
                    if (maxHeight.Count() == 0 || heights[i] == heights[maxHeight[0]])
                    {
                        maxHeight.Add(i);
                    }
                }
            }
            return maxHeight;
        }
    }
}