using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace AlgorithmsOnString_W1Q2
{
    public class W1Q2
    {
        public static void Main()
        {

            string text = Console.ReadLine();
            int n = int.Parse(Console.ReadLine());
            string[] patterns = new string[n];
            for (int i = 0; i < n; i++)
            {
                patterns[i] = Console.ReadLine();
            }
            Dictionary<char, int> cast = new Dictionary<char, int>();
            cast.Add('A', 1);
            cast.Add('C', 2);
            cast.Add('G', 3);
            cast.Add('T', 4);
            cast.Add('$', 0);
            Node root = new Node();
            Node current;
            int index;
            for (int i = 0; i < n; i++)
            {
                patterns[i] += "$";
                current = root;
                for (int j = 0; j < patterns[i].Length; j++)
                {
                    char c = patterns[i][j];
                    index = cast[c];
                    if (current.child[index] == null)
                    {
                        Node newNode = new Node();
                        current.child[index] = newNode;
                    }
                    current = current.child[index];
                }
            }
            List<long> ans = new List<long>();
            int k;
            for (int i = 0; i < text.Length; i++)
            {
                current = root;
                k = i;
                while (k < text.Length && current.child[0] == null && (current = current.child[cast[text[k]]]) != null) k++;
                if (current == null) continue;
                if (current.child[0] != null) ans.Add(i);
            }
            for (int i = 0; i < ans.Count(); i++)
            {
                Console.Write(ans[i] + " ");
            }
        }
    }
    public class Node
    {
        public Node[] child;
        public Node()
        {
            child = new Node[5];
        }
    }
}