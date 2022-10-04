using Nik300.InterpretLayer.Types.Runtime;
using runtime = Nik300.InterpretLayer.Types.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nik300.InterpretLayer.Runtime.Interop;

namespace Nik300.InterpretLayer.Types.Statements.General
{
    public sealed class FunctionCall : Statement
    {
        public override string Name => "FuncCall";
        public override string[] AllowedContexts => null;

        string FunctionName { get; }
        Reference FunctionReference { get; }
        string ContextName { get; }
        Reference ThisReference { get; }

        Element[] Args { get; }
        (string, Element)[] KWArgs { get; set; }

        public FunctionCall(string function, Reference obj = null, Element[] args = null, (string, Element)[] kwargs = null)
        {
            FunctionName = function;
            Args = args;
            KWArgs = kwargs;
            ThisReference = obj;
        }
        public FunctionCall(string contextName, string function, Reference obj = null, Element[] args = null, (string, Element)[] kwargs = null)
        {
            ContextName = contextName;
            FunctionName = function;
            ThisReference = obj;
            Args = args;
            KWArgs = kwargs;
        }
        public FunctionCall(Reference function, Reference obj = null, Element[] args = null, (string, Element)[] kwargs = null)
        {
            FunctionReference = function;
            ThisReference = obj;
            Args = args;
            KWArgs = kwargs;
        }

        protected override Context ExecuteStatement(Context currentContext, Document document)
        {
            Element f;

            if (ContextName != null && FunctionName != null) f = document.GetVariable(ContextName, FunctionName, currentContext).Value;
            else if (FunctionReference != null) f = FunctionReference.Value;
            else if (ThisReference != null && FunctionName != null) f = ThisReference.Type.Get(currentContext, ThisReference.Value, FunctionName);
            else if (FunctionName != null) f = currentContext.GetVariable(FunctionName).Value;
            else
            {
                throw new Exception("Function name was not given");
            }

            f.Type.Call(currentContext, document, f, ThisReference?.Value, Args, KWArgs);
            return currentContext;
        }
    }
}
