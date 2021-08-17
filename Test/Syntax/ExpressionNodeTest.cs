using Calyx;
using Calyx.Syntax;
using NUnit.Framework;

namespace Calyx.Test.Syntax
{
  public class ExpressionNodeTest
  {
    [Test]
    public void ExpressionTest()
    {
      Registry registry = new Registry();
      registry.DefineRule("term", new[] { "T E R M" });
      ExpressionNode expression = new ExpressionNode("term", registry);

      Expansion exp = expression.Evaluate(new Options());

      Assert.AreEqual(Exp.Expression, exp.symbol);
      Assert.AreEqual(Exp.Template, exp.tail[0].symbol);
      Assert.AreEqual(Exp.Atom, exp.tail[0].tail[0].symbol);
      Assert.AreEqual("T E R M", exp.tail[0].tail[0].term);
    }
  }
}
