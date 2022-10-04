using Nik300.InterpretLayer.Runtime.Interop;
using Nik300.InterpretLayer.Runtime.Types;
using Nik300.InterpretLayer.Types.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nik300.InterpretLayer.Types.Statements.Manipulation
{
    public sealed class VariableEqu : Statement
    {
        public override string Name => "VarEqu";
        public override string[] AllowedContexts => null;

        Reference VarRef { get; }
        Element NewVal { get; }
        Reference RefVal { get; }

        public VariableEqu(Reference varRef, Element newVal = null)
        {
            VarRef = varRef;
            NewVal = newVal ?? Primitives.Anything.Null;
        }
        public VariableEqu(Reference varRef, Reference otherVar = null)
        {
            VarRef = varRef;
            RefVal = otherVar;
        }

        protected override Context ExecuteStatement(Context currentContext, Document document)
        {
            if (!VarRef.Set) throw new Exception("Rvalue of assignment must be a modifiable value");
            if (NewVal == null)
                VarRef.Value = RefVal.Value;
            else
                VarRef.Value = NewVal;
            return currentContext;
        }
    }
}
