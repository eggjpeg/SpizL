using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spizL
{
    class Parser
    {
        public AST Parse(List<Token> tList)
        {
            AST ast = new AST();
            Node parent = ast;
            var lines = GetLines(tList);
            Print(lines);
            foreach (var item in lines)
               parent = ProcessLine(item, parent);    
            ast.PrintPretty(" ", true);
            return ast;
        }

        public void Print(List<List<Token>> list)
        {
            foreach (var item in list)
            {
                foreach (var item2 in item)
                    Console.Write(item2.ToString());
                Console.WriteLine();
            }
        }

        public List<List<Token>> GetLines(List<Token> list)
        {
            var r = new List<List<Token>>();
            var l = new List<Token>();
            foreach (var item in list)
            {
                if(item.Type == TokenType.Eol)
                {
                    r.Add(l);
                    l = new List<Token>();
                    continue;
                }                   
                l.Add(item);

            }
            return r;
        }

        private bool HasOp(List<Token> line, TokenType type)
        {
            foreach (var item in line)
                if (item.Type == type)
                    return true;
            return false;
        }

        private List<Token> GetRange(List<Token> line, int index, int count)
        {
            if (line.Count >= index + count)
                return line.GetRange(1, line.Count - 1);
            else
                return null;
        }


        public Node CreateNode(List<Token> line, Node parent)
        {
            if (HasOp(line, TokenType.Colon))
                return new FunctionDef(line);
            else if (line[0].IsType())
                if (line.Count == 2)
                    return new Declaration(line[0].Type, line[1].Value, null);
                else
                    return new Declaration(line[0].Type, line[1].Value, line.GetRange(3, line.Count - 3));
            else if (line[0].Type == TokenType.Dospiz)
                return new Dospiz(GetRange(line, 1, line.Count - 1));
            else if (line[0].Type == TokenType.Spif)
                return new Spif(line.GetRange(1, line.Count - 1));
            else if (line[0].Type == TokenType.Spelz)
                return new Spelz();
            else if (line[0].Type == TokenType.Spelzif)
                return new Spelzif(line.GetRange(1, line.Count - 1));
            else if (line[0].Type == TokenType.spizdun)
                return new spizdun(GetRange(line, 1, line.Count - 1));
            else if (line[0].Type == TokenType.spizout)
                return new spizout(GetRange(line, 1, line.Count - 1));
            else if (HasOp(line, TokenType.Equal)) 
                return new Assignment(line);
            else if (HasOp(line, TokenType.Oparen)) 
                return new FunctionCall(line);
            else
                throw new Exception("Unknown line.");
        }

        public Node ProcessLine(List<Token> line, Node parent)
        {
            
            int kd = KillDots(line);
            Node newParent = parent;

            if (line.Count > 0)
            {
                Node n = CreateNode(line, parent);
                parent.Add(n);
                n.Parent = parent;
                if (n.SetNewParent)
                    newParent = n;
            }
            for (int i = 0; i < kd; i++)
                newParent = newParent.Parent;
            return newParent;
        }

        public int KillDots(List<Token> list)
        {
            if (list.Count == 0)
                return 0;
            int i = 0;
            while (list[list.Count-1].Type == TokenType.Dot)
            {
                i++;
                list.RemoveAt(list.Count - 1);
            }
            return i;
        }

    }
}
