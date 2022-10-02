using Nik300.InterpretLayer.Types;
using Nik300.InterpretLayer.Types.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using runtime = Nik300.InterpretLayer.Types.Runtime;

namespace Nik300.InterpretLayer.Runtime.Types
{
    public static partial class Primitives
    {
        public sealed class TypeName : runtime.Type
        {
            public override string Name { get; }
            public override Context DefinitionContext { get; }
            public Document DefinitionDocument { get; }
            public Context TypeContext { get; }

            public TypeName(Document doc, string name, Context definitionContext, Context typeContext = null)
            {
                if (typeContext?.Name != null) throw new Exception("given context is already defined");
                Name = name;
                DefinitionContext = definitionContext;
                DefinitionDocument = doc;
                TypeContext = typeContext ?? new();
                TypeContext.UseName(FullName);
                doc.Contexts.Add(TypeContext.Name, TypeContext);
            }

            public override bool Callable()
            {
                return
                    TypeContext.Variables.ContainsKey("[call]") &&
                    TypeContext.Variables["[call]"].Value.Type.Compare(Method.Instance) &&
                    TypeContext.Variables["[call]"].Modifiers.Contains(Modifier.Operator);
            }

            public override Element Cast(Element element)
            {
                if (
                    !TypeContext.Variables.ContainsKey("[cast]") ||
                    !TypeContext.Variables["[cast]"].Value.Type.Compare(Method.Instance) ||
                    !TypeContext.Variables["[cast]"].Modifiers.Contains(Modifier.Operator)
                    ) return null;
                return ((Function)TypeContext.Variables["[cast]"].Value.Object).Run(new Context(TypeContext).AddVariable("element", new() { Type = element.Type, Modifiers = new Modifier[] { Modifier.Local, Modifier.Readonly }, Value = element }), DefinitionDocument);
                
            }

            public override bool Compare(runtime.Type other)
            {
                bool comp = TypeContext.Variables.ContainsKey("[typeEqu]") &&
                            TypeContext.Variables["[typeEqu]"].Value.Type.Compare(Method.Instance) &&
                            TypeContext.Variables["[typeEqu]"].Modifiers.Contains(Modifier.Operator);
                return 
                    other.FullName == FullName ||
                    other.FullName == "sys.any" ||
                    (comp && ((bool)((Function)TypeContext.Variables["[typeEqu]"].Value.Object).Run(new Context(TypeContext).AddVariable("type", new() { Type = String.Instance, Modifiers = new Modifier[] { Modifier.Local, Modifier.Readonly }, Value = new() { Type = String.Instance, Object = other.FullName } }), DefinitionDocument).Object));
            }

            public override bool Contains(string childName)
            {
                return TypeContext.Variables.ContainsKey(childName);
            }

            public override bool Scriptable()
            {
                return TypeContext.Variables.Count > 0;
            }

            public override Element Call(Context current, Document document, Element element, Element @this = null, Element[] args = null, (string name, Element element)[] kargs = null)
            {
                if (!Callable()) return null;
                Element e = TypeContext.Variables["[call]"].Value;
                runtime.Type t = e.Type;
                return t.Call(current, document, e, @this, args, kargs);
            }
            public override Element Get(Context current, Element @this, string childName)
            {
                if (!Scriptable() || !((Context)@this.Object).Variables.ContainsKey(childName)) return null;
                return ((Context)@this.Object).Variables[childName].Value;
            }
            public override void Set(Context current, Element @this, string childName, Element value)
            {
                if (!Scriptable() || !((Context)@this.Object).Variables.ContainsKey(childName) || !((Context)@this.Object).Variables[childName].Type.Compare(value.Type)) return;
                ((Context)@this.Object).Variables[childName].Value = value;
            }
            public override void Create(Context current, Element @this, string childName, Variable var)
            {
                if (current.FullName != TypeContext.FullName || ((Context)@this.Object).Variables.ContainsKey(childName)) return;
                ((Context)@this.Object).Variables.Add(childName, var);
            }
        }
    }
}
