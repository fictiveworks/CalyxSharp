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
    Memo,
    Uniq
  }

  public class Expansion
  {
    public readonly Exp Symbol;
    public readonly string Term;
    public readonly List<Expansion> Tail;

    public Expansion(Exp symbol, Expansion tail)
    {
      this.Symbol = symbol;
      this.Tail = new List<Expansion>();
      this.Tail.Add(tail);
    }

    public Expansion(Exp symbol, string term)
    {
      this.Symbol = symbol;
      this.Term = term;
    }

    public Expansion(Exp symbol, List<Expansion> tail)
    {
      this.Symbol = symbol;
      this.Tail = tail;
    }

    public StringBuilder Flatten()
    {
      StringBuilder concat = new StringBuilder();
      this.CollectAtoms(concat);
      return concat;
    }

    public void CollectAtoms(StringBuilder concat)
    {
      if (this.Symbol == Exp.Atom) {
        concat.Append(this.Term);
      } else {
        foreach (Expansion exps in this.Tail) {
          exps.CollectAtoms(concat);
        }
      }
    }
  }
}
