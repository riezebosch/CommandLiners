using System.Linq;
using CommandLiners.Options;
using FluentAssertions;
using Xunit;

namespace CommandLiners.Tests
{
    public static class CommandLineOptionsProviderTests
    {
        [Fact]
        public static void Multiple()
        {
            var provider = new CommandLineOptionsProvider(new[] { new OptionArgument("a", "1"), new OptionArgument("a", "2") });
            provider.Load();
            
            provider
                .GetChildKeys(Enumerable.Empty<string>(), null)
                .Should()
                .BeEquivalentTo("a", "a");
            
            provider
                .GetChildKeys(Enumerable.Empty<string>(), "a")
                .Should()
                .BeEquivalentTo("0", "1");
        }
        
        [Fact]
        public static void Values()
        {
            var provider = new CommandLineOptionsProvider(new[] { new OptionArgument("a", "1"), new OptionArgument("a", "2") });
            provider.Load();

            provider
                .TryGet("a:1", out var value)
                .Should()
                .BeTrue();

            value
                .Should()
                .Be("2");
        }
        
        [Fact]
        public static void Single()
        {
            var provider = new CommandLineOptionsProvider(new[] { new Option("a") });
            provider.Load();
            
            provider
                .GetChildKeys(Enumerable.Empty<string>(), null)
                .Should()
                .BeEquivalentTo("a");
        }
        
        [Fact]
        public static void Value()
        {
            var provider = new CommandLineOptionsProvider(new[] { new Option("a") });
            provider.Load();

            provider
                .TryGet("a", out var value)
                .Should()
                .BeTrue();

            value
                .Should()
                .Be("True");
        }
        
        [Fact]
        public static void Operands()
        {
            var provider = new CommandLineOptionsProvider(new[] { new Operand("a") });
            provider.Load();

            provider
                .TryGet("", out var value)
                .Should()
                .BeTrue();

            value
                .Should()
                .Be("a");
        }
    }
}