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

      Assert.AreEqual(Exp.Template, exp.symbol);
      Assert.AreEqual(Exp.Atom, exp.tail[0].symbol);
      Assert.AreEqual("one two three", exp.tail[0].term);
    }

    [Test]
    public void TemplateWithSingleExpansionTest()
    {
      Registry registry = new Registry();
      registry.DefineRule("one", new[] { "ONE" });
      TemplateNode node = TemplateNode.Parse("{one} two three", registry);

      Expansion exp = node.Evaluate(new Options());

      Assert.AreEqual(Exp.Template, exp.symbol);
      Assert.AreEqual(Exp.Expression, exp.tail[0].symbol);
      Assert.AreEqual(Exp.Template, exp.tail[0].tail[0].symbol);
      Assert.AreEqual(Exp.Atom, exp.tail[0].tail[0].tail[0].symbol);
      Assert.AreEqual("ONE", exp.tail[0].tail[0].tail[0].term);
      Assert.AreEqual(Exp.Atom, exp.tail[1].symbol);
      Assert.AreEqual(" two three", exp.tail[1].term);
    }

    [Test]
    public void TemplateWithMultipleExpansionsTest()
    {
      Registry registry = new Registry();
      registry.DefineRule("one", new[] { "ONE" });
      registry.DefineRule("two", new[] { "TWO" });
      TemplateNode node = TemplateNode.Parse("{one} {two} three", registry);

      Expansion exp = node.Evaluate(new Options());

      Assert.AreEqual(Exp.Template, exp.symbol);
      Assert.AreEqual(Exp.Expression, exp.tail[0].symbol);
      Assert.AreEqual(Exp.Template, exp.tail[0].tail[0].symbol);
      Assert.AreEqual(Exp.Atom, exp.tail[0].tail[0].tail[0].symbol);
      Assert.AreEqual("ONE", exp.tail[0].tail[0].tail[0].term);
      Assert.AreEqual(Exp.Atom, exp.tail[1].symbol);
      Assert.AreEqual(" ", exp.tail[1].term);
      Assert.AreEqual(Exp.Expression, exp.tail[2].symbol);
      Assert.AreEqual(Exp.Template, exp.tail[2].tail[0].symbol);
      Assert.AreEqual(Exp.Atom, exp.tail[2].tail[0].tail[0].symbol);
      Assert.AreEqual("TWO", exp.tail[2].tail[0].tail[0].term);
      Assert.AreEqual(Exp.Atom, exp.tail[3].symbol);
      Assert.AreEqual(" three", exp.tail[3].term);
    }
  }
}
