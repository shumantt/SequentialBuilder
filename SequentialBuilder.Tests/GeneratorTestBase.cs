using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SequentialBuilder.Tests
{
    public abstract class GeneratorTestBase
    {
        protected (CSharpCompilation compilation, ClassDeclarationSyntax classDeclarationSyntax) PrepareData(string code)
        {
            var tree = CSharpSyntaxTree.ParseText(code);

            var classDeclarationSyntax = tree.GetRoot().DescendantNodes().OfType<ClassDeclarationSyntax>().Single();
            var Mscorlib = MetadataReference.CreateFromFile(typeof(object).Assembly.Location);
            var compilation = CSharpCompilation.Create("MyCompilation",
                syntaxTrees: new[] {tree}, references: new[] {Mscorlib});

            return (compilation, classDeclarationSyntax);
        }
    }
}