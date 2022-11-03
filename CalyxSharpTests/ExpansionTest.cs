using Calyx.Expansions;
using System.Collections.Generic;
using NUnit.Framework;

namespace Calyx.Test
{
  public class ExpansionTest
  {
    [Test]
    public void FlattenExpansionToAtomsTest()
    {
      Expansion exp = new Expansions.Template(new List<Expansion>() {
        new Expansions.Atom("-ONE-"),
        new Expansions.Atom("-TWO-"),
        new Expansions.Atom("-THREE-"),
      });

      Assert.That(exp.Flatten().ToString(), Is.EqualTo("-ONE--TWO--THREE-"));
    }

    [Test]
    public void AtomsThatAreEqual()
    {
      Atom atom1 = new Atom("anAtom");
      Atom atom2 = new Atom("anAtom");

      Assert.That(atom1, Is.EqualTo(atom2));
    }

    [Test]
    public void AtomsThatAreUnequal()
    {
      Atom atom1 = new Atom("anAtom");
      Atom atom2 = new Atom("anotherAtom");

      Assert.That(atom1, Is.Not.EqualTo(atom2));
    }

    [Test]
    public void BranchesThatAreEqual()
    {
      UniformBranch uniform1 = new UniformBranch(new Atom("anAtom"));
      UniformBranch uniform2 = new UniformBranch(new Atom("anAtom"));

      Assert.That(uniform1, Is.EqualTo(uniform2));
    }


    [Test]
    public void BranchSubclassesAreDistinct() {
      UniformBranch uniform = new UniformBranch(new Atom("anAtom"));
      WeightedBranch weighted = new WeightedBranch(new Atom("anAtom"));

      // NUnit's Is.EqualTo() insists the two will always be unequal even though it's possible
      // to write an Equals() override that considered them equal. Because this is what we're
      // explicitly testing, we use this style of Assert to force the test to compile.
      Assert.That(uniform.Equals(weighted), Is.False);
    }

    [Test]
    public void BranchEqualityTestIsDeep() {
      UniformBranch uniform1 = new UniformBranch(new Atom("anAtom"));
      UniformBranch uniform2 = new UniformBranch(new Atom("anotherAtom"));

      Assert.That(uniform1, Is.Not.EqualTo(uniform2));
    }

    [Test]
    public void OrderOfItemsIsSignificant() {
      UniformBranch uniform1 = new UniformBranch(new List<Expansion> { new Atom("anAtom"), new Atom("anotherAtom") });
      UniformBranch uniform2 = new UniformBranch(new List<Expansion> { new Atom("anotherAtom"), new Atom("anAtom") });

      Assert.That(uniform1, Is.Not.EqualTo(uniform2));
    }
  }
}
