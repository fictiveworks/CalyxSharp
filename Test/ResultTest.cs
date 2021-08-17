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

      Assert.AreEqual(Exp.Template, result.Tree.symbol);
      Assert.AreEqual(Exp.Atom, result.Tree.tail[0].symbol);
      Assert.AreEqual("A T O M", result.Tree.tail[0].term);
    }

    public void FlattensExpressionTreeTest()
    {
      Result result = new Result(this.TripleAtomTemplateTree());

      Assert.AreEqual("O N E | T W O", result.Text);
    }
  }
}
