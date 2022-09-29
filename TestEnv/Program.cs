using Nik300.InterpretLayer.Types;
using Nik300.InterpretLayer.Types.Statements.General;
using System;

namespace TestEnv
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var doc = Document.Builder
                        .UseName("debugDoc")
                        .UseStatement(new Debug())
                        .Build();
            var context = doc.GetRoot();
            doc.RunNext(context);
        }
    }
}
