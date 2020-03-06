using System.Linq;
using CommandLiners;
using CommandLiners.Options;
using FluentAssertions;
using Xunit;

namespace PosixCommandline.Tests
{
    public static class ArgumentsParserTests
    {
        [Fact]
        public static void LongValue() =>
            new[]{"--my-value", "my-value"}
                .ToOptions()
                .Should()
                .BeEquivalentTo(new OptionArgument("my-value", "my-value"));

        [Fact]
        public static void LongInlineValue() =>
            new[]{"--my-value=my-value"}
                .ToOptions()
                .Should()
                .BeEquivalentTo(new OptionArgument("my-value", "my-value"));

        [Fact]
        public static void LongNoInline() =>
            new[]{"--=my-value"}
                .ToOptions()
                .Should()
                .BeEquivalentTo(new Option("=my-value"));

        [Fact]
        public static void Option() =>
            new[]{"--my-value"}
                .ToOptions()
                .Should()
                .BeEquivalentTo(new Option("my-value"));

        [Fact]
        public static void Options() =>
            new[]{"--my-value", "--my-other"}
                .ToOptions()
                .Should()
                .BeEquivalentTo(
                    new Option("my-value"),
                    new Option("my-other"));

        [Fact]
        public static void Short() =>
            new[]{"-m", "my-value"}
                .ToOptions()
                .Should()
                .BeEquivalentTo(new OptionArgument("m", "my-value"));

        [Fact]
        public static void Shorts() =>
            new[]{"-mabc" }
                .ToOptions()
                .Should()
                .BeEquivalentTo(
                    new Option("m"),
                    new Option("a"),
                    new Option("b"),
                    new Option("c")
                );

        [Fact]
        public static void Combined() =>
            new[]{"-abc", "--d" }
                .ToOptions()
                .Should()
                .BeEquivalentTo(
                    new Option("a"),
                    new Option("b"),
                    new Option("c"),
                    new Option("d")
                );

        [Fact]
        public static void No() =>
            new[]{"abc" }
                .ToOptions()
                .Should()
                .BeEquivalentTo(new Operand("abc"));
        
        [Fact]
        public static void Delimter() =>
            new[]{"-a", "--", "-abc" }
                .ToOptions()
                .Should()
                .BeEquivalentTo(
                    new Option("a"),
                    new Operand("-abc"));
    }
}