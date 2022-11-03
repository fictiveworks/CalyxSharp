using System.Collections.Generic;

namespace Calyx.Expansions
{
  public class Result: Branch 
  {
    public Result(Expansion tail) : base(tail) {}
    public Result(List<Expansion> tail) : base(tail) {}
  }
}
