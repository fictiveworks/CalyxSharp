namespace Calyx.Test
{
  public class ResultTest
  {
    private static Expansion AtomTemplateTree()
    {
      return new Expansion(Exp.Template, new Expansion(Exp.Atom, "A T O M"));
    }

    private static Expansion TripleAtomTemplateTree()
    {
      return new Expansion(Exp.Template, new List<Expansion>() {
        new Expansion(Exp.Atom, "O N E"),
        new Expansion(Exp.Atom, " | "),
        new Expansion(Exp.Atom, "T W O")
      });
    }

    [Test]
    public void WrapsExpressionTreeTest()
    {
      Result result = new Result(AtomTemplateTree());

      Assert.That(result.Tree.symbol, Is.EqualTo(Exp.Template));
      Assert.That(result.Tree.tail[0].symbol, Is.EqualTo(Exp.Atom));
      Assert.That(result.Tree.tail[0].term, Is.EqualTo("A T O M"));
    }

    [Test]
    public void FlattensExpressionTreeTest()
    {
      Result result = new Result(TripleAtomTemplateTree());

      Assert.That(result.Text, Is.EqualTo("O N E | T W O"));
    }
  }
}
