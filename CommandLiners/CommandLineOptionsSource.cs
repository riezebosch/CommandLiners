using System.Collections.Generic;
using CommandLiners.Options;
using Microsoft.Extensions.Configuration;

namespace CommandLiners
{
    public class CommandLineOptionsSource : IConfigurationSource
    {
        private readonly IEnumerable<Option> _args;

        public CommandLineOptionsSource(IEnumerable<Option> args) => _args = args;

        public IConfigurationProvider Build(IConfigurationBuilder builder) => 
            new CommandLineOptionsProvider(_args);
    }
}