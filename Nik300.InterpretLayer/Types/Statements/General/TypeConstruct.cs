using Nik300.InterpretLayer.Runtime.Interop;
using Nik300.InterpretLayer.Types.Runtime;
using runtime = Nik300.InterpretLayer.Types.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nik300.InterpretLayer.Runtime.Types;
using System.Security.Cryptography;

namespace Nik300.InterpretLayer.Types.Statements.General
{
    public class TypeConstruct : Statement
    {
        public override string Name => "TypeConstruct";

        public override string[] AllowedContexts => null;

        public string ContextName { get; }
        public string TypeName { get; }
        public Reference ResultReference { get; }
        public Element[] Args { get; }
        (string, Element)[] KWargs { get; }

        public TypeConstruct(string typeName, Reference resultReference = null, Element[] args = null, (string, Element)[] kwargs = null)
        {
            ContextName = null;
            TypeName = typeName;
            ResultReference = resultReference;
            Args = args;
            KWargs = kwargs;
        }
        public TypeConstruct(string contextName, string typeName, Reference resultReference = null, Element[] args = null, (string, Element)[] kwargs = null)
        {
            ContextName = contextName;
            TypeName = typeName;
            ResultReference = resultReference;
            Args = args;
            KWargs = kwargs;
        }

        protected override Context ExecuteStatement(Context currentContext, Document document)
        {
            Variable t;
            if (ContextName != null)
                t = document.GetVariable(ContextName, TypeName, currentContext);
            else
                t = currentContext.GetVariable(TypeName);
            if (!t.Type.Compare(Primitives.Type.Instance)) throw new Exception("expecting type");
            if (ResultReference != null) ResultReference.Value = ((runtime.Type)t.Value.Object).New(currentContext, document, Args, KWargs);
            return currentContext;
        }
    }
}
