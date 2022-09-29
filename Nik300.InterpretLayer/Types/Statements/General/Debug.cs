using Nik300.InterpretLayer.Types.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nik300.InterpretLayer.Types.Statements.General
{
    public class Debug: Statement
    {
        public override string Name => "Debug";
        public override string[] AllowedContexts => null;

        protected override Context ExecuteStatement(Context currentContext, Document document)
        {
            Console.WriteLine("DEBUG STATEMENT");
            return currentContext;
        }
    }
}
