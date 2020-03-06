using CommandLiners;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Mono.Options;
using MonoOptionsCommandLine.Tests.Options;
using Xunit;

namespace MonoOptionsCommandLine.Tests
{
    public static class ProviderTests
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
                .AddMonoOptions(map)
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
                .AddMonoOptions(map)
                .Build();

            var options = new Nested();
            builder.Bind(options);

            options
                .Options
                .Multiple
                .Should()
                .BeEquivalentTo("foo", "other");
        }
    }
}