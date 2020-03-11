using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

//
// Welcome to the Veodin/Templafy Software Development Test :)
//
// We have a tree that is built using the class Node where an instance of the class
// represents a node in the tree. For simplicity, the node has a single data field of
// type int.
//
// Your task is to write the extension method NodeExtensions.Next() to find the next
// element in the tree. Your solution should also contain a unit test to test your
// algorithm. You can write as many helper methods as you want.
// 
// In the Main method, we create an example tree. We then call the method you have to
// implement and show the expected output.
//
// - You are not allowed to make modifications to the class Node itself.
// - Your solution should work for all trees and not just for the example.
// - We favor readability over performance. But we care about performance.
//
// Submission: Please submit your solution within the next days to mma@templafy.com
//
//
namespace DevTest
{
    public class Node
    {
        public Node(int data, params Node[] nodes)
        {
            Data = data;
            AddRange(nodes);
        }

        public Node Parent { get; set; }
        public IEnumerable<Node> Children
        {
            get
            {
                return _children != null
                    ? _children
                    : Enumerable.Empty<Node>();
            }
        }
        public int Data { get; private set; }

        public void Add(Node node)
        {
            Debug.Assert(node.Parent == null);

            if (_children == null)
            {
                _children = new List<Node>();
            }
            _children.Add(node);

            node.Parent = this;
        }
        public void AddRange(IEnumerable<Node> nodes)
        {
            foreach (var node in nodes)
            {
                Add(node);
            }
        }

        public override string ToString()
        {
            return Data.ToString();
        }

        private List<Node> _children;
    }

    public static class NodeExtensions
    {
        public static Node Next(this Node node)
        {
            // TODO Implement extension method here
            if (node.HasChild())
                return node.FirstChild(); //returns first child if has any children
            else
            {
                do
                {
                    if (node.HasNextSibling())
                        return node.NextSibling();//returns next sibling node if has any

                    node = node.Parent;//repeat previous sibling check parent node
                }
                while (!node.IsRootNode());
            }
            return null;
        }

        public static Node NextSibling(this Node givenNode)
        {
            if (givenNode.IsRootNode())
                return null;
            else
            {
                bool givenNodIsPassed = false;
                foreach (Node currentChild in givenNode.Parent.Children)
                {
                    if (givenNodIsPassed)
                        return currentChild;

                    if (currentChild == givenNode)
                        givenNodIsPassed = true;
                }
            }
            return null;
        }

        public static bool HasChild(this Node node) => node.Children.Count() > 0;

        public static Node FirstChild(this Node node) => node.Children.FirstOrDefault();

        public static bool HasNextSibling(this Node node) => node.NextSibling() != null;

        public static bool IsRootNode(this Node node) => node.Parent == null;
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Test tree:
            // 
            // 1
            // +-2
            //   +-3
            //   +-4
            // +-5
            //   +-6
            //   +-7
            //
            var root = new Node(
                1,
                new Node(
                    2,
                    new Node(3),
                    new Node(4)),
                new Node(
                    5,
                    new Node(6),
                    new Node(7)));

            // Expected output:
            //
            // 1
            // 2
            // 3
            // 4
            // 5
            // 6
            // 7
            //
            var n = root;
            while (n != null)
            {
                Console.WriteLine(n.Data);
                n = n.Next();
            }

            // Test
            //
            n = root;
            Debug.Assert(n.Data == 1);
            n = n.Next();
            Debug.Assert(n.Data == 2);
            n = n.Next();
            Debug.Assert(n.Data == 3);
            n = n.Next();
            Debug.Assert(n.Data == 4);
            n = n.Next();
            Debug.Assert(n.Data == 5);
            n = n.Next();
            Debug.Assert(n.Data == 6);
            n = n.Next();
            Debug.Assert(n.Data == 7);
            n = n.Next();
            Debug.Assert(n == null);

            Console.ReadLine();
        }
    }
}
