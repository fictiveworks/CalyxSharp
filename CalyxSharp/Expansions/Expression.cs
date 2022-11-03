using System.Collections.Generic;

namespace Calyx.Expansions
{
  public class Expression: Branch 
  {
    public Expression(string atom) : base(atom) {}
    public Expression(Expansion tail) : base(tail) {}
    public Expression(List<Expansion> tail) : base(tail) {}
  }
}
