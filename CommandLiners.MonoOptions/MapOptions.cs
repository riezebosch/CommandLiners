using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CommandLiners.Options;

namespace CommandLiners
{
    public class MapOptions<TOptions> 
    {
        private readonly IList<Option> _data = new List<Option>();

        public IEnumerable<Option> Data => _data;

        public void Add<T>(string data, Expression<Func<TOptions, T>> to) => 
            _data.Add(new OptionArgument(PropertySelector.Do(to), data));
    }
}