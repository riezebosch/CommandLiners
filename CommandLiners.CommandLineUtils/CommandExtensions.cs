using System;
using System.Linq.Expressions;
using McMaster.Extensions.CommandLineUtils;

namespace CommandLiners
{
    public static class CommandExtensions
    {
        public static CommandOption Map<TOptions, TResult>(this CommandOption option, MapOptions<TOptions> map,
            Expression<Func<TOptions, TResult>> to)
        {
            map.Add(option, to);
            return option;
        }
        
        public static CommandArgument Map<TOptions, TResult>(this CommandArgument arg, MapOptions<TOptions> map,
            Expression<Func<TOptions, TResult>> to)
        {
            map.Add(arg, to);
            return arg;
        }
    }
}