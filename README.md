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