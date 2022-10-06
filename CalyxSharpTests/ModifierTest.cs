using Calyx.Syntax;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Calyx.Test
{
  public class ModifierTest
  {
    [Test]
    public void UpperCase() {
      Registry registry = new Registry(new Options(seed: 223344));
      registry.DefineRule("start", new[] { "{city.uppercase}" });
      registry.DefineRule("city", new[] { "Whangārei" });

      Expansion exp = registry.Evaluate("start");

      Assert.That(exp.Symbol, Is.EqualTo(Exp.Result));
      Assert.That(exp.Flatten().ToString(), Is.EqualTo("WHANGĀREI"));
    }

    [Test]
    public void LowerCase() {
      Registry registry = new Registry(new Options(seed: 223344));
      registry.DefineRule("start", new[] { "{city.lowercase}" });
      registry.DefineRule("city", new[] { "Whangārei" });

      Expansion exp = registry.Evaluate("start");

      Assert.That(exp.Symbol, Is.EqualTo(Exp.Result));
      Assert.That(exp.Flatten().ToString(), Is.EqualTo("whangārei"));
    }

    [Test]
    public void TitleCase() {
      Registry registry = new Registry(new Options(seed: 223344));
      registry.DefineRule("start", new[] { "{sentence.titlecase}" });
      registry.DefineRule("sentence", new[] { "New York is in USA. London is in England." });

      Expansion exp = registry.Evaluate("start");

      Assert.That(exp.Symbol, Is.EqualTo(Exp.Result));
      // TODO: this is .NET's idea of what Title Case means, we need a better implementation
      Assert.That(exp.Flatten().ToString(), Is.EqualTo("New York is in USA. London is in England."));
    }

    [Test]
    public void SentenceCase() {
      Registry registry = new Registry(new Options(seed: 223344));
      registry.DefineRule("start", new[] { "{sentence.sentencecase}" });
      registry.DefineRule("sentence", new[] { "Texas is in USA. london is in England." });

      Expansion exp = registry.Evaluate("start");

      Assert.That(exp.Symbol, Is.EqualTo(Exp.Result));
      Assert.That(exp.Flatten().ToString(), Is.EqualTo("Texas is in USA. London is in England."));
    }

    [Test]
    public void CanDefineACustomFilter() {
      Registry registry = new Registry(new Options(seed: 223344));
      
      registry.DefineFilter("backwards", new Modifiers.StringModifier((input) => {
        char[] chars = input.ToCharArray();
        Array.Reverse(chars);
        return new string(chars);
      }));

      registry.DefineRule("start", new[] { "{anadrome.backwards}" });
      registry.DefineRule("anadrome", new[] { "desserts" });

      Expansion exp = registry.Evaluate("start");

      Assert.That(exp.Symbol, Is.EqualTo(Exp.Result));
      Assert.That(exp.Flatten().ToString(), Is.EqualTo("stressed"));
    }
  }
}
