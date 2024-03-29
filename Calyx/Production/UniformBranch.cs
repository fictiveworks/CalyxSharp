using System;

namespace Calyx.Production
{
  public class UniformBranch : IProductionBranch
  {
    public static UniformBranch Parse(string[] raw, Registry registry)
    {
      IProduction[] choices = Array.ConvertAll(raw, choice => Syntax.TemplateNode.Parse(choice, registry));

      return new UniformBranch(choices, registry);
    }

    private IProduction[] choices;
    private Registry registry;

    public UniformBranch(IProduction[] choices, Registry registry)
    {
      this.choices = choices;
      this.registry = registry;
    }

    public Expansion Evaluate(Options options)
    {
      int index = options.Rng.Next(choices.Length);
      Expansion tail = choices[index].Evaluate(options);
      return new Expansion(Exp.UniformBranch, tail);
    }

    public Expansion EvaluateAt(int index, Options options)
    {
      Expansion tail = this.choices[index].Evaluate(options);
      return new Expansion(Exp.UniformBranch, tail);
    }

    public int Length => choices.Length;
  }
}
