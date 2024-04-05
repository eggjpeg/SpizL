using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spizL
{
    class Spif: Node
    {
        public Expression Exp { get; set; }

        public Spif(List<Token> e)
        {
            this.SetNewParent = true;
            Exp = new Expression(e);
        }
        public override string ToTreeString(int indent)
        {
            return "Spif " + Token.ToString(Exp.Exp);
        }
    }
}
