using runtime = Nik300.InterpretLayer.Types.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nik300.InterpretLayer.Types.Runtime;
using Nik300.InterpretLayer.Types.Builders;

namespace Nik300.InterpretLayer.Runtime.Types
{
    public static partial class Primitives
    {
        public sealed class String : runtime.Type
        {
            public static String Instance { get; } = new();

            public override string Name => "string";
            public override Context DefinitionContext => Anything.SystemContext;

            private static Element PrimitiveToString(Element element)
            {
                return Element.Builder
                    .UseType(Instance)
                    .UseObject(element.Object.ToString())
                    .Build();
            }

            public override bool Callable() => false;
            public override Element Cast(Element element)
            {
                return element.Type.IsPrimitive ? PrimitiveToString(element) : CtorCast(element);
            }
            public override bool Compare(runtime.Type other)
            {
                return other.FullName == FullName || other.FullName == "sys.anything";
            }
            public override bool Contains(string childName) => false;
            public override bool Scriptable() => false;
        }
    }
}
