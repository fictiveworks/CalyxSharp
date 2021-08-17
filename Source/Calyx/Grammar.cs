using System;

namespace Calyx
{
  public class Grammar
  {
    private Registry registry;

    public Grammar()
    {
      this.registry = new Registry();
    }

    public Grammar(bool strict = false, int seed = 0, Random rng = null)
    {
      Options opts = new Options(strict: strict, rng: rng, seed: seed);
      this.registry = new Registry(opts);
    }

    public Grammar(Action<Grammar> registration)
    {
      this.registry = new Registry();
      registration(this);
    }

    public Grammar(Action<Grammar> registration, bool strict = false, Random rng = null)
    {
      Options opts = new Options(strict: strict, rng: rng);
      this.registry = new Registry(opts);
      registration(this);
    }

    public Grammar Start(string[] productions)
    {
      this.registry.DefineRule("start", productions);
      return this;
    }

    public Grammar Start(string production)
    {
      this.registry.DefineRule("start", new[] { production });
      return this;
    }

    public Grammar Rule(string name, string[] productions)
    {
      this.registry.DefineRule(name, productions);
      return this;
    }

    public Grammar Rule(string name, string production)
    {
      this.registry.DefineRule(name, new[] { production });
      return this;
    }

    public Result Generate()
    {
      return new Result(this.registry.Evaluate("start"));
    }

    public Result Generate(string startSymbol)
    {
      return new Result(this.registry.Evaluate(startSymbol));
    }
  }
}
