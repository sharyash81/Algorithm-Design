using System;

namespace Week3
{
    internal class Q1
    {
        static void Main1(string[] args)
        {
            string[] s = Console.ReadLine().Split();
            int v = int.Parse(s[0]);
            int e = int.Parse(s[1]);
            long[,] matrix = new long[e, 2];
            for (int i = 0; i < e; i ++)
            {
                string[] s1 = Console.ReadLine().Split();
                for (int j = 0; j < 2; j++)
                {
                    matrix[i, j] = long.Parse(s1[j]);
                }
            }
            string[] ans = Solve(v, e, matrix);
            foreach (var item in ans)
            {
                Console.WriteLine(item);
            }
        }
        public static String[] Solve(int V, int E, long[,] matrix)
        {
            int constraint = V + 3 * E;
            String[] ans = new String[constraint + 1];
            int counter = 0;
            /*ans[counter++] = $"{constraint} {3 * V}";*/
            ans[counter++] = String.Format("{0} {1}", constraint, 3 * V);
            for (int i = 0; i < V; i++)
            {

                int c = 3 * i + 1;
                /*ans[counter++] = $"{c} {c + 1} {c + 2} 0";*/
                ans[counter++] = String.Format("{0} {1} {2} 0", c, c + 1, c + 2);
            }
            for (int i = 0; i < E; i++)
            {
                long start = (matrix[i, 0] - 1) * 3;
                long end = (matrix[i, 1] - 1) * 3;
                /*ans[counter++] = $"{-(start + 1)} {-(end + 1)} 0";
                ans[counter++] = $"{-(start + 2)} {-(end + 2)} 0";
                ans[counter++] = $"{-(start + 3)} {-(end + 3)} 0";*/
                ans[counter++] = String.Format("{0} {1} 0", -(start + 1), -(end + 1));
                ans[counter++] = String.Format("{0} {1} 0", -(start + 2), -(end + 2));
                ans[counter++] = String.Format("{0} {1} 0", -(start + 3), -(end + 3));
            }
            return ans;
        }

    }
}
