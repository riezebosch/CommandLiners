using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace CommandlineMultiple.Tests
{
    public static class ConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder AddPosixCommandLine(this IConfigurationBuilder builder, string[] args, Dictionary<string, string> alias = null) =>
            builder.Add(new PosixCommandLineSource(args, alias));
    }
}