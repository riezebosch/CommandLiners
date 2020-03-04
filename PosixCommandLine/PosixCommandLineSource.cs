using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace CommandlineMultiple.Tests
{
    public class PosixCommandLineSource : IConfigurationSource
    {
        private readonly string[] _args;
        private readonly Dictionary<string, string> _alias;

        public PosixCommandLineSource(string[] args, Dictionary<string, string> alias)
        {
            _args = args;
            _alias = alias;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder) => 
            new PosixCommandLineProvider(_args, _alias);
    }
}