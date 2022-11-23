using System.Collections.Generic;
using Calyx.Production;

namespace Calyx
{
  public class Rule
  {
    private string term;
    private IProductionBranch production;

    public Rule(string term, IProductionBranch branch)
    {
      this.term = term;
      production = branch;
    }

    public static Rule Build(string term, string[] productions, Registry registry)
    {
      IProductionBranch branch = UniformBranch.Parse(productions, registry);
      return new Rule(term, branch);
    }

    public static Rule Build(string term, Dictionary<string, int> productions, Registry registry)
    {
      IProductionBranch branch = WeightedBranch.Parse(productions, registry);
      return new Rule(term, branch);
    }

    public static Rule Build(string term, Dictionary<string, double> productions, Registry registry)
    {
      IProductionBranch branch = WeightedBranch.Parse(productions, registry);
      return new Rule(term, branch);
    }

    public static Rule Build(string term, Dictionary<string, decimal> productions, Registry registry)
    {
      IProductionBranch branch = WeightedBranch.Parse(productions, registry);
      return new Rule(term, branch);
    }

    public static Rule Empty(string term)
    {
      return new Rule(term, new EmptyBranch());
    }

    public int Length => production.Length;

    public Expansion Evaluate(Options options)
    {
      return production.Evaluate(options);
    }

    public Expansion EvaluateAt(int index, Options options)
    {
      return production.EvaluateAt(index, options);
    }
  }
}
