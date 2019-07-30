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
        {
            string newick = "(((A:0.2, B:0.3)LA:0.3,(C:0.5, D:0.3)LB:0.2)LD:0.3, E:0.7)LC:1.0;";
            NewickReader n = new NewickReader();
            Tree rtree = n.Read(newick);



            Tree gentree = new Tree();
            Random r = new Random();

            for (int i = 0; i < 9; i++)
            {
                int jl = gentree.nodeList.Count;
                for (int j = 0; j < jl; j++)
                {
                    for (int k = 0; k < r.Next(0,4); k++)
                    {
                        gentree.AddNode(gentree.nodeList[j]);
                    }
                }
            }
            

            gentree.LayoutPosition();
            Console.Write(gentree.Print());
            }
    }
}
