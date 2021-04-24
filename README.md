# SequentialBuilder

Simple C#  source generator which allows building builder classes without handwriting boilerplate code.

## Usage

Two types of builder generators are implemented:

1. SimpleBuilder - generates simple set methods for any field in a class or for annotated with `[BuilderField]` fields.

Builder class:
```c#
[SimpleBuilder]
public partial class GeneratedConsoleOutputBuilder
{
    private string name;
    private string mainText;
    private string signature;
        
    public string Build()
    {
        return $"Hello, {name}! {mainText}. {signature}";
    }
}
```
Usage 
```c
var generatedOutput = new GeneratedConsoleOutputBuilder()
                .WithName("Andrey")
                .WithMainText("Wish you all the best")
                .WithSignature("Me")
                .Build();

Console.WriteLine(generatedOutput);
```

Generated code
```c#
using System;

namespace SequentialBuilder.Sample
{
    public partial class GeneratedConsoleOutputBuilder
    {
        public GeneratedConsoleOutputBuilder WithName(String name)
        {
            this.name = name;
            return this;
        }

        public GeneratedConsoleOutputBuilder WithMainText(String mainText)
        {
            this.mainText = mainText;
            return this;
        }

        public GeneratedConsoleOutputBuilder WithSignature(String signature)
        {
            this.signature = signature;
            return this;
        }
    }
}
```

2. Sequential Builder - generates code builder class which methods should be called in exact order. It is implemented using interfaces. Every builder method returns an interface with a single method. This method sets the next field of builder a class.

```c#
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
```

Usage same as above.


Generated code

```c#
using System;

namespace SequentialBuilder.Sample
{
    public partial class GeneratedSequentialConsoleOutputBuilder :
        GeneratedSequentialConsoleOutputBuilder.INameGeneratedSequentialConsoleOutputBuilder,
        GeneratedSequentialConsoleOutputBuilder.IMainTextGeneratedSequentialConsoleOutputBuilder,
        GeneratedSequentialConsoleOutputBuilder.ISignatureGeneratedSequentialConsoleOutputBuilder
    {
        public IMainTextGeneratedSequentialConsoleOutputBuilder WithName(String name)
        {
            this.name = name;
            return this;
        }

        public interface INameGeneratedSequentialConsoleOutputBuilder
        {
            IMainTextGeneratedSequentialConsoleOutputBuilder WithName(String name);
        }
        
        public ISignatureGeneratedSequentialConsoleOutputBuilder WithMainText(String mainText)
        {
            this.mainText = mainText;
            return this;
        }
        
        public interface IMainTextGeneratedSequentialConsoleOutputBuilder
        {
            ISignatureGeneratedSequentialConsoleOutputBuilder WithMainText(String mainText);
        }

        public GeneratedSequentialConsoleOutputBuilder WithSignature(String signature)
        {
            this.signature = signature;
            return this;
        }
        
        public interface ISignatureGeneratedSequentialConsoleOutputBuilder
        {
            GeneratedSequentialConsoleOutputBuilder WithSignature(String signature);
        }
    }
}
```

The main profit of a such builder is that the method calls' order is ensured by the type system.