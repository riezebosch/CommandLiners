using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace PosixCommandline
{
    internal class PosixSource : IConfigurationSource
    {
        private readonly string[] _args;
        private readonly Dictionary<string, string> _aliases;

        public PosixSource(string[] args, Dictionary<string, string> aliases)
        {
            _args = args;
            _aliases = aliases;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder) => 
            new PosixProvider(_args.ToOptions(), _aliases);
    }
}