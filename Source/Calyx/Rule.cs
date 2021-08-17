using Calyx.Production;

namespace Calyx
{
  public class Rule
  {
    private string term;
    private IProduction production;

    public Rule(string term, IProduction branch)
    {
      this.term = term;
      this.production = branch;
    }

    public static Rule Build(string term, string[] productions, Registry registry)
    {
      IProduction branch = UniformBranch.Parse(productions, registry);
      return new Rule(term, branch);
    }

    public static Rule Empty()
    {
      return new Rule("", new Syntax.AtomNode(""));
    }

    public int Length { get; private set; }

    public Expansion Evaluate(Options options)
    {
      return this.production.Evaluate(options);
    }
  }
}
