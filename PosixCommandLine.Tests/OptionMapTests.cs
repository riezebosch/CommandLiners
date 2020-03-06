using System;
using FluentAssertions;
using Xunit;

namespace PosixCommandline.Tests
{
    public static class OptionMapTests
    {
        [Fact]
        public static void Nested() =>
            new OptionMap<Triple>()
                .Add("f", options => options.Nested.Options.A)
                .Mappings
                .Should()
                .ContainKey("f")
                .WhichValue
                .Should()
                .Be("Nested:Options:A");
        
        [Fact]
        public static void Property() =>
            new OptionMap<Options>()
                .Add("f", options => options.A)
                .Mappings
                .Should()
                .ContainKey("f")
                .WhichValue
                .Should()
                .Be("A");

        [Fact]
        public static void Method() =>
            new OptionMap<int>()
                .Invoking(m => m.Add("f", options => options.Equals(3)))
                .Should()
                .Throw<ArgumentException>();
    }
}