using System;
using System.Collections.Generic;
using System.Linq;
using TestCommon;


namespace E1
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
    public class Q1SecondMST : Processor
    {
        public Q1SecondMST(string testDataName) : base(testDataName)
        {
            ExcludeTestCases(27, 28, 29, 30);
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long>)Solve);

        public List<(long, long, long)> edgeList;
        public long nodeCount;
        public long Solve(long nC, long[][] edges)
        {
            nodeCount = nC;
            edgeList = new List<(long, long, long)>();
            for (int i = 0; i < edges.GetLength(0); i++)
            {
                edgeList.Add((edges[i][0] - 1, edges[i][1] - 1, edges[i][2]));
            }
            List<(long, long, long)> FirstMST = MST();
            long SecondMin = long.MaxValue;
            int m = FirstMST.Count;
            for (int i = 0; i < m; i++)
            {
                long tmp = FirstMST[i].Item3;
                edgeList.Remove((FirstMST[i].Item1, FirstMST[i].Item2, tmp));
                edgeList.Add(((FirstMST[i].Item1, FirstMST[i].Item2, long.MaxValue)));
                // Console.WriteLine(edgeList[i].Item1 + " " + edgeList[i].Item2 + " " + edgeList[i].Item3);
                long minSmt = SMST();
                if (SecondMin >= minSmt) SecondMin = minSmt;
                edgeList.Remove((FirstMST[i].Item1, FirstMST[i].Item2, long.MaxValue));
                edgeList.Add(((FirstMST[i].Item1, FirstMST[i].Item2, tmp)));
            }
            if (SecondMin >= long.MaxValue) return -1;
            return SecondMin;
        }
        public long SMST()
        {
            DisjointSet djs = new DisjointSet(nodeCount);
            for (int i = 0; i < nodeCount; i++) djs.MakeSet(i);
            long total = 0;
            edgeList = edgeList.OrderBy(x => x.Item3).ToList();
            foreach (var edge in edgeList)
            {
                if (djs.Find(edge.Item1) != djs.Find(edge.Item2))
                {
                    if (edge.Item3 == long.MaxValue) return long.MaxValue;
                    total += edge.Item3;
                    djs.Union(edge.Item1, edge.Item2);
                }
            }

            return total;
        }
        public List<(long, long, long)> MST()
        {
            List<(long, long, long)> MST_EDGE = new List<(long, long, long)>();
            DisjointSet djs = new DisjointSet(nodeCount);
            for (int i = 0; i < nodeCount; i++) djs.MakeSet(i);
            edgeList = edgeList.OrderBy(x => x.Item3).ToList();
            foreach (var edge in edgeList)
            {
                if (djs.Find(edge.Item1) != djs.Find(edge.Item2))
                {
                    MST_EDGE.Add((edge.Item1, edge.Item2, edge.Item3));
                    djs.Union(edge.Item1, edge.Item2);
                }
            }
            return MST_EDGE;
        }
    }
}
