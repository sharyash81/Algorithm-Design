using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace Q2
{
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
            long clusterCount = long.Parse(Console.ReadLine());
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
            List<double> MST = new List<double>();
            edges = edges.OrderBy(x => x.Item3).ToList();
            foreach (var edge in edges)
            {
                if (djs.Find(edge.Item1) != djs.Find(edge.Item2))
                {
                    MST.Add(edge.Item3);
                    djs.Union(edge.Item1, edge.Item2);
                }
            }
            MST = MST.OrderByDescending(x => x).ToList();
            Console.WriteLine(Math.Round(MST[(int)clusterCount - 2], 6));
        }
    }
}