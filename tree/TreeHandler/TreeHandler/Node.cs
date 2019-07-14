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
        public string name;
        public double distance;//distance to parent
        public int depth;

        public Node()//root
        {
            parent = null;
            name = "root";
            depth = 0;
        }

        public Node(H3Layout pos, Node par)
        {
            position = pos;
            parent = par;
            name = "";
            depth = par.depth + 1;
            par.children.AddLast(this);
        }

        public string Print()
        {
            string returnstring = "";
            for (int i = 0; i < depth; i++)
            {
                returnstring += "-";
            }

            returnstring += "name[" + name + 
                "] distance[" + distance + "]";
            foreach (Node n in children)
            {
                returnstring += "\r\n";
                returnstring += n.Print();
            }
            return returnstring;
        }
    }
}
