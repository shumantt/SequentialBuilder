using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SequentialBuilder.Model
{
    public static class CompilationExtensions
    {
        public static IEnumerable<BuilderClass> GetBuilders(this Compilation compilation)
        {
            return
                compilation
                    .SyntaxTrees
                    .SelectMany(s => s.GetRoot().DescendantNodes())
                    .OfType<ClassDeclarationSyntax>()
                    .Select(component => component.TryConvertToBuilder(compilation))
                    .Where(builder => builder is not null)
                    .Cast<BuilderClass>();
        }
    }
}