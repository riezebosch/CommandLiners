using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace PosixCommandline
{
    public static class ConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder AddPosixCommandLine(this IConfigurationBuilder builder, string[] args, IDictionary<string, string> aliases = null) =>
            builder.Add(new PosixSource(args, aliases));
    }
}