using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spizL
{

    class FunctionParam
    {
        public TokenType Type { get; }
        public string Name { get;}

        public FunctionParam(TokenType type, string name)
        {
            this.Name = name;
            this.Type = type;
        }

        public override string ToString()
        {
            return Type.ToString() + " " + Name;
        }

    }


    class FunctionDef : Node
    {
        public string Name { get;}
        public List<FunctionParam> Params { get;}
        public TokenType ReturnType { get; }




        public FunctionDef(List<Token> expList)
        {
            this.SetNewParent = true;
            this.Name = expList[0].Value;
            this.Params = new List<FunctionParam>();

            //myFun int a, int b : int
            for (int i = 1; i < expList.Count; i++)
            {
                Token thisGuy = expList[i];
                Token nextGuy = expList.Count > i + 1 ? expList[i + 1]: null;

                //farm parens
                if (thisGuy.Type == TokenType.Oparen || thisGuy.Type == TokenType.Cparen || thisGuy.Type == TokenType.Comma)
                    continue;
                //If you hit a colon, then check for return type
                if (thisGuy.Type == TokenType.Colon)
                {
                    //Is there a return type?
                    if(nextGuy != null)
                        this.ReturnType = nextGuy.Type;
                    else
                        this.ReturnType = TokenType.Void;
                    break;
                }

                string name = nextGuy.Value;
                //here we expect a var type and a name 
                FunctionParam p = new FunctionParam(thisGuy.Type, name);
                Params.Add(p);
                i++;
            }



        }
        public override string ToTreeString(int indent)
        {
            string s =  "Func Def " + Name + "(";
            foreach (FunctionParam p in Params)
                s += p.ToString() + ",";
            s = s.Trim(',') + ')'; //MIND EXPLOSION
            s += ":" + ReturnType.ToString("G");
            return s;
        }
    }
}
