﻿using Nik300.InterpretLayer.Types.Builders;
using Nik300.InterpretLayer.Types.Runtime;
using Nik300.InterpretLayer.Types.Statements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nik300.InterpretLayer.Types
{
    public sealed class Document
    {
        internal string Name { get; private set; }

        internal Dictionary<string, Context> Contexts { get; private set; }
        public Statement[] Statements { get; private set; } = Array.Empty<Statement>();

        internal int ptr = 0;

        public static DocumentBuilder Builder { get; private set; } = new DocumentBuilder();

        internal Document(string name, Statement[] statements)
        {
            Name = name;
            Statements = statements;
            Contexts = new();
            Contexts.Add(Name, new());
            Contexts[Name].UseName(Name);
        }

        public Variable GetVariable(string context, string varname, Context current)
        {
            if (
                !Contexts.ContainsKey(context) || !Contexts[context].Variables.ContainsKey(varname) ||
                (current.Name != Contexts[context].Name && Contexts[context].Variables[varname].Modifiers.Contains(Modifier.Local)) ||
                (current.Imported && !current.Variables[varname].Modifiers.Contains(Modifier.Export))
                ) return null;
            return current.Variables[varname];
        }
        public Context GetRoot()
        {
            return Contexts[Name];
        }

        public Context RunNext(Context current) => Statements[ptr++].Execute(current, this);
    }
}
