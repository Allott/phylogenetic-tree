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
            
        }

        public void TopDownPass()//place children on the surface of their parent hemisphere
        {

        }
    }
}
