using Nik300.InterpretLayer.Types.Runtime;
using Nik300.InterpretLayer.Types.Statements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using runtime = Nik300.InterpretLayer.Types.Runtime;

namespace Nik300.InterpretLayer.Types.Builders
{
    public sealed class FunctionBuilder
    {
        (string name, Variable variable)[] Parameters { get; set; } = Array.Empty<(string, Variable)>();
        Statement[] Statements { get; set; } = Array.Empty<Statement>();
        runtime.Type ReturnType { get; set; }
        Function.BuiltInFunction BuiltIn { get; set; }

        internal FunctionBuilder() { }

        public FunctionBuilder UseStatement(Statement statement)
        {
            if (BuiltIn != null) return this;
            Statement[] temp = new Statement[Statements.Length + 1];
            for (int i = 0; i < Statements.Length; i++)
            {
                temp[i] = Statements[i];
            }
            temp[^1] = statement;
            Statements = temp;
            return this;
        }
        public FunctionBuilder UseParameter(string name, Variable param)
        {
            (string name, Variable variable)[] temp = new (string name, Variable variable)[Parameters.Length + 1];
            for (int i = 0; i < Parameters.Length; i++)
            {
                if (Parameters[i].name == name) return this;
                temp[i] = Parameters[i];
            }
            Parameters = temp;
            Parameters[^1] = (name, param);
            return this;
        }
        public FunctionBuilder DiscardParameter(string name)
        {
            (string name, Variable variable)[] temp = new (string name, Variable variable)[Parameters.Length - 1];
            for (int i = 0, s = 0; i < Parameters.Length && s < temp.Length; i++, s++)
            {
                if (Parameters[i].name == name) s--;
                else temp[i] = Parameters[i];
            }
            Parameters = temp;
            return this;
        }
        public FunctionBuilder UseCallback(Function.BuiltInFunction callback)
        {
            BuiltIn = callback;
            return this;
        }

        public Function Build() => new() { Parameters = Parameters, ReturnType = ReturnType, Statements = Statements, BuiltIn = BuiltIn };
    }
}
