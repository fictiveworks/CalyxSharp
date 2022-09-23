using Calyx.Syntax;
using NUnit.Framework;

namespace Calyx.Test.Syntax
{
  public class TemplateNodeTest
  {
    [Test]
    public void TemplateWithNoDelimitersTest()
    {
      TemplateNode node = TemplateNode.Parse("one two three", new Registry());

      Expansion exp = node.Evaluate(new Options());

      Assert.That(exp.Symbol, Is.EqualTo(Exp.Template));
      Assert.That(exp.Tail[0].Symbol, Is.EqualTo(Exp.Atom));
      Assert.That(exp.Tail[0].Term, Is.EqualTo("one two three"));
    }

    [Test]
    public void TemplateWithSingleExpansionTest()
    {
      Registry registry = new Registry();
      registry.DefineRule("one", new[] { "ONE" });
      TemplateNode node = TemplateNode.Parse("{one} two three", registry);

      Expansion exp = node.Evaluate(new Options());

      Assert.That(exp.Symbol, Is.EqualTo(Exp.Template));
      Assert.That(exp.Tail[0].Symbol, Is.EqualTo(Exp.Expression));
      Assert.That(exp.Tail[0].Tail[0].Symbol, Is.EqualTo(Exp.Template));
      Assert.That(exp.Tail[0].Tail[0].Tail[0].Symbol, Is.EqualTo(Exp.Atom));
      Assert.That(exp.Tail[0].Tail[0].Tail[0].Term, Is.EqualTo("ONE"));
      Assert.That(exp.Tail[1].Symbol, Is.EqualTo(Exp.Atom));
      Assert.That(exp.Tail[1].Term, Is.EqualTo(" two three"));
    }

    [Test]
    public void TemplateWithMultipleExpansionsTest()
    {
      Registry registry = new Registry();
      registry.DefineRule("one", new[] { "ONE" });
      registry.DefineRule("two", new[] { "TWO" });
      TemplateNode node = TemplateNode.Parse("{one} {two} three", registry);

      Expansion exp = node.Evaluate(new Options());

      Assert.That(exp.Symbol, Is.EqualTo(Exp.Template));
      Assert.That(exp.Tail[0].Symbol, Is.EqualTo(Exp.Expression));
      Assert.That(exp.Tail[0].Tail[0].Symbol, Is.EqualTo(Exp.Template));
      Assert.That(exp.Tail[0].Tail[0].Tail[0].Symbol, Is.EqualTo(Exp.Atom));
      Assert.That(exp.Tail[0].Tail[0].Tail[0].Term, Is.EqualTo("ONE"));
      Assert.That(exp.Tail[1].Symbol, Is.EqualTo(Exp.Atom));
      Assert.That(exp.Tail[1].Term, Is.EqualTo(" "));
      Assert.That(exp.Tail[2].Symbol, Is.EqualTo(Exp.Expression));
      Assert.That(exp.Tail[2].Tail[0].Symbol, Is.EqualTo(Exp.Template));
      Assert.That(exp.Tail[2].Tail[0].Tail[0].Symbol, Is.EqualTo(Exp.Atom));
      Assert.That(exp.Tail[2].Tail[0].Tail[0].Term, Is.EqualTo("TWO"));
      Assert.That(exp.Tail[3].Symbol, Is.EqualTo(Exp.Atom));
      Assert.That(exp.Tail[3].Term, Is.EqualTo(" three"));
    }

    [Test]
    public void TemplateWithSingleMemoExpansion()
    {
      Registry registry = new Registry();
      registry.DefineRule("one", new[] { "ONE", "One", "1" });
      registry.ResetEvaluationContext();
      TemplateNode node = TemplateNode.Parse("{@one}{@one}{@one}", registry);

      Expansion exp = node.Evaluate(new Options());
      Assert.That(exp.symbol, Is.EqualTo(Exp.Template));
      Assert.That(exp.tail[0].symbol, Is.EqualTo(Exp.Memo));
      Assert.That(exp.tail[0].tail[0].symbol, Is.EqualTo(Exp.UniformBranch));
      Assert.That(exp.tail[0].tail[0].tail[0].symbol, Is.EqualTo(Exp.Template));
      Assert.That(exp.tail[0].tail[0].tail[0].tail[0].symbol, Is.EqualTo(Exp.Atom));
      Assert.That(exp.tail[1].symbol, Is.EqualTo(Exp.Memo));
      Assert.That(exp.tail[1].tail[0].symbol, Is.EqualTo(Exp.UniformBranch));
      Assert.That(exp.tail[1].tail[0].tail[0].symbol, Is.EqualTo(Exp.Template));
      Assert.That(exp.tail[1].tail[0].tail[0].tail[0].symbol, Is.EqualTo(Exp.Atom));
      Assert.That(exp.tail[2].symbol, Is.EqualTo(Exp.Memo));
      Assert.That(exp.tail[2].tail[0].symbol, Is.EqualTo(Exp.UniformBranch));
      Assert.That(exp.tail[2].tail[0].tail[0].symbol, Is.EqualTo(Exp.Template));
      Assert.That(exp.tail[2].tail[0].tail[0].tail[0].symbol, Is.EqualTo(Exp.Atom));

      string firstTerm = exp.tail[0].tail[0].tail[0].tail[0].term;
      string secondTerm = exp.tail[1].tail[0].tail[0].tail[0].term;
      string thirdTerm = exp.tail[2].tail[0].tail[0].tail[0].term;
      Assert.That(new[] { firstTerm, secondTerm }, Is.All.EqualTo(thirdTerm));
    }
  }
}
