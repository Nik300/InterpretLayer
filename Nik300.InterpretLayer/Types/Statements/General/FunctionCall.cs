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

        public Context TargetContext { get; }
        public runtime.Type TargetType { get; }
        public string TargetVarName { get; }
        public string TargetContextName { get; }
        public string Function { get; }
        public Reference FuncRef { get; }
        public Reference ThisRef { get; }
        public (string, Element)[] KParams { get; }
        public Element[] Params { get; }

        public FunctionCall(string function, Element[] args = null, (string, Element)[] kparams = null)
        {
            Function = function;
            KParams = kparams ?? Array.Empty<(string, Element)>();
            Params = args;
        }
        public FunctionCall(string contextName, string function, Element[] args = null, (string, Element)[] kparams = null)
        {
            TargetContextName = contextName;
            Function = function;
            KParams = kparams ?? Array.Empty<(string, Element)>();
            Params = args;
        }
        public FunctionCall(Context context, string varName, string function, Element[] args = null, (string, Element)[] kparams = null)
        {
            TargetVarName = varName;
            TargetContext = context;
            Function = function;
            KParams = kparams ?? Array.Empty<(string, Element)>();
            Params = args;
        }
        public FunctionCall(string contextName, string varName, string function, Element[] args = null, (string, Element)[] kparams = null)
        {
            TargetVarName = varName;
            TargetContextName = contextName;
            Function = function;
            KParams = kparams ?? Array.Empty<(string, Element)>();
            Params = args;
        }
        public FunctionCall(Reference funcRef, Reference obj = null, Element[] args = null, (string, Element)[] kparams = null)
        {
            FuncRef = funcRef;
            ThisRef = obj;
            KParams = kparams ?? Array.Empty<(string, Element)>();
            Params = args;
        }

        protected override Context ExecuteStatement(Context currentContext, Document document)
        {
            if (TargetVarName != null && TargetContext != null)
            {
                var v = document.GetVariable(TargetContext.FullName, TargetVarName, currentContext);
                if (v is not null)
                {
                    var f = v.Type.Get(currentContext, v.Value, Function);
                    currentContext.Return = f.Type.Call(currentContext, document, f, v.Value, Params, KParams);
                }
            }
            else if (TargetVarName != null && TargetContextName != null)
            {
                var v = document.GetVariable(TargetContextName, TargetVarName, currentContext);
                if (v is not null)
                {
                    var f = v.Type.Get(currentContext, v.Value, Function);
                    currentContext.Return = f.Type.Call(currentContext, document, f, v.Value, Params, KParams);
                }
            }
            else if (TargetVarName != null)
            {
                var v = document.GetVariable(currentContext.FullName, TargetVarName, currentContext);
                if (v is not null)
                {
                    var f = v.Type.Get(currentContext, v.Value, Function);
                    currentContext.Return = f.Type.Call(currentContext, document, f, v.Value, Params, KParams);
                }
            }
            else if (TargetContext != null)
            {
                var f = document.GetVariable(TargetContext.FullName, Function, currentContext);
                currentContext.Return = f.Type.Call(currentContext, document, f.Value, null, Params, KParams);
            }
            else if (TargetContextName != null)
            {
                var f = document.GetVariable(TargetContextName, Function, currentContext);
                currentContext.Return = f.Type.Call(currentContext, document, f.Value, null, Params, KParams);
            }
            else if (FuncRef != null)
            {
                currentContext.Return = FuncRef.Type.Call(currentContext, document, FuncRef.Value, ThisRef?.Value, Params, KParams);
            }
            else
            {
                var f = document.GetVariable(currentContext.FullName, Function, currentContext);
                currentContext.Return = f.Type.Call(currentContext, document, f.Value, null, Params, KParams);
            }
            return currentContext;
        }
    }
}
