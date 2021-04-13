using System;
using System.Globalization;
using System.Linq;

namespace SequentialBuilder.Model
{
    public class BuilderField
    {
        public BuilderField(string name, string typeName, int? order = null)
        {
            if(string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Name can not be empty");
            }
            
            if(string.IsNullOrEmpty(typeName))
            {
                throw new ArgumentException("TypeName can not be empty");
            }
            
            
            Name = name;
            TypeName = typeName;
            Order = order;
        }
        
        public string Name { get; }
        public string TypeName { get; }
        public int? Order { get; }

        public string CamelCaseName => Name.First().ToString().ToUpper() + Name.Substring(1);
    }
}