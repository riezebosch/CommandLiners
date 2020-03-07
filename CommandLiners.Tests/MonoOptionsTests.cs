using CommandLiners.Options;
using CommandLiners.Tests.Options;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Mono.Options;
using Xunit;

namespace CommandLiners.Tests
{
    public static class MonoOptionsTests
    {
        [Fact]
        public static void Simple()
        {
            var map = new Map<Simple>();
            new OptionSet
            {
                {"f|files=", data => map.Add(data, x => x.Multiple)}
            }.Parse(new[] {"--files", "foo", "asdf"});
            
            var builder = new ConfigurationBuilder()
                .AddCommandLineOptions(map)
                .Build();

            var options = new Simple();
            builder.Bind(options);

            options
                .Multiple
                .Should()
                .BeEquivalentTo("foo");
        }
        
        [Fact]
        public static void Nested()
        {
            var map = new Map<Nested>();
            new OptionSet
            {
                {"f|files=", data => map.Add(data, x => x.Options.Multiple)}
            }.Parse(new[] {"--files", "foo", "-f", "other"});
            
            var builder = new ConfigurationBuilder()
                .AddCommandLineOptions(map)
                .Build();

            var options = new Nested();
            builder.Bind(options);

            options
                .Options
                .Multiple
                .Should()
                .BeEquivalentTo("foo", "other");
        }

        [Fact]
        public static void Map()
        {
            var map = new Map<Simple>();
            new OptionSet
            {
                {"f|files=", data => map.Add(data, x => x.Multiple)}
            }.Parse(new[] {"--files", "foo", "asdf"});

            map
                .Options
                .Should()
                .BeEquivalentTo(new OptionArgument("Multiple", "foo"));
        }
    }
}