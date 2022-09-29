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
        public abstract Type Parent { get; }
        public Context DefinitionContext { get; internal set; }
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
    }
}
