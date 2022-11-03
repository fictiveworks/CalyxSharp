using System.Collections.Generic;

namespace Calyx.Expansions
{
  public class AffixTable: Branch 
  {
    public AffixTable(Expansion tail) : base(tail) {}
    public AffixTable(List<Expansion> tail) : base(tail) {}
  }
}
