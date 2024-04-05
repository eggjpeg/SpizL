using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spizL
{
    /// <summary>
    /// 1. dospiz
    /// 2. dospiz i>2
    /// 3. dospiz int item col
    /// 
    /// lint col
    /// spadd(col,2)
    /// spadd(col,3)
    /// 
    /// dospiz int item col
    ///     sprint(item).
    ///     
    /// </summary>
    /// 

    public enum DospizType
    {
        Infinite = 1,
        While = 2,
        Foreach = 3
    }

    class Dospiz : Node
    {
        
        //Either starting declaration or condition
        public Expression Exp { get; set; }

        public DospizType Type { get; set; }

        public TokenType ItemType { get; set; }
        public string ItemName { get; set; }
        public string Collection { get; set; }




        private bool HasOp(List<Token> list)
        {
            foreach (var item in list)
                if (item.IsOp())
                    return true;               
            return false;
        }

        public Dospiz(List<Token> e)
        {
            this.SetNewParent = true;
            if (e == null || e.Count == 0) // Type 1
            {
                Exp = null;
                Type = DospizType.Infinite;
            }
            else if (HasOp(e)) //Type 2
            {
                Exp = new Expression(e);
                Type = DospizType.While;
            }
            else if(e.Count == 3) //Type 3
            {
                ItemType = e[0].Type;
                if(!(ItemType == TokenType.Int || ItemType == TokenType.Str))
                    throw new Exception(" your dospiz variable has to be type int or string ");
                ItemName = e[1].Value;
                Collection = e[2].Value;
                Type = DospizType.Foreach;
            }
            else
            {
                throw new Exception(" incorrect dospiz type,  ");
            }
        }
        public override string ToTreeString(int indent)
        {
            switch(Type)
            {
                case DospizType.Infinite: return "Infinite spiz...";
                case DospizType.While: return "Dospiz " + Token.ToString(Exp.Exp);
                case DospizType.Foreach: return "Dospiz " + ItemType.ToString() + " " + ItemName + " " + Collection;
            }
            return "";
        }

    }
}
