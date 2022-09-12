using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace AlgoString_W1Q1
{
    public class W1Q1
    {
        public static char[] letters = new char[] { 'A', 'C', 'G', 'T' };
        public static void Main()
        {
            int n = int.Parse(Console.ReadLine());
            string[] patterns = new string[n];
            for (int i = 0; i < n; i++)
            {
                patterns[i] = Console.ReadLine();
            }
            Dictionary<char, int> cast = new Dictionary<char, int>();
            cast.Add('A', 0);
            cast.Add('C', 1);
            cast.Add('G', 2);
            cast.Add('T', 3);
            TrieNode root = new TrieNode(0);
            TrieNode current;
            int counter = 1;
            for (int i = 0; i < n; i++)
            {
                current = root;
                for (int j = 0; j < patterns[i].Length; j++)
                {
                    char c = patterns[i][j];
                    if (current.outgoing_edges[cast[c]] == null)
                    {
                        TrieNode newNode = new TrieNode(counter++);
                        current.outgoing_edges[cast[c]] = newNode;
                    }
                    current = current.outgoing_edges[cast[c]];
                }
            }
            dfs(root);
        }
        public static void dfs(TrieNode node)
        {
            for (int i = 0; i < 4; i++)
            {
                TrieNode item = node.outgoing_edges[i];
                if (item != null)
                {
                    Console.WriteLine(node.value + "->" + item.value + ":" + letters[i]);
                    dfs(item);
                }
            }
        }
    }
    public class TrieNode
    {
        public long value;
        public TrieNode[] outgoing_edges;
        public TrieNode(long value)
        {
            this.value = value;
            outgoing_edges = new TrieNode[4];
        }
    }

}
