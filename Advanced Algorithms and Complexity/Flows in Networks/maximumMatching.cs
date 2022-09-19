using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W1_2
{
    public class Q2
    {
        public static Node[] graph;
        public static Node[] residualGraph;
        public static List<int> path;
        public static int[] parent;
        public static Node currentNode;
        static void Main()
        {
            string[] input = Console.ReadLine().Split();
            int n = int.Parse(input[0]);
            int m = int.Parse(input[1]);
            int[,] info = new int[n,m];
            for (int i = 0; i < n; i++)
            {
                input = Console.ReadLine().Split();
                for (int j = 0; j < m; j++)
                    info[i,j] = int.Parse(input[j]);
            }
            // important note : all the capacities is equal to 0
            // total numbers of  nodes + one source + one end
            int nodeCount = n + m + 2;
            graph = new Node[nodeCount];
            // construct a graph nodes 
            for (int i = 0; i < nodeCount; i++) graph[i] = new Node(i);
            // construct a gprah edges
            for (int i = 1; i <= n; i++) graph[0].Edges.Add(new Edge(i, 1, 0));
            for (int i = 1; i <= n; i++)
                for (int j = 0; j < m; j++)
                    if (info[i - 1,j] == 1)
                        graph[i].Edges.Add(new Edge(n + j + 1, 1, 0));
            for (int i = n + 1; i < nodeCount - 1; i++) graph[i].Edges.Add(new Edge(nodeCount - 1, 1, 0));
            // construct the residual graph

            // important note : our residual graph doesnt have any flow
            residualGraph = new Node[nodeCount];
            for (int i = 0; i < nodeCount; i++) residualGraph[i] = new Node(i);
            for (int i = 0; i < nodeCount; i++)
            {
                foreach (Edge edge in graph[i].Edges)
                {
                    residualGraph[edge.End].Edges.Add(new Edge(i, edge.Flow, 0));
                    residualGraph[i].Edges.Add(new Edge(edge.End, edge.Capacity - edge.Flow, 0));
                }
            }
            while (true)
            {
                parent = new int[nodeCount];
                bool[] visited = new bool[nodeCount];
                BFS(nodeCount, visited);
                if (visited[nodeCount - 1] == false) break;
                path = PathFinder(nodeCount);
                int minCapacity = MinCapacity_in_augmenting_path();
                currentNode = residualGraph[0];
                Update_ResidualGraph(-minCapacity, 0);
                path.Reverse();
                path.Add(0);
                currentNode = residualGraph[path[0]];
                Update_ResidualGraph(minCapacity, 1);
            }
            int[] result = new int[n];
            for (int i = 0; i < result.Length; i++) result[i] = -1;
            for (int i = n + 1; i < nodeCount - 1; i++)
            {
                foreach (Edge edge in residualGraph[i].Edges)
                {
                    if (edge.Capacity == 1 && edge.End - 1 < n)
                    {
                        result[edge.End - 1] = i - n;
                        break;
                    }
                }
            }
            foreach (int item in result)
            {
                Console.Write(item + " ");
            }
        }
        public static void Update_ResidualGraph(int min, int start)
        {
            for (int i = start; i < path.Count; i++)
            {
                foreach (Edge edge in currentNode.Edges)
                {
                    if (edge.End == path[i])
                    {
                        if (start == 0 && edge.Capacity != 0)
                        {
                            edge.Capacity += min;
                            currentNode = residualGraph[path[i]];
                            break;
                        }
                        else
                        {
                            edge.Capacity += min;
                            currentNode = residualGraph[path[i]];
                            break;
                        }
                    }
                }
            }
        }

        public static int MinCapacity_in_augmenting_path()
        {
            int min = int.MaxValue;
            currentNode = residualGraph[0];
            for (int i = 0; i < path.Count; i++)
            {
                foreach (var edge in currentNode.Edges)
                {
                    if (edge.End == path[i] && edge.Capacity != 0)
                    {
                        min = Math.Min(min, edge.Capacity);
                        currentNode = residualGraph[path[i]];
                        break;
                    }
                }
            }
            return min;
        }

        public static List<int> PathFinder(int nodeCount)
        {
            List<int> path = new List<int>();
            path.Add(nodeCount - 1);
            int cur = parent[nodeCount - 1];
            while (cur != 0)
            {
                path.Add(cur);
                cur = parent[cur];
            }
            path.Reverse();
            return path;
        }

        public static void BFS(int nodeCount, bool[] visited)
        {
            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(residualGraph[0]);
            visited[0] = true;
            while (queue.Count != 0)
            {
                Node node = queue.Dequeue();
                foreach (var edge in node.Edges)
                {
                    if (visited[edge.End] == false && edge.Capacity != 0)
                    {
                        queue.Enqueue(residualGraph[edge.End]);
                        visited[edge.End] = true;
                        parent[edge.End] = node.Id;
                    }

                    if (edge.End == nodeCount - 1 && edge.Capacity != 0)    queue.Clear();
                }
            }
        }
    }
    public class Node
    {
        public int Id;
        public List<Edge> Edges;
        public Node(int id)
        {
            this.Id = id;
            this.Edges = new List<Edge>();
        }
    }
    public class Edge
    {
        public int End;
        public int Capacity;
        public int Flow;
        public Edge(int end, int capacity, int flow)
        {
            this.End = end;
            this.Capacity = capacity;
            this.Flow = flow;
        }
    }

}
