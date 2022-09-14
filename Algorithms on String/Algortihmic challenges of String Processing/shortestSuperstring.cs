using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace E1
{
    public class Q3LeastLengthString : Processor
    {
        public Q3LeastLengthString(string testDataName) : base(testDataName)
        {
            this.VerifyResultWithoutOrder = true;
        }

        public override string Process(string inStr) =>
        E1Processors.ProcessQ3FindAllOccur(inStr, Solve);

        public long Solve(string text, long k)
        {
            long totallenght = 0;
            long n = ComputePrefixFunction(text);
            return k * text.Length - (k - 1) * n;
        }
        public long ComputePrefixFunction(string P)
        {
            int n = P.Length;
            long[] s = new long[n];
            s[0] = 0;
            long border = 0;
            for (int i = 1; i < n; i++)
            {
                while (border > 0 && P[i] != P[(int)border])
                {
                    border = s[border - 1];
                }
                if (P[i] == P[(int)border]) border++;
                else border = 0;
                s[i] = border;
            }
            return s[n - 1];
        }
    }
}