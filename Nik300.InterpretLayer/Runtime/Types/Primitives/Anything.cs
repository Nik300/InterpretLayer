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
        public sealed class Anything : runtime.Type
        {
            public static Anything Instance { get; } = new();
            public static Element Null { get; } = new() { Type = Instance, Object = null };

            public override string Name => "anything";
            internal static Context SystemContext { get; } = new() { Name = "sys" };
            public override Context DefinitionContext => SystemContext;

            public override bool Callable() => false;
            public override Element Cast(Element element)
            {
                return null;
            }
            public override bool Compare(runtime.Type other)
            {
                return other.FullName == FullName;
            }
            public override bool Contains(string childName) => false;
            public override bool Scriptable() => false;
        }
    }
}
