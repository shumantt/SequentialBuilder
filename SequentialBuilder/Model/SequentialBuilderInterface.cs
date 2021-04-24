namespace SequentialBuilder.Model
{
    public class SequentialBuilderInterface
    {
        private readonly string builderClassName;

        public SequentialBuilderInterface(string builderClassName, BuilderField field)
        {
            this.builderClassName = builderClassName;
            FieldToSet = field;
        }

        public BuilderField FieldToSet { get; }

        public string Name => $"I{FieldToSet.CamelCaseName}{builderClassName}";

        public string GetBuilderMethodSignature(string returnType)
        {
            return $"{returnType} With{FieldToSet.CamelCaseName}({FieldToSet.TypeName} {FieldToSet.Name})";
        }

        public string GetGeneratedCode(string returnType)
        {
            return $@"
public interface {Name}
{{
    {GetBuilderMethodSignature(returnType)};
}}";
        }
    }
}