using System;

namespace SequentialBuilder.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var output = ConsoleOutputBuilder
                .Start()
                .WithName("Andrey")
                .WithMainText("Wish you all the best")
                .WithSignature("Me")
                .Build();
            
            Console.WriteLine(output);
            var generatedOutput = new GeneratedConsoleOutputBuilder()
                .WithName("Andrey")
                .WithMaintext("Wish you all the best")
                .WithSignature("Me")
                .Build();
            
            Console.WriteLine(generatedOutput);
        }
    }
}