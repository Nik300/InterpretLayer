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
    public sealed class VariableDef : Statement
    {
        public override string Name => "VarDef";
        public override string[] AllowedContexts => null;

        private string VarName { get; }
        private runtime.Type VarType { get; }
        private Element VarElement { get; }
        private Modifier[] VarModifiers { get; }
        private Reference VarReference { get; }

        protected override Context ExecuteStatement(Context currentContext, Document document)
        {
            Variable v = new() { Type = VarType, Value = VarElement, Modifiers = VarModifiers };
            VarReference.Ref = v;
            return currentContext.AddVariable(VarName, v);
        }

        public VariableDef(string name, out Reference reference, runtime.Type type = null, Element element = null, Modifier[] modifiers = null)
        {
            VarName = name;
            VarType = type ?? Primitives.Anything.Instance;
            VarElement = element ?? Primitives.Anything.Null;
            VarModifiers = modifiers ?? Array.Empty<Modifier>();
            reference = VarReference = new();
        }
        public VariableDef(string name, runtime.Type type = null, Element element = null, Modifier[] modifiers = null)
        {
            VarName = name;
            VarType = type ?? Primitives.Anything.Instance;
            VarElement = element ?? Primitives.Anything.Null;
            VarModifiers = modifiers ?? Array.Empty<Modifier>();
            VarReference = new();
        }
    }
}
