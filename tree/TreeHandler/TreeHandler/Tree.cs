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
        string[] colours = new string[] { "#FF0000", "#FF8700", "#FFFF00", "#49FF00", "#00FFB6", "#008BFF", "#0008FF", "#7400FF", "#FF00FB" };
        const double anglemod1 = 1;
        const double anglemod2 = 2;

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
            returnstring += @"""range"":""" + range * 1000 + @""",";
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

        public void LayoutPosition2()//2 because it was the second version, original was removed
        {
            BottomUpPass2(root);
            TopDownPass2(root);
            ColourByDepth();
        }

        public void BottomUpPass2(Node n)//find an approximate radius for each hemisphere
        {
            if (n.children.Count == 0)//leaf node
            {
                n.radius = h3.CalcRadius(0.0025);
                n.parent.radius += 7.2 * h3.CalcHArea(n.radius);
                n.size = 0;
                n.parent.size += 1;
            }
            else
            {
                foreach (Node c in n.children)
                {
                    BottomUpPass2(c);
                }

                n.radius = h3.CalcRadius(n.radius);

                if (n.name != "root")
                {
                    n.parent.radius += 7.2 * h3.CalcHArea(n.radius);
                    n.parent.size += n.size;
                }

                
            }
            
        }


        public void TopDownPass2(Node n)//place children on the surface of their parent hemisphere
        {
            if (n.children.Count != 0)
            {
                n.children = n.children.OrderByDescending(x => x.radius).ToList();


                double anglePhi = 0; // hemishpere angle
                double angleTheta = 0; //rotation angle
                double rp = n.radius;
                double maxPhi = 0;

                n.children[0].phi = 0;//place first node
                n.children[0].theta = 0;
                h3.SphereToCartisian(n.children[0]);

                TopDownPass2(n.children[0]);

                anglePhi += h3.CalcChangePhi(rp, n.children[0].radius) * anglemod1;

                for (int i = 1; i < n.children.Count; i++)
                {
                    double changeTheta = h3.CalcChangeTheta(rp, n.children[i].radius, anglePhi) * anglemod2;//const of 2 for adjustment

                    if (angleTheta + changeTheta <= Math.PI * 2)
                    {
                        angleTheta += changeTheta;
                        if (h3.CalcChangePhi(rp, n.children[i].radius) * anglemod1 > maxPhi)
                        {
                            maxPhi = h3.CalcChangePhi(rp, n.children[i].radius) * anglemod1;
                        }
                    }
                    else//reset new band
                    {
                        angleTheta = changeTheta;
                        anglePhi += maxPhi;
                    }

                    n.children[i].phi = anglePhi;
                    n.children[i].theta = angleTheta;
                    h3.SphereToCartisian(n.children[i]);

                    TopDownPass2(n.children[i]);
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

    }
}
