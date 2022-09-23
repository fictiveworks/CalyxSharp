namespace Calyx.Production
{
  public class EmptyBranch : IProductionBranch
  {
    public Expansion Evaluate(Options options) => new Expansion(Exp.Atom, "");

    public Expansion EvaluateAt(int index, Options options) => new Expansion(Exp.Atom, "");

    public int Length => 0;
  }
}
