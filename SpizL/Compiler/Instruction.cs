using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spizL
{

    public enum InstructionType
    {
        Arith_Add,
        Arith_Sub,
        Arith_Mul,
        Arith_Div,
        Arith_Mod,
        Stack_Push_ConstInt32,
        Stack_Pop_Into_Local,
        Ret
    }

    public class Instruction
    {
        public InstructionType Type { get; set; }
        public string Value { get; set; }


        public Instruction()
        {
        }
        public Instruction(InstructionType type)
        {
            this.Type = type;
        }
        public Instruction(InstructionType type, string value)
        {
            this.Type = type;
            this.Value = value;
        }

        public string ToMSIL()
        {
            switch (Type)
            {
                case InstructionType.Arith_Add: return "add";
                case InstructionType.Arith_Mul: return "mul";
                case InstructionType.Arith_Sub: return "sub";
                case InstructionType.Arith_Div: return "div";
                case InstructionType.Ret: return "ret";
                case InstructionType.Stack_Push_ConstInt32: return "ldc.i4 " + Value;
                case InstructionType.Stack_Pop_Into_Local: return "stloc " + Value;
            }
            return string.Empty;
        }
    }
}
