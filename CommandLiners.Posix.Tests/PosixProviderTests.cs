using CommandLiners;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using PosixCommandline.Tests.Options;
using Xunit;

namespace PosixCommandline.Tests
{
    public static class PosixProviderTests
    {
        [Fact]
        public static void OptionArgument()
        {
            var args = new[] {"--single", "my-value"};
            var builder = new ConfigurationBuilder()
                .AddCommandLineOptions(args.ToPosix())
                .Build();

            var options = new Simple();
            builder.Bind(options);

            options
                .Single
                .Should()
                .Be("my-value");
        }
        
        [Fact]
        public static void Option()
        {
            var args = new[] {"--flag"};
            var builder = new ConfigurationBuilder()
                .AddCommandLineOptions(args.ToPosix())
                .Build();

            var options = new Simple();
            builder.Bind(options);

            options
                .Flag
                .Should()
                .BeTrue();
        }
        
        [Fact]
        public static void Combined()
        {
            var args = new[] {"--flag", "--single", "hello"};
            var builder = new ConfigurationBuilder()
                .AddCommandLineOptions(args.ToPosix())
                .Build();

            var options = new Simple();
            builder.Bind(options);

            options
                .Should()
                .BeEquivalentTo(new Simple
                {
                    Flag = true,
                    Single = "hello"
                });
        }
        
        [Fact]
        public static void Short()
        {
            var builder = new ConfigurationBuilder()
                .AddCommandLineOptions(new[] { "-f" }.ToPosix<Simple>(map => map.Add("f", x => x.Flag)))
                .Build();

            var options = new Simple();
            builder.Bind(options);

            options
                .Flag
                .Should()
                .BeTrue();
        }
        
        [Fact]
        public static void Compound()
        {
            var args = new[] { "--compound-word", "my-value" };
            var builder = new ConfigurationBuilder()
                .AddCommandLineOptions(args.ToPosix())
                .Build();

            var options = new Simple();
            builder.Bind(options);

            options
                .CompoundWord
                .Should()
                .Be("my-value");
        }
        
        [Fact]
        public static void Options()
        {
            var args = new[] { "-ab" };
            var builder = new ConfigurationBuilder()
                .AddCommandLineOptions(args.ToPosix())
                .Build();

            var options = new Simple();
            builder.Bind(options);

            options
                .Should()
                .BeEquivalentTo(new Simple
                {
                    A = true,
                    B = true
                });
        }
        
        [Fact]
        public static void Nested()
        {
            var args = new[] {"--options:single", "my-value"};
            var builder = new ConfigurationBuilder()
                .AddCommandLineOptions(args.ToPosix())
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
            var args = new[] {"--nested:options:single", "my-value"};
            var builder = new ConfigurationBuilder()
                .AddCommandLineOptions(args.ToPosix())
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
                .AddCommandLineOptions(args.ToPosix())
                .Build();

            var options = new Simple();
            builder.Bind(options);

            options
                .Multiple
                .Should()
                .Equal("my-value-1", "my-value-2");
        }
        
        [Fact]
        public static void NestedMultiple()
        {
            var args = new[] {"--options:multiple", "my-value-1", "--options:multiple", "my-value-2", "--options:multiple", "my-value-3"};
            var builder = new ConfigurationBuilder()
                .AddCommandLineOptions(args.ToPosix())
                .Build();

            var options = new Nested();
            builder.Bind(options);

            options
                .Options
                .Multiple
                .Should()
                .Equal("my-value-1", "my-value-2", "my-value-3");
        }
        
        [Fact]
        public static void NestedCompound()
        {
            var args = new[] {"--options:compound-word", "my-value"};
            var builder = new ConfigurationBuilder()
                .AddCommandLineOptions(args.ToPosix())
                .Build();

            var options = new Nested();
            builder.Bind(options);

            options
                .Options
                .CompoundWord
                .Should()
                .Be("my-value");
        }
        
        [Fact]
        public static void Operands()
        {
            var args = new[] {"my-value-1", "my-value-2", "my-value-3"};
            var builder = new ConfigurationBuilder()
                .AddCommandLineOptions(args.ToPosix<Simple>(map => map.Operands(x => x.Multiple)))
                .Build();

            var options = new Simple();
            builder.Bind(options);

            options
                .Multiple
                .Should()
                .Equal("my-value-1", "my-value-2", "my-value-3");
        }
    }
}