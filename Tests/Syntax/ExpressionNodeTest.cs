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

      Assert.AreEqual(Exp.Expression, exp.Symbol);
      Assert.AreEqual(Exp.Template, exp.Tail[0].Symbol);
      Assert.AreEqual(Exp.Atom, exp.Tail[0].Tail[0].Symbol);
      Assert.AreEqual("T E R M", exp.Tail[0].Tail[0].Term);
    }
  }
}
