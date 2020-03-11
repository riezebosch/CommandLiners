using System.Collections.Generic;
using CommandLine;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace CommandLiners.CommandLineParser
{
    public class UnitTest1
    {
        [Fact]
        public void Root()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("test.json")
                .Build();
            
            var options = new Simple();
            builder.Bind(options);

            Parser.Default.ParseArguments(() => options, new[] {"--file", "input1.txt", "input2.txt"});

            options.Should()
                .BeEquivalentTo(new Simple
                {
                    Single = "my-value",
                    Multiple = new[] { "input1.txt", "input2.txt" }
                });
        }
        
        [Fact]
        public void Nested()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("test.json")
                .Build();

            var options = new Nested();
            builder.Bind(options);

            Parser.Default.ParseArguments(() => options.Options,
                new[] {"--file", "input1.txt", "input2.txt"});
            
            options.Should()
                .BeEquivalentTo(new Nested
                {
                    Single = "my-value",
                    Options = new Simple
                    {
                        Single = "nested-value",
                        Multiple = new[] { "input1.txt", "input2.txt" }
                    }
                });
        }
    }

    [Verb("simple")]
    public class Simple
    {
        [Option('f', "file", Max = 1234)]
        public IEnumerable<string> Multiple { get; set; }

        [Option('s', "single", Required = true)]
        public string Single { get; set; }
    }

    [Verb("nested")]
    public class Nested
    {
        public Simple Options { get; set; }
        public string Single { get; set; }
    }
}