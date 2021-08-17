using Calyx;
using NUnit.Framework;
using System;
using System.Text;
using System.Collections.Generic;

namespace Calyx.Test
{
  public class ExpansionTest
  {
    [Test]
    public void ConstructExpansionTerminalTest()
    {
      Expansion exp = new Expansion(Exp.Atom, "T E R M");

      Assert.AreEqual("T E R M", exp.term);
    }

    [Test]
    public void FlattenExpansionToAtomsTest()
    {
      Expansion exp = new Expansion(Exp.Template, new List<Expansion>() {
        new Expansion(Exp.Atom, "-ONE-"),
        new Expansion(Exp.Atom, "-TWO-"),
        new Expansion(Exp.Atom, "-THREE-")
      });

      StringBuilder atoms = exp.Flatten();
      Assert.AreEqual("-ONE--TWO--THREE-", atoms.ToString());
    }
  }
}
