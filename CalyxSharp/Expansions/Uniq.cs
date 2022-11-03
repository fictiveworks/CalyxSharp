using System.Collections.Generic;

namespace Calyx.Expansions
{
  public class Uniq: Branch 
  {
    public Uniq(Expansion tail) : base(tail) {}
    public Uniq(List<Expansion> tail) : base(tail) {}
  }
}
