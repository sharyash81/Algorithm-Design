using System;
using System.Collections.Generic;
using TestCommon;
using System.Linq;

namespace E1
{
    public class Q2SubStrings : Processor
    {

        public class Node
        {
            public Node[] child;
            public Node()
            {
                child = new Node[5];
            }
        }
        public Q2SubStrings(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
        E1Processors.ProcessQ3SubStrings(inStr, Solve);

        public long Count;
        long[] order;
        long[] _class;
        public virtual long Solve(long n, String text)
        {
            Count = 0;

            order = SortCharacters(text);
            _class = ComputeCharClasses(text);
            long L = 1;
            while (L < n)
            {
                order = SortDoubled(text, L);
                _class = UpdateClasses(L);
                L *= 2;
            }
            return n * (n + 1) / 2 - LCP_Array(text);
        }
        public long[] SortCharacters(string S)
        {
            int n = S.Length;
            long[] order = new long[n];
            long[] count = new long[256];
            for (int i = 0; i < n; i++) count[S[i]]++;
            for (int j = 1; j < 256; j++) count[j] += count[j - 1];
            for (int i = n - 1; i >= 0; i--)
            {
                char c = S[i];
                count[c]--;
                order[count[c]] = i;
            }
            return order;
        }
        public long[] ComputeCharClasses(string S)
        {
            int n = S.Length;
            long[] _classes = new long[n];
            _classes[order[0]] = 0;
            for (int i = 1; i < n; i++)
            {
                if (S[(int)order[i]] != S[(int)order[i - 1]]) _classes[order[i]] = _classes[order[i - 1]] + 1;
                else _classes[order[i]] = _classes[order[i - 1]];
            }
            return _classes;
        }
        public long[] SortDoubled(string S, long L)
        {
            int n = S.Length;
            long[] count = new long[n];
            long[] newOrder = new long[n];
            for (int i = 0; i < n; i++) count[_class[i]]++;
            for (int i = 1; i < n; i++) count[i] += count[i - 1];
            for (int i = n - 1; i >= 0; i--)
            {
                long start = (order[i] - L + n) % n;
                long cl = _class[(int)start];
                count[cl]--;
                newOrder[count[cl]] = start;
            }
            return newOrder;
        }
        public long[] UpdateClasses(long L)
        {
            int n = order.Length;
            long[] newClass = new long[n];
            newClass[order[0]] = 0;
            for (int i = 1; i < n; i++)
            {
                long cur = order[i];
                long prev = order[i - 1];
                long mid = (cur + L) % n;
                long midPrev = (prev + L) % n;
                if (_class[cur] != _class[prev] || _class[mid] != _class[midPrev]) newClass[cur] = newClass[prev] + 1;
                else newClass[cur] = newClass[prev];
            }
            return newClass;
        }
        public long LCP_Array(string s)
        {
            int n = s.Length;
            long[] lcpArray = new long[n - 1];
            long lcp = 0;
            long[] PosInOrder = new long[order.Length];
            for (int i = 0; i < n; i++)
            {
                PosInOrder[order[i]] = i;
            }
            long suffix = order[0];
            for (int i = 0; i < n; i++)
            {
                long orderIndex = PosInOrder[suffix];
                if (orderIndex == n - 1)
                {
                    lcp = 0;
                    suffix = (suffix + 1) % n;
                    continue;
                }
                long nextSuffix = order[orderIndex + 1];
                lcp = LCPofSuffixes(s, suffix, nextSuffix, lcp - 1);
                lcpArray[orderIndex] = lcp;
                suffix = (suffix + 1) % n;
            }
            long total = 0;
            for (int i = 0; i < lcpArray.Length; i++)
            {
                total += lcpArray[i];
            }
            return total;
        }
        public long LCPofSuffixes(string s, long i, long j, long equal)
        {
            int n = s.Length;
            long lcp = Math.Max(0, equal);
            while (i + lcp < n && j + lcp < n)
            {
                if (s[(int)(i + lcp)] == s[(int)(j + lcp)]) lcp++;
                else break;
            }
            return lcp;
        }
    }
}
