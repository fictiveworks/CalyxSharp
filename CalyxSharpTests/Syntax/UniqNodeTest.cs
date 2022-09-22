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

      string firstTerm = firstExp.Tail[0].Tail[0].Tail[0].Term;
      string secondTerm = secondExp.Tail[0].Tail[0].Tail[0].Term;
      string thirdTerm = thirdExp.Tail[0].Tail[0].Tail[0].Term;

      Console.WriteLine(firstTerm);
      Console.WriteLine(secondTerm);
      Console.WriteLine(thirdTerm);

      Assert.That(secondTerm, Is.Not.EqualTo(firstTerm));
      Assert.That(thirdTerm, Is.Not.EqualTo(firstTerm));
      Assert.That(thirdTerm, Is.Not.EqualTo(secondTerm));
    }

    [Test]
    public void UniqueRulesCycleOnceSequenceIsConsumed()
    {
      Registry registry = new Registry(new Options(seed: 87654321));
      registry.DefineRule("num", new[] { "tahi", "rua" });
      registry.ResetEvaluationContext();
      TemplateNode node = TemplateNode.Parse("{$num}{$num}{$num}", registry);

      Expansion exp = node.Evaluate(new Options());

      Assert.That(exp.Tail[0].Tail[0].Tail[0].Tail[0].Term, Is.EqualTo("tahi"));
      Assert.That(exp.Tail[1].Tail[0].Tail[0].Tail[0].Term, Is.EqualTo("rua"));
      Assert.That(exp.Tail[2].Tail[0].Tail[0].Tail[0].Term, Is.EqualTo("tahi"));
    }

    [Test]
    public void UniqueRuleCyclesDifferEachTime()
    {
      // by using a different seed to `UniqueRulesCycleOnceSequenceIsConsumed` we
      // verify the resulting cycle will be different each time
      Registry registry = new Registry(new Options(seed: 87654323));
      registry.DefineRule("num", new[] { "tahi", "rua" });
      registry.ResetEvaluationContext();
      TemplateNode node = TemplateNode.Parse("{$num}{$num}{$num}", registry);

      Expansion exp = node.Evaluate(new Options());

      Assert.That(exp.Tail[0].Tail[0].Tail[0].Tail[0].Term, Is.EqualTo("tahi"));
      Assert.That(exp.Tail[1].Tail[0].Tail[0].Tail[0].Term, Is.EqualTo("rua"));
      Assert.That(exp.Tail[2].Tail[0].Tail[0].Tail[0].Term, Is.EqualTo("rua"));
    }
  }
}
