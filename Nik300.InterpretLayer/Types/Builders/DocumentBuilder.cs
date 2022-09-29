using Nik300.InterpretLayer.Types.Runtime;
using Nik300.InterpretLayer.Types.Statements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nik300.InterpretLayer.Types.Builders
{
    public sealed class DocumentBuilder
    {
        public string Name { get; private set; }
        public Statement[] Statements { get; private set; } = Array.Empty<Statement>();
        
        internal DocumentBuilder() { }

        public DocumentBuilder UseName(string name)
        {
            if (Name != null) return this;
            Name = name;
            return this;
        }
        public DocumentBuilder UseStatement(Statement statement)
        {
            Statement[] temp = new Statement[Statements.Length + 1];
            for (int i = 0; i < Statements.Length; i++) temp[i] = Statements[i];
            temp[^1] = statement;
            Statements = temp;
            return this;
        }
        public DocumentBuilder DiscardStatement(Statement statement)
        {
            Statement[] temp = new Statement[Statements.Length - 1];
            for (int i = 0, s = 0; i < Statements.Length && s < temp.Length; i++, s++)
            {
                if (Statements[i].Name == statement.Name)
                {
                    s--;
                    continue;
                }
                temp[s] = Statements[i];
            }
            return this;
        }
        public Document Build() => new(Name, Statements);
    }
}
