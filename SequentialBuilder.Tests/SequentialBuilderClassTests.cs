using FluentAssertions;
using SequentialBuilder.Model;
using Xunit;

namespace SequentialBuilder.Tests
{
    public class SequentialBuilderClassTests : GeneratorTestBase
    {
        [Fact]
        public void TestGetFields()
        {
            var (compilation, classDeclarationSyntax) = PrepareData(@"
using SequentialBuilder.Attributes;
using System;

[SequentialBuilder]
class TestClass 
{
    private int field1;

    private int field2;
}");
            var sequentialBuilder = classDeclarationSyntax.TryConvertToBuilder(compilation);

            var actual = sequentialBuilder!.GetFields();

            actual.Should().BeEquivalentTo(
                new BuilderFieldInfo("field1", "Int32"),
                new BuilderFieldInfo("field2", "Int32")
            );
        }
    }
}