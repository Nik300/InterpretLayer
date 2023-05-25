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
using Nik300.InterpretLayer.Runtime.Interop;
using Nik300.InterpretLayer.Types.Statements.Flow;

namespace Development.Tests
{
    public class LabelFlow : Test
    {
        public DocumentBuilder Script =>
            Document.Builder
                .UseName("labelTestDoc")
                .UseStatement(new LabelDef("test"))
                .UseStatement(
                    new FunctionCall(
                        "sys", "ioprintln",
                        args: new Reference[]
                        {
                            Element.Builder
                                .UseType(Primitives.String.Instance)
                                .UseObject("Loop")
                                .BuildRef()
                        }
                    )
                )
                .UseStatement(new LabelJump("test"));

        public string Name => "Label Flow";
    }
}
