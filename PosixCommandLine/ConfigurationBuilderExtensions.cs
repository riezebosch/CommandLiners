using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace CommandLiners
{
    public static class ConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder AddPosixCommandLine(this IConfigurationBuilder builder, IEnumerable<string> args) =>
            builder.Add(new CommandLineOptionsSource(args.ToOptions()));
        
        public static IConfigurationBuilder AddPosixCommandLine(this IConfigurationBuilder builder, IEnumerable<string> args, Map map) =>
            builder.Add(new CommandLineOptionsSource(args.ToOptions(map.Mappings)));
    }
}