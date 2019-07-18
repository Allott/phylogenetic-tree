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
        public List<Node> nodeList = new List<Node>();

        public Tree()
        {
            root = new Node();
            nodeList.Add(root);
        }

        public Node AddNode(Node par)
        {
            Node n = new Node(par);
            nodeList.Add(n);
            return n;
        }

        public Node AddLeaf(Node par,string s)
        {
            Node n = new Node(par);
            //n.SetName(s);
            nodeList.Add(n);
            return n;
        }

        public Node GetRoot()
        {
            return root;
        }

        public string Print()
        {
            string returnstring = "{\r\n  ";
            returnstring += @"""name"":""tree name"",";
            returnstring += "\r\n  ";
            returnstring += @"""length"":15,";
            returnstring += "\r\n  ";
            returnstring += @"""nodes"":[";
            

            foreach (Node n in nodeList)//fix parent ID system
            {
                returnstring += "\r\n     ";
                returnstring += n.Print();
            }
            returnstring += "\r\n   ]\r\n  }";
            return returnstring;
        }

        public void LayoutPosition()
        {
            SortList();
            BottomUpPass();
            TopDownPass();
        }

        public void SortList()//sort list by depth for Bottom Up and Top Down Passes
        {
            nodeList = nodeList.OrderBy(x => x.depth).ToList();
        }


        public void BottomUpPass()//find an approximate radius for each hemisphere
        {
            for (int i = nodeList.Count - 1; i > 0; i--)//messy
            {
                if (nodeList[i].children.Count == 0)
                {
                    nodeList[i].position.r = 1;
                }
                nodeList[i].parent.position.r += nodeList[i].position.r;
            }
        }

        public void TopDownPass()//place children on the surface of their parent hemisphere
        {
            foreach (Node n in nodeList)//assuming biggest is at the top
            {
                if (n.children.Count() != 0)
                {
                    List<Node> orderedC = n.children.OrderBy(x => x.position.r).ToList();//list of children by "radius"

                    //sphere packing
                }
            }
        }
    }
}
