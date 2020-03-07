[![Mutation testing badge](https://img.shields.io/endpoint?style=flat&url=https%3A%2F%2Fbadge-api.stryker-mutator.io%2Fgithub.com%2Friezebosch%2FCommandLiners%2Fmaster)](https://dashboard.stryker-mutator.io/reports/github.com/riezebosch/CommandLiners/master)
[![codecov](https://codecov.io/gh/riezebosch/commandliners/branch/master/graph/badge.svg)](https://codecov.io/gh/riezebosch/commandliners)
[![build status](https://ci.appveyor.com/api/projects/status/o6xaw74qx8nayfbc/branch/master?svg=true)](https://ci.appveyor.com/project/riezebosch/commandliners/branch/master)
[![nuget](https://img.shields.io/nuget/v/CommandLiners.svg)](https://www.nuget.org/packages/CommandLiners/)


# CommandLiners

This is an extensible replacement for the `CommandLineProvider` fixing the wacky multiple argument notation and
bringing together some widely used command line parser and the modern extensible configuration world of dotnet core.

The default command line provider:

```c#
var args = new[] { "--input-file:0", "my-value-1", "--input-file:1", "my-value-2", "--input-file:2", "my-value-3"};
var builder = new ConfigurationBuilder()
    .AddCommandLine(args)
    .Build();
```

## POSIX

```c#
var builder = new ConfigurationBuilder()
    .AddCommandLineOptions(args.ToPosix())
    .Build();

var options = new Options();
builder.Bind(options);
```

For details see: [README](PosixCommandLine/README.md)

## Mono.Options

The popular Getopt::Long-inspired option parsing library [Mono.Options](https://www.nuget.org/packages/Mono.Options) is integrated via the Map<TOptions> class: 

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

Use the `OptionSet` as usual and map the options to properties with `Map<TOptions>.Add`.