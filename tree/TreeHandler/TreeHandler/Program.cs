using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace TreeHandler
{
    class Program
    {
        static void Main(string[] args)
        {//
            string newick = "(((A:0.2, B:0.3)LA:0.3,(C:0.5, D:0.3)LB:0.2)LD:0.3, E:0.7)LC:1.0;";
            NewickReader n = new NewickReader();
            Tree rtree = n.Read(newick);
            //rtree.LayoutPosition();
            //Console.Write(rtree.Print());


            Tree gentree = new Tree();
            Random r = new Random();

            
            for (int i = 0; i < 20; i++)
            {
                gentree.AddNode(gentree.root);
            }

            foreach (Node nc in gentree.root.children)
            {
                for (int i = 0; i < r.Next(0,50); i++)
                {
                    gentree.AddNode(nc);
                }

                foreach (Node nd in nc.children)
                {
                    for (int i = 0; i < 1; i++)
                    {
                        gentree.AddNode(nd);
                    }
                }
            }

            for (int i = 0; i < 40; i++)
            {
                gentree.AddNode(gentree.root);
            }

            foreach (Node nc in gentree.root.children)
            {
                for (int i = 0; i < r.Next(0, 50); i++)
                {
                    gentree.AddNode(nc);
                }
            }

                gentree.LayoutPosition();
            Console.Write(gentree.Print());
            }
    }
}
