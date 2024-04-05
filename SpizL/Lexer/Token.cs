using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spizL
{


    
    enum TokenType
    {
        //Var Types
        Int = 0,
        Str = 1,
        Lint = 2,
        Lstr = 3,
        Void = 4,
        Unknown = 5,
        // Commands
        Spif = 100,
        Sprint = 101,
        spizout =102,
        spizdun = 103,
        Spelz = 104,
        Dospiz = 105,
        Spelzif = 106,
        // Op types
        Plus = 200,
        Minus = 201,
        Equal = 202,
        Multiply = 203,
        Divide = 204,
        Mod = 205,
        Morethan = 206,
        Lessthan = 207,
        MoreThanEq = 208,
        LessThanEq = 209,
        NotEq = 210,
        Not = 211,
        Oparen = 212,
        Cparen = 213,
        Comma = 214,
        Obrack = 215,
        Cbrack = 216,
        Squote = 217,
        Colon = 218,
        
        // dudes
        VarName = -1,
        Const = -2,
        Eol = -3,
        Dot = -4,
        Comment = -5
    }
   
    class Token
    {

        public bool IsOp()
        {
            return (int)Type >= 200 && (int)Type < 300;
        }
        public bool IsCommand()
        {
            return (int)Type >= 100 && (int)Type < 200;
        }
        public bool IsType()
        {
            return (int)Type >= 0 && (int)Type < 10;
        }
        public TokenType Type;
        public string Value;

        public Token(TokenType type)
        {
            this.Type = type;

        }
        public Token(TokenType type, string value)
        {
            Value = value;
            Type = type;
        }

        public override string ToString()
        {
            return (Type + " " + Value);
        }

        public static string ToString(List<Token> list)
        {
            StringBuilder sb = new StringBuilder();
            foreach(var token in list)
                sb.Append(token.ToString());
            return sb.ToString();
        }
    }
}