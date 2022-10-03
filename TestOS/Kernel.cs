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
using Nik300.InterpretLayer.Runtime.Interop;
using Nik300.InterpretLayer.Types.Statements.Manipulation;

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
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Starting tests...\n");

            Tester.RunTest(new Tests.VariableDeclaration());
            Tester.RunTest(new Tests.FunctionDeclaration());
            Tester.RunTest(new Tests.SystemLibrary());

            while (true) ;
        }
    }
}
