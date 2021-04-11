using System.Globalization;

namespace SequentialBuilder.Model
{
    public class BuilderField
    {
        public BuilderField(string name, string typeName)
        {
            Name = name;
            TypeName = typeName;
        }
        
        public string Name { get; }
        public string TypeName { get; }
        
        public string CamelCaseName => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Name);
    }
}