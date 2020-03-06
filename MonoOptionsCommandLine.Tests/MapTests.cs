using CommandLiners;
using CommandLiners.Options;
using FluentAssertions;
using Mono.Options;
using MonoOptionsCommandLine.Tests.Options;
using Xunit;

namespace MonoOptionsCommandLine.Tests
{
    public static class MapTests
    {
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
                .BeEquivalentTo(new OptionArgument("Multiple","foo"));
        }
    }
}