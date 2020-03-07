using System.Collections.Generic;
using System.Linq;
using CommandLiners.Options;

namespace CommandLiners
{
    public class PosixParser
    {
        private readonly MapOptions _map;

        public PosixParser(MapOptions map) => _map = map;

        public IEnumerable<Option> ToOptions(IEnumerable<string> args)
        {
            var queue = new Queue<string>(args);
            while (queue.Any())
            {
                var arg = queue.Dequeue();
                IEnumerable<Option> options;
                
                if (arg == "--") // Guideline 10
                {
                    options = ToOperands(queue);
                }
                else if (arg.StartsWith("--"))
                {
                    options = FromLong(arg.Substring(2), queue);
                }
                else if (arg.StartsWith("-"))
                {
                    options = FromShort(arg.Substring(1), queue);
                }
                else
                {
                    options = ToOperand(arg);
                }

                foreach (var option in options)
                {
                    yield return option;
                }
            }
        }

        private IEnumerable<Option> ToOperand(string arg)
        {
            yield return new OptionArgument(_map.ToName(""), arg);
        }

        private IEnumerable<Operand> ToOperands(Queue<string> queue)
        {
            while (queue.Any())
            {
                yield return new Operand(queue.Dequeue());
            }
        }

        private IEnumerable<Option> FromShort(string keys, Queue<string> args)
        {
            if (keys.Length > 1)
            {
                foreach (var key in keys)
                {
                    yield return new Option(_map.ToName(key.ToString()));
                }
            }
            else
            {
                yield return OptionalArgument(keys, args);
            }
        }

        private IEnumerable<Option> FromLong(string key, Queue<string> args)
        {
            yield return InlineArgument(key) ?? OptionalArgument(key, args);
        }

        private Option InlineArgument(string key)
        {
            var separator = key.IndexOf('=');
            return separator > 0 
                ? new OptionArgument(_map.ToName(key.Substring(0, separator)), key.Substring(separator + 1))
                : null;
        }

        private Option OptionalArgument(string key, Queue<string> args) => 
            IsNextArgument(args) 
                ? new OptionArgument(_map.ToName(key),  args.Dequeue())
                : new Option(_map.ToName(key));

        private static bool IsNextArgument(Queue<string> args) => 
            args.Any() && !args.Peek().StartsWith("-");
    }
}