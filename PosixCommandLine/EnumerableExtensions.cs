using System;
using System.Collections.Generic;
using CommandLiners.Options;

namespace CommandLiners
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<Option> ToPosix(this IEnumerable<string> args) =>
            new PosixParser(new MapOptions()).ToOptions(args);
        
        public static IEnumerable<Option> ToPosix<TOptions>(this IEnumerable<string> args, Func<MapOptions<TOptions>, MapOptions<TOptions>> map) =>
            new PosixParser(map(new MapOptions<TOptions>())).ToOptions(args);
        
    }
}