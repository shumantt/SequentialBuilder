
using SequentialBuilder.Attributes;

namespace SequentialBuilder.Sample
{
    [SimpleBuilder]
    public partial class GeneratedConsoleOutputBuilder
    {
        private string name;
        private string mainText;
        private string signature;
        
        public ConsoleOutput Build()
        {
            return new($"Hello, {name}! {mainText}. {signature}");
        }
    }
}