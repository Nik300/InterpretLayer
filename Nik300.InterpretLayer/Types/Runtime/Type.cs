using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nik300.InterpretLayer.Types.Runtime
{
    public abstract class Type
    {
        public abstract string Name { get; }
        public virtual Type Parent { get; } = null;
        public virtual Context DefinitionContext { get; }
        public string FullName => $"{DefinitionContext.Name}.{Name}";

        public abstract bool Contains(string childName);
        
        public abstract bool Scriptable();
        public abstract bool Callable();

        public abstract Element Cast(Element element);

        internal bool Validate()
        {
            return 
                Name != null && DefinitionContext != null && DefinitionContext.Name != null &&
                Name.Trim() != "" && DefinitionContext.Name.Trim() != "" &&
                !Name.Contains(' ') && !DefinitionContext.Name.Contains(' ');
        }

        public abstract bool Compare(Type other);
        public virtual Element Call(Context current, Document document, Element element, Element @this = null, Element[] args = null, (string name, Element element)[] kargs = null) { return null; }

        public virtual Element Get(Context current, Element @this, string childName) => null;
        public virtual void Set(Context current, Element @this, string childName, Element value) { }
        public virtual void Create(Context current, Element @this, string childName, Variable var) { }
    }
}
