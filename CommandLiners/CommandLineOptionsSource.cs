using System.Collections.Generic;
using CommandLiners.Options;
using Microsoft.Extensions.Configuration;

namespace CommandLiners
{
    public class CommandLineOptionsSource : IConfigurationSource
    {
        private readonly IEnumerable<Option> _args;
        private readonly IDictionary<string, string> _aliases;

        public CommandLineOptionsSource(IEnumerable<Option> args, IDictionary<string, string> aliases)
        {
            _args = args;
            _aliases = aliases;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder) => 
            new CommandLineOptionsProvider(_args, _aliases);
    }
}