using System;
using System.Collections.Generic;

namespace Calyx
{
  public class Grammar
  {
    private Registry registry;

    public Grammar()
    {
      registry = new Registry();
    }

    private Grammar(Options opts) {
      registry = new Registry(opts);
    }

    /// <summary>
    /// Create a new Grammar
    /// </summary>
    /// <param name="strict">Determines if the parser should throw an error when encountering an undefined key</param>
    /// <returns></returns>
    public Grammar(bool strict = Options.DefaultStrict): this(new Options(strict)) {}

    /// <summary>
    /// Create a new Grammar with the supplied random number generator
    /// </summary>
    /// <param name="rng">The random number generator to use</param>
    /// <param name="strict">Determines if the parser should throw an error when encountering an undefined key</param>
    /// <returns></returns>
    public Grammar(Random rng, bool strict = Options.DefaultStrict): this(new Options(rng, strict)) {}

    /// <summary>
    /// Create a new Grammar with the supplied random seed
    /// </summary>
    /// <param name="seed">An initial seed for the random number generator</param>
    /// <param name="strict">Determines if the parser should throw an error when encountering an undefined key</param>
    /// <returns></returns>
    public Grammar(int seed, bool strict = Options.DefaultStrict): this(new Options(seed, strict)) {}

    /// <summary>
    /// Create a new Grammar
    /// </summary>
    /// <param name="registration">A delegate method which is called after the Grammar is created</param>
    /// <param name="strict">Determines if the parser should throw an error when encountering an undefined key</param>
    /// <returns></returns>
    public Grammar(Action<Grammar> registration, bool strict = Options.DefaultStrict) : this(strict)
    {
      registration(this);
    }

    /// <summary>
    /// Create a new Grammar
    /// </summary>
    /// <param name="registration">A delegate method which is called after the Grammar is created</param>
    /// <param name="rng">The random number generator to use</param>
    /// <param name="strict">Determines if the parser should throw an error when encountering an undefined key</param>
    /// <returns></returns>
    public Grammar(Action<Grammar> registration, Random rng, bool strict = Options.DefaultStrict) : this(rng, strict)
    {
      registration(this);
    }

    /// <summary>
    /// Create a new Grammar
    /// </summary>
    /// <param name="registration">A delegate method which is called after the Grammar is created</param>
    /// <param name="seed">An initial seed for the random number generator</param>
    /// <param name="strict">Determines if the parser should throw an error when encountering an undefined key</param>
    /// <returns></returns>
    public Grammar(Action<Grammar> registration, int seed, bool strict = Options.DefaultStrict) : this(seed, strict)
    {
      registration(this);
    }

    public Grammar Start(string[] productions)
    {
      registry.DefineRule("start", productions);
      return this;
    }

    public Grammar Start(string production)
    {
      registry.DefineRule("start", new[] { production });
      return this;
    }

    public Grammar Start(Dictionary<string, int> productions)
    {
      registry.DefineRule("start", productions);
      return this;
    }

    public Grammar Start(Dictionary<string, double> productions)
    {
      registry.DefineRule("start", productions);
      return this;
    }

    public Grammar Start(Dictionary<string, decimal> productions)
    {
      registry.DefineRule("start", productions);
      return this;
    }

    public Grammar Rule(string name, string[] productions)
    {
      registry.DefineRule(name, productions);
      return this;
    }

    public Grammar Rule(string name, string production)
    {
      registry.DefineRule(name, new[] { production });
      return this;
    }

    public Grammar Rule(string name, Dictionary<string, int> productions)
    {
      registry.DefineRule(name, productions);
      return this;
    }

    public Grammar Rule(string name, Dictionary<string, double> productions)
    {
      registry.DefineRule(name, productions);
      return this;
    }

    public Grammar Rule(string name, Dictionary<string, decimal> productions)
    {
      registry.DefineRule(name, productions);
      return this;
    }

    public Grammar Filters(Type filterClass)
    {
      registry.AddFilterClass(filterClass);
      return this;
    }

    public Result Generate()
    {
      return new Result(registry.Evaluate("start"));
    }

    public Result Generate(string startSymbol)
    {
      return new Result(registry.Evaluate(startSymbol));
    }

    public Result Generate(Dictionary<string, string[]> context)
    {
      return new Result(registry.Evaluate("start", context));
    }

    public Result Generate(string startSymbol, Dictionary<string, string[]> context)
    {
      return new Result(registry.Evaluate(startSymbol, context));
    }
  }
}
