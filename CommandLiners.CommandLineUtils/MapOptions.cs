using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CommandLiners.Options;
using McMaster.Extensions.CommandLineUtils;

namespace CommandLiners
{
    public class MapOptions<TOptions>
    {
        private readonly IDictionary<CommandOption, string> _options = new Dictionary<CommandOption, string>();
        private readonly IDictionary<CommandArgument, string> _arguments = new Dictionary<CommandArgument, string>();

        public void Add<TResult>(CommandOption option, Expression<Func<TOptions, TResult>> to) =>
            _options[option] = PropertySelector.Do(to);

        public void Add<TResult>(CommandArgument arg, Expression<Func<TOptions, TResult>> to) =>
            _arguments[arg] = PropertySelector.Do(to);

        public IEnumerable<Option> FromCommand(CommandLineApplication cmd) => 
            cmd.Options
                .SelectMany(ToOption)
                .Concat(cmd.Arguments.SelectMany(ToOption));

        private IEnumerable<OptionArgument> ToOption(CommandArgument arg) => 
            arg.Values.Select(value => new OptionArgument(_arguments[arg], value));

        private IEnumerable<Option> ToOption(CommandOption option)
        {
            var name = ToName(option);
            return option.OptionType switch
            {
                CommandOptionType.NoValue => new[] { new Option(name) },
                CommandOptionType.SingleOrNoValue => option.HasValue() ? new[] {  ToOptionalArgument(option, name) } : Enumerable.Empty<Option>(),
                _ => option.Values.Select(value => new OptionArgument(name, value))
            };
        }

        private static Option ToOptionalArgument(CommandOption option, string name) => 
            option.Value() != null 
                ? new OptionArgument(name, option.Value()) 
                : new Option(name);

        private string ToName(CommandOption option) => 
            _options.TryGetValue(option, out var name) 
                ? name 
                : option.LongName ?? option.ShortName;
    }
}