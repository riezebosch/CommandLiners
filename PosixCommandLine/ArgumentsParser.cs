using System.Collections.Generic;
using System.Linq;
using CommandLiners.Options;

namespace CommandLiners
{
    public static class ArgumentsParser
    {
        public static IEnumerable<Option> ToOptions(this IEnumerable<string> args, IDictionary<string, string> aliases = null)
        {
            aliases ??= new Dictionary<string, string>();
            
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
                    option.Name = aliases.TryGetValue(option.Name, out var name) ? name : option.Name.Replace("-", "");
                    yield return option;
                }
            }
        }

        private static IEnumerable<Operand> ToOperand(string arg)
        {
            yield return new Operand(arg);
        }

        private static IEnumerable<Operand> ToOperands(Queue<string> queue)
        {
            while (queue.Any())
            {
                yield return new Operand(queue.Dequeue());
            }
        }

        private static IEnumerable<Option> FromShort(string keys, Queue<string> args)
        {
            if (keys.Length > 1)
            {
                foreach (var key in keys)
                {
                    yield return new Option(key.ToString());
                }
            }
            else
            {
                yield return OptionalArgument(keys, args);
            }
        }

        private static IEnumerable<Option> FromLong(string key, Queue<string> args)
        {
            yield return InlineArgument(key) ?? OptionalArgument(key, args);
        }

        private static Option InlineArgument(string key)
        {
            var separator = key.IndexOf('=');
            return separator > 0 
                ? new OptionArgument(key.Substring(0, separator), key.Substring(separator + 1))
                : null;
        }

        private static Option OptionalArgument(string key, Queue<string> args) => 
            IsNextArgument(args) 
                ? new OptionArgument(key,  args.Dequeue())
                : new Option(key);

        private static bool IsNextArgument(Queue<string> args) => 
            args.Any() && !args.Peek().StartsWith("-");
    }
}