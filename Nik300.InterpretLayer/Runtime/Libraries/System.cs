using Nik300.InterpretLayer.Runtime.Types;
using Nik300.InterpretLayer.Types.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nik300.InterpretLayer.Runtime.Libraries
{
    internal sealed class SystemLib : Library
    {
        public override string Name => "sys";
        public static SystemLib Instance { get; } = new();

        private SystemLib()
        {
            Lib.AddVariable(
                "[ioprint]",
                new()
                {
                    Type = Primitives.Method.Instance,
                    Value = new()
                    {
                        Type = Primitives.Method.Instance,
                        Object = Function.Builder
                                    .UseParameter(
                                        "string",
                                        Variable.Builder
                                            .UseType(Primitives.String.Instance)
                                            .UseValue(
                                                Element.Builder
                                                    .UseType(Primitives.String.Instance)
                                                    .UseObject("Hello World")
                                                    .Build()
                                            )
                                            .Build()
                                    )
                                    .UseCallback(
                                        (c, d) =>
                                        {
                                            Console.Write(c.Variables["string"].Value.Object);
                                            return Primitives.Anything.Null;
                                        }
                                    )
                                    .Build()
                    },
                    Modifiers = new Modifier[] { Modifier.Export }
                }
            );

            Lib.AddVariable(
                "[ioprintln]",
                new() {
                    Type = Primitives.Method.Instance,
                    Value = new() {
                        Type = Primitives.Method.Instance,
                        Object = Function.Builder
                                    .UseParameter(
                                        "string",
                                        Variable.Builder
                                            .UseType(Primitives.String.Instance)
                                            .UseValue(
                                                Element.Builder
                                                    .UseType(Primitives.String.Instance)
                                                    .UseObject("Hello World")
                                                    .Build()
                                            )
                                            .Build()
                                        )
                                    .UseCallback(
                                        (c, d) =>
                                        {
                                            Console.WriteLine(c.Variables["string"].Value.Object);
                                            return Primitives.Anything.Null;
                                        })
                                    .Build()
                    },
                    Modifiers = new Modifier[] { Modifier.Export }
                }
            );
        }
    }
}
