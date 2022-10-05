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
    public class TypeDeclaration : Test
    {
        public override DocumentBuilder Script => 
            Document.Builder
                .UseName("typeTestDoc")
                .UseStatement(
                    new TypeDef(
                        TypeLayout.Builder
                            .UseName("TestType")
                            .UseStaticConstructor(
                                Function.Builder
                                    .UseStatement(
                                        new FunctionDef(
                                            "[ctor]",
                                            Function.Builder
                                                .UseStatement(
                                                    new FunctionCall(
                                                        "sys", "[ioprintln]",
                                                        args: new Element[]
                                                        {
                                                            Element.Builder
                                                                .UseType(Primitives.String.Instance)
                                                                .UseObject("CTOR called!")
                                                                .Build()
                                                        }
                                                    )
                                                )
                                                .Build()
                                        )
                                    )
                                    .Build()
                            )
                            .UseObjectConstructor(
                                Function.Builder
                                    .UseStatement(
                                        new VariableDef(
                                            "testObjectVar",
                                            Primitives.String.Instance,
                                            Element.Builder
                                                .UseType(Primitives.String.Instance)
                                                .UseObject("Object created!")
                                                .Build()
                                        )
                                    )
                                    .Build()
                            )
                            .Build()
                    )
                )
                .UseStatement(
                    new FunctionCall(
                        "sys", "[ioprintln]",
                        args: new Element[]
                        {
                            Element.Builder
                                .UseType(Primitives.String.Instance)
                                .UseObject("Type built!")
                                .Build()
                        }
                    )
                )
                .UseStatement(
                    new VariableDef(
                        "testRes",
                        out Reference resRef
                    )
                )
                .UseStatement(
                    new TypeConstruct(
                        "TestType",
                        resRef
                    )
                );

        public override string Name => "TypeDeclaration";
    }
}
