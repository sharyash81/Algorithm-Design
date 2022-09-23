using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week2
{
    public class Q2
    {
        public static void Main()
        {
            string[] inpute = Console.ReadLine().Split();

            long n = long.Parse(inpute[0]);
            long m = long.Parse(inpute[1]);

            double[,] matrix = new double[n + 1, m + 1];
            for (int i = 0; i < n; i++)
            {
                inpute = Console.ReadLine().Split();
                for (int j = 0; j < inpute.Length; j++)
                {
                    matrix[i, j] = double.Parse(inpute[j]);
                }
            }

            inpute = Console.ReadLine().Split();
            for (int i = 0; i < inpute.Length; i++)
            {
                matrix[i, m] = double.Parse(inpute[i]);
            }

            inpute = Console.ReadLine().Split();
            for (int j = 0; j < inpute.Length; j++)
            {
                matrix[n, j] = double.Parse(inpute[j]);
            }

            var ans = Solve((int)n, (int)m, matrix);

            Console.WriteLine(ans);
        }
        public static double[,] matrix;
        public static double[,] AuxiliarySlackMarix;
        public static int constraint;
        public static int variables;
        public static double[,] slackMatrix;
        public static int slackRow;
        public static int slackCol;
        public static double[,] NewMatrix;
        public static double[] ObjectiveFunc;
        public static List<string> ans;
        public static string Solve(int c, int v, double[,] matrix1)
        {
            for (int i = 0; i < c + 1; i++)
            {
                matrix1[i, v] += (double)1 / Math.Pow(10, 10);
            }
            ans = new List<string>();
            matrix = matrix1;
            constraint = c;
            variables = v;
            ToSlackForm();
            bool is_feasible = BFC();
            if (is_feasible) Simplex();
            else
            {
                is_feasible = Auxiliary();
                if (is_feasible)
                {
                    GNM();
                    slackMatrix = NewMatrix;
                    Simplex();
                }
                else return "No solution";
            }
            if (ans.Count == 1) return "Infinity";
            string res = ans[0];
            return res + String.Join(" ", ans.GetRange(1, ans.Count - 1));
        }
        public static void GNM()
        {
            int newVariable = constraint + variables;
            NewMatrix = new double[slackRow - 1, slackCol - 1];
            for (int i = 0; i < newVariable; i++)
            {
                for (int j = 0; j < newVariable; j++)
                {
                    NewMatrix[i, j] = slackMatrix[i, j];
                }
                for (int j = newVariable + 1; j < slackCol; j++)
                {
                    NewMatrix[i, j - 1] = slackMatrix[i, j];
                }
            }
            for (int j = 0; j < slackCol - 1; j++)
            {
                NewMatrix[newVariable, j] = ObjectiveFunc[j];
            }
            for (int j = 0; j < slackCol - 2; j++)
            {
                if (NewMatrix[j, slackCol - 2] == 1)
                {
                    COF(j);
                }
            }
            slackRow--;
            slackCol--;
            NewMatrix[slackRow - 1, slackCol - 1] = 1;
        }
        // Complete Objective Function
        public static void COF(int col)
        {
            double constant = NewMatrix[slackRow - 2, col];
            for (int j = 0; j < slackCol - 1; j++)
            {
                if (j == col) NewMatrix[slackRow - 2, j] = 0;
                else
                {
                    NewMatrix[slackRow - 2, j] += constant * (NewMatrix[col, j]);
                }
            }
        }
        public static bool AuxSimplex()
        {
            int enteringVariable;
            int departingVariable;
            while (true)
            {
                enteringVariable = CEV();
                if (slackMatrix[slackRow - 1, enteringVariable] <= 0) break;
                // if (!HasOptimalSolution(enteringVariable)) return false;
                departingVariable = CDV(enteringVariable);
                if (departingVariable == -1) break;
                SwitchRole(departingVariable, enteringVariable);
            }
            double tmp;
            if ((tmp = slackMatrix[slackRow - 1, slackCol - 2]) >= -0.001 && slackMatrix[slackRow - 1, slackCol - 2] <= 0.001) return true;
            return false;
        }
        public static void SwitchRole(int FirstVariable, int SecondVariable)
        {
            double Fraction_Rate = slackMatrix[FirstVariable, SecondVariable];
            double Reverse = 1 / Fraction_Rate;
            //Generating SecondVariable
            for (int j = 0; j < slackCol - 1; j++)
            {
                slackMatrix[SecondVariable, j] = -Reverse * slackMatrix[FirstVariable, j];
            }
            slackMatrix[SecondVariable, SecondVariable] = 0;
            slackMatrix[SecondVariable, FirstVariable] = Reverse;
            // Complete Matrix
            for (int i = 0; i < slackRow; i++)
            {
                if (slackMatrix[i, slackCol - 1] == 1 && i != FirstVariable)
                {
                    double fr = slackMatrix[i, SecondVariable];
                    for (int j = 0; j < slackCol - 1; j++)
                    {
                        slackMatrix[i, j] += fr * slackMatrix[SecondVariable, j];
                    }
                    slackMatrix[i, SecondVariable] = 0;
                }
            }
            // Switch Role --> Basic and Non Basic Variable 
            slackMatrix[FirstVariable, slackCol - 1] = 0;
            slackMatrix[SecondVariable, slackCol - 1] = 1;

        }
        public static void Simplex()
        {
            bool OptimalSolution = true;
            int enteringVariable;
            int departingVariable;
            while (true)
            {
                enteringVariable = CEV();
                if (slackMatrix[slackRow - 1, enteringVariable] <= 0) break;
                if (!HasOptimalSolution(enteringVariable))
                {
                    OptimalSolution = false;
                    break;
                }
                departingVariable = CDV(enteringVariable);
                if (departingVariable >= -1 - ((double)1 / Math.Pow(10, 2)) && departingVariable <= -1 + ((double)1 / Math.Pow(10, 2)))
                {
                    OptimalSolution = false;
                    break;
                }
                SwitchRole(departingVariable, enteringVariable);
            }
            if (!OptimalSolution) ans.Add("Infinity");
            else
            {
                ans.Add("Bounded solution\n");
                double res;
                for (int i = 0; i < variables; i++)
                {
                    if (slackMatrix[i, slackCol - 1] == 1)
                    {
                        res = slackMatrix[i, slackCol - 2];
                        /*if (Math.Abs(res - Math.Truncate(res)) < 0.25)
                        {
                            res = Math.Truncate(res);
                            if (res == -0) res = 0;
                        }
                        else if (Math.Abs(res - Math.Truncate(res)) >= 0.75) res = Math.Round(res);
                        else res = Math.Ceiling(res) - 0.5;*/
                        ans.Add(res.ToString());
                    }
                    else ans.Add("0");
                }
            }
        }
        public static bool Auxiliary()
        {
            AuxiliarySlackMarix = new double[slackRow + 1, slackCol + 1];
            int newVariable = constraint + variables;
            for (int i = 0; i < slackRow; i++)
            {
                for (int j = 0; j < slackCol + 1; j++)
                {
                    if (i < variables || i == newVariable) AuxiliarySlackMarix[i, j] = 0;
                    else if (j == newVariable) AuxiliarySlackMarix[i, j] = 1;
                    else if (i < newVariable && j < newVariable) AuxiliarySlackMarix[i, j] = slackMatrix[i, j];
                    else if (i > newVariable && j < newVariable) AuxiliarySlackMarix[i, j] = slackMatrix[i - 1, j];
                    else if (i < newVariable && j > newVariable) AuxiliarySlackMarix[i, j] = slackMatrix[i, j - 1];
                    else AuxiliarySlackMarix[i, j] = slackMatrix[i - 1, j - 1];
                }
            }
            slackCol++;
            slackRow++;
            for (int j = 0; j < slackCol; j++)
            {
                AuxiliarySlackMarix[slackRow - 1, j] = 0;
            }
            AuxiliarySlackMarix[slackRow - 1, newVariable] = -1;
            AuxiliarySlackMarix[slackRow - 1, slackCol - 1] = 1;
            int Min = 0;
            for (int i = 1; i < newVariable; i++)
            {
                if (AuxiliarySlackMarix[i, slackCol - 1] == 1 && AuxiliarySlackMarix[i, slackCol - 2] < AuxiliarySlackMarix[Min, slackCol - 2]) Min = i;
            }
            AuxiliarySlackMarix[slackRow - 1, slackCol - 1] = 1;
            slackMatrix = AuxiliarySlackMarix;
            SwitchRole(Min, newVariable);
            if (AuxSimplex()) return true;
            return false;
        }
        public static bool HasOptimalSolution(int col)
        {
            for (int i = 0; i < slackRow - 1; i++)
                if (slackMatrix[i, slackCol - 1] == 1)
                    if (slackMatrix[i, col] < 0.01) return true;
            return false;
        }
        public static int CEV()
        {
            int maxPivot = 0;
            for (int j = 1; j < slackCol - 2; j++)
                if (slackMatrix[slackRow - 1, j] > slackMatrix[slackRow - 1, maxPivot]) maxPivot = j;
            return maxPivot;
        }

        // Choose Departing Varaible
        public static int CDV(int col)
        {
            int minRow = -1;
            double Fraction_Rate = double.MaxValue;
            double fr;
            for (int i = 0; i < slackRow - 1; i++)
            {
                if (slackMatrix[i, slackCol - 1] == 1)
                {
                    if ((fr = Math.Abs(slackMatrix[i, slackCol - 2] / slackMatrix[i, col])) < Fraction_Rate && slackMatrix[i, col] < 0)
                    {
                        minRow = i;
                        Fraction_Rate = fr;
                    }
                }
            }
            return minRow;
        }
        public static bool BFC()
        {
            for (int i = 0; i < slackRow; i++)
                if (slackMatrix[i, slackCol - 1] == 1)
                    if (slackMatrix[i, slackCol - 2] < 0)
                        return false;
            return true;
        }
        public static void ToSlackForm()
        {
            slackRow = constraint + variables + 1;
            slackCol = constraint + variables + 2;
            slackMatrix = new double[slackRow, slackCol];
            ObjectiveFunc = new double[slackCol];
            for (int i = 0; i < variables; i++)
                for (int j = 0; j < slackCol; j++)
                    slackMatrix[i, j] = 0;
            for (int i = variables; i < slackRow - 1; i++)
            {
                for (int j = 0; j < slackCol; j++)
                {
                    if (j < variables)
                    {
                        if (matrix[i - variables, j] != 0) slackMatrix[i, j] = -matrix[i - variables, j];
                        else slackMatrix[i, j] = 0;
                    }
                    else if (j == slackCol - 2) slackMatrix[i, j] = matrix[i - variables, variables];
                    else if (j == slackCol - 1) slackMatrix[i, j] = 1;
                    else slackMatrix[i, j] = 0;
                }
            }
            for (int j = 0; j < variables; j++) slackMatrix[slackRow - 1, j] = matrix[slackRow - 1 - variables, j];
            for (int j = variables; j < slackCol - 1; j++) slackMatrix[slackRow - 1, j] = 0;
            slackMatrix[slackRow - 1, slackCol - 1] = 1;
            for (int j = 0; j < slackCol; j++)
            {
                ObjectiveFunc[j] = slackMatrix[slackRow - 1, j];
            }
        }

    }
}
