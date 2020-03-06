# POSIX Command Line Provider

This is a drop-in replacement for the `CommandLineProvider` following the POSIX 
guidelines for the command-line options of a program.

```c#
var builder = new ConfigurationBuilder()
    .AddPosixCommandLine(args)
    .Build();

var options = new Options();
builder.Bind(options);
```

## Flag arguments

```c#
var aliases = new Map<Options>()
    .Add("d", o => o.Debug)
    .Add("l", o => Options.Log);

var args = new[] { "-dl" };

var builder = new ConfigurationBuilder()
    .AddPosixCommandLine(args, aliases.Mappings)
    .Build();
```

## Unnamed arguments

```c#
var args = new[] {"my-value-1", "my-value-2", "my-value-3"};
var builder = new ConfigurationBuilder()
    .AddPosixCommandLine(args, new Map<Options>().Operands(o => o.Files))
    .Build();
```

## Multiples

Options can be repeated when the value accepts a list of inputs:

```c#
var args = new[] { "--input-file", "my-value-1", "--input-file", "my-value-2", "--input-file", "my-value-3"};
var builder = new ConfigurationBuilder()
    .AddPosixCommandLine(args)
    .Build();
```

Opposed to the `CommandLineProvider`:

```c#
var args = new[] { "--input-file:0", "my-value-1", "--input-file:1", "my-value-2", "--input-file:2", "my-value-3"};
var builder = new ConfigurationBuilder()
    .AddPosixCommandLine(args)
    .Build();
```



GNU documentation for [POSIX guidelines for the command-line options of a program](https://www.gnu.org/prep/standards/html_node/Command_002dLine-Interfaces.html) and 
man pages for [getopt and getopt_long](http://man7.org/linux/man-pages/man3/getopt.3.html).

