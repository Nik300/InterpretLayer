using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using Nik300.InterpretLayer.Types;
using Nik300.InterpretLayer.Types.Statements.General;

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
                        .UseStatement(new Debug())
                        .Build();
            var context = doc.GetRoot();
            doc.RunNext(context);
            while (true) ;
        }
    }
}
