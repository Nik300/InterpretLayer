using Nik300.InterpretLayer.Runtime.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nik300.InterpretLayer.Types.Runtime
{
    public sealed class Context
    {
        private Context Parent { get; }
        internal string Name { get; set; }
        internal Dictionary<string, Variable> Variables { get; set; }
        public bool Imported { get; internal set; } = false;
        public string FullName
        {
            get
            {
                if (Parent is not null) return $"{Parent.Name}.{Name}";
                else return Name;
            }
        }

        internal Element Return { get; set; } = Primitives.Anything.Null;

        internal Context(Context parent = null)
        {
            Variables = new();
            Parent = parent;
        }

        public Context UseName(string name)
        {
            if (Name != null) return this;
            Name = name;
            return this;
        }
        public Context AddVariable(string name, Variable var)
        {
            if (Variables.ContainsKey(name)) Variables[name] = var;
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
        public Variable GetVariable(string name)
        {
            if (!Variables.ContainsKey(name)) return null;
            return Variables[name];
        }
    }
}
