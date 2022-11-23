namespace Calyx.Production
{
  public class EmptyBranch : IProductionBranch
  {
    public Expansion Evaluate(Options options)
    {
      return new Expansion(Exp.EmptyBranch, new Expansion(Exp.Atom, ""));
    }

    public Expansion EvaluateAt(int index, Options options)
    {
      return new Expansion(Exp.EmptyBranch, new Expansion(Exp.Atom, ""));
    }

    public int Length => 1;
  }
}
