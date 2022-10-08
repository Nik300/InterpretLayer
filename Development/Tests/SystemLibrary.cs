using Nik300.InterpretLayer.Types;
using Nik300.InterpretLayer.Types.Builders;
using Nik300.InterpretLayer.Types.Runtime;
using Nik300.InterpretLayer.Types.Statements.General;
using Nik300.InterpretLayer.Runtime.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Development.Tests
{
    public class SystemLibrary: Test
    {
        public override DocumentBuilder Script { get; } =
            Document.Builder
                .UseName("systemTestDoc")
                .UseStatement(
                    new FunctionCall(
                        "sys", "ioprintln",
                        kwargs: new (string, Element)[]
                        {
                            (
                                "string",
                                Element.Builder
                                    .UseType(Primitives.String.Instance)
                                    .UseObject("Hello System!")
                                    .Build()
                            )
                        }
                    )
                );
        public override string Name { get; } = "System Library";
    }
}
