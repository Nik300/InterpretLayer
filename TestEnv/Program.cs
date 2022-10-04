using Nik300.InterpretLayer.Types;
using Nik300.InterpretLayer.Types.Statements.Definition;
using Nik300.InterpretLayer.Types.Statements.General;
using Nik300.InterpretLayer.Runtime.Types;
using Nik300.InterpretLayer.Types.Runtime;
using System;
using Nik300.InterpretLayer.Runtime.Interop;
using Nik300.InterpretLayer.Types.Statements.Manipulation;
using Development;
using Development.Tests;
using static System.Net.Mime.MediaTypeNames;

namespace TestEnv
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Starting tests...\n");

            Tester.RunTest(new VariableDeclaration());
            Tester.RunTest(new FunctionDeclaration());
            Tester.RunTest(new SystemLibrary());
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