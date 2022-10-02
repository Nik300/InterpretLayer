using Nik300.InterpretLayer.Types.Runtime;
using runtime = Nik300.InterpretLayer.Types.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Nik300.InterpretLayer.Runtime.Interop
{
    public sealed class Reference
    {
        internal Variable Ref { private get; set; }

        public runtime.Type Type
        {
            get => Ref.Type;
        }
        public Element Value
        {
            get => Ref.Value;
            set
            {
                if (!Ref.UpdateValue(value)) throw new("Variable is not modifiable");
            }
        }
        public object Object
        {
            get => Ref.Value.Object;
        }
        internal bool Set => Ref != null;

        internal Reference(Variable var = null)
        {
            Ref = var;
        }
    }
}
