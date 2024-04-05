using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spizL
{
    class spizdun: Node
    {
        public Expression Exp { get; set; }
        public spizdun(List<Token> exp)
        {
            if (exp != null)
                Exp = new Expression(exp);
        }

        public override string ToTreeString(int indent)
        {
            return "spizdun ";
        }
    }
}
