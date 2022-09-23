using Calyx.Production;
using NUnit.Framework;

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

      Assert.That(exp.symbol, Is.EqualTo(Exp.UniformBranch));
      Assert.That(exp.tail[0].symbol, Is.EqualTo(Exp.Template));
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

      Assert.That(exp.symbol, Is.EqualTo(Exp.UniformBranch));
      Assert.That(exp.tail[0].symbol, Is.EqualTo(Exp.Template));
      Assert.That(exp.tail[0].tail[0].symbol, Is.EqualTo(Exp.Atom));
      Assert.That(exp.tail[0].tail[0].term, Is.EqualTo("silicon"));
    }
  }
}
