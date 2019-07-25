using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace TreeHandler
{
    class Node
    {

        
        public Node parent;
        public List<Node> children = new List<Node>();
        public string name;
        public string colour = "#F41FB5";
        public int listid = 0;
        public double distance;
        
        public Vector4 position;
        public double phi;
        public double theta;
        public double radius;
        public double area;
        public int depth;
        public int size;
        public Matrix4x4 m;

        public Node()//root
        {
            parent = null;
            name = "root";
            depth = 0;
            parent = null;
            position = new Vector4(0);
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

            H3Layout h3 = new H3Layout();

            returnstring += h3.GetStringCoords(position);

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
