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
        public string name;
        public List<Node> nodeList = new List<Node>();
        H3Layout h3 = new H3Layout();
        string[] colours = new string[] {"#FFFFFF", "#FF0000", "#FF8700", "#FFFF00", "#49FF00", "#00FFB6", "#008BFF", "#0008FF", "#7400FF", "#FF00FB" };

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

        public Node GetRoot()
        {
            return root;
        }

        public string Print()
        {
            double range = -1; ;

            foreach (Node n in nodeList)
            {
                if (n.position.Z > range)
                {
                    range = n.position.Z;
                }
            }

            string returnstring = "{\r\n  ";
            returnstring += @"""name"":""" + name + @""",";
            returnstring += "\r\n  ";
            returnstring += @"""range"":""" + range*1000 + @""",";
            returnstring += "\r\n  ";
            returnstring += @"""length"":" + nodeList.Count + ",";
            returnstring += "\r\n  ";
            returnstring += @"""nodes"":[";

            for (int i = 0; i < nodeList.Count; i++)
            {
                foreach (Node c in nodeList[i].children)
                {
                    c.listid = i;
                }
            }
            bool com = true;//for placing , after each node
            foreach (Node n in nodeList)//fix parent ID system
            {
                if (com)
                {
                    com = false;
                }
                else
                {
                    returnstring += ",";
                }

                returnstring += "\r\n     ";
                returnstring += n.Print(range);


            }
            returnstring += "\r\n   ]\r\n  }";
            return returnstring;
        }

        public void LayoutPosition()
        {
            SortByDepth();
            BottomUpPass();
            SortBySize();
            TopDownPass();
            ColourByDepth();
        }

        public void SortByDepth()//sort list by depth for Bottom Up and Top Down Passes
        {
            nodeList = nodeList.OrderBy(x => x.depth).ToList();
        }

        public void SortBySize()//sort child lists by their sizes
        {
            foreach (Node n in nodeList)
            {
                n.children = n.children.OrderByDescending(x => x.size).ToList();
            }
        }


        public void BottomUpPass()//find an approximate radius for each hemisphere
        {
            for (int i = nodeList.Count - 1; i > -1; i--)//messy
            {
                if (nodeList[i].children.Count == 0)//leaf node
                {
                    nodeList[i].radius = h3.CalcRadius(0.0025);
                }
                else
                {
                    double r = 0;
                    int s = 0;
                    foreach (Node n in nodeList[i].children)
                    {
                        r += 7.2 * h3.CalcHArea(n.radius);//why 7.2?
                        s += 1;
                        if (n.children.Count != 0)
                        {
                            s += n.size;
                        }
                    }
                    nodeList[i].radius = h3.CalcRadius(r);
                    nodeList[i].size = s;
                }
            }
        }
        public void TopDownPass()//place children on the surface of their parent hemisphere
        {
            foreach (Node np in nodeList)
            {
                if (np.children.Count != 0)
                {
                    double anglePhi = 0; // hemishpere angle
                    double angleTheta = 0; //rotation angle
                    double rp = np.radius;
                    double maxPhi = 0;

                    np.children[0].phi = 0;
                    np.children[0].theta = 0;
                    h3.SphereToCartisian(np.children[0]);


                    anglePhi += h3.CalcChangePhi(rp, np.children[0].radius);

                    for (int i = 1; i < np.children.Count; i++)
                    {
                        double changeTheta = h3.CalcChangeTheta(rp, np.children[i].radius, anglePhi);

                        if (angleTheta + changeTheta <= Math.PI*2)
                        {
                            angleTheta += changeTheta;
                            if (h3.CalcChangePhi(rp, np.children[i].radius) > maxPhi)
                            {
                                maxPhi = h3.CalcChangePhi(rp, np.children[i].radius);
                            }
                        }
                        else//reset new band
                        {
                            angleTheta = changeTheta;
                            anglePhi += maxPhi;
                        }

                        np.children[i].phi = anglePhi;
                        np.children[i].theta = angleTheta;
                        h3.SphereToCartisian(np.children[i]);
                    }
                }
            }
        }

        public void ColourByDepth()
        {
            foreach (Node n in nodeList)
            {
                n.colour = colours[n.depth%colours.Length];
            }
        }

        public double calculateAngle(double r, double w1, double w2, double m)//fix this
        {
            double w3 = w1 + w2;
            w3 = w3 * Math.PI;
            return 2 * Math.Asin(w3 / r) * m;
        }

    }
}
