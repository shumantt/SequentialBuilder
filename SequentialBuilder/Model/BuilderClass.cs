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
        
        protected IEnumerable<BuilderField> GetFields()
        {
            var fields = classTypeSymbol
                .GetMembers()
                .OfType<IFieldSymbol>()
                .ToList();

            var attributedFields =
                fields
                    .Select(TryGetAttributedField)
                    .Where(x => x is not null)
                    .Cast<BuilderField>()
                    .ToList();

            if (attributedFields.Count == 0)
            {
                return fields.Select(x => new BuilderField(x.Name, x.Type.Name));
            }

            return attributedFields;
        }

        private BuilderField? TryGetAttributedField(IFieldSymbol fieldSymbol)
        {
            var builderFieldAttribute = fieldSymbol.GetAttributes()
                .FirstOrDefault(x => x.AttributeClass?.Name == AttributeName<BuilderFieldAttribute>.GetPlainName());
            if (builderFieldAttribute is null)
            {
                return null;
            }

            if (builderFieldAttribute.ConstructorArguments.Length != 1)
            {
                return null;
            }

            var orderArgument = builderFieldAttribute.ConstructorArguments.Single();
            if (orderArgument.Type?.Name != "int")
            {
                return null;
            }

            var order = (int) orderArgument.Value!;

            return new BuilderField(fieldSymbol.Name, fieldSymbol.Type.Name, order);
        }
    }
}