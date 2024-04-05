using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spizL
{

    class Function
    {

        private string MapTypeToMSIL(TokenType type)
        {
            switch(type)
            {
                case TokenType.Void: return "void";
                case TokenType.Int: return "int32";
                case TokenType.Str: return "string";
                //TODO: Take care of Lint and Lstr
            }

            return "";
        }

        public string Compile(FunctionDef fd)
        {
            string header = CompileHeader(fd);
            List<Instruction> list = new List<Instruction>();
            CompileBody(fd, list);
            list.Add(new Instruction(InstructionType.Ret));
            string body = ToMSIL(list);
            return header +  Environment.NewLine +body + "}";
        }

        private string ToMSIL(List<Instruction> list)
        {
            StringBuilder sb = new StringBuilder();
            foreach (Instruction ins in list)
                sb.AppendLine(ins.ToMSIL());
            return sb.ToString();
        }

        private string CompileHeader(FunctionDef fd)
        {
            StringBuilder sb = new StringBuilder();

            string methodSig = ".method public static [ret_type] [func_name]([params]) cil managed {";
            string entryPoint = ".entrypoint";
            string locals = ".locals init ([decs])";


            methodSig = methodSig.Replace("[ret_type]", MapTypeToMSIL(fd.ReturnType));
            methodSig = methodSig.Replace("[func_name]", fd.Name);

            string prms = "";
            foreach(var param in fd.Params)
                prms += MapTypeToMSIL(param.Type) + " " + param.Name + ",";
            prms = prms.TrimEnd(',');

            methodSig = methodSig.Replace("[params]", prms);

            sb.AppendLine(methodSig);

            if (fd.Name == "spiz")
                sb.AppendLine(entryPoint);

            List<Declaration> decList = new List<Declaration>();
            FindAllDeclarations(fd, decList);

            string decs = "";
            foreach (var dec in decList)
                decs += MapTypeToMSIL(dec.Type) + " " + dec.VarName + ",";
            decs = decs.TrimEnd(',');

            locals = locals.Replace("[decs]", decs);

            sb.AppendLine(locals);

            return sb.ToString();
        }


        private void FindAllDeclarations(Node node, List<Declaration> list)
        {
            if (node is Declaration)
            { 
                list.Add((Declaration)node);
                return;
            }

            if(node.Children!=null)
                foreach (Node child in node.Children)
                    FindAllDeclarations(child, list);
        }



       

        private void CompileBody(Node node, List<Instruction> list)
        {
            if(node is Declaration)
                CompileDecAss(list, ((Declaration)node).Exp, ((Declaration)node).VarName);
            else if (node is Assignment)
                CompileDecAss(list, ((Assignment)node).Exp, ((Assignment)node).VarName);
            
            if (node.Children != null)
                foreach (Node child in node.Children)
                    CompileBody(child, list);
        }


        private void CompileDecAss(List<Instruction> list, Expression exp, string varName)
        {
            List<Instruction> expList = CompileExp(exp);
            expList.Add(new Instruction(InstructionType.Stack_Pop_Into_Local, varName));
            list.AddRange(expList);
        }


        private List<Instruction> CompileExp(Expression exp)
        {
            List<Instruction> list = new List<Instruction>();
            CompileExp(exp.ExpTree, list);
            return list;
        }


        private void CompileExp(ExpNode expNode, List<Instruction> list)
        {

            if (expNode.ChildList != null)
            {
                foreach (ExpNode child in expNode.ChildList)
                    CompileExp(child, list);
            }

            Instruction i = MapTokenToInstruction(expNode.Token);
            list.Add(i);
        }

        private Instruction MapTokenToInstruction(Token token)
        {
            Instruction ins = new Instruction();
            switch (token.Type)
            {
                case TokenType.Const:
                    ins.Type = InstructionType.Stack_Push_ConstInt32;
                    ins.Value = token.Value;
                    break;
                case TokenType.Minus:
                    ins.Type = InstructionType.Arith_Sub;
                    break;
                case TokenType.Plus:
                    ins.Type = InstructionType.Arith_Add;
                    break;
                case TokenType.Multiply:
                    ins.Type = InstructionType.Arith_Mul;
                    break;
                case TokenType.Divide:
                    ins.Type = InstructionType.Arith_Div;
                    break;
            }
            return ins;

        }

    }
}
