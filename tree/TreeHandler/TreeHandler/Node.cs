using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeHandler
{
    class Node
    {
        public H3Layout position = new H3Layout();
        public Node parent;
        public LinkedList<Node> children = new LinkedList<Node>();
        public string name;
        public double distance;//distance to parent
        public int depth;
        public string colour = "#F41FB5";

        public Node()//root
        {
            parent = null;
            name = "root";
            depth = 0;
            parent = this;
        }

        public Node(Node par)
        {
            parent = par;
            name = "";
            depth = par.depth + 1;
            par.children.AddLast(this);
        }

        public string Print()
        {
            string returnstring = "";
            returnstring += @"{""position"":""";
            returnstring += position.GetCartisianPosition();
            returnstring += @""",""colour"":""";
            returnstring += colour;
            returnstring += @""",""text"":""";
            returnstring += name;
            returnstring += @""",""parentIDFix"":""";
            returnstring += depth;
            //returnstring += parent.name;
            //returnstring += @"""}";
            return returnstring;
        }
    }
}
