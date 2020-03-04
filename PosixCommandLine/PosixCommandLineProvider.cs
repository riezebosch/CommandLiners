using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace CommandlineMultiple.Tests
{
    public class PosixCommandLineProvider : ConfigurationProvider
    {
        private readonly IEnumerable<string> _args;
        private readonly Dictionary<string, string> _alias;

        public PosixCommandLineProvider(IEnumerable<string> args, Dictionary<string, string> alias)
        {
            _args = args;
            _alias = alias ?? new Dictionary<string, string>();
        }

        public override void Load()
        {
            var keys = new Dictionary<string, int>();
            var args = new Queue<string>(_args);
            
            while (args.Any())
            {
                var key = ToKey(args.Dequeue());
                if (!args.Any() || args.Peek().StartsWith("--"))
                {
                    Data[key] = true.ToString();
                    continue;
                }
            
                var value = args.Dequeue();
                keys.TryGetValue(key, out var count);
                keys[key] = count + 1;
                
                Data[$"{key}:{count}"] = value;
            }
        }

        private string ToKey(string value)
        {
            if (value.StartsWith("--"))
            {
                return value.Replace("-", "");
            }

            return value.StartsWith("-") ? _alias[value.Substring(1)] : value;
        }

        public override bool TryGet(string key, out string value) => 
            base.TryGet(key, out value) || base.TryGet(key + ":0", out value);

        public override IEnumerable<string> GetChildKeys(IEnumerable<string> earlierKeys, string parentPath)
        {
            parentPath ??= string.Empty;

            return Multiple(parentPath)
                .Concat(Children(parentPath)
                .Concat(earlierKeys));
        }

        private IEnumerable<string> Children(string parentPath)
        {
            return _args
                .Where(x => x.StartsWith($"--{parentPath}", StringComparison.OrdinalIgnoreCase))
                .Select(x => x.Substring(2 + parentPath.Length))
                .Where(x => !string.IsNullOrEmpty(x));
        }

        private IEnumerable<string> Multiple(string parentPath)
        {
            var count = _args
                .Select(x => x.Substring(2))
                .Count(x => x.Equals(parentPath, StringComparison.OrdinalIgnoreCase));

            return Enumerable.Range(0, count).Select(x => x.ToString());
        }
    }
}