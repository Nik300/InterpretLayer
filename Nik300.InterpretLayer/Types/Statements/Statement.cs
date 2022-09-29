using Nik300.InterpretLayer.Types.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nik300.InterpretLayer.Types.Statements
{
    public abstract class Statement
    {
        public abstract string Name { get; }
        public abstract string[] AllowedContexts { get; }

        protected abstract Context ExecuteStatement(Context currentContext, Document document);
        internal Context Execute(Context currentContext, Document document)
        {
            if (AllowedContexts != null && !AllowedContexts.Contains(currentContext.Name)) return null;
            return ExecuteStatement(currentContext, document);
        }
    }
}
