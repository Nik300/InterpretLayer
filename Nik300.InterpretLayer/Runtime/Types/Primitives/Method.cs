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
                return other.FullName == FullName || other.FullName == "sys.anything";
            }
            public override bool Contains(string childName) => false;
            public override bool Scriptable() => false;
            public override Element Call(Context current, Document document, Element element, Element @this = null, Element[] args = null, (string name, Element element)[] kargs = null)
            {
                try
                {
                    if (!Callable()) return null;
                    Function f = (Function)element.Object;
                    Context c = new(current) { Name = "function.context", Variables = new() };
                    if (@this != null) c.AddVariable("this", new Variable { Modifiers = new Modifier[] { Modifier.Readonly }, Type = @this.Type, Value = @this });

                    if (args != null)
                        for (int i = 0; i < args.Length; i++)
                        {
                            var e = args[i];
                            var k = f.Parameters[i].name;
                            var p = f.Parameters[i].variable;

                            c.AddVariable(k, new Variable());
                            c.Variables[k].UseType(p.Type);
                            c.Variables[k].AddModifier(Modifier.Local);
                            c.Variables[k].UpdateValue(e);
                        }

                    if (kargs != null)
                        for (int i = 0; i < kargs.Length; i++)
                        {
                            var v = kargs[i].element;
                            var k = kargs[i].name;
                            var ip = f.FindParamIndex(k);
                            if (ip == -1) throw new Exception($"Unknown parameter {k} on function call");
                            c.AddVariable(k, new Variable());
                            c.Variables[k].UseType(f.Parameters[ip].variable.Type);
                            c.Variables[k].AddModifier(Modifier.Local);
                            c.Variables[k].UpdateValue(v);
                        }

                    for (int i = 0; i < f.Parameters.Length; i++)
                        if (!c.Variables.ContainsKey(f.Parameters[i].name)) c.AddVariable(f.Parameters[i].name, f.Parameters[i].variable.AddModifier(Modifier.Local));

                    return f.Run(c, document);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return Anything.Null;
                }
            }
        }
    }
}
