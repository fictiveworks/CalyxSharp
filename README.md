# Calyx

Generative text processing for C# and Unity applications.

## Build

```
msbuild
```

## Tests

```
msbuild Calyx.csproj -t:Test
```

## Usage

### Configuring the PRNG

Requires an instance of `System.Random` not the static class `UnityEngine.Random`.

If you have a predefined integer to use as a seed value, pass it in using the `Seed` option:

```cs
int aSeed = 72365472659210;

Grammar grammar = new Grammar(Seed: aSeed);
```

You can also pass in an instance of `Random` with the `Rng` option.

```cs
int bSeed = 82673452385460;

Grammar grammar = new Grammar(
    Rng: new System.Random(bSeed)
);
```

### Defining grammar rules

Use the object delegate constructor to return a preconfigured instance with all the rules defined:

```cs
Grammar plums = new Grammar(P => {
    P.Start("I have {eaten} the {plums}");
    P.Rule("eaten", new[] { "eaten", "taken", "devoured" });
    P.Rule("plums", new[] { "plums", "apricots", "pears" });
});
```

Rule definitions support fluent interface conventions, so can also be constructed as a single statement:

```cs
Grammar plums = new Grammar(P => {
    P.Start("I have {eaten} the {plums}")
        .Rule("eaten", new[] { "eaten", "taken", "devoured" })
        .Rule("plums", new[] { "plums", "apricots", "pears" });
});
```

The `Start` and `Rule` methods are overloaded, supporting both arrays of strings, and single strings:

```cs
Grammar plums = new Grammar(P => {
    P.Rule("single", "I have eaten the plums")
     .Rule("alternate", new[] {
        "I have eaten the plums",
        "That were in the icebox"
    });
});
```

### Generating Results

Call `Generate` on the grammar instance to get a result object from a unique run of the grammar and use the `Text` property of the result to access the generated text.

```cs
Result result = plums.Generate();

Console.WriteLine(result.Text);
```

To append template context rules at runtime, you can pass a transient block of rules to the grammar as part of the call to `Generate`. This can be useful if you want to mix data from different life cycles of your program state but (presumably—we haven’t performance tested this yet!) will make grammar generation much slower.

```cs
Result result = plums.Generate();

Console.WriteLine(result.Text);
```

### Other Features

Not all the features are ported over yet and the template syntax is not yet documented but you can read about the general format and structure of the language design in the original [SYNTAX](https://github.com/maetl/calyx/blob/main/SYNTAX.md) document.

## Roadmap

Rough plan for stabilising the API and features for a `1.0` release.

| Version | Features planned                                                   |
|---------|--------------------------------------------------------------------|
| `0.1`   | ~~block constructors and basic grammar features~~                  |
| `0.2`   | runtime generator context in template substitution                 |
| `0.3`   | memo nodes and expression syntax                                   |
| `0.4`   | uniq nodes and expression syntax                                   |
| `0.5`   | weighted branch productions and definition parser                   |
| `0.6`   | modifying filter chains and expression syntax                       |
| `0.7`   | affix tree productions, definition parser and expression syntax      |
| `0.8`   | error handling and stack trace diagnostics                         |
| `0.9`   | Unity JSON affordances and file format integration                  |
| `0.10`  | Parity check with JavaScript and Ruby engines                      |
| `0.11`  | API documentation and code comments                                |
| `0.12`  | NuGet and Unity package manager nonsense                           |
