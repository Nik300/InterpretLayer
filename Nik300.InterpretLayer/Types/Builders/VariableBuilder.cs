using Nik300.InterpretLayer.Types.Runtime;
using runtime = Nik300.InterpretLayer.Types.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nik300.InterpretLayer.Runtime.Types;

namespace Nik300.InterpretLayer.Types.Builders
{
    public sealed class VariableBuilder
    {
        public Element Value { get; private set; } = null;
        public runtime.Type Type { get; private set; } = null;
        public Modifier[] Modifiers { get; private set; } = Array.Empty<Modifier>();

        internal VariableBuilder() { }

        public VariableBuilder UseValue(Element element)
        {
            if (Value != null) return this;
            Value = element;
            return this;
        }
        public VariableBuilder UseType(runtime.Type type)
        {
            if (Type != null) return this;
            Type = type;
            return this;
        }
        public VariableBuilder UseModifiers(Modifier[] modifiers)
        {
            Modifiers = modifiers ?? Array.Empty<Modifier>();
            return this;
        }
        public VariableBuilder AddModifier(Modifier modifier)
        {
            Modifier[] temp = new Modifier[Modifiers.Length + 1];
            for (int i = 0; i < Modifiers.Length; i++)
            {
                if (Modifiers[i] == modifier) return this;
                temp[i] = Modifiers[i];
            }
            temp[^1] = modifier;
            Modifiers = temp;
            return this;
        }
        public VariableBuilder RemModifier(Modifier modifier)
        {
            Modifier[] temp = new Modifier[Modifiers.Length - 1];
            for (int i = 0, s = 0; i < Modifiers.Length && s < temp.Length; i++, s++)
            {
                if (Modifiers[i] == modifier)
                {
                    s--;
                    continue;
                }
                temp[s] = Modifiers[i];
            }
            Modifiers = temp;
            return this;
        }
        public Variable Build() => new() { Value = Value ?? Primitives.Anything.Null, Type = Type ?? Primitives.Anything.Instance };
    }
}
