namespace Calyx
{
  public interface IProduction
  {
    Expansion Evaluate(Options options);
  }

  public interface IProductionBranch : IProduction
  {
    Expansion EvaluateAt(int index, Options options);
  }
}
