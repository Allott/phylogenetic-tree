using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeHandler
{
    class Tree
    {
        public Node root;
        public LinkedList<Node> nodeList = new LinkedList<Node>();

        public Tree()
        {
            root = new Node();
            nodeList.AddFirst(root);
        }

        public Node AddNode(Node par)
        {
            Node n = new Node(null, par);
            nodeList.AddLast(n);
            return n;
        }

        public Node AddLeaf(Node par,string s)
        {
            Node n = new Node(null, par);
            //n.SetName(s);
            nodeList.AddLast(n);
            return n;
        }

        public Node GetRoot()
        {
            return root;
        }

        public string Print()
        {
            string returnstring = "";
            returnstring += root.Print();
            return returnstring;
        }
    }
}
