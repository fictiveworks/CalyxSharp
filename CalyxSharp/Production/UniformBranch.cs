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
      return new Expansions.UniformBranch(tail);
    }

    public Expansion EvaluateAt(int index, Options options)
    {
      return new Expansions.UniformBranch(choices[index].Evaluate(options));
    }

    public int Length => choices.Length;
  }
}
