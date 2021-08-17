using Calyx;
using Calyx.Errors;
using NUnit.Framework;
using System;

namespace Calyx.Test
{
  public class GrammarTest
  {
    [Test]
    public void EmptyDefaultOptionsTest()
    {
      Grammar grammar = new Grammar();
      Result result = grammar.Generate();

      Assert.AreEqual("", result.Text);
    }

    [Test]
    public void EmptyStrictOptionsTest()
    {
      Grammar grammar = new Grammar(strict: true);

      UndefinedRule undefined = Assert.Throws<UndefinedRule>(
        delegate { Result result = grammar.Generate(); });


      Assert.That(undefined.Message, Is.EqualTo("undefined rule: 'start'"));
    }

    [Test]
    public void ConstructorRuleTest()
    {
      Grammar grammar = new Grammar(def => {
        def.Rule("start", new[] { "++++|||++++" });
      });

      Result result = grammar.Generate();

      Assert.AreEqual("++++|||++++", result.Text);
    }

    [Test]
    public void ConstructorStartTest()
    {
      Grammar grammar = new Grammar(def => {
        def.Start(new[] { "{plus}|||{plus}" })
           .Rule("plus", new[] { "++++" });
      });

      Result result = grammar.Generate();

      Assert.AreEqual("++++|||++++", result.Text);
    }

    [Test]
    public void SingleStringTest()
    {
      Grammar grammar = new Grammar(def => {
        def.Start("{tilde}|||{tilde}")
           .Rule("tilde", "~~~~");
      });

      Result result = grammar.Generate();

      Assert.AreEqual("~~~~|||~~~~", result.Text);
    }



    // [Test]
    // public void GrammarMemoFeatureTest()
    // {
    //   Grammar grammar = new Grammar(def => {
    //     def.Start(new[] { "{@plus}{@plus}" })
    //        .Rule("plus", new[] { "++++", "----", "^^^^" });
    //   });
    //
    //   Result result = grammar.Generate();
    //
    //   Assert.AreEqual("++++///++++", result.Text);
    // }
  }
}
