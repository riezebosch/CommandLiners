using System.Collections.Generic;
using CommandLiners.Options;
using Microsoft.Extensions.Configuration;

namespace CommandLiners
{
    public static class ConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder AddCommandLineOptions(this IConfigurationBuilder builder, IEnumerable<Option> options) =>
            builder.Add(new CommandLineOptionsSource(options));
    }
}