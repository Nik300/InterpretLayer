using Nik300.InterpretLayer.Runtime.Interop;
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
        Reference FuncReference { get; }

        public FunctionDef(string name, Function function, out Reference reference, Modifier[] modifiers = null)
        {
            Fname = name;
            Func = function;
            Modifiers = modifiers ?? Array.Empty<Modifier>();
            reference = FuncReference = new();
        }
        public FunctionDef(string name, Function function, Modifier[] modifiers = null)
        {
            Fname = name;
            Func = function;
            Modifiers = modifiers ?? Array.Empty<Modifier>();
            FuncReference = new();
        }

        protected override Context ExecuteStatement(Context currentContext, Document document)
        {
            Variable v = new()
            {
                Type = Primitives.Method.Instance,
                Modifiers = Modifiers,
                Value = new()
                {
                    Type = Primitives.Method.Instance,
                    Object = Func
                }
            };
            FuncReference.Ref = v;
            return currentContext.AddVariable(Fname, v);
        }


    }
}
