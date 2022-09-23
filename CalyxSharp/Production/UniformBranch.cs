using System;
using Calyx;

namespace Calyx.Production
{
  public class UniformBranch : Calyx.IProductionBranch
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
      int index = options.Rng.Next(this.choices.Length);
      Expansion tail = this.choices[index].Evaluate(options);
      return new Expansion(Exp.UniformBranch, tail);
    }

    public Expansion EvaluateAt(int index, Options options)
    {
      Expansion tail = this.choices[index].Evaluate(options);
      return new Expansion(Exp.UniformBranch, tail);
    }
  }
}
