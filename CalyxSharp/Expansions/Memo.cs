using System.Collections.Generic;

namespace Calyx.Expansions
{
  public class Memo: Branch 
  {
    public Memo(Expansion tail) : base(tail) {}
    public Memo(List<Expansion> tail) : base(tail) {}
  }
}
