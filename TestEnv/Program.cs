using Nik300.InterpretLayer.Types;
using Nik300.InterpretLayer.Types.Statements.Definition;
using Nik300.InterpretLayer.Types.Statements.General;
using Nik300.InterpretLayer.Runtime.Types;
using Nik300.InterpretLayer.Types.Runtime;
using System;
using Nik300.InterpretLayer.Runtime.Interop;
using Nik300.InterpretLayer.Types.Statements.Manipulation;

namespace TestEnv
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var doc = Document.Builder
                        .UseName("debugDoc")
                        .UseStatement(
                            new FunctionDef(
                                "test",
                                Function.Builder
                                    .UseCallback((c, d) => { Console.WriteLine(c.GetVariable("test").Value.Object); return Primitives.Anything.Null; })
                                    .UseParameter("test", Variable.Builder.UseType(Primitives.String.Instance).UseValue(Element.Builder.UseType(Primitives.String.Instance).UseObject("Optional").Build()).Build())
                                    .Build(),
                                out Reference testFunc
                            )
                        )
                        .UseStatement(new FunctionCall(testFunc))
                        .UseStatement(new FunctionCall(testFunc, kparams: new (string, Element)[] { ("test", Element.Builder.UseType(Primitives.String.Instance).UseObject("Hello World!").Build()) }))
                        .Build();
            var context = doc.GetRoot();
            while ((context = doc.RunNext(context)) != null) ;
        }
    }
}

/*
function test(test: string)
{
    console.log(test);
}
test("Hello world!");
 */