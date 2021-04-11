namespace SequentialBuilder.Sample
{
    public class ConsoleOutput
    {
        public string Text { get; }

        public ConsoleOutput(string text)
        {
            Text = text;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}