using FluentAssertions;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace CommandLiners.Tests.CommandLineUtils
{
    public class MapOptionsTests
    {
        [Fact]
        public void OptionArguments()
        {
            var map = new MapOptions<Simple>();

            var app = new CommandLineApplication();
            app.Option("-f|--files <files>", "Input files", CommandOptionType.MultipleValue)
                .Map(map, to => to.Multiple);

            var result = app.Parse("-f", "input1.txt", "--files", "input2.txt");

            var builder = new ConfigurationBuilder()
                .AddJsonFile("test.json")
                .AddCommandLineOptions(map.FromCommand(result.SelectedCommand))
                .Build();

            var options = new Simple();
            builder.Bind(options);

            options
                .Should()
                .BeEquivalentTo(new Simple
                {
                    Multiple = new[] {"input1.txt", "input2.txt"},
                    Single = "my-value"
                });
        }
        
        [Fact]
        public void Operands()
        {
            var map = new MapOptions<Simple>();

            var app = new CommandLineApplication();
            app.Argument("files", "Input files", true)
                .Map(map, to => to.Multiple);

            var result = app.Parse("input1.txt", "input2.txt");
            var builder = new ConfigurationBuilder()
                .AddCommandLineOptions(map.FromCommand(result.SelectedCommand))
                .Build();

            var options = new Simple();
            builder.Bind(options);

            options
                .Should()
                .BeEquivalentTo(new Simple
                {
                    Multiple = new[] { "input1.txt", "input2.txt" }
                });
        }
        
        [Fact]
        public void Options()
        {
            var map = new MapOptions<Simple>();

            var app = new CommandLineApplication();
            app.Option("-d|--debug", "flag", CommandOptionType.NoValue)
                .Map(map, to => to.Debug);

            var result = app.Parse("-d");
            var builder = new ConfigurationBuilder()
                .AddCommandLineOptions(map.FromCommand(result.SelectedCommand))
                .Build();

            var options = new Simple();
            builder.Bind(options);

            options
                .Should()
                .BeEquivalentTo(new Simple
                {
                    Debug =  true
                });
        }
        
        [Theory]
        [InlineData(false, "-d=false")]
        [InlineData(true, "-d=true")]
        [InlineData(true, "-d")]
        [InlineData(true, "--debug")]
        [InlineData(true, "--debug", "-d")]
        [InlineData(false)]
        public void SingleOrNoValue(bool expected, params string[] args)
        {
            var map = new MapOptions<Simple>();

            var app = new CommandLineApplication();
            app.Option<bool>("-d|--debug", "enable debug", CommandOptionType.SingleOrNoValue)
                .Map(map, to => to.Debug);

            var result = app.Parse(args);
            var builder = new ConfigurationBuilder()
                .AddCommandLineOptions(map.FromCommand(result.SelectedCommand))
                .Build();

            var options = new Simple();
            builder.Bind(options);

            options
                .Should()
                .BeEquivalentTo(new Simple
                {
                    Debug = expected
                });
        }
        
        [Fact]
        public void NotMapped()
        {
            var map = new MapOptions<Simple>();

            var app = new CommandLineApplication();
            app.Option("-m|--multiple <files>", 
                "Input files", CommandOptionType.MultipleValue);

            var result = app.Parse("-m", "input1.txt", "--multiple", "input2.txt");

            var builder = new ConfigurationBuilder()
                .AddCommandLineOptions(map.FromCommand(result.SelectedCommand))
                .Build();
            
            var options = new Simple();
            builder.Bind(options);
            
            options
                .Should()
                .BeEquivalentTo(new Simple
                {
                    Multiple = new[] { "input1.txt", "input2.txt" }
                });
        }

        [Fact]
        public void NotMappedShort()
        {
            var map = new MapOptions<Simple>();

            var app = new CommandLineApplication();
            app.Option("-c", 
                "Input files", CommandOptionType.NoValue);

            var result = app.Parse("-c");

            var builder = new ConfigurationBuilder()
                .AddCommandLineOptions(map.FromCommand(result.SelectedCommand))
                .Build();
            
            var options = new Simple();
            builder.Bind(options);
            
            options
                .Should()
                .BeEquivalentTo(new Simple
                {
                    C = true
                });
        }
        
        public class Simple
        {
            public string[] Multiple { get; set; }
            public string Single { get; set; }
            public bool Debug { get; set; }
            public bool C { get; set; }
        }
    }
}