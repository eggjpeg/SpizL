using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spizL
{
    abstract class Node
    {
        public Node Parent;
        public bool SetNewParent;


        private Guid id = Guid.NewGuid();

        public Guid Id {
            get
            {
                return id;
            }
        }

        public List<Node> Children;
        public void Add(Node n)
        {
            if (Children == null)
                Children = new List<Node>();
            Children.Add(n);
        }

        public abstract string ToTreeString(int indent);
        
        public void PrintPretty(string indent, bool last)
        {
            Console.Write(indent);
            if (last)
            {
                Console.Write("\\-");
                indent += "  ";
            }
            else
            {
                Console.Write("|-");
                indent += "| ";
            }
            
            Console.WriteLine(ToTreeString(indent.Length));

            if (Children == null)
                return;

            for (int i = 0; i < Children.Count; i++)
                Children[i].PrintPretty(indent, i == Children.Count - 1);
        }
    }
}
