using System.Collections.Generic;
using CommandlineMultiple.Tests;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace PosixCommandline.Tests
{
    public static class UnitTest1
    {
        [Fact]
        public static void Single()
        {
            var builder = new ConfigurationBuilder()
                .AddPosixCommandLine(new[] {"--single", "my-value"})
                .Build();

            var options = new Options();
            builder.Bind(options);

            options
                .Single
                .Should()
                .Be("my-value");
        }
        
        [Fact]
        public static void Flag()
        {
            var builder = new ConfigurationBuilder()
                .AddPosixCommandLine(new[] {"--flag"})
                .Build();

            var options = new Options();
            builder.Bind(options);

            options
                .Flag
                .Should()
                .BeTrue();
        }
        
        [Fact]
        public static void Combined()
        {
            var builder = new ConfigurationBuilder()
                .AddPosixCommandLine(new[] {"--flag", "--single", "hello"})
                .Build();

            var options = new Options();
            builder.Bind(options);

            options
                .Should()
                .BeEquivalentTo(new Options
                {
                    Flag = true,
                    Single = "hello"
                });
        }
        
        [Fact]
        public static void Short()
        {
            var mappings = new Dictionary<string, string>
            {
                ["f"] = nameof(Options.Flag)
            };
            var builder = new ConfigurationBuilder()
                .AddPosixCommandLine(new[] { "-f" }, mappings)
                .Build();

            var options = new Options();
            builder.Bind(options);

            options
                .Flag
                .Should()
                .BeTrue();
        }
        
        [Fact]
        public static void Compound()
        {
            var mappings = new Dictionary<string, string>
            {
                ["f"] = nameof(Options.Flag)
            };
            var builder = new ConfigurationBuilder()
                .AddPosixCommandLine(new[] { "--compound-word", "my-value" }, mappings)
                .Build();

            var options = new Options();
            builder.Bind(options);

            options
                .CompoundWord
                .Should()
                .Be("my-value");
        }
        
        [Fact]
        public static void Flags()
        {
            var mappings = new Dictionary<string, string>
            {
                ["a"] = nameof(Options.A),
                ["b"] = nameof(Options.B)
            };
            var builder = new ConfigurationBuilder()
                .AddPosixCommandLine(new[] { "-ab" }, mappings)
                .Build();

            var options = new Options();
            builder.Bind(options);

            options
                .Should()
                .BeEquivalentTo(new Options
                {
                    A = true,
                    B = true
                });
        }
        
        [Fact]
        public static void Nested()
        {
            var builder = new ConfigurationBuilder()
                .AddPosixCommandLine(new[] {"--options:single", "my-value"})
                .Build();

            var options = new Nested();
            builder.Bind(options);

            options
                .Options
                .Single
                .Should()
                .Be("my-value");
        }
        
        [Fact]
        public static void Triple()
        {
            var builder = new ConfigurationBuilder()
                .AddPosixCommandLine(new[] {"--nested:options:single", "my-value"})
                .Build();

            var options = new Triple();
            builder.Bind(options);

            options
                .Nested
                .Options
                .Single
                .Should()
                .Be("my-value");
        }
        
        [Fact]
        public static void Multiple()
        {
            var args = new[] {"--multiple", "my-value-1", "--multiple", "my-value-2"};
            var builder = new ConfigurationBuilder()
                .AddPosixCommandLine(args)
                .Build();

            var options = new Options();
            builder.Bind(options);

            options
                .Multiple
                .Should()
                .Equal("my-value-1", "my-value-2");
        }
        
        [Fact]
        public static void MultipleNested()
        {
            var args = new[] {"--options:multiple", "my-value-1", "--options:multiple", "my-value-2", "--options:multiple", "my-value-3"};
            var builder = new ConfigurationBuilder()
                .AddPosixCommandLine(args)
                .Build();

            var options = new Nested();
            builder.Bind(options);

            options
                .Options
                .Multiple
                .Should()
                .Equal("my-value-1", "my-value-2", "my-value-3");
        }
    }

    public class Options
    {
        public bool A { get; set; }
        public bool B { get; set; }
        public string Single { get; set; }
        public string[] Multiple { get; set; }
        public bool Flag { get; set; }
        public string CompoundWord { get; set; }
    }

    public class Triple
    {
        public Nested Nested { get; set; }
    }

    public class Nested
    {
        public Options Options { get; set; }
    }
}