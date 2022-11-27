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

      Assert.That(exp.Symbol, Is.EqualTo(Exp.Expression));
      Assert.That(exp.Tail[0].Symbol, Is.EqualTo(Exp.Template));
      Assert.That(exp.Tail[0].Tail[0].Symbol, Is.EqualTo(Exp.Atom));
      Assert.That(exp.Tail[0].Tail[0].Term, Is.EqualTo("T E R M"));
    }
  }
}
