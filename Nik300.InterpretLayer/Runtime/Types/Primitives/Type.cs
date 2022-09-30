using Nik300.InterpretLayer.Types.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using runtime = Nik300.InterpretLayer.Types.Runtime;

namespace Nik300.InterpretLayer.Runtime.Types
{
    public static partial class Primitives
    {
        public sealed class Type : runtime.Type
        {
            internal static Type Instance { get; } = new();

            internal Type() { }

            public override string Name => "typeInfo";
            public override Context DefinitionContext => Anything.SystemContext;

            public override bool Callable() => false;

            public override Element Cast(Element element) => null;

            public override bool Compare(runtime.Type other)
            {
                return other.FullName == FullName || other.FullName == "sys.any";
            }

            public override bool Contains(string childName) => false;

            public override bool Scriptable() => false;
        }
    }
}
