using SequentialBuilder.Attributes;

namespace SequentialBuilder.Sample
{
    [SequentialBuilder]
    public partial class GeneratedSequentialConsoleOutputBuilder
    {
        [BuilderField(0)]
        private string name;
        
        [BuilderField(1)]
        private string mainText;
        
        [BuilderField(2)]
        private string signature;
        
        public ConsoleOutput Build()
        {
            return new($"Hello, {name}! {mainText}. {signature}");
        }
    }
}