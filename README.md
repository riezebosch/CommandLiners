[![Mutation testing badge](https://img.shields.io/endpoint?style=flat&url=https%3A%2F%2Fbadge-api.stryker-mutator.io%2Fgithub.com%2Friezebosch%2FCommandLiners%2Fmaster)](https://dashboard.stryker-mutator.io/reports/github.com/riezebosch/CommandLiners/master)
[![codecov](https://codecov.io/gh/riezebosch/commandliners/branch/master/graph/badge.svg)](https://codecov.io/gh/riezebosch/commandliners)
[![build status](https://ci.appveyor.com/api/projects/status/o6xaw74qx8nayfbc/branch/master?svg=true)](https://ci.appveyor.com/project/riezebosch/commandliners/branch/master)
[![nuget](https://img.shields.io/nuget/v/CommandLiners.svg)](https://www.nuget.org/packages/CommandLiners/)


# CommandLiners

An extensible replacement for the default `CommandLineProvider` fixing the wacky multiple argument notation and
opening up the integration for some widely used command-line parser with the modern extensible configuration world of dotnet core.

## CommandlineUtils

[CommandLineUtils](https://www.nuget.org/packages/McMaster.Extensions.CommandLineUtils)

```c#
var map = new MapOptions<YourOptions>();
var app = new CommandLineApplication();

app.Argument("files", "Input files", true)
    .Map(map, to => to.Multiple);

var result = app.Parse("input1.txt", "input2.txt");
var builder = new ConfigurationBuilder()
    .AddCommandLineOptions(map.FromCommand(result.SelectedCommand))
    .Build();

var options = new YourOptions();
builder.Bind(options);
```

## Mono.Options

To integrate [Mono.Options](https://www.nuget.org/packages/Mono.Options) library into the configuration providers world with the Map<TOptions> class, map options to properties, do the parsing, and load the results into the builder.

```c#
var map = new Map<YourOptions>();
new OptionSet
{
    { "f|files=", data => map.Add(data, x => x.Files) }
}.Parse(new[] { "--files", "foo", "-f", "other" });

var builder = new ConfigurationBuilder()
    .AddCommandLineOptions(map)
    .Build();

var options = new YourOptions();
builder.Bind(options);
```

`Mono.Options` has the additional benefits of providing feedback on the usage of options and having a battle-tested notation for flags. 

## POSIX

While building this configuration provider, I first included my own arguments parser to find out later that with some little refactoring, I could open up for extensibility. The parser is now in a separate package.

```c#
var builder = new ConfigurationBuilder()
    .AddCommandLineOptions(args.ToPosix())
    .Build();

var options = new Options();
builder.Bind(options);
```

For details see the [README](CommandLiners.Posix/README.md).

## Others?

I haven't fully discovered the argument parser space for C#. Still, my guess is that other popular frameworks are hard(er) to integrate since the parsing of arguments and settings of properties of the configuration object are not separated. But I'd be happy to be proven wrong on that.

## Why?

The default command line configuration provider does support the [binding to an array](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-3.1#bind-an-array-to-a-class)
but in the wacky command line index notation way:

```c#
var args = new[] { "--input-file:0", "my-value-1", "--input-file:1", "my-value-2", "--input-file:2", "my-value-3" };
var builder = new ConfigurationBuilder()
    .AddCommandLine(args)
    .Build();
```

It turned out to be impossible to hook into its parsing process and after fiddling around
I discovered that there wasn't much needed to build a custom configuration provider altogether. 