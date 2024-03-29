﻿using Nik300.InterpretLayer.Types.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nik300.InterpretLayer.Runtime.Types
{
    public abstract class Library
    {
        public abstract string Name { get; }
        protected Context Lib { get; private set; } = new();
        private bool Built { get; set; } = false;

        public Context Import()
        {
            if (Lib.Name != null && !Built) return null;
            if (Built) return Lib;

            Built = true;

            return
                Lib.UseName(Name)
                   .ToggleImported();
        }
    }
}
