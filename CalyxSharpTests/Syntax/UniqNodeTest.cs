using Calyx;
using Calyx.Syntax;
using NUnit.Framework;
using System;

namespace Calyx.Test.Syntax
{
  public class UniqNodeTest
  {
    [Test]
    public void UniqNodesCycleThroughEachTemplateInBranch()
    {
      Registry registry = new Registry();
      registry.DefineRule("medal", new[] { "gold", "silver", "bronze" });
      registry.ResetEvaluationContext();

      UniqNode firstNode = new UniqNode("medal", registry);
      UniqNode secondNode = new UniqNode("medal", registry);
      UniqNode thirdNode = new UniqNode("medal", registry);

      Expansion firstExp = firstNode.Evaluate(new Options());
      Expansion secondExp = secondNode.Evaluate(new Options());
      Expansion thirdExp = thirdNode.Evaluate(new Options());

      string firstTerm = firstExp.tail[0].tail[0].tail[0].term;
      string secondTerm = secondExp.tail[0].tail[0].tail[0].term;
      string thirdTerm = thirdExp.tail[0].tail[0].tail[0].term;

      Console.WriteLine(firstTerm);
      Console.WriteLine(secondTerm);
      Console.WriteLine(thirdTerm);

      Assert.AreNotEqual(firstTerm, secondTerm);
      Assert.AreNotEqual(firstTerm, thirdTerm);
      Assert.AreNotEqual(secondTerm, thirdTerm);
    }
  }
}
