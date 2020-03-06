using System;
using CommandLiners;
using FluentAssertions;
using PosixCommandline.Tests.Options;
using Xunit;

namespace PosixCommandline.Tests
{
    public static class MapTests
    {
        [Fact]
        public static void Nested() =>
            new Map<Triple>()
                .Add("f", options => options.Nested.Options.A)
                .Mappings
                .Should()
                .ContainKey("f")
                .WhichValue
                .Should()
                .Be("Nested:Options:A");
        
        [Fact]
        public static void Property() =>
            new Map<Simple>()
                .Add("f", options => options.A)
                .Mappings
                .Should()
                .ContainKey("f")
                .WhichValue
                .Should()
                .Be("A");

        [Fact]
        public static void Method() =>
            new Map<int>()
                .Invoking(m => m.Add("f", options => options.Equals(3)))
                .Should()
                .Throw<ArgumentException>();
    }
}