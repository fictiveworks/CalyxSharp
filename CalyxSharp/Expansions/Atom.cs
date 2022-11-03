using System.Text;

namespace Calyx.Expansions
{
  public class Atom : Expansion
  {
    public readonly string Term;

    public Atom(string term)
    {
      Term = term;
    }

    protected internal override void CollectAtoms(StringBuilder concat)
    {
      concat.Append(Term);
    }

    public override bool Equals(object obj)
    {
      if (obj == null || ! this.GetType().Equals(obj.GetType())) 
      {
        return false;
      } 
      else {
        Atom a = (Atom) obj;
        return a.Term.Equals(Term);
      }
    }

    public override string ToString()
    {
      return $"[ {base.ToString()}, \"{Term}\" ]";
    }

    public override int GetHashCode()
    {
      return this.GetType().GetHashCode() ^ Term.GetHashCode();
    }
  }
}