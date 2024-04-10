using CommandLiners.Options;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Mono.Options;
using Xunit;

namespace CommandLiners.MonoOptions.Tests;

public static class MapOptionsTests
{
    [Fact]
    public static void Test()
    {
        var map = new MapOptions<Simple>();
        var set = new OptionSet
        {
            {"f|files=", data => map.Add(data, x => x.Multiple)}
        };
        set.Parse(new[] {"--files", "foo", "asdf"});
            
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
    public static void Map()
    {
        var map = new MapOptions<Simple>();
        new OptionSet
        {
            {"f|files=", data => map.Add(data, x => x.Multiple)}
        }.Parse(new[] {"--files", "foo", "asdf"});

        map
            .Data
            .Should()
            .BeEquivalentTo(new OptionArgument("Multiple", "foo"));
    }

    private class Simple
    {
        public string[] Multiple { get; set; }
    }

}