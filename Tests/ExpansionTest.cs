using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Calyx.Test
{
  public class ExpansionTest
  {
    [Test]
    public void ConstructExpansionTerminalTest()
    {
      Expansion exp = new Expansion(Exp.Atom, "T E R M");

      Assert.That(exp.Term, Is.EqualTo("T E R M"));
    }

    [Test]
    public void ConstructNestedExpansionTest()
    {
      Expansion exp = new Expansion(Exp.Template, new List<Expansion>() {
        new Expansion(Exp.Atom, "-TAHI-"),
        new Expansion(Exp.Atom, "-RUA-"),
        new Expansion(Exp.Atom, "-TORU-")
      });

      Assert.That(exp.Symbol, Is.EqualTo(Exp.Template));
      Assert.That(exp.Tail[0].Symbol, Is.EqualTo(Exp.Atom));
      Assert.That(exp.Tail[0].Term, Is.EqualTo("-TAHI-"));
      Assert.That(exp.Tail[1].Symbol, Is.EqualTo(Exp.Atom));
      Assert.That(exp.Tail[1].Term, Is.EqualTo("-RUA-"));
      Assert.That(exp.Tail[2].Symbol, Is.EqualTo(Exp.Atom));
      Assert.That(exp.Tail[2].Term, Is.EqualTo("-TORU-"));
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
