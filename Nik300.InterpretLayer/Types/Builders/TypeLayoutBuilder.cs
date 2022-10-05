using Nik300.InterpretLayer.Types.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nik300.InterpretLayer.Types.Builders
{
    public sealed class TypeLayoutBuilder
    {
        Function SCTOR { get; set; }
        Function OCTOR { get; set; }
        string Name { get; set; }

        public TypeLayoutBuilder UseStaticConstructor(Function ctor)
        {
            SCTOR = ctor;
            return this;
        }
        public TypeLayoutBuilder UseObjectConstructor(Function ctor)
        {
            OCTOR = ctor;
            return this;
        }
        public TypeLayoutBuilder UseName(string name)
        {
            Name = name;
            return this;
        }
        public TypeLayout Build()
        {
            if (Name is null || SCTOR is null || OCTOR is null) return null;
            return new() { OCTOR = OCTOR, SCTOR = SCTOR, Name = Name };
        }
    }
}
