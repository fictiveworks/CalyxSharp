using Calyx;
using Calyx.Syntax;
using NUnit.Framework;
using System;

namespace Calyx.Test.Syntax
{
  public class MemoNodeTest
  {
    [Test]
    public void MemoNodesReferToIdenticalSymbolExpansion()
    {
      Registry registry = new Registry();
      registry.DefineRule("one", new[] { "ONE", "One", "1" });
      registry.ResetEvaluationContext();

      MemoNode firstNode = new MemoNode("one", registry);
      MemoNode secondNode = new MemoNode("one", registry);
      MemoNode thirdNode = new MemoNode("one", registry);

      Expansion firstExp = firstNode.Evaluate(new Options());
      Expansion secondExp = secondNode.Evaluate(new Options());
      Expansion thirdExp = thirdNode.Evaluate(new Options());

      string firstTerm = firstExp.Flatten().ToString();
      string secondTerm = secondExp.Flatten().ToString();
      string thirdTerm = thirdExp.Flatten().ToString();

      Assert.That(new[] { firstTerm, secondTerm }, Is.All.EqualTo(thirdTerm));
    }
  }
}
