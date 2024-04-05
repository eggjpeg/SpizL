using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spizL
{
    class State : SortedDictionary<string,VarState>
    {
        public bool IsTrace { get; set; }
        public StringBuilder Trace { get; set; }
        
        public State()
        {
            this.Trace = new StringBuilder();
            IsTrace = true;
        }
    }
}
