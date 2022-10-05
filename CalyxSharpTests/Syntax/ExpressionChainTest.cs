using Calyx.Syntax;
using NUnit.Framework;
using System.Collections.Generic;

namespace Calyx.Test
{
  public class ExpressionChainTest
  {
    [Test]
    public void ModfiersWork() {
      Registry registry = new Registry(new Options(seed: 223344));
      registry.DefineRule("start", new[] { "{greekLetter.uppercase}" });
      registry.DefineRule("greekLetter", new[] { "alpha" });

      Expansion exp = registry.Evaluate("start");

      Assert.That(exp.Symbol, Is.EqualTo(Exp.Result));
      Assert.That(exp.Flatten().ToString(), Is.EqualTo("ALPHA"));
    }

    [Test]
    public void ChainedModfiersWork() {
      Registry registry = new Registry(new Options(seed: 223344));
      registry.DefineRule("start", new[] { "{greekLetter.uppercase.emphasis}" });
      registry.DefineRule("greekLetter", new[] { "alpha" });

      Expansion exp = registry.Evaluate("start");

      Assert.That(exp.Symbol, Is.EqualTo(Exp.Result));
      Assert.That(exp.Flatten().ToString(), Is.EqualTo("*ALPHA*"));
    }

    [Test]
    public void FiltersAreEvaluatedLeftToRight() {
      Registry registry = new Registry(new Options(seed: 223344));
      registry.DefineRule("start", new[] { "{greekLetter.uppercase.length}" });
      registry.DefineRule("greekLetter", new[] { "alpha" });

      Expansion exp = registry.Evaluate("start");

      Assert.That(exp.Symbol, Is.EqualTo(Exp.Result));
      Assert.That(exp.Flatten().ToString(), Is.EqualTo("5"));
    }

    [Test]
    public void UndefinedFilterThrowsException() {
      Registry registry = new Registry(new Options(seed: 223344));
      registry.DefineRule("start", new[] { "{greekLetter.notAValidFilterName}" });
      registry.DefineRule("greekLetter", new[] { "alpha" });

      Assert.Throws<Calyx.Errors.UndefinedFilter>(() => {
        registry.Evaluate("start");
      });
    }
  }
}
