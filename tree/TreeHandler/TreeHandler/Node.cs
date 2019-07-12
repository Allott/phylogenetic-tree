using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeHandler
{
    class Node
    {
        public H3Layout position;
        public Node parent;
        public LinkedList<Node> children = new LinkedList<Node>();
        public string colour;

        public Node()//root
        {
            //h3position
            parent = null;
            colour = "#ffffff";
        }

        public Node(H3Layout pos, Node par, string col)
        {
            position = pos;
            parent = par;
            colour = col;
        }


    }
}
