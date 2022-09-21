using Calyx;
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

      Assert.AreEqual(Exp.Atom, exp.symbol);
      Assert.AreEqual("T E R M", exp.term);
    }
  }
}
