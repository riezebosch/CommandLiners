using System.Collections.Generic;
using System.Linq;
using CommandLiners.Options;
using Microsoft.Extensions.Configuration;

namespace CommandLiners
{
    public class CommandLineOptionsProvider : ConfigurationProvider
    {
        private readonly IEnumerable<Option> _options;

        public CommandLineOptionsProvider(IEnumerable<Option> options) => 
            _options = options;

        public override void Load()
        {
            foreach (var option in _options.GroupBy(x => x.Name))
            {
                var count = 0;
                foreach (var o in option)
                {
                    // Store everything as potential array
                    Set($"{option.Key}:{count++}", o.Value);
                }
            }
        }

        public override bool TryGet(string key, out string value) => 
            base.TryGet(key, out value) || base.TryGet(key + ":0", out value); // Lookup first value when not array
    }
}