using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CommandLiners
{
    public class MapOptions
    {
        public IDictionary<string, string> Mappings { get; } = new Dictionary<string, string>();

        public string ToName(string key) => 
            Mappings.TryGetValue(key, out var result) ? result : key.Replace("-", "");
    }

    public class MapOptions<T> : MapOptions
    {
        public MapOptions<T> Add<TResult>(string alias, Expression<Func<T, TResult>> to)
        {
            Mappings[alias] = PropertySelector.Do(to);
            return this;
        }

        public MapOptions<T> Operands<TResult>(Expression<Func<T, TResult>> to) =>
            Add("", to);
    }
}