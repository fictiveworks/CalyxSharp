using System.Collections.Generic;
using System.Text;

namespace Calyx
{
  public enum Exp
  {
    Result,
    UniformBranch,
    WeightedBranch,
    EmptyBranch,
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
      Symbol = symbol;
      Tail = new List<Expansion>();
      Tail.Add(tail);
    }

    public Expansion(Exp symbol, string term)
    {
      Symbol = symbol;
      Term = term;
    }

    public Expansion(Exp symbol, List<Expansion> tail)
    {
      Symbol = symbol;
      Tail = tail;
    }

    public StringBuilder Flatten()
    {
      StringBuilder concat = new StringBuilder();
      CollectAtoms(concat);
      return concat;
    }

    public void CollectAtoms(StringBuilder concat)
    {
      if (Symbol == Exp.Atom) {
        concat.Append(Term);
      } else {
        foreach (Expansion exps in Tail) {
          exps.CollectAtoms(concat);
        }
      }
    }
  }
}
