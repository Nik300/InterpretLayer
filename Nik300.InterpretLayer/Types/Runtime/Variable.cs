﻿using Nik300.InterpretLayer.Runtime.Types;
using Nik300.InterpretLayer.Types.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nik300.InterpretLayer.Types.Runtime
{
    public sealed class Variable
    {
        public Type Type { get; internal set; }
        public Element Value { get; internal set; }
        public Modifier[] Modifiers { get; internal set; }

        public static VariableBuilder Builder { get { return new(); } }

        internal Variable()
        {
            Modifiers = Array.Empty<Modifier>();
        }

        internal bool ContainsModifier(Modifier mod)
        {
            for (int i = 0; i < Modifiers.Length; i++)
                if (Modifiers[i] == mod) return true;
            return false;
        }

        public bool UpdateValue(Element value)
        {
            Type ??= Primitives.Anything.Instance;
            if (!value.Type.Compare(Type) || ContainsModifier(Modifier.Constant) || ContainsModifier(Modifier.Readonly)) return false;
            Value = value;
            return true;
        }

        public Variable UseType(Type type)
        {
            if (Type != null) return this;
            Type = type;
            return this;
        }
        public Variable AddModifier(Modifier modifier)
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
        public Variable RemModifier(Modifier modifier)
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
    }
}
