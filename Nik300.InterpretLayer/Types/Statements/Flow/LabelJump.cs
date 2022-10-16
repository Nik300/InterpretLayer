using Nik300.InterpretLayer.Types.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nik300.InterpretLayer.Types.Statements.Flow
{
    public sealed class LabelJump : Statement
    {
        public override string Name => "LabelJump";

        public override string[] AllowedContexts => null;

        internal string Label { get; private set; }

        public LabelJump(string label)
        {
            Label = label ?? throw new Exception("Missing label name");
        }

        protected override Context ExecuteStatement(Context currentContext, Document document)
        {
            if (!document.labels.ContainsKey(Label)) throw new Exception($"Label '{Label}' doesn't exist");
            return document.labels[Label].Fallback(currentContext, document);
        }
    }
}
