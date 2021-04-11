using System;

namespace SequentialBuilder.Sample
{
    public class ConsoleOutputBuilder : INameConsoleOutputBuilder, IMainTextConsoleOutputBuilder, ISignatureConsoleOutputBuilder, IConsoleOutputBuilder
    {
        private string name;
        private string mainText;
        private string signature;

        private ConsoleOutputBuilder()
        {
            
        }

        public static INameConsoleOutputBuilder Start() => new ConsoleOutputBuilder();

        public IMainTextConsoleOutputBuilder WithName(string name)
        {
            this.name = name;
            return this;
        }

        public ISignatureConsoleOutputBuilder WithMainText(string mainText)
        {
            this.mainText = mainText;
            return this;
        }

        public IConsoleOutputBuilder WithSignature(string signature)
        {
            this.signature = signature;
            return this;
        }

        public ConsoleOutput Build()
        {
            return new ConsoleOutput($"Hello, {name}! {mainText}. {signature}");
        }
    }

    public interface INameConsoleOutputBuilder
    {
        IMainTextConsoleOutputBuilder WithName(string name);
    }
    
    public interface IMainTextConsoleOutputBuilder
    {
        ISignatureConsoleOutputBuilder WithMainText(string mainText);
    }

    public interface ISignatureConsoleOutputBuilder
    {
        IConsoleOutputBuilder WithSignature(string signature);
    }

    public interface IConsoleOutputBuilder
    {
        ConsoleOutput Build();
    }
}