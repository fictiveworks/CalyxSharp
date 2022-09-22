using Calyx.Syntax;
using NUnit.Framework;

namespace Calyx.Test.Syntax
{
  public class AtomNodeTest
  {
    [Test]
    public void AtomTermTest()
    {
      AtomNode atom = new AtomNode("T E R M");

      Expansion exp = atom.Evaluate(new Options());

      Assert.That(exp.Symbol, Is.EqualTo(Exp.Atom));
      Assert.That(exp.Term, Is.EqualTo("T E R M"));
    }
  }
}
