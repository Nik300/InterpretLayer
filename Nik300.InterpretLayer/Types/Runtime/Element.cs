using Nik300.InterpretLayer.Types.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nik300.InterpretLayer.Types.Runtime
{
    public class Element
    {
        public object Object { get; internal set; }
        public Type Type { get; internal set; }

        public static ElementBuilder Builder { get { return new(); } }
    }
}
