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

      Assert.That(exp.term, Is.EqualTo("T E R M"));
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
      Assert.That(atoms.ToString(), Is.EqualTo("-ONE--TWO--THREE-"));
    }
  }
}
