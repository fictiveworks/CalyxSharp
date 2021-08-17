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
      Assert.AreEqual(Exp.Result, exp.symbol);
      Assert.AreEqual(Exp.UniformBranch, exp.tail[0].symbol);
      Assert.AreEqual(Exp.Template, exp.tail[0].tail[0].symbol);
      Assert.AreEqual(Exp.Atom, exp.tail[0].tail[0].tail[0].symbol);
      Assert.AreEqual("atom", exp.tail[0].tail[0].tail[0].term);
    }

    [Test]
    public void EvaluateGremlinNameGrammarTest()
    {
      Registry registry = new Registry();
      registry.DefineRule("start", new[] { "{unter_prefix}unter", "{arter_prefix}arter" });
      registry.DefineRule("unter_prefix", new[] { "M", "P", "Bl", "R", "G" });
      registry.DefineRule("arter_prefix", new[] { "Sh", "F", "Cz", "Dj" });

      int i = 0;
      while (i < 5) {
        Expansion start = registry.Evaluate("start");
        string gremlinName = start.Flatten().ToString();
        Console.WriteLine(gremlinName);
        i++;
      }

      Expansion exp = registry.Evaluate("start");
      Assert.AreEqual(Exp.Result, exp.symbol);
    }
  }
}
