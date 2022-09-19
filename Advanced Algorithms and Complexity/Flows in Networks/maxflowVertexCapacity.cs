using TestCommon;

namespace E2;

public class Q2MaxflowVertexCapacity : Processor
{
    public Q2MaxflowVertexCapacity(string testDataName) : base(testDataName)
    {
        // this.ExcludeTestCases(1, 2, 3);
        // this.ExcludeTestCaseRangeInclusive(1, 3);
    }
    public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<long, long, long[][], long[], long, long, long>)Solve);

    public long[,] adj;
    public virtual long Solve(long nodeCount, long edgeCount, long[][] edges, long[] nodeWeight
, long startNode, long endNode)
    {
        nodeCount = 2 * nodeCount;
        adj = new long[nodeCount + 1, nodeCount + 1];
        for (int i = 0; i <= nodeCount; i++)
            for (int j = 0; j <= nodeCount; j++)
                adj[i, j] = 0;
        for (int i = 0; i < nodeWeight.Count(); i++)
        {
            long v = i + 1;
            long u = i + 1 + nodeCount / 2;
            long c = nodeWeight[i];
            adj[v, u] += c;
        }
        for (int i = 0; i < edgeCount; i++)
        {
            long v = edges[i][0];
            long u = edges[i][1];
            long c = edges[i][2];
            adj[v + nodeCount / 2, u] += c;
        }
        return findMaxFlow(nodeCount, startNode, endNode + nodeCount / 2);
    }
    public long findMaxFlow(long n, long startNode, long endNode)
    {
        long answer = 0;
        bool[] visited = new bool[n + 1];
        long[] parent = new long[n + 1];
        long[] staurating = new long[n + 1];
        var queue = new Queue<long>();
        while (true)
        {
            for (int i = 0; i < visited.Length; i++) visited[i] = false;
            queue.Enqueue(startNode);
            parent[startNode] = startNode - n / 2;
            staurating[startNode] = long.MaxValue;
            while (queue.Count > 0)
            {
                long v = queue.Dequeue();
                if (visited[v]) continue;
                visited[v] = true;
                if (v == n) break;
                for (int i = 1; i <= n; i++)
                {
                    if (adj[v, i] > 0 && !visited[i])
                    {
                        queue.Enqueue(i);
                        parent[i] = v;
                        staurating[i] = Math.Min(staurating[v], adj[v, i]);
                    }
                }
            }
            if (!visited[endNode]) return answer;
            long tmpVertex = endNode;
            while (tmpVertex != startNode)
            {
                adj[parent[tmpVertex], tmpVertex] -= staurating[n];
                adj[tmpVertex, parent[tmpVertex]] += staurating[n];
                tmpVertex = parent[tmpVertex];
            }
            answer += staurating[n];
            queue.Clear();
        }
    }
}
