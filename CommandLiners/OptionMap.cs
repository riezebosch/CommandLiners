using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CommandLiners
{
    public class OptionMap<T>
    {
        public IDictionary<string, string> Mappings { get; } = new Dictionary<string, string>();

        public OptionMap<T> Add<TResult>(string alias, Expression<Func<T, TResult>> to)
        {
            Mappings[alias] = PropertySelector.Do(to);
            return this;
        }

        public OptionMap<T> MapOperands<TResult>(Expression<Func<T, TResult>> to) =>
            Add("", to);
    }
}