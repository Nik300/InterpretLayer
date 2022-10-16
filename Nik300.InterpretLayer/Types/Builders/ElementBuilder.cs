using runtime = Nik300.InterpretLayer.Types.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nik300.InterpretLayer.Types.Runtime;
using Nik300.InterpretLayer.Runtime.Interop;

namespace Nik300.InterpretLayer.Types.Builders
{
    sealed class ElementReference : Reference
    {
        internal Element ERef;

        internal override bool Modifiable => false;
        public override object Object => ERef.Object;
        public override Element Value { get => ERef; set { } }
        public override runtime.Type Type => ERef.Type;

        internal ElementReference(Element eRef)
        {
            ERef = eRef;
        }
    }

    public sealed class ElementBuilder
    {
        public object Object { get; private set; } = null;
        public runtime.Type Type { get; private set; } = null;

        internal ElementBuilder() { }

        public ElementBuilder UseObject(object obj)
        {
            if (Object != null) return this;
            Object = obj;
            return this;
        }
        public ElementBuilder UseType(runtime.Type type)
        {
            if (Type != null) return this;
            Type = type;
            return this;
        }
        public Element Build() => new() { Object = Object, Type = Type };
        public Reference BuildRef() => new ElementReference(Build());
    }
}
