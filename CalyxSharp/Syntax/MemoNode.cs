using System;

namespace Calyx.Syntax
{
  public class MemoNode : IProduction
  {
    private string symbol;
    private Registry registry;

    public MemoNode(string symbol, Registry registry)
    {
      this.symbol = symbol;
      this.registry = registry;
    }

    public Expansion Evaluate(Options options)
    {
      Expansion eval = this.registry.MemoizeExpansion(symbol);
      return new Expansion(Exp.Memo, eval);
    }
  }
}
