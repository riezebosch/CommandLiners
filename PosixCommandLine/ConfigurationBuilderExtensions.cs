using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace CommandLiners
{
    public static class ConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder AddPosixCommandLine(this IConfigurationBuilder builder, IEnumerable<string> args, IDictionary<string, string> aliases = null) =>
            builder.Add(new CommandLineOptionsSource(args.ToOptions(), aliases));
    }
}