using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nik300.InterpretLayer.Runtime.Types;
using Nik300.InterpretLayer.Types.Builders;

namespace Nik300.InterpretLayer.Types.Runtime
{
    public sealed class TypeLayout
    {
        public static TypeLayoutBuilder Builder { get => new(); }

        internal Function SCTOR { get; set; }
        internal Function OCTOR { get; set; }
        internal Primitives.TypeName Type { get; set; }
        public string Name { get; set; }
        internal Context DefinitionContext { get; set; }

        internal Primitives.TypeName StaticConstructor(Document document)
        {
            Type = new(document, Name, DefinitionContext, OCTOR);
            SCTOR.Run(Type.TypeContext, document);
            return Type;
        }

        internal TypeLayout() { }
    }
}
