using System;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace SequentialBuilder.Model
{
    public class SequentialBuilderClass : BuilderClass
    {
        public SequentialBuilderClass(ITypeSymbol classTypeSymbol) : base(classTypeSymbol)
        {
        }

        public override string GetGeneratedCode()
        {
            throw new NotImplementedException();
        }
    }
}