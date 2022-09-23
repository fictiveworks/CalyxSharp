using NUnit.Framework;
using System.Collections.Generic;

namespace Calyx.Test
{
  public class RegistryTest
  {
    [Test]
    public void EvaluateStartRule()
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

    [Test]
    public void EvaluateRecursiveRules()
    {
      Registry registry = new Registry();
      registry.DefineRule("start", new[] { "{atom}" });
      registry.DefineRule("atom", new[] { "atom" });
      Expansion exp = registry.Evaluate("start");
      Assert.That(exp.symbol, Is.EqualTo(Exp.Result));
      Assert.That(exp.tail[0].symbol, Is.EqualTo(Exp.UniformBranch));
      Assert.That(exp.tail[0].tail[0].symbol, Is.EqualTo(Exp.Template));
      Assert.That(exp.tail[0].tail[0].tail[0].symbol, Is.EqualTo(Exp.Expression));
      Assert.That(exp.tail[0].tail[0].tail[0].tail[0].symbol, Is.EqualTo(Exp.Template));
      Assert.That(exp.tail[0].tail[0].tail[0].tail[0].tail[0].symbol, Is.EqualTo(Exp.Atom));
      Assert.That(exp.tail[0].tail[0].tail[0].tail[0].tail[0].term, Is.EqualTo("atom"));
    }

    [Test]
    public void EvaluateRulesWithInitializedContext()
    {
      Registry registry = new Registry();
      registry.DefineRule("start", new[] { "{atom}" });
      Expansion exp = registry.Evaluate("start", new Dictionary<string, string[]>() {
        ["atom"] = new[] { "atom" }
      });
      Assert.That(exp.symbol, Is.EqualTo(Exp.Result));
      Assert.That(exp.tail[0].symbol, Is.EqualTo(Exp.UniformBranch));
      Assert.That(exp.tail[0].tail[0].symbol, Is.EqualTo(Exp.Template));
      Assert.That(exp.tail[0].tail[0].tail[0].symbol, Is.EqualTo(Exp.Expression));
      Assert.That(exp.tail[0].tail[0].tail[0].tail[0].symbol, Is.EqualTo(Exp.Template));
      Assert.That(exp.tail[0].tail[0].tail[0].tail[0].tail[0].symbol, Is.EqualTo(Exp.Atom));
      Assert.That(exp.tail[0].tail[0].tail[0].tail[0].tail[0].term, Is.EqualTo("atom"));
    }

    [Test]
    public void EvaluateOnlyInitializedContext()
    {
      Registry registry = new Registry();
      registry.DefineRule("start", new[] { "{atom}" });
      Expansion exp = registry.Evaluate("start", new Dictionary<string, string[]>() {
        { "start", new[] { "{atom}" }},
        { "atom", new[] { "atom" }}
      });
      Assert.That(exp.symbol, Is.EqualTo(Exp.Result));
      Assert.That(exp.tail[0].symbol, Is.EqualTo(Exp.UniformBranch));
      Assert.That(exp.tail[0].tail[0].symbol, Is.EqualTo(Exp.Template));
      Assert.That(exp.tail[0].tail[0].tail[0].symbol, Is.EqualTo(Exp.Expression));
      Assert.That(exp.tail[0].tail[0].tail[0].tail[0].symbol, Is.EqualTo(Exp.Template));
      Assert.That(exp.tail[0].tail[0].tail[0].tail[0].tail[0].symbol, Is.EqualTo(Exp.Atom));
      Assert.That(exp.tail[0].tail[0].tail[0].tail[0].tail[0].term, Is.EqualTo("atom"));
    }

    [Test]
    public void MemoizedRulesReturnIdenticalExpansion()
    {
      Registry registry = new Registry(new Options(seed: 556677));
      registry.DefineRule("start", new[] { "{@atom}{@atom}{@atom}" });
      registry.DefineRule("atom", new[] { ",", ":", ";" });
      Expansion exp = registry.Evaluate("start");
      Assert.That(exp.symbol, Is.EqualTo(Exp.Result));
      Assert.That(exp.tail[0].tail[0].tail[0].symbol, Is.EqualTo(Exp.Memo));
      Assert.That(exp.tail[0].tail[0].tail[1].symbol, Is.EqualTo(Exp.Memo));
      Assert.That(exp.tail[0].tail[0].tail[2].symbol, Is.EqualTo(Exp.Memo));

      string term1 = exp.tail[0].tail[0].tail[0].tail[0].term;
      string term2 = exp.tail[0].tail[0].tail[1].tail[0].term;
      string term3 = exp.tail[0].tail[0].tail[2].tail[0].term;
      Assert.That(new[] { term1, term2 }, Is.All.EqualTo(term3));
    }
  }
}
