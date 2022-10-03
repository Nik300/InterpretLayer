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
using Nik300.InterpretLayer.Types.Statements.Definition;
using Nik300.InterpretLayer.Runtime.Interop;

namespace TestOS.Tests
{
    internal class FunctionDeclaration: Test
    {
        public override DocumentBuilder Script { get; } =
            Document.Builder
                .UseName("functionTestDoc")
                .UseStatement(
                    new FunctionDef(
                        "testFunc",
                        Function.Builder
                            .UseStatement(
                                new VariableDef(
                                    "testVar",
                                    Primitives.String.Instance,
                                    Element.Builder
                                        .UseType(Primitives.String.Instance)
                                        .UseObject("Hello Func!")
                                        .Build()
                                )
                            )
                            .UseStatement(new Debug())
                            .Build(),
                        out Reference testFunc
                    )
                )
                .UseStatement(new FunctionCall(testFunc));
        public override string Name { get; } = "Function Declaration";
    }
}
