using Nik300.InterpretLayer.Types.Runtime;
using runtime = Nik300.InterpretLayer.Types.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using Nik300.InterpretLayer.Runtime.Types;

namespace Nik300.InterpretLayer.Runtime.Interop
{
    public class Reference
    {
        internal Variable Ref { private get; set; }

        public virtual runtime.Type Type
        {
            get => Ref.Type;
        }
        public virtual Element Value
        {
            get => Ref.Value ?? Primitives.Anything.Null;
            set
            {
                if (!Ref.UpdateValue(value)) throw new("Variable is not modifiable");
            }
        }
        public virtual object Object
        {
            get => Ref.Value.Object;
        }
        internal bool Set => Ref != null;
        internal virtual bool Modifiable => Set && !Ref.ContainsModifier(Modifier.Readonly) && !Ref.ContainsModifier(Modifier.Constant);

        internal Reference(Variable var = null)
        {
            Ref = var;
        }
    }
}
