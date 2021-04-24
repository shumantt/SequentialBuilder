using System;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace SequentialBuilder.Model
{
    public class SimpleBuilderClass : BuilderClass
    {
        public SimpleBuilderClass(ITypeSymbol classTypeSymbol, string[] usedNamespaces) : base(classTypeSymbol, usedNamespaces)
        {
        }

        public override string GetGeneratedCode()
        {
            return $@"
using System;
namespace {Namespace}
{{
    public partial class {Name}
    {{
        {GenerateMethods()}
    }}
}}
";
        }

        private string GenerateMethods()
        {
            var methods = GetFields().Select(GenerateMethod);
            return string.Join(Environment.NewLine, methods);
        }

        private string GenerateMethod(BuilderFieldInfo fieldInfo)
        {
            return $@"
 public {Name} With{fieldInfo.CamelCaseName}({fieldInfo.TypeName} {fieldInfo.Name})
 {{
        {fieldInfo.GetSetCode()}
 }}
";
        }
    }
}