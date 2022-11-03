using System.Collections.Generic;

namespace Calyx.Expansions
{
  public class WeightedBranch: Branch 
  {
    public WeightedBranch(Expansion tail) : base(tail) {}
    public WeightedBranch(List<Expansion> tail) : base(tail) {}
  }
}
