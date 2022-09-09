using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace Q1
{
    public class DisjointSet
    {
        long[] parent;
        long[] rank;

        public DisjointSet(long n)
        {
            parent = new long[n];
            rank = new long[n];
        }

        public void MakeSet(long i)
        {
            parent[i] = i;
        }

        public long Find(long i)
        {
            while (i != parent[i]) i = parent[i];
            return i;
        }
        public long FindPath(long i)
        {
            if (i != parent[i]) parent[i] = FindPath(parent[i]);
            return parent[i];
        }
        public void Union(long i, long j)
        {
            long i_id = Find(i);
            long j_id = Find(j);
            if (i_id == j_id) return;
            if (rank[i_id] > rank[j_id]) parent[j_id] = i_id;
            else
            {
                parent[i_id] = j_id;
                if (rank[i_id] == rank[j_id]) rank[j_id]++;
            }
        }
    }
    public class Program
    {
        public static void Main()
        {
            long pointCount = long.Parse(Console.ReadLine());
            long[][] points = new long[pointCount][];
            for (int i = 0; i < pointCount; i++)
            {
                string[] s = Console.ReadLine().Split();
                points[i] = new long[] { long.Parse(s[0]), long.Parse(s[1]) };
            }
            Console.WriteLine(Solve(pointCount, points));
        }
        public static double Solve(long pointCount, long[][] points)
        {
            List<Tuple<long, long, double>> edges = new List<Tuple<long, long, double>>();
            for (int i = 0; i < pointCount - 1; i++)
            {
                for (int j = i + 1; j < pointCount; j++)
                {
                    edges.Add(new Tuple<long, long, double>(i, j, Math.Sqrt(Math.Pow(points[i][0] - points[j][0], 2) + Math.Pow(points[i][1] - points[j][1], 2))));
                }
            }

            DisjointSet djs = new DisjointSet(pointCount);
            for (int i = 0; i < pointCount; i++)
            {
                djs.MakeSet(i);
            }
            double total = 0;
            edges = edges.OrderBy(x => x.Item3).ToList();
            foreach (var edge in edges)
            {
                if (djs.Find(edge.Item1) != djs.Find(edge.Item2))
                {
                    total += edge.Item3;
                    djs.Union(edge.Item1, edge.Item2);
                }
            }
            return Math.Round(total, 6);
        }
    }
}