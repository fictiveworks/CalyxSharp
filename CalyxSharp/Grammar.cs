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
    public Grammar(bool strict = false): this(new Options(strict)) {}

    /// <summary>
    /// Create a new Grammar with the supplied random number generator
    /// </summary>
    /// <param name="rng"></param>
    /// <param name="strict">Determines if the parser should throw an error when encountering an undefined key</param>
    /// <returns></returns>
    public Grammar(Random rng, bool strict = false): this(new Options(rng, strict)) {}

    /// <summary>
    /// Create a new Grammar with the supplied random seed
    /// </summary>
    /// <param name="seed">An initial seed for the random number generator</param>
    /// <param name="strict">Determines if the parser should throw an error when encountering an undefined key</param>
    /// <returns></returns>
    public Grammar(int seed, bool strict = false): this(new Options(seed, strict)) {}

    public Grammar(Action<Grammar> registration)
    {
      registry = new Registry();
      registration(this);
    }

    public Grammar(Action<Grammar> registration, bool strict = false, Random rng = null)
    {
      Options opts = new Options(strict: strict, rng: rng);
      registry = new Registry(opts);
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
