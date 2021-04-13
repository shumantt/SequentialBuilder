using System.Globalization;

namespace SequentialBuilder.Model
{
    public class BuilderField
    {
        public BuilderField(string name, string typeName, int? order = null)
        {
            Name = name;
            TypeName = typeName;
            Order = order;
        }
        
        public string Name { get; }
        public string TypeName { get; }
        public int? Order { get; }

        public string CamelCaseName => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Name);
    }
}