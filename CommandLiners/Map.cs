using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CommandLiners.Options;

namespace CommandLiners
{
    public class Map<TOptions> 
    {
        private readonly IList<Option> _options = new List<Option>();

        public IEnumerable<Option> Options => _options;

        public void Add<T>(string data, Expression<Func<TOptions, T>> to) => 
            _options.Add(new OptionArgument(PropertySelector.Do(to), data));
    }
}