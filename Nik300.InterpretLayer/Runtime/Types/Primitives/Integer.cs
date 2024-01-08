using runtime = Nik300.InterpretLayer.Types.Runtime;
using Nik300.InterpretLayer.Types.Runtime;

namespace Nik300.InterpretLayer.Runtime.Types
{
  public static partial class Primitives
  {
    public sealed class Integer : runtime.Type
    {
      public static Integer Instance { get; } = new();

      public override string Name => "integer";
      public override Context DefinitionContext => Anything.SystemContext;

      private static Element StringToInteger(Element element)
      {
        if (!int.TryParse(element.Object as string, out var i))
          return Anything.Null;
        
        return Element.Builder
          .UseType(Instance)
          .UseObject(i)
          .Build();
      }
      private static Element PrimitiveToInteger(Element element)
      {
        return element.Type.Name switch {
          "string" => StringToInteger(element),
          "list" or "array" => Anything.Null,
          "float" or "double" => Element.Builder
            .UseType(Instance)
            .UseObject((long)element.Object)
            .Build(),
          _ => Anything.Null
        };
      }

      public override bool Callable() => false;
      public override Element Cast(Element element)
      {
        return element.Type.IsPrimitive ? PrimitiveToInteger(element) : CtorCast(element);
      }
      public override bool Compare(runtime.Type other)
      {
        return other.FullName == FullName || other.FullName == "sys.anything";
      }
      public override bool Contains(string childName) => false;
      public override bool Scriptable() => false;
    }
  }
}

