using System;
using System.Linq;

namespace SequentialBuilder.Model
{
    public class BuilderCodeGenerator
    {
        public string GenerateBuilderCode(BuilderClass builderClass)
        {
            return $@"
using System;
namespace {builderClass.Namespace}
{{
    public partial class {builderClass.Name}
    {{
        {GenerateMethods(builderClass)}
    }}
}}
";
        }

        private string GenerateMethods(BuilderClass builderClass)
        {
            var methods = builderClass.GetFields()
                .Select(x => GenerateMethod(x, builderClass));
            return string.Join(Environment.NewLine, methods);
        }

        private string GenerateMethod(BuilderField field, BuilderClass builderClass)
        {
            return $@"
 public {builderClass.Name} With{field.CamelCaseName}({field.TypeName} {field.Name})
 {{
        this.{field.Name} = {field.Name};
        return this;
 }}
";
        }
    }
}