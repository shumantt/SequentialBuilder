using System;

namespace SequentialBuilder.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class BuilderFieldAttribute : Attribute
    {
        public int Order { get; }

        public BuilderFieldAttribute(int order)
        {
            Order = order;
        }
    }
}