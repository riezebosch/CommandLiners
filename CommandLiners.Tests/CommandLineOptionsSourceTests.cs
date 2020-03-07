using CommandLiners.Options;
using CommandLiners.Tests.Options;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace CommandLiners.Tests
{
    public static class CommandLineOptionsSourceTests
    {
        [Fact]
        public static void Multiple()
        {
            var source = new CommandLineOptionsSource(new[] { new OptionArgument("Multiple", "1"), new OptionArgument("Multiple", "2") });
            var builder = new ConfigurationBuilder()
                .Add(source)
                .Build();
            
            var options =  new Simple();
            builder.Bind(options);

            options
                .Multiple
                .Should()
                .BeEquivalentTo("1", "2");
        }
    }
}