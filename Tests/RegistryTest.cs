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

      Assert.That(exp.Symbol, Is.EqualTo(Exp.Result));
      Assert.That(exp.Flatten().ToString(), Is.EqualTo("atom"));
    }

    [Test]
    public void EvaluateRecursiveRules()
    {
      Registry registry = new Registry();
      registry.DefineRule("start", new[] { "{atom}" });
      registry.DefineRule("atom", new[] { "atom" });

      Expansion exp = registry.Evaluate("start");

      Assert.That(exp.Symbol, Is.EqualTo(Exp.Result));
      Assert.That(exp.Flatten().ToString(), Is.EqualTo("atom"));
    }

    [Test]
    public void EvaluateRulesWithInitializedContext()
    {
      Registry registry = new Registry();
      registry.DefineRule("start", new[] { "{atom}" });

      Expansion exp = registry.Evaluate("start", new Dictionary<string, string[]>() {
        ["atom"] = new[] { "atom" }
      });

      Assert.That(exp.Symbol, Is.EqualTo(Exp.Result));
      Assert.That(exp.Flatten().ToString(), Is.EqualTo("atom"));
    }

    [Test]
    public void EvaluateOnlyInitializedContext()
    {
      Registry registry = new Registry();

      Expansion exp = registry.Evaluate("start", new Dictionary<string, string[]>() {
        { "start", new[] { "{atom}" }},
        { "atom", new[] { "atom" }}
      });

      Assert.That(exp.Symbol, Is.EqualTo(Exp.Result));
      Assert.That(exp.Flatten().ToString(), Is.EqualTo("atom"));
    }

    [Test]
    public void MemoizedRulesReturnIdenticalExpansion()
    {
      Registry registry = new Registry(new Options(seed: 556677));
      registry.DefineRule("start", new[] { "{@atom}{@atom}{@atom}" });
      registry.DefineRule("atom", new[] { "~", ":", ";" });

      Expansion exp = registry.Evaluate("start");

      Assert.That(exp.Symbol, Is.EqualTo(Exp.Result));
      Assert.That(exp.Flatten().ToString(), Is.EqualTo("~~~"));
    }

    [Test]
    public void UniqueRulesReturnCycleSequence()
    {
      Registry registry = new Registry(new Options(seed: 223344));
      registry.DefineRule("start", new[] { "{$medal};{$medal};{$medal}" });
      registry.DefineRule("medal", new[] { "gold", "silver", "bronze" });

      Expansion exp = registry.Evaluate("start");

      Assert.That(exp.Symbol, Is.EqualTo(Exp.Result));
      Assert.That(exp.Flatten().ToString(), Is.EqualTo("bronze;silver;gold"));
    }
  }
}
