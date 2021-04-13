using System;

namespace SequentialBuilder.Model
{
    public static class AttributeName<T> where T : Attribute 
    {
        public static string GetPlainName()
        {
            return nameof(T).Replace("Attribute", string.Empty);
        }
    }
}