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

        
        public Node parent;//parent node
        public List<Node> children = new List<Node>();//list of child nodes
        public string name;//nodes name
        public string colour = "#F41FB5";//colour of the node
        public int listid = 0;//Id in tree classes list of nodes, used for converting to JSon
        public double distance;//distace from parent

        public Vector4 position;//poition in cartisian space
        public Matrix4x4 cumulativeRotation;//cumulative rotation of parents
        public double phi;//angles of placement on parent hemisphere 
        public double theta;
        public double radius;//radius of hemisphere

        public int depth;//nodes depth in the tree
        public int size;//number of nodes in this nodes subtree

        public Node()//root node only
        {
            parent = null;
            name = "root";
            depth = 0;
            parent = null;
            position = new Vector4(0);
            phi = 0;
            theta = 0;
            cumulativeRotation = new Matrix4x4(1, 0, 0, 0,
                                   0, 1, 0, 0,
                                   0, 0, 1, 0,
                                   0, 0, 0, 1);//identity matrix
        }

        public Node(Node par)
        {
            parent = par;
            name = "";
            depth = par.depth + 1;
            par.children.Add(this);
        }

        public string Print(double range)
        {
            string returnstring = "";
            returnstring += @"{""position"":""";

            H3Layout h3 = new H3Layout();

            returnstring += h3.GetStringCoords(position, range);

            returnstring += @""",""colour"":""";
            returnstring += colour;
            returnstring += @""",""name"":""";
            returnstring += name;
            returnstring += @""",""distance"":""";
            returnstring += distance;
            returnstring += @""",""parentID"":""";
            returnstring += listid;
            returnstring += @"""}";
            return returnstring;
        }
    }
}
