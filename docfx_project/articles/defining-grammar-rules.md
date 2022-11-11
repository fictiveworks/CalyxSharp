# Defining grammar rules

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
