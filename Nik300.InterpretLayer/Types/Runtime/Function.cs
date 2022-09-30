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
        public static FunctionBuilder Builder { get { return new(); } }

        public IReadOnlyDictionary<string, Variable> Parameters { get; internal set; }
        public Type ReturnType { get; internal set; }
        public Statement[] Statements { get; internal set; }

        internal Element Run(Context context, Document document)
        {
            for (int i = 0; i < Statements.Length; i++)
                context = Statements[i].Execute(context, document);
            return context.Return;
        }
    }
}
