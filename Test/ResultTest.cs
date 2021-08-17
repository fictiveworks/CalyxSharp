using Calyx;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Calyx.Test
{
  public class ResultTest
  {
    public Expansion AtomTemplateTree()
    {
      return new Expansion(Exp.Template, new Expansion(Exp.Atom, "A T O M"));
    }

    public Expansion TripleAtomTemplateTree()
    {
      return new Expansion(Exp.Template, new List<Expansion>() {
        new Expansion(Exp.Atom, "O N E"),
        new Expansion(Exp.Atom, " | "),
        new Expansion(Exp.Atom, "T W O")
      });
    }

    [Test]
    public void WrapsExpressionTreeTest()
    {
      Result result = new Result(this.AtomTemplateTree());

      Assert.That(result.Tree.symbol, Is.EqualTo(Exp.Template));
      Assert.That(result.Tree.tail[0].symbol, Is.EqualTo(Exp.Atom));
      Assert.That(result.Tree.tail[0].term, Is.EqualTo("A T O M"));
    }

    public void FlattensExpressionTreeTest()
    {
      Result result = new Result(this.TripleAtomTemplateTree());

      Assert.AreEqual("O N E | T W O", result.Text);
    }
  }
}
