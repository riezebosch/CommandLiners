using System;
using System.Linq.Expressions;
using FluentAssertions;
using Xunit;

namespace CommandLiners.Tests
{
    public static class PropertySelectorTests
    {
        [Fact]
        public static void Nested()
        {
            Expression<Func<Nested, bool>> to = o => o.Options.A;
            PropertySelector.Do(to)
                .Should()
                .Be("Options:A");
        }

        [Fact]
        public static void Property()
        {
            Expression<Func<Options, bool>> to = o => o.A;
            PropertySelector.Do(to)
                .Should()
                .Be("A");
        }

        [Fact]
        public static void Method()
        {
            Expression<Func<int, string>> to = o => o.ToString();
            Action test = () => PropertySelector.Do(to);

            test
                .Should()
                .Throw<ArgumentException>();
        }
    }

    internal class Nested
    {
        public Options Options { get; set; }
    }

    internal class Options
    {
        public bool A { get; set; }
    }
}