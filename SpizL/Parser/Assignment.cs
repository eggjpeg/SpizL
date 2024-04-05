using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spizL
{
    class Assignment:Node
    {
        public string VarName { get; set; }
        public Expression RightExpression { get; set; }
        public Expression LeftExpression { get; set; }
        public Expression LeftIndexExpression { get; set; }

        public bool IsListIndexAssignment()
        {
            return LeftIndexExpression != null;
        }

        public Expression Exp { get; set; }

        public Assignment(List<Token> exp)
        {
            Exp = new Expression(exp);
            if (!(Exp.ExpTree.Token.Type == TokenType.Equal))
                throw new Exception("Not Assignment !");

            RightExpression = new Expression(Exp.ExpTree.ChildList[1]);
            LeftExpression = new Expression(Exp.ExpTree.ChildList[0]);

            if (LeftExpression.ExpTree.IsListIndex)
            {
                VarName = LeftExpression.ExpTree.ListName;
                LeftIndexExpression = new Expression(LeftExpression.ExpTree.ChildList[0]);
            }
            else
                VarName = LeftExpression.ExpTree.Token.Value;
            
        }

        public override string ToTreeString(int indent)
        {
            return "Assignment " + VarName + Environment.NewLine + RightExpression.ToTreeString(indent);
        }
    }
}
