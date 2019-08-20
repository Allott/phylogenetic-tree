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

        public static Random r = new Random();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Phylogenetic Tree to H3 Json File");
                Console.WriteLine("1. generate from newick file");
                Console.WriteLine("2. generate testing file");
                Console.WriteLine("3. exit");

                switch (Console.ReadLine())
                {
                    case "1":
                        NewickGenerate();
                        break;
                    case "2":
                        TestGenerate();
                        break;
                    case "3":
                        return;
                    default:
                        break;
                }
            } 
        }

        public static void NewickGenerate()
        {
            Console.Clear();
            Console.WriteLine("enter newick file");
            string newick = Console.ReadLine();
            Console.Clear();
            NewickReader n = new NewickReader();
            Tree tree = n.Read(newick);
            tree.LayoutPosition();
            Console.WriteLine(tree.Print());
            Console.ReadLine();
        }

        public static void TestGenerate()
        {
            Console.Clear();
            Tree gentree = new Tree();

            for (int i = 0; i < 8; i++)
            {
                int jl = gentree.nodeList.Count;
                for (int j = 0; j < jl; j++)
                {
                    for (int k = 0; k < r.Next(0, 4); k++)
                    {
                        gentree.AddNode(gentree.nodeList[j]);
                    }
                }
            }
            for (int i = 0; i < 20; i++)
            {
                gentree.AddNode(gentree.root);
            }

            foreach (Node n in gentree.nodeList)
            {
                n.distance = r.Next(0, 10);
                n.name = randomname();
            }

            gentree.LayoutPosition();
            Console.Write(gentree.Print());
            Console.ReadLine();
        }


        public static string randomname()//just for fun
        {
            string s = "";
            string vowel = "aeiou";
            string consonant = "bcdfghjklmnpqrstvwxz";
            

            for (int i = 0; i < r.Next(1,5); i++)
            {
                s += consonant[r.Next(0, consonant.Length)];
                s += vowel[r.Next(0, vowel.Length)];
            }

            s += consonant[r.Next(0, consonant.Length)];

            return s;
        }
    }
}
