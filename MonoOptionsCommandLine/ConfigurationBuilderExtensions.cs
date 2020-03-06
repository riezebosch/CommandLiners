using Microsoft.Extensions.Configuration;

namespace CommandLiners
{
    public static class ConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder AddMonoOptions<T>(this IConfigurationBuilder builder, Map<T> options) =>
            builder.Add(new CommandLineOptionsSource(options.Options, null));
    }
}