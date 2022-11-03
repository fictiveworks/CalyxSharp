using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calyx.Expansions
{
  public abstract class Branch: Expansion {
    public readonly List<Expansion> Tail;

    public Branch(string atom) : this(new Atom(atom)) {}

    public Branch(Expansion tail) {
      Tail = new List<Expansion>{ tail };
    }

    public Branch(List<Expansion> tail)
    {
      Tail = tail;
    }

    protected internal override void CollectAtoms(StringBuilder concat) {
      foreach (Expansion expansion in Tail) {
        expansion.CollectAtoms(concat);
      }
    }

    public Expansions.Expression AsExpression() {
      return new Expansions.Expression(Tail);
    }

    public override bool Equals(object obj)
    {
      if (obj == null || ! this.GetType().Equals(obj.GetType()))
      {
        return false;
      }
      else {
        Branch b = (Branch)obj;
        return b.Tail.SequenceEqual(Tail);
      }
    }

    public override int GetHashCode()
    {
      int hash = GetType().GetHashCode();
      foreach (Expansion child in Tail)
      {
        hash ^= child.GetHashCode();
      }
      return hash;
    }

    public override string ToString()
    {
      return $"[ {base.ToString()}, {string.Join(",", Tail.Select(t => t.ToString()))} ]";
    }
  }
}
