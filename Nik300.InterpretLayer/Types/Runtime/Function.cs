using Nik300.InterpretLayer.Types.Builders;
using Nik300.InterpretLayer.Types.Statements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nik300.InterpretLayer.Types.Runtime
{
    public sealed class Function
    {
        public delegate Element BuiltInFunction(Context context, Document document);

        public static FunctionBuilder Builder { get { return new(); } }

        public (string name, Variable variable)[] Parameters { get; internal set; }
        public Type ReturnType { get; internal set; }
        public Statement[] Statements { get; internal set; }
        public BuiltInFunction BuiltIn { get; internal set; }

        internal Element Run(Context context, Document document)
        {
            if (BuiltIn is not null) return BuiltIn(context, document);
            for (int i = 0; i < Statements.Length; i++)
                context = Statements[i].Execute(context, document);
            return context.Return;
        }
        internal int FindParamIndex(string name)
        {
            for (int i = 0; i < Parameters.Length; i++) if (name == Parameters[i].name) return i;
            return -1;
        }
    }
}
