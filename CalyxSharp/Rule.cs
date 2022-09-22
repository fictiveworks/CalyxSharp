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

    public static Rule Empty()
    {
      return new Rule("", new EmptyBranch());
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
