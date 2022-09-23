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

      Assert.That(exp.Symbol, Is.EqualTo(Exp.UniformBranch));
      Assert.That(exp.Tail[0].Symbol, Is.EqualTo(Exp.Template));
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

      Assert.That(exp.Symbol, Is.EqualTo(Exp.UniformBranch));
      Assert.That(exp.Tail[0].Symbol, Is.EqualTo(Exp.Template));
      Assert.That(exp.Tail[0].Tail[0].Symbol, Is.EqualTo(Exp.Atom));
      Assert.That(exp.Tail[0].Tail[0].Term, Is.EqualTo("silicon"));
    }
  }
}
