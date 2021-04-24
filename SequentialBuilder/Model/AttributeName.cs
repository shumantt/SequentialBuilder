using System;

namespace SequentialBuilder.Model
{
    public static class AttributeName<T> where T : Attribute 
    {
        public static string GetPlainName()
        {
            return typeof(T).Name.Replace("Attribute", string.Empty);
        }
    }
}