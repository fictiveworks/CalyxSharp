using System;
using Calyx;

namespace Calyx.Production
{
  public class EmptyBranch : Calyx.IProductionBranch
  {
    public Expansion Evaluate(Options options)
    {
      return new Expansion(Exp.Atom, "");
    }

    public Expansion EvaluateAt(int index, Options options)
    {
      return new Expansion(Exp.Atom, "");
    }
  }
}
