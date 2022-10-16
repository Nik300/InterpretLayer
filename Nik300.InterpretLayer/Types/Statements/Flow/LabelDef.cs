using Nik300.InterpretLayer.Types.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nik300.InterpretLayer.Types.Statements.Flow
{
    public sealed class LabelDef : Statement
    {
        public override string Name => "LabelDef";

        public override string[] AllowedContexts => null;

        internal string LabelName { get; private set; }

        public LabelDef(string labelName)
        {
            LabelName = labelName ?? throw new Exception("label must be named");
        }
        protected override Context ExecuteStatement(Context currentContext, Document document)
        {
            if (document.labels.ContainsKey(LabelName)) return currentContext;
            document.labels.Add(LabelName, new(currentContext.Variables.ToDictionary(x => x.Key, y => y.Value), document.ptr));
            return currentContext;
        }
    }
}
