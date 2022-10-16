using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nik300.InterpretLayer.Types.Runtime
{
    internal sealed class Label
    {
        public readonly Dictionary<string, Variable> ContextVars;
        public readonly int Pointer;

        public Label(Dictionary<string, Variable> contextVars, int pointer)
        {
            ContextVars = contextVars ?? new();
            Pointer = pointer;
        }

        public Context Fallback(Context context, Document doc)
        {
            doc.ptr = Pointer;
            foreach (var VarPair in context.Variables)
                if (!ContextVars.ContainsKey(VarPair.Key)) context.RemVariable(VarPair.Key);
            foreach (var VarPair in ContextVars)
                if (!context.Variables.ContainsKey(VarPair.Key)) context.AddVariable(VarPair.Key, VarPair.Value);
            return context;
        }
    }
}
