using Nik300.InterpretLayer.Types;
using Nik300.InterpretLayer.Types.Statements.Definition;
using Nik300.InterpretLayer.Types.Statements.General;
using Nik300.InterpretLayer.Runtime.Types;
using Nik300.InterpretLayer.Types.Runtime;
using System;

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
        }
    }
}
