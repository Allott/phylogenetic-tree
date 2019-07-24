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
            snake(gentree, gentree.root, 4);

            Random r = new Random();
            foreach (Node no in gentree.nodeList)
            {
                no.position.d = r.Next(10) + 1;
            }

            gentree.LayoutPosition();
            Console.Write(gentree.Print());
        }

        static void snake(Tree g, Node n, int i)
        {
            i--;
            if (i>0)
            {
                for (int j = 0; j < 10; j++)
                {
                    g.AddNode(n);
                    snake(g, n.children[j], i);
                }
            }
        }
    }
}
