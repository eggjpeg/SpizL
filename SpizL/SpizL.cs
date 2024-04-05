using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spizL
{
    public class spizL
    {
        private string file;
        public spizL(string file)
        {
            this.file = file;
        }

        public string Result;

        public void Run()
        {
            //Step 1. 
            Lexer l = new Lexer();
            var list = l.Tokenize(file);

            //Step 2.
            Parser p = new Parser();
            AST ast = p.Parse(list);

            Function f = new Function();

           string s =  f.Compile((FunctionDef)ast.Children[0]);
            Console.WriteLine(s);



            //Step 3.
            Squirrel sq = new Squirrel(ast);
            sq.Traverse();
            this.Result = sq.GetTrace();
        }
    }
}
