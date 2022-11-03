using System.Collections.Generic;

namespace Calyx.Expansions
{
  public class Template: Branch 
  {
    public Template(string atom) : base(atom) {}
    public Template(Expansion tail) : base(tail) {}
    public Template(List<Expansion> tail) : base(tail) {}
  }
}
