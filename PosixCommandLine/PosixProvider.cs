using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using PosixCommandline.Options;

namespace PosixCommandline
{
    internal class PosixProvider : ConfigurationProvider
    {
        private readonly IEnumerable<Option> _options;
        private readonly Dictionary<string, string> _aliases;

        public PosixProvider(IEnumerable<Option> options, Dictionary<string, string> aliases)
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
                    Set($"{option.Key}:{count++}", o.ToString());
                }
            }
        }

        private static string LookupKey(string key, IReadOnlyDictionary<string, string> aliases) =>
            aliases.TryGetValue(key, out var alias) 
                ? alias 
                : key.Replace("-", "");

        public override bool TryGet(string key, out string value) => 
            base.TryGet(key, out value) || base.TryGet(key + ":0", out value); // Lookup first value when not array
    }
}