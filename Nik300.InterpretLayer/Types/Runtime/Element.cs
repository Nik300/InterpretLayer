using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nik300.InterpretLayer.Types.Runtime
{
    public abstract class Element
    {
        public object Object { get; private set; }
        public Type Type { get; private set; }

        public abstract Element Get(string childName);
        public abstract void Set(string childName, object value);
        public abstract void Set(string childName, Element value);
    }
}
