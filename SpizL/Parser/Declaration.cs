using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spizL
{
    class Declaration: Node
    {
        public TokenType Type { get; set; }
        public string VarName { get; set; }
        public Expression Exp { get; set; }
        public Declaration(TokenType type, string varName, List<Token> exp)
        {
            Type = type;
            VarName = varName;
            if(exp!=null)
                Exp = new Expression(exp);
        }

        public override string ToTreeString(int offset)
        {
            return "Dec " + VarName + Environment.NewLine + Exp.ToTreeString(offset);
        }
    }
}
