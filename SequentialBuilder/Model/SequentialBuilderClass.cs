using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace SequentialBuilder.Model
{
    public class SequentialBuilderClass : BuilderClass
    {
        public SequentialBuilderClass(ITypeSymbol classTypeSymbol, string[] usedNamespaces)
            : base(classTypeSymbol, usedNamespaces)
        {
        }

        public override string GetGeneratedCode()
        {
            var builderFields = GetFields()
                .OrderBy(x => x.Order)
                .ToArray();

            var interfaces = builderFields.Select(x => new SequentialBuilderInterface(Name, x)).ToArray();
            var interfacesImplementationString = string.Join(", ", interfaces.Select(x => $"{Name}.{x.Name}"));
            var usedNamespaceString = string.Join(Environment.NewLine, UsedNamespaces.Select(x => $"using {x};"));    
            
            return $@"
{usedNamespaceString}
using System;

namespace {Namespace}
{{
    public partial class {Name} : {interfacesImplementationString}
    {{
        {GenerateMethods(interfaces)}
    }}
}}
";
        }

        private string GenerateMethods(SequentialBuilderInterface[] interfaces)
        {
            if (interfaces.Length == 0)
            {
                return string.Empty;
            }
            
            var methods = new List<string>();
            for (int i = 0; i < interfaces.Length - 1; i++)
            {
                var currentInterface = interfaces[i];
                var nextInterface = interfaces[i + 1];
                methods.Add(GenerateMethod(currentInterface, nextInterface.Name));
            }
            
            methods.Add(GenerateMethod(interfaces.Last(), Name));

            return string.Join(Environment.NewLine, methods);
        }

        private string GenerateMethod(SequentialBuilderInterface fieldSetInterface, string nextSetterType)
        {
            return $@"
 public {fieldSetInterface.GetBuilderMethodSignature(nextSetterType)}
 {{
        {fieldSetInterface.FieldToSet.GetSetCode()}
 }}

{fieldSetInterface.GetGeneratedCode(nextSetterType)}
";
        }
    }
}