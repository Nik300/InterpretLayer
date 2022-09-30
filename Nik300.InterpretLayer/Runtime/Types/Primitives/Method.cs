using Nik300.InterpretLayer.Types;
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
        public sealed class Method : runtime.Type
        {
            public static Method Instance { get; } = new();

            public override string Name => "method";
            public override Context DefinitionContext => Anything.SystemContext;

            public override bool Callable() => true;
            public override Element Cast(Element element)
            {
                return null;
            }
            public override bool Compare(runtime.Type other)
            {
                return other.FullName == FullName || other.FullName == "sys.any";
            }
            public override bool Contains(string childName) => false;
            public override bool Scriptable() => false;
            public override Element Call(Context current, Document document, Element element, Element @this = null, Dictionary<string, Variable> args = null)
            {
                if (!Callable()) return null;
                Function f = (Function)element.Object;
                Context c = new() { Name = "function.context" };
                if (@this != null) c.AddVariable("this", new Variable { Modifiers = new Modifier[] { Modifier.Readonly }, Type = @this.Type, Value = @this });

                if (args != null)
                    for (int i = 0; i < args.Count; i++)
                        c.AddVariable(args.Keys.ElementAt(i), args.Values.ElementAt(i));

                return f.Run(c, document);
            }
        }
    }
}
