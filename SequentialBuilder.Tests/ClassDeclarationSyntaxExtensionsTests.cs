using System;
using System.Linq;
using FluentAssertions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SequentialBuilder.Model;
using Xunit;

namespace SequentialBuilder.Tests
{
    public class ClassDeclarationSyntaxExtensionsTests : GeneratorTestBase
    {
        [Fact]
        public void TestConvertNotBuilderClass()
        {
            var (compilation, classDeclarationSyntax) = PrepareData(@"
class TestClass 
{
    void TestMethod ()
    {
        int i;
    }
}");

            var actual = classDeclarationSyntax.TryConvertToBuilder(compilation);

            actual.Should().BeNull();
        }

        [Fact]
        public void TestConvertSimpleBuilderClass()
        {
            var (compilation, classDeclarationSyntax) = PrepareData(@"
using SequentialBuilder.Attributes;
using System;

[SimpleBuilder]
class TestClass 
{
    private int field1;
}");
            var actual = classDeclarationSyntax.TryConvertToBuilder(compilation);

            actual.Should().NotBeNull();
            actual.Should().BeOfType<SimpleBuilderClass>();
            actual!.Name.Should().Be("TestClass");
            actual!.UsedNamespaces.Should().BeEquivalentTo("SequentialBuilder.Attributes", "System");
        }

        [Fact]
        public void TestConvertSequentialBuilderClass()
        {
            var (compilation, classDeclarationSyntax) = PrepareData(@"
using SequentialBuilder.Attributes;
using System;

[SequentialBuilder]
class TestClass 
{
    private int field1;
}");
            var actual = classDeclarationSyntax.TryConvertToBuilder(compilation);

            actual.Should().NotBeNull();
            actual.Should().BeOfType<SequentialBuilderClass>();
            actual!.Name.Should().Be("TestClass");
            actual!.UsedNamespaces.Should().BeEquivalentTo("SequentialBuilder.Attributes", "System");
        }
    }
}