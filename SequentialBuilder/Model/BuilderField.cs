using System;
using System.Linq;

namespace SequentialBuilder.Model
{
    public class BuilderField
    {
        private Lazy<string> camelCaseName;

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
            camelCaseName = new Lazy<string>(() => Name.First().ToString().ToUpper() + Name.Substring(1));
        }
        
        public string Name { get; }
        public string TypeName { get; }
        public int? Order { get; }

        public string CamelCaseName => camelCaseName.Value;

        public string GetSetCode()
        {
            return $@"
                this.{Name} = {Name};
                return this;
            ";
        }
    }
}