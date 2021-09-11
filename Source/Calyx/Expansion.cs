using System.Text;
using System.Collections.Generic;

namespace Calyx
{
  public enum Exp
  {
    Result,
    UniformBranch,
    WeightedBranch,
    AffixTable,
    Template,
    Expression,
    Atom,
    Memo
  }

  public class Expansion
  {
    public readonly Exp symbol;
    public readonly string term;
    public readonly List<Expansion> tail;

    public Expansion(Exp symbol, Expansion tail)
    {
      this.symbol = symbol;
      this.tail = new List<Expansion>();
      this.tail.Add(tail);
    }

    public Expansion(Exp symbol, string term)
    {
      this.symbol = symbol;
      this.term = term;
    }

    public Expansion(Exp symbol, List<Expansion> tail)
    {
      this.symbol = symbol;
      this.tail = tail;
    }

    public StringBuilder Flatten()
    {
      StringBuilder concat = new StringBuilder();
      this.CollectAtoms(concat);
      return concat;
    }

    public void CollectAtoms(StringBuilder concat)
    {
      if (this.symbol == Exp.Atom) {
        concat.Append(this.term);
      } else {
        foreach (Expansion exps in this.tail) {
          exps.CollectAtoms(concat);
        }
      }
    }
  }
}
