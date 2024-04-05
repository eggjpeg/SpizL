using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spizL
{
    class Spelz:Node
    {
        public Spelz()
        {
            this.SetNewParent = true;
        }
        public override string ToTreeString(int indent)
        {
            return "Spelz";
        }
    }
}
