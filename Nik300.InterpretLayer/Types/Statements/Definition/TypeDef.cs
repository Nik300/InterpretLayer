using Nik300.InterpretLayer.Runtime.Interop;
using Nik300.InterpretLayer.Runtime.Types;
using Nik300.InterpretLayer.Types.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using runtime = Nik300.InterpretLayer.Types.Runtime;

namespace Nik300.InterpretLayer.Types.Statements.Definition
{
    public sealed class TypeDef : Statement
    {
        public override string Name => "TypeDef";

        public override string[] AllowedContexts => null;

        private TypeLayout TypeLayout { get; }
        private Modifier[] TypeModifiers { get; }
        private Reference TypeReference { get; }

        public TypeDef(TypeLayout layout, Reference typeReference, Modifier[] typeModifiers = null)
        {
            TypeModifiers = typeModifiers ?? Array.Empty<Modifier>();
            TypeReference = typeReference;
            TypeLayout = layout;
        }
        public TypeDef(TypeLayout layout, Modifier[] typeModifiers = null)
        {
            TypeModifiers = typeModifiers ?? Array.Empty<Modifier>();
            TypeReference = null;
            TypeLayout = layout;
        }

        protected override Context ExecuteStatement(Context currentContext, Document document)
        {
            TypeLayout.DefinitionContext = currentContext;
            var t = TypeLayout.StaticConstructor(document);
            currentContext.AddVariable(t.Name, Variable.Builder.UseType(Primitives.Type.Instance).UseValue(Element.Builder.UseType(Primitives.Type.Instance).UseObject(t).Build()).UseModifiers(TypeModifiers).Build());
            if (TypeReference is not null)
            {
                TypeReference.Ref = currentContext.Variables[t.Name];
            }
            return currentContext;
        }
    }
}
