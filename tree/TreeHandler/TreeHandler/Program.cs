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
                Console.WriteLine("3. generate from other file type (experimental)");
                Console.WriteLine("4. speedtest");
                Console.WriteLine("5. exit");

                switch (Console.ReadLine())
                {
                    case "1":
                        NewickGenerate();
                        break;
                    case "2":
                        TestGenerate();
                        break;
                    case "3":
                        GenerateOther();
                        break;
                    case "4":
                        SpeedTest();
                        break;
                    case "5":
                        return;
                    default:
                        break;
                }
            } 
        }

        public static void NewickGenerate()
        {
            Console.Clear();
            Console.WriteLine("enter newick file location");
            string newick = System.IO.File.ReadAllText(Console.ReadLine());
            Console.Clear();
            NewickReader n = new NewickReader();
            Tree tree = n.Read(newick);
            tree.root.name = "root";
            tree.LayoutPosition2();
            Console.WriteLine(tree.Print());
            Console.ReadLine();
        }

        public static void TestGenerate()
        {
            Console.Clear();
            Tree gentree = new Tree();
            Console.WriteLine("enter number of nodes in the tree generated");
            int number = Convert.ToInt32(Console.ReadLine())-1;

            for (int i = 0; i < number; i++)
            {
                gentree.AddNode(gentree.nodeList[r.Next(0, gentree.nodeList.Count-1)]);
            }

            for (int i = 1; i < gentree.nodeList.Count -1; i++)
            {
                gentree.nodeList[i].name = randomname();
                gentree.nodeList[i].distance = r.Next(0, 100);
            }

            var watch = new System.Diagnostics.Stopwatch();

            watch.Start();


            gentree.LayoutPosition2();

            watch.Stop();

            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
            Console.WriteLine("print? (y/n)");
            if (Console.ReadLine() == "y")
            {
                Console.Clear();
                Console.Write(gentree.Print());
                Console.ReadLine();
            }
            
            
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
        public static void GenerateOther()//generating from other files
        {
            Console.Clear();
            Console.WriteLine("enter newick file location");
            string newick = System.IO.File.ReadAllText(Console.ReadLine());
            Console.Clear();
            NewickReader n = new NewickReader();
            Tree tree = n.Read(newick);


            Console.WriteLine("enter assinment file location");
            string[] lines = System.IO.File.ReadAllLines(Console.ReadLine());


            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = lines[i].Replace("\t", "");
            }

            foreach (Node node in tree.nodeList)
            {
                if (node.name != "")
                {
                    node.name = lines[Convert.ToInt32(node.name)-1];
                }
            }
            tree.root.name = "root";
            tree.LayoutPosition2();
            Console.WriteLine(tree.Print());
            Console.ReadLine();
        }

        public static void SpeedTest()
        {
            for (int i = 1; i < 11; i++)
            {

                long count = 0;

                for (int j = 0; j < 10; j++)
                {
                    Tree gentree = new Tree();

                    int f = (i * 500000) -1;

                    for (int k = 0; k < f; k++)
                    {
                        gentree.AddNode(gentree.nodeList[r.Next(0, gentree.nodeList.Count - 1)]);
                    }

                    for (int k = 1; k < gentree.nodeList.Count - 1; k++)
                    {
                        gentree.nodeList[k].name = randomname();
                        gentree.nodeList[k].distance = r.Next(0, 100);
                    }

                    var watch = new System.Diagnostics.Stopwatch();
                    watch.Start();
                    gentree.LayoutPosition2();
                    watch.Stop();

                    count += watch.ElapsedMilliseconds;
                    
                }

                count = count / 10;
                Console.WriteLine(i * (500000) + "  " + count + " ms");

            }
        }
    }
}
