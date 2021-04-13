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
                return BuildBuilderClass(t => new SimpleBuilderClass(t));
            }
            
            if (attributes.Any(x => x.Name.ToString() == AttributeName<SequentialBuilderAttribute>.GetPlainName()))
            {
                return BuildBuilderClass(t => new SequentialBuilderClass(t));
            }

            return null;
            
            BuilderClass? BuildBuilderClass(Func<ITypeSymbol, BuilderClass> create)
            {
                var typeSymbol = compilation
                    .GetSemanticModel(@class.SyntaxTree)
                    .GetDeclaredSymbol(@class);
                return typeSymbol != null ? create(typeSymbol) : null;
            }
        }
    }
}