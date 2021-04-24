using System;

namespace SequentialBuilder.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class BuilderFieldAttribute : Attribute
    {
        public BuilderFieldAttribute()
        {
            
        }
    }
}