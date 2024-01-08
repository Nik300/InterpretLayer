using Nik300.InterpretLayer.Runtime.Types;

namespace Nik300.InterpretLayer.Types.Runtime
{
    public abstract class Type
    {
        public abstract string Name { get; }
        public virtual Type Parent { get; } = null;
        public virtual Context DefinitionContext { get; }
        public string FullName => $"{DefinitionContext.Name}.{Name}";
        public virtual bool IsPrimitive => true;

        public abstract bool Contains(string childName);

        public abstract bool Scriptable();
        public abstract bool Callable();

        protected Element CtorCast(Element element)
        {
            if (element.Type is not Primitives.TypeName typeName || !element.Type.Contains("[string]"))
                return Primitives.Anything.Null;

            Element ctor = typeName.Get(DefinitionContext, element, $"[{Name}]");
            return ctor.Type.Call(
                typeName.DefinitionContext,
                typeName.DefinitionDocument,
                ctor,
                element
            );

        }
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
        public virtual Element New(Context current, Document document, Element[] args = null, (string, Element)[] kwargs = null) => Primitives.Anything.Null;
    }
}
