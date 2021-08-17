using Calyx;
using NUnit.Framework;
using System;

namespace Calyx.Test
{
  public class RegistryTest
  {
    [Test]
    public void EvaluateStartRuleTest()
    {
      Registry registry = new Registry();
      registry.DefineRule("start", new[] { "atom" });
      Expansion exp = registry.Evaluate("start");
      Assert.That(exp.symbol, Is.EqualTo(Exp.Result));
      Assert.That(exp.tail[0].symbol, Is.EqualTo(Exp.UniformBranch));
      Assert.That(exp.tail[0].tail[0].symbol, Is.EqualTo(Exp.Template));
      Assert.That(exp.tail[0].tail[0].tail[0].symbol, Is.EqualTo(Exp.Atom));
      Assert.That(exp.tail[0].tail[0].tail[0].term, Is.EqualTo("atom"));
    }
  }
}
