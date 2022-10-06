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

    public Grammar(bool strict = false, int seed = 0, Random rng = null)
    {
      Options opts = new Options(strict: strict, rng: rng, seed: seed);
      registry = new Registry(opts);
    }

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

    public Grammar Filter(string name, Modifiers.IStringModifier filter)
    {
      registry.DefineFilter(name, filter);
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
