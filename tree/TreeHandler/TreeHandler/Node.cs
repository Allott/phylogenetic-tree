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
        public List<Node> children = new List<Node>();
        public string name;
        public int depth;
        public string colour = "#F41FB5";
        public int listid = 0;

        public Node()//root
        {
            parent = null;
            name = "root";
            depth = 0;
            parent = null;
            position.normal.Y = 1;
        }

        public Node(Node par)
        {
            parent = par;
            name = "";
            depth = par.depth + 1;
            par.children.Add(this);
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
            returnstring += @""",""parentID"":""";
            returnstring += listid;
            returnstring += @"""}";
            return returnstring;
        }
    }
}
