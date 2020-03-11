using System;
using System.Linq.Expressions;
using CommandLiners.Tests.Options;
using FluentAssertions;
using Xunit;

namespace CommandLiners.Tests
{
    public static class PropertySelectorTests
    {
        [Fact]
        public static void Simple()
        {
            Expression<Func<Simple, string[]>> to = o => o.Multiple;
            PropertySelector
                .Do(to)
                .Should()
                .Be("Multiple");
        }
        
        [Fact]
        public static void Nested()
        {
            Expression<Func<Nested, string[]>> to = o => o.Options.Multiple;
            PropertySelector
                .Do(to)
                .Should()
                .Be("Options:Multiple");
        }
        
        [Fact]
        public static void MethodFromProperty()
        {
            Expression<Func<Nested, string>> to = o => o.Options.Multiple.ToString();
            Action test = () => PropertySelector.Do(to);
            test
                .Should()
                .Throw<ArgumentException>();
        }
        
        [Fact]
        public static void MethodFromBody()
        {
            Expression<Func<Nested, string>> to = o => o.ToString();
            Action test = () => PropertySelector.Do(to);
            test
                .Should()
                .Throw<ArgumentException>();
        }
    }
}