using Nik300.InterpretLayer.Types;
using Nik300.InterpretLayer.Types.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Development
{
    public interface Test
    {
        public abstract DocumentBuilder Script { get; }
        public abstract string Name { get; }
    }
    public static class Tester
    {
        public static void RunTest(Test test)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Executing test ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(test.Name);

            Console.ForegroundColor = ConsoleColor.Gray;
            var doc = test.Script.Build();
            var context = doc.GetRoot();
            while ((context = doc.RunNext(context)) != null) ;
        }
    }
}
