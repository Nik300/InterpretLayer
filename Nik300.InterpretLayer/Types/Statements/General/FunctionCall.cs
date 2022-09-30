using Nik300.InterpretLayer.Types.Runtime;
using runtime = Nik300.InterpretLayer.Types.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public Dictionary<string, Variable> Params { get; }

        public FunctionCall(string function, Dictionary<string, Variable> @params = null)
        {
            Function = function;
            Params = @params ?? new();
        }
        public FunctionCall(string varName, string function, Dictionary<string, Variable> @params = null)
        {
            TargetVarName = varName;
            Function = function;
            Params = @params ?? new();
        }
        public FunctionCall(Context context, string varName, string function, Dictionary<string, Variable> @params = null)
        {
            TargetVarName = varName;
            TargetContext = context;
            Function = function;
            Params = @params ?? new();
        }
        public FunctionCall(string contextName, string varName, string function, Dictionary<string, Variable> @params = null)
        {
            TargetVarName = varName;
            TargetContextName = contextName;
            Function = function;
            Params = @params ?? new();
        }

        protected override Context ExecuteStatement(Context currentContext, Document document)
        {
            if (TargetVarName != null && TargetContext != null)
            {
                var v = document.GetVariable(TargetContext.FullName, TargetVarName, currentContext);
                if (v is not null)
                {
                    var f = v.Type.Get(currentContext, v.Value, Function);
                    currentContext.Return = f.Type.Call(currentContext, document, f, v.Value, Params);
                }
            }
            else if (TargetVarName != null && TargetContextName != null)
            {
                var v = document.GetVariable(TargetContextName, TargetVarName, currentContext);
                if (v is not null)
                {
                    var f = v.Type.Get(currentContext, v.Value, Function);
                    currentContext.Return = f.Type.Call(currentContext, document, f, v.Value, Params);
                }
            }
            else if (TargetVarName != null)
            {
                var v = document.GetVariable(currentContext.FullName, TargetVarName, currentContext);
                if (v is not null)
                {
                    var f = v.Type.Get(currentContext, v.Value, Function);
                    currentContext.Return = f.Type.Call(currentContext, document, f, v.Value, Params);
                }
            }
            else if (TargetContext != null)
            {
                var f = document.GetVariable(TargetContext.FullName, Function, currentContext);
                currentContext.Return = f.Type.Call(currentContext, document, f.Value, null, Params);
            }
            else if (TargetContextName != null)
            {
                var f = document.GetVariable(TargetContextName, Function, currentContext);
                currentContext.Return = f.Type.Call(currentContext, document, f.Value, null, Params);
            }
            else
            {
                var f = document.GetVariable(currentContext.FullName, Function, currentContext);
                currentContext.Return = f.Type.Call(currentContext, document, f.Value, null, Params);
            }
            return currentContext;
        }
    }
}
