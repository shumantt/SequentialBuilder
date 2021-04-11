using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using SequentialBuilder.Attributes;
using SequentialBuilder.Model;

namespace SequentialBuilder.Generators
{
    [Generator]
    public class SimpleBuilderGenerator : ISourceGenerator
    {
        private static readonly string BuilderAttributeName =
            nameof(BuilderAttribute).Replace("Attribute", string.Empty);

        private BuilderCodeGenerator codeGenerator;

        public void Initialize(GeneratorInitializationContext context)
        {
            codeGenerator = new BuilderCodeGenerator();
        }

        public void Execute(GeneratorExecutionContext context)
        {
            var builders = GetBuilders(context.Compilation);
            foreach (var builder in builders)
            {
                var code = codeGenerator.GenerateBuilderCode(builder);
                context.AddSource($"{builder.Name}.Generated.cs", SourceText.From(code, Encoding.UTF8));
            }
        }

        private IEnumerable<BuilderClass> GetBuilders(Compilation compilation)
        {
            return
                compilation
                    .SyntaxTrees
                    .SelectMany(s => s.GetRoot().DescendantNodes())
                    .OfType<ClassDeclarationSyntax>()
                    .Select(component => TryGetBuilderClass(compilation, component))
                    .Where(builder => builder is not null)
                    .Cast<BuilderClass>();
        }

        private BuilderClass? TryGetBuilderClass(Compilation compilation, ClassDeclarationSyntax @class)
        {
            var builderClassAttribute = @class.AttributeLists
                .SelectMany(x => x.Attributes)
                .FirstOrDefault(attr => attr.Name.ToString() == BuilderAttributeName);

            if (builderClassAttribute == null)
            {
                return null;
            }

            var typeSymbol = compilation
                .GetSemanticModel(@class.SyntaxTree)
                .GetDeclaredSymbol(@class);
            if (typeSymbol == null)
            {
                return null;
            }

            return new BuilderClass(typeSymbol);
        }
    }
}