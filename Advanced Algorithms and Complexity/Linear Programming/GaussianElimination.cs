using System;

namespace Week2
{
    public class Q1
    {
        public static double[,] Matrix;
        public static int UNKNOWN_VARIABLES;
        public static void Main1(string[] args)
        {
			long MATRIX_SIZE = long.Parse(Console.ReadLine());

			double[,] matrix = new double[MATRIX_SIZE, MATRIX_SIZE + 1];

			for (int i = 0; i < MATRIX_SIZE; i++)
			{
				string[] inpute = Console.ReadLine().Split();

				for (int j = 0; j < inpute.Length; j++)
				{
					matrix[i, j] = double.Parse(inpute[j]);
				}
			}

			var ans = Solve(MATRIX_SIZE, matrix);

			foreach (var item in ans)
			{
				Console.Write(item + " ");
			}
		}
        public static double[] Solve(long MATRIX_SIZE, double[,] matrix)
        {
            Matrix = matrix;
            UNKNOWN_VARIABLES = (int)MATRIX_SIZE;
            // Forward Elimination 
            for (int i = 0; i < UNKNOWN_VARIABLES; i++)
            {
                double Max = Matrix[i, i];
                int MaxRow = i;
                for (int k = i + 1; k < UNKNOWN_VARIABLES; k++)
                {
                    if (Math.Abs(Matrix[k, i]) > Math.Abs(Max))
                    {
                        Max = Matrix[k, i];
                        MaxRow = k;
                    }
                }
                if (i != MaxRow) Swap(i, MaxRow);
                for (int k = i + 1; k < UNKNOWN_VARIABLES; k++)
                {
                    double Normalize_Coef = Matrix[k, i] / Max;
                    for (int col = i + 1; col < UNKNOWN_VARIABLES + 1; col++)
                    {
                        Matrix[k, col] -= Normalize_Coef * Matrix[i, col];
                    }
                    Matrix[k, i] = 0;
                }
            }
            double[] ans = new double[UNKNOWN_VARIABLES];
            for (int i = UNKNOWN_VARIABLES - 1; i >= 0; i--)
            {
                ans[i] = Matrix[i, UNKNOWN_VARIABLES];
                for (int j = i + 1; j < UNKNOWN_VARIABLES; j++)
                {
                    ans[i] -= Matrix[i, j] * ans[j];
                }
                ans[i] = ans[i] / Matrix[i, i];
            }
            return ans;
        }
        public static void Swap(int FirstRow, int SecondRow)
        {
            for (int j = 0; j < UNKNOWN_VARIABLES + 1; j++)
            {
                double tmp = Matrix[FirstRow, j];
                Matrix[FirstRow, j] = Matrix[SecondRow, j];
                Matrix[SecondRow, j] = tmp;
            }
        }
    }
}
