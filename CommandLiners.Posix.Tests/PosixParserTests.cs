using CommandLiners;
using CommandLiners.Options;
using FluentAssertions;
using PosixCommandline.Tests.Options;
using Xunit;

namespace PosixCommandline.Tests
{
    public static class PosixParserTests
    {
        [Fact]
        public static void LongValue() =>
            new[]{"--my-value", "my-value"}.ToPosix()
                .Should()
                .BeEquivalentTo(new OptionArgument("myvalue", "my-value"));

        [Fact]
        public static void LongInlineValue() =>
            new[]{"--my-value=my-value"}.ToPosix()
                .Should()
                .BeEquivalentTo(new OptionArgument("myvalue", "my-value"));

        [Fact]
        public static void LongNoInline() =>
            new[]{"--=my-value"}.ToPosix()
                .Should()
                .BeEquivalentTo(new Option("=myvalue"));

        [Fact]
        public static void Option() =>
            new[]{"--my-value"}.ToPosix()
                .Should()
                .BeEquivalentTo(new Option("myvalue"));

        [Fact]
        public static void Options() =>
            new[]{"--my-value", "--my-other"}.ToPosix()
                .Should()
                .BeEquivalentTo(
                    new Option("myvalue"),
                    new Option("myother"));

        [Fact]
        public static void Short() =>
            new[]{"-m", "my-value"}.ToPosix()
                .Should()
                .BeEquivalentTo(new OptionArgument("m", "my-value"));

        [Fact]
        public static void Shorts() =>
            new[]{"-mabc" }.ToPosix()
                .Should()
                .BeEquivalentTo(
                    new Option("m"),
                    new Option("a"),
                    new Option("b"),
                    new Option("c")
                );

        [Fact]
        public static void Combined() =>
            new[]{"-abc", "--d" }.ToPosix()
                .Should()
                .BeEquivalentTo(
                    new Option("a"),
                    new Option("b"),
                    new Option("c"),
                    new Option("d")
                );

        [Fact]
        public static void No() =>
            new[]{"abc" }.ToPosix()
                .Should()
                .BeEquivalentTo(new Operand("abc"));
        
        [Fact]
        public static void Delimiter() =>
            new[]{"-a", "--", "-abc" }.ToPosix()
                .Should()
                .BeEquivalentTo(
                    new Option("a"),
                    new Operand("-abc"));
        
        [Fact]
        public static void Alias() =>
            new[] {"-a", "my-value"}.ToPosix<Simple>(map => map.Add("a", x => x.Flag))
                .Should()
                .BeEquivalentTo(new OptionArgument("Flag", "my-value"));
    }
}