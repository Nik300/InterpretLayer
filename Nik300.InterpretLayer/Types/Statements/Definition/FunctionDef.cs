using Nik300.InterpretLayer.Runtime.Types;
using Nik300.InterpretLayer.Types.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using runtime = Nik300.InterpretLayer.Types.Runtime;

namespace Nik300.InterpretLayer.Types.Statements.Definition
{
    public sealed class FunctionDef : Statement
    {
        public override string Name => "FuncDef";
        public override string[] AllowedContexts => null;

        string Fname { get; }
        Function Func { get; }
        Modifier[] Modifiers { get; }

        public FunctionDef(string name, Function function, Modifier[] modifiers = null)
        {
            Fname = name;
            Func = function;
            Modifiers = modifiers ?? Array.Empty<Modifier>();
        }

        protected override Context ExecuteStatement(Context currentContext, Document document)
        {
            return 
                currentContext.AddVariable(
                    Fname,
                    new() {
                        Type = Primitives.Method.Instance,
                        Modifiers = Modifiers,
                        Value = new() {
                            Type = Primitives.Method.Instance,
                            Object = Func
                        }
                    }
                );
        }


    }
}
