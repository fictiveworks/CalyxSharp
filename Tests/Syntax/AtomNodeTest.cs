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

      Assert.That(exp.Equals(new Expansions.Atom("T E R M")));
    }
  }
}
