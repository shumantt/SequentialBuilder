using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace SequentialBuilder.Model
{
    public class BuilderClass
    {
        private readonly ITypeSymbol classTypeSymbol;

        public BuilderClass(ITypeSymbol classTypeSymbol)
        {
            this.classTypeSymbol = classTypeSymbol;
        }
        
        public string Name => classTypeSymbol.Name;
        
        public string Namespace => classTypeSymbol.ContainingNamespace.ToDisplayString();

        public IEnumerable<BuilderField> GetFields()
        {
            return classTypeSymbol
                .GetMembers()
                .OfType<IFieldSymbol>()
                .Select(x => new BuilderField(x.Name, x.Type.Name));
        }
    }
}