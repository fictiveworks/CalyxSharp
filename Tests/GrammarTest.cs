using Calyx.Errors;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Calyx.Test
{
  public class GrammarTest
  {
    [Test]
    public void EmptyDefaultTextTest()
    {
      Grammar grammar = new Grammar();
      Result result = grammar.Generate();

      Assert.That(result.Text, Is.EqualTo(""));
    }

    public void EmptyDefaultTreeTest()
    {
      Grammar grammar = new Grammar();
      Result result = grammar.Generate();

      Assert.That(result.Tree, Is.InstanceOf<Expansion>());
    }

    [Test]
    public void EmptyStrictOptionsTest()
    {
      Grammar grammar = new Grammar(strict: true);

      UndefinedRule undefined = Assert.Throws<UndefinedRule>(
        delegate {
          Result result = grammar.Generate();
        }
      );

      Assert.That(undefined.Message, Is.EqualTo("undefined rule: 'start'"));
    }

    [Test]
    public void ConstructorRuleTest()
    {
      Grammar grammar = new Grammar(def => {
        def.Rule("start", new[] { "#|||#" });
      });

      Result result = grammar.Generate();

      Assert.That(result.Text, Is.EqualTo("#|||#"));
    }

    [Test]
    public void ConstructorStartTest()
    {
      Grammar grammar = new Grammar(def => {
        def.Start(new[] { "{plus}|||{plus}" })
           .Rule("plus", new[] { "++++" });
      });

      Result result = grammar.Generate();

      Assert.That(result.Text, Is.EqualTo("++++|||++++"));
    }

    [Test]
    public void SingleStringTest()
    {
      Grammar grammar = new Grammar(def => {
        def.Start("{tilde}|||{tilde}")
           .Rule("tilde", "~~~~");
      });

      Result result = grammar.Generate();

      Assert.That(result.Text, Is.EqualTo("~~~~|||~~~~"));
    }

    [Test]
    public void CustomStartSymbolTest()
    {
      Grammar grammar = new Grammar(def => {
        def.Rule("banner", "{tilde}|||{tilde}")
           .Rule("tilde", "~~~~");
      });

      Result result = grammar.Generate("banner");

      Assert.That(result.Text, Is.EqualTo("~~~~|||~~~~"));
    }

    [Test]
    public void GenerateRuleAndContextTest()
    {
      Grammar grammar = new Grammar();

      grammar.Rule("doubletilde", "{tilde}{tilde}");

      Dictionary<string, string[]> context = new Dictionary<string, string[]>() {
        { "tilde", new[] { "~~~~" } }
      };

      Result result = grammar.Generate("doubletilde", context);

      Assert.That(result.Text, Is.EqualTo("~~~~~~~~"));
    }

    [Test]
    public void GenerateOnlyContextTest()
    {
      Grammar grammar = new Grammar();

      Dictionary<string, string[]> context = new Dictionary<string, string[]>() {
        { "doubletilde", new[] { "{tilde}{tilde}" } },
        { "tilde", new[] { "~~~~" } }
      };

      Result result = grammar.Generate("doubletilde", context);

      Assert.That(result.Text, Is.EqualTo("~~~~~~~~"));
    }

    [Test]
    public void MemoizationExpressionTest()
    {
      Grammar grammar = new Grammar(def => {
        def.Start(new[] { "{@glyph}|{@glyph}" })
           .Rule("glyph", new[] { "+", "-", "^" });
      });

      Result result = grammar.Generate();

      Assert.That(result.Text, Is.EqualTo("+|+") | Is.EqualTo("-|-") | Is.EqualTo("^|^"));
    }
    
    [Test]
    public void FilterExpressionTest() 
    {
      Grammar grammar = new Grammar(def => {
        def.Start(new[] { "{greekLetter.uppercase}" })
           .Rule("greekLetter", new[] { "alpha", "beta", "gamma" });
      });

      Result result = grammar.Generate();

      Assert.That(result.Text, Is.EqualTo("ALPHA") | Is.EqualTo("BETA") | Is.EqualTo("GAMMA"));
    }

    [Test]
    public void CustomFilterExpressionTest() 
    {
      Grammar grammar = new Grammar(def => {
        def.Start(new[] { "{word.vowelcount}" })
           .Rule("word", new[] { "autobiographies" })
           .Filters(typeof(TestFilter));
      });

      Result result = grammar.Generate();

      Assert.That(result.Text, Is.EqualTo("8"));
    }

    internal static class TestFilter {
      [FilterName("vowelcount")]
      public static string VowelCount(string input, Options options) => input.Count(c => "aeiou".Contains(char.ToLower(c))).ToString(); 
    }
  }
}
