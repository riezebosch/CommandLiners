using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace PosixCommandline
{
    internal class PosixSource : IConfigurationSource
    {
        private readonly string[] _args;
        private readonly IDictionary<string, string> _aliases;

        public PosixSource(string[] args, IDictionary<string, string> aliases)
        {
            _args = args;
            _aliases = aliases;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder) => 
            new PosixProvider(_args.ToOptions(), _aliases);
    }
}