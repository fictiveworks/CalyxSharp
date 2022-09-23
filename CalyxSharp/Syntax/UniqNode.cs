using System;

namespace Calyx.Syntax
{
  public class UniqNode : IProduction
  {
    private string symbol;
    private Registry registry;

    public UniqNode(string symbol, Registry registry)
    {
      this.symbol = symbol;
      this.registry = registry;
    }

    public Expansion Evaluate(Options options)
    {
      Expansion eval = registry.UniqueExpansion(symbol);
      return new Expansion(Exp.Uniq, eval);
    }
  }
}
