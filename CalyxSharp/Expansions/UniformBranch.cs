using System.Collections.Generic;

namespace Calyx.Expansions
{
  public class UniformBranch: Branch 
  {
    public UniformBranch(string atom) : base(atom) {}
    public UniformBranch(Expansion tail) : base(tail) {}
    public UniformBranch(List<Expansion> tail) : base(tail) {}
  }
}
