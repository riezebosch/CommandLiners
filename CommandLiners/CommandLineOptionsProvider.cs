using System.Collections.Generic;
using System.Linq;
using CommandLiners.Options;
using Microsoft.Extensions.Configuration;

namespace CommandLiners
{
    public class CommandLineOptionsProvider : ConfigurationProvider
    {
        private readonly IEnumerable<Option> _options;
        private readonly IDictionary<string, string> _aliases;

        public CommandLineOptionsProvider(IEnumerable<Option> options, IDictionary<string, string> aliases)
        {
            _options = options;
            _aliases = aliases ?? new Dictionary<string, string>();
        }

        public override void Load()
        {
            foreach (var option in _options.GroupBy(x => LookupKey(x.Name, _aliases)))
            {
                var count = 0;
                foreach (var o in option)
                {
                    // Store everything as potential array
                    Set($"{option.Key}:{count++}", o.Value);
                }
            }
        }

        private static string LookupKey(string key, IDictionary<string, string> aliases) =>
            aliases.TryGetValue(key, out var alias) 
                ? alias 
                : key.Replace("-", "");

        public override bool TryGet(string key, out string value) => 
            base.TryGet(key, out value) || base.TryGet(key + ":0", out value); // Lookup first value when not array
    }
}