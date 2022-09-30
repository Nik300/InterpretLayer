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
        Dictionary<string, Variable> Parameters { get; } = new();
        Statement[] Statements { get; set; } = Array.Empty<Statement>();
        runtime.Type ReturnType { get; set; }

        internal FunctionBuilder() { }

        public FunctionBuilder UseStatement(Statement statement)
        {
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
            if (Parameters.ContainsKey(name)) return this;
            Parameters.Add(name, param);
            return this;
        }
        public FunctionBuilder DiscardParameter(string name)
        {
            if (!Parameters.ContainsKey(name)) return this;
            Parameters.Remove(name);
            return this;
        }

        public Function Build() => new() { Parameters = Parameters, ReturnType = ReturnType, Statements = Statements };
    }
}
