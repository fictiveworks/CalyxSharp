# Generating Results

Call `Generate` on the grammar instance to get a result object from a unique run of the grammar and use the `Text` property of the result to access the generated text.

```cs
Result result = plums.Generate();

Console.WriteLine(result.Text);
```

By default, grammars will generate top-down from the `start` rule. To start from an alternative rule in the grammar, pass the start symbol as a string to `Generate`:

```cs
Result result = plums.Generate("alternate");

Console.WriteLine(result.Text);
```

To append template context rules at runtime, you can pass a transient block of rules to the grammar as part of the call to `Generate`. This can be useful if you want to mix data from different life cycles of your program state but (presumably—we haven’t performance tested this yet!) will make grammar generation slower though in most cases shouldn’t be noticeable.

```cs
Result result = plums.Generate(new Dictionary<string, string[]>() {
  ["single"] = new[] { "I have eaten the plums" }
  ["alternate"] = {
    "I have eaten the plums",
    "That were in the icebox"
  }
});

Console.WriteLine(result.Text);
```
