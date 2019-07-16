using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeHandler
{
    class Program
    {
        static void Main(string[] args)
        {//
            string newick = "(((A:0.2, B:0.3):0.3,(C:0.5, D:0.3):0.2):0.3, E:0.7):1.0;";
            NewickReader n = new NewickReader();

            //test 1
            Tree mytree = new Tree();
            mytree.AddNode(mytree.GetRoot());

            //test 2
            Tree rtree = n.Read(newick);
            rtree.SortList();
            Console.Write(rtree.Print());
        }
    }
}
