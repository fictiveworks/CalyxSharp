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

      Assert.That(exp, Is.EqualTo(new Expansions.UniformBranch(new Expansions.Template("atom"))));
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

      Assert.That(exp, Is.EqualTo(new Expansions.UniformBranch(new Expansions.Template("silicon"))));
    }
  }
}
