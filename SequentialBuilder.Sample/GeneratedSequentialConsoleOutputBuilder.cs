using SequentialBuilder.Attributes;

namespace SequentialBuilder.Sample
{
    [SequentialBuilder]
    public partial class GeneratedSequentialConsoleOutputBuilder
    {
        [BuilderField]
        private string name;
        
        [BuilderField]
        private string mainText;
        
        [BuilderField]
        private string signature;
        
        public ConsoleOutput Build()
        {
            return new($"Hello, {name}! {mainText}. {signature}");
        }
    }
}