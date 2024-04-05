using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spizL
{
    class ExpNode
    {
        public List<ExpNode> ChildList = null;
        public void AddChild(ExpNode n)
        {
            if (ChildList == null)
                ChildList = new List<ExpNode>();
            ChildList.Add(n);
        }

        public bool IsFunction { get; set; }
        public bool IsListIndex { get; set; }

        public Token Token;

        public string ListName
        {   
            get
            {
                if (Token.Type == TokenType.VarName)
                    return Token.Value;
                else
                    throw new Exception("Not a list name.");
            }
        }

        public string FunctionName
        {
            get {
                if (Token.IsCommand())
                    return Token.Type.ToString();
                else if (Token.Type == TokenType.VarName)
                    return Token.Value;
                else
                    throw new Exception("Not a function.");
            }
        }

        public ExpNode(TokenType type)
        {
            this.Token = new Token(type);
        }


        public ExpNode(Token t)
        {
            this.Token = t;
        }

        

        public bool IsLeaf()
        {
            if (ChildList == null)
                return true;
            else
                return false;
        }



        public string ToTreeString(int indent)
        {
            StringBuilder sb = new StringBuilder();
            ToStr(sb, new string(' ',indent), false);
            return sb.ToString();
        }

        public void ToStr(StringBuilder sb, string indent, bool last)
        {
            sb.Append(indent);
            if (last)
            {
                sb.Append("\\-");
                indent += "  ";
            }
            else
            {
                sb.Append("|-");
                indent += "| ";
            }

            if(IsFunction)
                sb.AppendLine("func " + Token.Value);
            else
                sb.AppendLine(Token.ToString());

            if (ChildList == null)
                return;

            for (int i = 0; i < ChildList.Count; i++)
                ChildList[i].ToStr(sb,indent, i == ChildList.Count - 1);
        }
    }
}
