using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nik300.InterpretLayer.Types.Runtime
{
    public sealed class Context
    {
        public string Name { get; internal set; }
        public Dictionary<string, Variable> Variables { get; private set; }
        public bool Imported { get; internal set; } = false;

        internal Context()
        {
            Variables = new();
        }

        public Context UseName(string name)
        {
            if (Name != null) return this;
            Name = name;
            return this;
        }
        public Context AddVariable(string name, Variable var)
        {
            if (Variables.ContainsKey(name)) return this;
            Variables.Add(name, var);
            return this;
        }
        public Context RemVariable(string name)
        {
            if (!Variables.ContainsKey(name)) return this;
            Variables.Remove(name);
            return this;
        }
        internal Context ToggleImported()
        {
            Imported = !Imported;
            return this;
        }
    }
}
