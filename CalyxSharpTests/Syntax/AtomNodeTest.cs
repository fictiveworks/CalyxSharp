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

      Assert.That(exp.symbol, Is.EqualTo(Exp.Atom));
      Assert.That(exp.term, Is.EqualTo("T E R M"));
    }
  }
}
