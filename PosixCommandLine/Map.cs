using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CommandLiners
{
    public class Map
    {
        public IDictionary<string, string> Mappings { get; } = new Dictionary<string, string>();
    }

    public class Map<T> : Map
    {
        public Map<T> Add<TResult>(string alias, Expression<Func<T, TResult>> to)
        {
            Mappings[alias] = PropertySelector.Do(to);
            return this;
        }

        public Map<T> Operands<TResult>(Expression<Func<T, TResult>> to) =>
            Add("", to);
    }
}