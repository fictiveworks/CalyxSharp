using Calyx.Errors;
using Calyx.Syntax;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Calyx.Test
{
  public class ModifierTest
  {
    [Test]
    public void UpperCase() {
      Registry registry = new Registry();
      registry.DefineRule("start", new[] { "{city.uppercase}" });
      registry.DefineRule("city", new[] { "Whangārei" });

      Expansion exp = registry.Evaluate("start");

      Assert.That(exp.Symbol, Is.EqualTo(Exp.Result));
      Assert.That(exp.Flatten().ToString(), Is.EqualTo("WHANGĀREI"));
    }

    [Test]
    public void LowerCase() {
      Registry registry = new Registry();
      registry.DefineRule("start", new[] { "{city.lowercase}" });
      registry.DefineRule("city", new[] { "Whangārei" });

      Expansion exp = registry.Evaluate("start");

      Assert.That(exp.Symbol, Is.EqualTo(Exp.Result));
      Assert.That(exp.Flatten().ToString(), Is.EqualTo("whangārei"));
    }

    [Test]
    public void TitleCase() {
      Registry registry = new Registry();
      registry.DefineRule("start", new[] { "{sentence.titlecase}" });
      registry.DefineRule("sentence", new[] { "New York is in USA. London is in England." });

      Expansion exp = registry.Evaluate("start");

      Assert.That(exp.Symbol, Is.EqualTo(Exp.Result));
      // TODO: this is .NET's idea of what Title Case means, we need a better implementation
      Assert.That(exp.Flatten().ToString(), Is.EqualTo("New York is in USA. London is in England."));
    }

    [Test]
    public void SentenceCase() {
      Registry registry = new Registry();
      registry.DefineRule("start", new[] { "{sentence.sentencecase}" });
      registry.DefineRule("sentence", new[] { "Texas is in USA. london is in England." });

      Expansion exp = registry.Evaluate("start");

      Assert.That(exp.Symbol, Is.EqualTo(Exp.Result));
      Assert.That(exp.Flatten().ToString(), Is.EqualTo("Texas is in USA. London is in England."));
    }

    [Test]
    public void CanDefineACustomFilter() {
      Registry registry = new Registry();
      
      registry.AddFilterClass(typeof(TestFilter));

      registry.DefineRule("start", new[] { "{anadrome.backwards}" });
      registry.DefineRule("anadrome", new[] { "desserts" });

      Expansion exp = registry.Evaluate("start");

      Assert.That(exp.Symbol, Is.EqualTo(Exp.Result));
      Assert.That(exp.Flatten().ToString(), Is.EqualTo("stressed"));
    }

    [Test]
    public void IncorrectFilterSignatureThrowsException() {
      Registry registry = new Registry();
      
      registry.AddFilterClass(typeof(TestFilter));

      registry.DefineRule("start", new[] { "{ball.incorrectparameters}" });
      registry.DefineRule("ball", new[] { "⚽️", "🏀", "⚾️" });

      Assert.Throws<IncorrectFilterSignature>(() => registry.Evaluate("start"));
    }

    [Test]
    public void IncorrectFilterParameterCountThrowsException() {
      Registry registry = new Registry();
      
      registry.AddFilterClass(typeof(TestFilter));

      registry.DefineRule("start", new[] { "{ball.incorrectparametercount}" });
      registry.DefineRule("ball", new[] { "⚽️", "🏀", "⚾️" });

      Assert.Throws<IncorrectFilterSignature>(() => registry.Evaluate("start"));
    }

    internal static class TestFilter {
      [FilterName("backwards")]
      public static string Backwards(string input, Options options) {
        char[] chars = input.ToCharArray();
        Array.Reverse(chars);
        return new string(chars);
      }

      [FilterName("incorrectparameters")]
      public static string IncorrectParameterTypes(string input, string incorrectParameterType) => "";

      [FilterName("incorrectparametercount")]
      public static string IncorrectParameterCount(string input) => "";
    }
  }
}
