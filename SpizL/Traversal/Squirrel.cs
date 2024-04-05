using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spizL
{
    class Squirrel
    {
        private State State = new State();
        private string funcName = "spiz";
        private AST ast;
        List<object> argList = new List<object>();

        public string GetTrace()
        {
            return State.Trace.ToString();
        }

        public Squirrel(AST ast)
        {
            this.ast = ast;
        }

        private void AddArgsToState(FunctionDef fd)
        {
            for (int i = 0; i < fd.Params.Count; i++)
            {
                //suspicious copying - needs testing
                VarState vs = new VarState(fd.Params[i].Name, fd.Params[i].Type, argList[i]);
                State.Add(fd.Params[i].Name, vs);
            }
            
        }
        //Used by functions
        public Squirrel(AST ast, List<object> argList, string funcName)
        {
            this.ast = ast;
            this.funcName = funcName;
            this.argList = argList;
        }
        private void ValidateArgs(FunctionDef fd)
        {
            if (fd.Params.Count > argList.Count)
                throw new Exception(" DOESNT HAVE ENOUGH ARGUMENTS ");
            if (fd.Params.Count < argList.Count)
                throw new Exception(" HAS TOO MANY ARGUMENTS");
        }
        private Node FindFunc(string name)
        {
            foreach (var item in ast.Children)
                if(item is FunctionDef)
                    if ((item as FunctionDef).Name == name)
                        return item;

            if (name == "spiz")
                throw new Exception("couldnt find function : " + name  );
            else
                throw new Exception("couldnt find function : " + name );
        }

        public object Traverse()
        {
            Node spizFunc = FindFunc(funcName);
            return Traverse(spizFunc);
        }

        private bool swordOfKhali = false;
        private int spizoutLevel = 0;
        
        private object Traverse(Node node)
        {
            bool conditionCompleted = false;
            object ret = null;

            while (true)
            {
                if (swordOfKhali)
                    return ret;

                if (node is FunctionDef)
                {
                    FunctionDef fd = (node as FunctionDef);
                    ValidateArgs(fd);
                    AddArgsToState(fd);

                    if (node.Children.Count == 0)
                        throw new Exception("do spiz is empty");

                    node = node.Children[0];
                    continue;
                }
                else if (node is spizdun)
                {
                    if ((node as spizdun).Exp == null)
                        return null;

                    ret = (node as spizdun).Exp.Eval(ast, State);
                    swordOfKhali = true;
                    return ret;
                }
                else if (node is spizout)
                {
                    if ((node as spizout).Exp == null)
                        spizoutLevel = 1;
                    else
                        spizoutLevel = Convert.ToInt32((node as spizout).Exp.Eval(ast, State));
                }
                else if (node is FunctionCall)
                {
                    (node as FunctionCall).Exp.Eval(ast, State);
                }
                else if (node is Declaration)
                {
                    Declaration d = (node as Declaration);

                    object r = null;
                    if (d.Exp != null)
                        r = d.Exp.Eval(ast, State);

                    VarState vs = new VarState(d.VarName, d.Type, r);
                    State.Add(vs.Name, vs);
                }
                else if (node is Assignment)
                {
                    Assignment a = (node as Assignment);
                    object r = a.RightExpression.Eval(ast, State);
                    if (a.IsListIndexAssignment())
                    {

                        object leftIndex = a.LeftIndexExpression.Eval(ast, State);
                        int i = int.Parse(leftIndex.ToString());
                        var list = (List<object>)State[a.VarName].Value;
                        list[i] = r;
                    }
                    else
                        State[a.VarName].Value = r;

                }
                else if (node is Spif)
                {
                    Spif spif = (node as Spif);
                    object r = spif.Exp.Eval(ast, State);
                    if (!(r is bool))
                        throw new Exception(" spif must evaluate to bool ");
                    bool rb = (bool)r;
                    if (rb)
                    {
                        if (node.Children.Count == 0)
                            throw new Exception("empty if statement");
                        node = node.Children[0];
                        conditionCompleted = true;
                        continue;
                    }
                    else
                        conditionCompleted = false;
                }
                else if (node is Spelzif)
                {
                    Spelzif spelzIf = (node as Spelzif);
                    var prevSib = GetPrevChild(node);
                    if (!(prevSib is Spif || prevSib is Spelzif))
                        throw new Exception(" cant have else if without previous spif or spelzif ");
                    if (!(conditionCompleted))
                    {
                        object r = spelzIf.Exp.Eval(ast, State);
                        if (!(r is bool))
                            throw new Exception(" spelzif must evaluate to bool ");
                        bool rb = (bool)r;
                        if (rb)
                        {
                            if (node.Children.Count == 0)
                                throw new Exception("empty spelzif ");
                            node = node.Children[0];
                            conditionCompleted = true;
                            continue;
                        }
                    }
                }
                else if (node is Spelz)
                {
                    var prevSib = GetPrevChild(node);
                    if (!(prevSib is Spif || prevSib is Spelzif))
                        throw new Exception(" cant have spelz without previous spif or elseif ");
                    if (!(conditionCompleted))
                    {
                        if (node.Children.Count == 0)
                            throw new Exception("empty spelz");
                        node = node.Children[0];
                        continue;
                    }
                }
                else if (node is Dospiz)
                {
                    Dospiz loop = (node as Dospiz);

                    bool r = true;

                    if (loop.Type == DospizType.While)
                        r = (bool)loop.Exp.Eval(ast, State);
                    else if (loop.Type == DospizType.Foreach)
                        r = Foreach(loop, r);

                    conditionCompleted = false;

                    if (r)
                    {
                        if (node.Children.Count == 0)
                            throw new Exception("empty dospiz");
                        node = node.Children[0];
                        continue;
                    }
                }

                //Regular execution
                Node next = GetNextChild(node);
                if (next != null && !swordOfKhali)
                    node = next;
                else
                    return null;
            }
        }

        private bool Foreach(Dospiz loop, bool r)
        {
            VarState index;
            VarState item;
            //See if your index and variable name are in state
            if (!State.ContainsKey(loop.ItemName))
            {
                index = new VarState(loop.ItemName + "_i", TokenType.Int, 0);

                if (!State.ContainsKey(loop.Collection))
                    throw new Exception(" must have collection '" + loop.Collection + "' defined ");

                if (State[loop.Collection].Type != TokenType.Lint && State[loop.Collection].Type != TokenType.Lstr)
                    throw new Exception("'" + loop.Collection + "' must be a collection "); 

                List<object> list = (List<object>)State[loop.Collection].Value;
                item = new VarState(loop.ItemName, loop.ItemType, list[0]);
                State.Add(index.Name, index);
                State.Add(item.Name, item);
            }
            else
            {
                //Grab the index and item from the state
                item = State[loop.ItemName];
                index = State[loop.ItemName + "_i"];
                index.Value = (int)index.Value + 1;
                List<object> list = (List<object>)State[loop.Collection].Value;

                if (list.Count == (int)index.Value)
                    r = false;
                else
                    item.Value = list[(int)index.Value];
            }

            return r;
        }
        private Node GetspizoutNext(Node n)
        {
            int count = 0;
            while(true)
            {
                if (n is Dospiz)
                    count++;
                if (count == spizoutLevel || n.Parent == null)
                    break;              
                n = n.Parent;
            }
            spizoutLevel = 0;
            return GetNextChild(n);//its re-evaluating the dudes so spizout doesnt do anything
        }

        private Node GetNextChild(Node n)
        {
            if (n == null || n.Parent == null || n.Parent is AST)
                return null;

            if (spizoutLevel > 0)
                return GetspizoutNext(n);


            for (int i = 0; i < n.Parent.Children.Count - 1; i++)
                if(n.Id == n.Parent.Children[i].Id)
                    return n.Parent.Children[i + 1];

            //If you are here you are looking at the last guy in that branch
            if (n.Parent is Dospiz)
                return n.Parent;
            else
                return GetNextChild(n.Parent);
        }

        private Node GetPrevChild(Node n)
        {
            for (int i = 1; i < n.Parent.Children.Count; i++)
                if (n.Id == n.Parent.Children[i].Id)
                    return n.Parent.Children[i - 1];
            return null;
        }

    }
}
