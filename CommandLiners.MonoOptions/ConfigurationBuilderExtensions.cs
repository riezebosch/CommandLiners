using Microsoft.Extensions.Configuration;

namespace CommandLiners
{
    public static class ConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder AddCommandLineOptions<T>(this IConfigurationBuilder builder, MapOptions<T> options) =>
            builder.Add(new CommandLineOptionsSource(options.Data));
    }
}