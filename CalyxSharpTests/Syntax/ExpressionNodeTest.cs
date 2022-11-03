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

      Assert.That(exp, Is.EqualTo(new Expansions.Expression(new Expansions.Template(new Expansions.Atom("T E R M")))));
    }
  }
}
