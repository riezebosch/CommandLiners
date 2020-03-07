using System.Collections.Generic;
using CommandLiners.Options;
using Microsoft.Extensions.Configuration;

namespace CommandLiners
{
    public static class ConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder AddCommandLineOptions<T>(this IConfigurationBuilder builder, Map<T> options) =>
            builder.Add(new CommandLineOptionsSource(options.Options));
        
        public static IConfigurationBuilder AddCommandLineOptions(this IConfigurationBuilder builder, IEnumerable<Option> options) =>
            builder.Add(new CommandLineOptionsSource(options));
    }
}