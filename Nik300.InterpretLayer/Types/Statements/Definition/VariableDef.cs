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
        private object VarElement { get; }
        private Modifier[] VarModifiers { get; }

        protected override Context ExecuteStatement(Context currentContext, Document document)
        {
            return currentContext.AddVariable(VarName, new() { Type = VarType, Value = Element.Builder.UseType(VarType).UseObject(VarElement).Build(), Modifiers = VarModifiers });
        }

        public VariableDef(string name, runtime.Type type = null, object element = null, Modifier[] modifiers = null)
        {
            VarName = name;
            VarType = type ?? Primitives.Anything.Instance;
            VarElement = element;
            VarModifiers = modifiers;
        }
    }
}
