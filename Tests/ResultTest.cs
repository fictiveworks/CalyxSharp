using NUnit.Framework;
using System.Collections.Generic;

namespace Calyx.Test
{
  public class ResultTest
  {
    private static Expansion TripleAtomTemplateTree()
    {
      return new Expansions.Template(new List<Expansion>() {
        new Expansions.Atom("O N E"),
        new Expansions.Atom(" | "),
        new Expansions.Atom("T W O"),
      });
    }

    [Test]
    public void FlattensExpressionTreeTest()
    {
      Result result = new Result(TripleAtomTemplateTree());

      Assert.That(result.Text, Is.EqualTo("O N E | T W O"));
    }
  }
}
