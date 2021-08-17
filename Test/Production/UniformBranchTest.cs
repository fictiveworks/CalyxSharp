using Calyx;
using Calyx.Production;
using NUnit.Framework;
using System;

namespace Calyx.Test.Production
{
  public class UniformBranchTest
  {
    [Test]
    public void BranchWithSingleChoiceTest()
    {
      UniformBranch branch = UniformBranch.Parse(new[] {
        "atom"
      }, new Registry());

      Expansion exp = branch.Evaluate(new Options());

      Assert.AreEqual(Exp.UniformBranch, exp.symbol);
      Assert.AreEqual(Exp.Template, exp.tail[0].symbol);
      //Assert.AreEqual("atom", exp.tail.Last().tail.Last().terminal);
    }

    [Test]
    public void BranchWithMultiChoiceTest()
    {
      UniformBranch branch = UniformBranch.Parse(new[] {
        "lithium",
        "silicon",
        "carbon"
      }, new Registry());

      Expansion exp = branch.Evaluate(new Options(seed: 1234));

      Assert.AreEqual(Exp.UniformBranch, exp.symbol);
      Assert.AreEqual(Exp.Template, exp.tail[0].symbol);
      Assert.AreEqual(Exp.Atom, exp.tail[0].tail[0].symbol);
      Assert.AreEqual("silicon", exp.tail[0].tail[0].term);
    }
  }
}
