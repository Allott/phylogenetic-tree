using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeHandler
{
    class NewickReader
    {
        public NewickReader()
        { }

        public Tree Read(string newick)
        {
            Tree returntree = new Tree();
            Node current = returntree.GetRoot();
            string name = "";
            char[] newarray = newick.ToCharArray();

            for (int i = 0; i < newarray.Length; i++)
            {
                switch (newarray[i])
                {
                    case ';':
                        NameNode(current, name);
                        return returntree;
                    case '(':
                        current = returntree.AddNode(current);
                        break;
                    case ')':
                        NameNode(current, name);
                        name = "";
                        current = current.parent;
                        break;
                    case ',':
                        NameNode(current, name);
                        name = "";
                        current = current.parent;
                        current = returntree.AddNode(current);
                        break;
                    default:
                        name += newarray[i];
                        break;
                }
            }
            return returntree;
        }


        private void NameNode(Node current, string name)//divides name:distance and updates the nodes
        {
            string[] parts = name.Split(':');
            if (parts.Length == 2)
            {
                current.name = parts[0];
                current.position.d = (float) Convert.ToDouble(parts[1]);//hmmm think about types more
            }
            else
            {
                current.name = name;
            }

        }
    }
}
