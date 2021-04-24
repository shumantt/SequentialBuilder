using System;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SequentialBuilder.Attributes;

namespace SequentialBuilder.Model
{
    public static class ClassDeclarationSyntaxExtensions
    {
        public static BuilderClass? TryConvertToBuilder(this ClassDeclarationSyntax @class, Compilation compilation)
        {
            var attributes = @class.AttributeLists.SelectMany(x => x.Attributes).ToList();

            if (attributes.Any(x => x.Name.ToString() == AttributeName<SimpleBuilderAttribute>.GetPlainName()))
            {
                return BuildBuilderClass((typeSymbol, usedNamespace) => new SimpleBuilderClass(typeSymbol, usedNamespace));
            }
            
            if (attributes.Any(x => x.Name.ToString() == AttributeName<SequentialBuilderAttribute>.GetPlainName()))
            {
                return BuildBuilderClass((typeSymbol, usedNamespace) => new SequentialBuilderClass(typeSymbol, usedNamespace));
            }

            return null;
            
            BuilderClass? BuildBuilderClass(Func<ITypeSymbol, string[], BuilderClass> create)
            {
                var usedNamespaces = @class
                    .SyntaxTree
                    .GetRoot()
                    .DescendantNodes()
                    .OfType<UsingDirectiveSyntax>()
                    .Select(x => x.Name.ToString())
                    .ToArray();
                var typeSymbol = compilation
                    .GetSemanticModel(@class.SyntaxTree)
                    .GetDeclaredSymbol(@class);
                return typeSymbol != null ? create(typeSymbol, usedNamespaces) : null;
            }
        }
    }
}