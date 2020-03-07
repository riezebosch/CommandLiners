using System;
using CommandLiners;
using FluentAssertions;
using PosixCommandline.Tests.Options;
using Xunit;

namespace PosixCommandline.Tests
{
    public static class MapOptionsTests
    {
        [Fact]
        public static void Nested() =>
            new MapOptions<Triple>()
                .Add("f", options => options.Nested.Options.A)
                .ToName("f")
                .Should()
                .Be("Nested:Options:A");
        
        [Fact]
        public static void Property() =>
            new MapOptions<Simple>()
                .Add("f", options => options.A)
                .ToName("f")
                .Should()
                .Be("A");
        
        [Fact]
        public static void Default() =>
            new MapOptions<Simple>()
                .ToName("f")
                .Should()
                .Be("f");
        
        [Fact]
        public static void Compound() =>
            new MapOptions<Simple>()
                .ToName("compound-word")
                .Should()
                .Be("compoundword");

        [Fact]
        public static void Method() =>
            new MapOptions<int>()
                .Invoking(m => m.Add("f", options => options.Equals(3)))
                .Should()
                .Throw<ArgumentException>();
    }
}