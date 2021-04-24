using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using SequentialBuilder.Attributes;

namespace SequentialBuilder.Model
{
    public abstract class BuilderClass
    {
        private readonly ITypeSymbol classTypeSymbol;

        protected BuilderClass(ITypeSymbol classTypeSymbol, string[] usedNamespaces)
        {
            if (!classTypeSymbol.IsReferenceType)
            {
                throw new ArgumentException("Builder class should be reference type");
            }

            this.classTypeSymbol = classTypeSymbol;
            UsedNamespaces = usedNamespaces;
        }
        
        public string[] UsedNamespaces { get; }

        public string Name => classTypeSymbol.Name;

        public string Namespace => classTypeSymbol.ContainingNamespace.ToDisplayString();

        public abstract string GetGeneratedCode();
        
        public IEnumerable<BuilderFieldInfo> GetFields()
        {
            var fields = classTypeSymbol
                .GetMembers()
                .OfType<IFieldSymbol>()
                .ToList();

            var attributedFields =
                fields
                    .Select(TryGetAttributedField)
                    .Where(x => x is not null)
                    .Cast<BuilderFieldInfo>()
                    .ToList();

            if (attributedFields.Count == 0)
            {
                return fields.Select(x => new BuilderFieldInfo(x.Name, x.Type.Name));
            }

            return attributedFields;
        }

        private BuilderFieldInfo? TryGetAttributedField(IFieldSymbol fieldSymbol)
        {
            var builderFieldAttribute = fieldSymbol.GetAttributes()
                .FirstOrDefault(x => x.AttributeClass?.Name == AttributeName<BuilderFieldAttribute>.GetName()
                || x.AttributeClass?.Name == AttributeName<BuilderFieldAttribute>.GetPlainName());
           
            if (builderFieldAttribute is null)
            {
                return null;
            }

            return new BuilderFieldInfo(fieldSymbol.Name, fieldSymbol.Type.Name);
        }
    }
}