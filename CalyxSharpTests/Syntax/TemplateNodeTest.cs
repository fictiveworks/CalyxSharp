using Calyx;
using Calyx.Syntax;
using NUnit.Framework;
using System;

namespace Calyx.Test.Syntax
{
  public class TemplateNodeTest
  {
    [Test]
    public void TemplateWithNoDelimitersTest()
    {
      TemplateNode node = TemplateNode.Parse("one two three", new Registry());

      Expansion exp = node.Evaluate(new Options());

      Assert.AreEqual(Exp.Template, exp.Symbol);
      Assert.AreEqual(Exp.Atom, exp.Tail[0].Symbol);
      Assert.AreEqual("one two three", exp.Tail[0].Term);
    }

    [Test]
    public void TemplateWithSingleExpansionTest()
    {
      Registry registry = new Registry();
      registry.DefineRule("one", new[] { "ONE" });
      TemplateNode node = TemplateNode.Parse("{one} two three", registry);

      Expansion exp = node.Evaluate(new Options());

      Assert.AreEqual(Exp.Template, exp.Symbol);
      Assert.AreEqual(Exp.Expression, exp.Tail[0].Symbol);
      Assert.AreEqual(Exp.Template, exp.Tail[0].Tail[0].Symbol);
      Assert.AreEqual(Exp.Atom, exp.Tail[0].Tail[0].Tail[0].Symbol);
      Assert.AreEqual("ONE", exp.Tail[0].Tail[0].Tail[0].Term);
      Assert.AreEqual(Exp.Atom, exp.Tail[1].Symbol);
      Assert.AreEqual(" two three", exp.Tail[1].Term);
    }

    [Test]
    public void TemplateWithMultipleExpansionsTest()
    {
      Registry registry = new Registry();
      registry.DefineRule("one", new[] { "ONE" });
      registry.DefineRule("two", new[] { "TWO" });
      TemplateNode node = TemplateNode.Parse("{one} {two} three", registry);

      Expansion exp = node.Evaluate(new Options());

      Assert.AreEqual(Exp.Template, exp.Symbol);
      Assert.AreEqual(Exp.Expression, exp.Tail[0].Symbol);
      Assert.AreEqual(Exp.Template, exp.Tail[0].Tail[0].Symbol);
      Assert.AreEqual(Exp.Atom, exp.Tail[0].Tail[0].Tail[0].Symbol);
      Assert.AreEqual("ONE", exp.Tail[0].Tail[0].Tail[0].Term);
      Assert.AreEqual(Exp.Atom, exp.Tail[1].Symbol);
      Assert.AreEqual(" ", exp.Tail[1].Term);
      Assert.AreEqual(Exp.Expression, exp.Tail[2].Symbol);
      Assert.AreEqual(Exp.Template, exp.Tail[2].Tail[0].Symbol);
      Assert.AreEqual(Exp.Atom, exp.Tail[2].Tail[0].Tail[0].Symbol);
      Assert.AreEqual("TWO", exp.Tail[2].Tail[0].Tail[0].Term);
      Assert.AreEqual(Exp.Atom, exp.Tail[3].Symbol);
      Assert.AreEqual(" three", exp.Tail[3].Term);
    }

    [Test]
    public void TemplateWithSingleMemoExpansion()
    {
      Registry registry = new Registry();
      registry.DefineRule("one", new[] { "ONE", "One", "1" });
      registry.ResetEvaluationContext();
      TemplateNode node = TemplateNode.Parse("{@one}{@one}{@one}", registry);

      Expansion exp = node.Evaluate(new Options());
      Assert.That(exp.Symbol, Is.EqualTo(Exp.Template));
      Assert.That(exp.Tail[0].Symbol, Is.EqualTo(Exp.Memo));
      Assert.That(exp.Tail[0].Tail[0].Symbol, Is.EqualTo(Exp.UniformBranch));
      Assert.That(exp.Tail[0].Tail[0].Tail[0].Symbol, Is.EqualTo(Exp.Template));
      Assert.That(exp.Tail[0].Tail[0].Tail[0].Tail[0].Symbol, Is.EqualTo(Exp.Atom));
      Assert.That(exp.Tail[1].Symbol, Is.EqualTo(Exp.Memo));
      Assert.That(exp.Tail[1].Tail[0].Symbol, Is.EqualTo(Exp.UniformBranch));
      Assert.That(exp.Tail[1].Tail[0].Tail[0].Symbol, Is.EqualTo(Exp.Template));
      Assert.That(exp.Tail[1].Tail[0].Tail[0].Tail[0].Symbol, Is.EqualTo(Exp.Atom));
      Assert.That(exp.Tail[2].Symbol, Is.EqualTo(Exp.Memo));
      Assert.That(exp.Tail[2].Tail[0].Symbol, Is.EqualTo(Exp.UniformBranch));
      Assert.That(exp.Tail[2].Tail[0].Tail[0].Symbol, Is.EqualTo(Exp.Template));
      Assert.That(exp.Tail[2].Tail[0].Tail[0].Tail[0].Symbol, Is.EqualTo(Exp.Atom));

      string firstTerm = exp.Tail[0].Tail[0].Tail[0].Tail[0].Term;
      string secondTerm = exp.Tail[1].Tail[0].Tail[0].Tail[0].Term;
      string thirdTerm = exp.Tail[2].Tail[0].Tail[0].Tail[0].Term;
      Assert.That(new[] { firstTerm, secondTerm }, Is.All.EqualTo(thirdTerm));
    }
  }
}
