using Calyx;
using Calyx.Syntax;
using NUnit.Framework;
using System;
using System.Linq;

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

      string[] expansions = Enumerable
        .Range(0, 3)
        .Select(_ => new UniqNode("medal", registry).Evaluate(new Options()).Tail[0].Tail[0].Tail[0].Term)
        .ToArray();

      Assert.That(expansions, Is.Unique);
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
