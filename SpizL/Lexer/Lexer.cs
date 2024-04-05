using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace spizL
{
    class Lexer
    {
        public char[] OpList = { '*', '-', '+', '/', '%', '=', '>', '<','!','(',')','.',',','[',']',':','#'};
        public void PrintTokens(List<Token> list)
        {
            foreach (var item in list)
            {
                Console.WriteLine(item.ToString());
            }

        }

        
        
        public List<Token> PostProcessOps(List<Token> list)
        {
            var nlist = new List<Token>();

            for (int i = 0; i < list.Count; i++)
            {
                bool isDefault = false;
                if (list.Count-1>i)
                {
                    if (list[i].IsOp() && list[i + 1].Type == TokenType.Equal)
                    {
                        switch (list[i].Type)
                        {
                            case TokenType.Lessthan: nlist.Add(new Token(TokenType.LessThanEq)); break;
                            case TokenType.Morethan: nlist.Add(new Token(TokenType.MoreThanEq)); break;
                            case TokenType.Not: nlist.Add(new Token(TokenType.NotEq)); break;
                            default: isDefault = true; break;
                        }

                        if (!isDefault)
                        {
                            i++;
                            continue;
                        }
                    }

                }
                nlist.Add(list[i]);
            }


            return nlist;
        }

        //a 's f f' s
        public static List<string> Split(string line, char seperator)
        {
            List<string> result = new List<string>();
            bool isInString = false;

            StringBuilder sb = new StringBuilder();
            foreach (char c in line)
            {
                if (c == '\'')
                {
                    isInString = !isInString;
                    if (!isInString)
                    {
                        result.Add("'" + sb.ToString() + "'");
                        sb.Clear();
                    }
                }
                else
                {
                    if(c == seperator && !isInString)
                    {
                        if (sb.Length > 0)
                        {
                            result.Add(sb.ToString());
                            sb.Clear();
                        }
                    }
                    else
                        sb.Append(c);
                }
            }

            if(sb.Length > 0)
                result.Add(sb.ToString());

            return result;

        }

        public List<Token> Tokenize(string spizfile)
        {
            var list = new List<Token>();
            using (StreamReader sr = new StreamReader(spizfile))
            {
                while(!sr.EndOfStream)
                {
                    string line = sr.ReadLine();

                    line = SpaceOut(line);
                    List<string> linear = Split(line,' ');

                    foreach (var item in linear)
                    {
                        if (item.Trim() == "")
                            continue;

                        var itml = item;
                        if(!item.StartsWith("'"))
                            itml = item.ToLower().Trim();

                        var token = Classify(itml);
                        if (token.Type == TokenType.Comment)
                            break;
                        list.Add(token);
                    }
                    list.Add(new Token(TokenType.Eol));
                }
            }
            return PostProcessOps(list);
        }

        private bool IsOp(char c)
        {
            foreach(var op in OpList)
                if (op == c)
                    return true;
            return false;
        }

        //TODO: Fix when dealing with quotes suspicious
        private string SpaceOut(string line)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < line.Length; i++)
            { 
                if(IsOp(line[i]))
                    sb.Append(' ' + line[i].ToString() + ' ');
                else
                    sb.Append(line[i].ToString());
            }
            return sb.ToString();
        }

        private string RemoveQuotes(string item)
        {
            return item.Replace("'", "");
        }

        private Token Classify(string item)
        {
            //Unspezify. KLUDGE
            if(item.StartsWith("'"))
                return new Token(TokenType.Const, RemoveQuotes(item));
            
            switch (item)
            {
                case "spif": return new Token(TokenType.Spif);
                case "spelz": return new Token(TokenType.Spelz);
                case "spelzif": return new Token(TokenType.Spelzif);
                case "spizout": return new Token(TokenType.spizout);
                case "spizdun": return new Token(TokenType.spizdun);
                case "dospiz": return new Token(TokenType.Dospiz);
                case "sprint": return new Token(TokenType.Sprint);
                case "/": return new Token(TokenType.Divide);
                case "*": return new Token(TokenType.Multiply);
                case "+": return new Token(TokenType.Plus);
                case "-": return new Token(TokenType.Minus);
                case "!": return new Token(TokenType.Not);
                case "=": return new Token(TokenType.Equal);
                case "%": return new Token(TokenType.Mod);
                case ">": return new Token(TokenType.Morethan);
                case "<": return new Token(TokenType.Lessthan);
                case "int": return new Token(TokenType.Int);
                case "str": return new Token(TokenType.Str);
                case "lint": return new Token(TokenType.Lint);
                case "lstr": return new Token(TokenType.Lstr);
                case "\n": return new Token(TokenType.Eol);
                case ".": return new Token(TokenType.Dot);
                case "(": return new Token(TokenType.Oparen);
                case ")": return new Token(TokenType.Cparen);
                case "[": return new Token(TokenType.Obrack);
                case "]": return new Token(TokenType.Cbrack);
                case ",": return new Token(TokenType.Comma);
                case "'": return new Token(TokenType.Squote);
                case ":": return new Token(TokenType.Colon);
                case "#": return new Token(TokenType.Comment);

                default: break;
            }

            if (Util.IsNumber(item))
                return new Token(TokenType.Const, item);
            else
                return new Token(TokenType.VarName, item);
        }
    }
}
