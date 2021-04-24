namespace SequentialBuilder.Model
{
    public class SequentialBuilderInterface
    {
        private readonly string builderClassName;

        public SequentialBuilderInterface(string builderClassName, BuilderFieldInfo fieldInfo)
        {
            this.builderClassName = builderClassName;
            FieldInfoToSet = fieldInfo;
        }

        public BuilderFieldInfo FieldInfoToSet { get; }

        public string Name => $"I{FieldInfoToSet.CamelCaseName}{builderClassName}";

        public string GetBuilderMethodSignature(string returnType)
        {
            return $"{returnType} With{FieldInfoToSet.CamelCaseName}({FieldInfoToSet.TypeName} {FieldInfoToSet.Name})";
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