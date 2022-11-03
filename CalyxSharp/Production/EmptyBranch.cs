namespace Calyx.Production
{
  public class EmptyBranch : IProductionBranch
  {
    public Expansion Evaluate(Options options)
    {
      return new Expansions.EmptyBranch();
    }

    public Expansion EvaluateAt(int index, Options options)
    {
      return new Expansions.EmptyBranch();
    }

    public int Length => 1;
  }
}
