using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using SequentialBuilder.Model;

namespace SequentialBuilder.Generators
{
    [Generator]
    public class BuilderGenerator : ISourceGenerator
    {

        public void Initialize(GeneratorInitializationContext context)
        {
        }

        public void Execute(GeneratorExecutionContext context)
        {
            var builders = context.Compilation.GetBuilders();
            foreach (var builder in builders)
            {
                var code = builder.GetGeneratedCode();
                context.AddSource($"{builder.Name}.Generated.cs", SourceText.From(code, Encoding.UTF8));
            }
        }
    }
}