using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuffmanTreeBuilder
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string s = Console.ReadLine();
            int[] freqs = new int[65536];
            foreach (char c in s)
                ++freqs[c];
            var nodes = new SortedSet<HuffmanNode>();
            for (int i = 0; i < freqs.Length; ++i)
                if (freqs[i] > 0)
                    nodes.Add(new HuffmanNode(freqs[i], (char) i));
            while (nodes.Count > 1)
            {
                HuffmanNode min0 = nodes.Min;
                nodes.Remove(min0);
                HuffmanNode min1 = nodes.Min;
                nodes.Remove(min1);
                nodes.Add(new HuffmanNode(min0.Frequency + min1.Frequency, min0, min1));
            }
            PrintNode(nodes.Min, 0);
            Console.ReadLine();
        }

        private static void PrintNode(HuffmanNode node, int recursionDepth)
        {
            if (node.Character != null)
                Console.WriteLine(new string(' ', 2*recursionDepth) + node.Character);
            else
            {
                PrintNode(node.LeftChild, recursionDepth + 1);
                Console.WriteLine(new string(' ', 2 * recursionDepth) + '.');
                PrintNode(node.RightChild, recursionDepth + 1);
            }
        }

}

    class HuffmanNode : IComparable<HuffmanNode>
    {
        public HuffmanNode(int frequency, char character)
        {
            Frequency = frequency;
            Character = character;
        }

        public HuffmanNode(int frequency, HuffmanNode left, HuffmanNode right)
        {
            Frequency = frequency;
            LeftChild = left;
            RightChild = right;
        }

        public int Frequency { get; private set; }

        public char? Character { get; private set; }

        public HuffmanNode LeftChild { get; private set; }
        public HuffmanNode RightChild { get; private set; }

        int IComparable<HuffmanNode>.CompareTo(HuffmanNode other)
        {
            //consider these nodes equal
            if (Character != null && Character == other.Character || Character == null &&
                other.LeftChild == LeftChild && other.RightChild == RightChild)
                return 0;
            //consider these one different
            return Frequency <= other.Frequency ? -1 : 1;
        }
    }
}
