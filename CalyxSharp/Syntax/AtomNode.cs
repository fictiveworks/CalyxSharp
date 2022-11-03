namespace Calyx.Syntax
{
  public class AtomNode : IProduction
  {
    private string atom;

    public AtomNode(string atom)
    {
      this.atom = atom;
    }

    public Expansion Evaluate(Options options)
    {
      return new Expansions.Atom(atom);
    }
  }
}
