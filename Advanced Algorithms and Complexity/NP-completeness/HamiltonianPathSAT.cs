using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week3
{
    public class Q2
    {
        public static String[] ans;
        public static int counter;
        public static int NUMBER_OF_VERTICES;
        public static int size;
        public static void Main()
        {
            string[] s = Console.ReadLine().Split();
            int v = int.Parse(s[0]);
            int e = int.Parse(s[1]);
            long[,] matrix = new long[e, 2];
            for (int i = 0; i < e; i++)
            {
                string[] s1 = Console.ReadLine().Split();
                for (int j = 0; j < 2; j++)
                {
                    matrix[i, j] = long.Parse(s1[j]);
                }
            }
            var ans = Solve(v, e, matrix);
            foreach (var item in ans)
            {
                Console.WriteLine(item);
            }
        }
        public static String[] Solve(int V, int E, long[,] matrix)
        {
            NUMBER_OF_VERTICES = V;
            size = NUMBER_OF_VERTICES * (NUMBER_OF_VERTICES - 1) / 2;
            ans = new String[2 * V * (size + 1) + (size - E) * 2 * (V - 1) + 1];
            counter = 0;
            ans[counter++] = $"{2 * V * (size + 1) + (size - E)} {V * V}";
            for (int i = 0; i < V; i++)
            {
                Exactly_One_Row(i);
                Exactly_One_Col(i);
            }
            long[,] adj_matrix = new long[V, V];
            for (int i = 0; i < E; i++)
            {
                int fn = (int)matrix[i, 0] - 1;
                int sn = (int)matrix[i, 1] - 1;
                adj_matrix[fn, sn] = 1;
                adj_matrix[sn, fn] = 1;
            }
            for (int i = 0; i < V; i++)
            {
                for (int j = i + 1; j < V; j++)
                {
                    if (adj_matrix[i, j] == 0)
                    {
                        for (int k = 0; k < V - 1; k++)
                        {
                            ans[counter++] = $"{-(i * V + k + 1)} {-(j * V + (k + 2))} 0";
                            ans[counter++] = $"{-(j * V + k + 1)} {-(i * V + (k + 2))} 0";
                        }
                    }
                }
            }
            return ans;
        }
        public static void Exactly_One_Row(int row)
        {
            int constant = row * NUMBER_OF_VERTICES;
            long[][] clauses = new long[size + 1][];
            int c = 0;
            clauses[c++] = new long[NUMBER_OF_VERTICES];

            // each row must have at least one true variable
            for (int t = 0; t < NUMBER_OF_VERTICES; t++)
                clauses[0][t] = constant + t + 1;

            // each row must have ONLY one true variable
            for (int i = 0; i < NUMBER_OF_VERTICES - 1; i++)
                for (int j = i + 1; j < NUMBER_OF_VERTICES; j++)
                    clauses[c++] = new long[2] { -(constant + i + 1), -(constant + j + 1) };

            foreach (var clause in clauses) ans[counter++] = (String.Join(' ', clause) + " 0");
        }
        public static void Exactly_One_Col(int col)
        {
            long[][] clauses = new long[size + 1][];
            int c = 0;
            clauses[c++] = new long[NUMBER_OF_VERTICES];

            // each col must have at least one true variable
            for (int t = 0; t < NUMBER_OF_VERTICES; t++)
                clauses[0][t] = t * NUMBER_OF_VERTICES + col + 1;

            // each col must have ONLY one true variable
            for (int i = 0; i < NUMBER_OF_VERTICES - 1; i++)
                for (int t = i + 1; t < NUMBER_OF_VERTICES; t++)
                    clauses[c++] = new long[2] { -(i * NUMBER_OF_VERTICES + col + 1), -(t * NUMBER_OF_VERTICES + col + 1) };

            foreach (var clause in clauses) ans[counter++] = (String.Join(' ', clause) + " 0");
        }
    }
}
