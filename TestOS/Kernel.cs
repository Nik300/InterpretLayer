using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using Nik300.InterpretLayer.Types;
using Nik300.InterpretLayer.Types.Statements.General;
using Nik300.InterpretLayer.Types.Statements.Definition;
using Nik300.InterpretLayer.Runtime.Types;
using runtime = Nik300.InterpretLayer.Types.Runtime;
using Nik300.InterpretLayer.Types.Runtime;

namespace TestOS
{
    public class Kernel : Sys.Kernel
    {

        protected override void BeforeRun()
        {
            Console.Clear();
        }

        protected override void Run()
        {
            var doc = Document.Builder
                        .UseName("debugDoc")
                        .UseStatement(
                            new FunctionDef(
                                "debug",
                                Function.Builder
                                    .UseStatement(new VariableDef("testVar", Primitives.String.Instance, "FuncDef, FuncCall and VarDef working!", new Modifier[] { Modifier.Readonly }))
                                    .UseStatement(new Debug())
                                    .Build()
                            )
                         )
                        .UseStatement(new FunctionCall("debug"))
                        .Build();
            var context = doc.GetRoot();
            while ((context = doc.RunNext(context)) != null) ;
            Console.WriteLine("Document executed");
            while (true) ;
        }
    }
}
