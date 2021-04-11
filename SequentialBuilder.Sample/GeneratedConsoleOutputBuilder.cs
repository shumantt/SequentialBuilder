using SequentialBuilder.Attributes;

namespace SequentialBuilder.Sample
{
    [Builder]
    public partial class GeneratedConsoleOutputBuilder
    {
        private string name;
        private string mainText;
        private string signature;
        
        public ConsoleOutput Build()
        {
            return new ConsoleOutput($"Hello, {name}! {mainText}. {signature}");
        }
    }
}