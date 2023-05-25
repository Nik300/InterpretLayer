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

namespace Development.Tests
{
    public class VariableDeclaration: Test
    {
        public DocumentBuilder Script { get; } =
            Document.Builder
                .UseName("varTestDoc")
                .UseStatement(
                    new VariableDef(
                        "testVar",
                        Primitives.String.Instance,
                        Element.Builder
                            .UseType(Primitives.String.Instance)
                            .UseObject("Hello Var!")
                            .BuildRef()
                    )
                )
                .UseStatement(new Debug());
        public string Name { get; } = "Variable Declaration";
    }
}
