using Calyx.Errors;
using NUnit.Framework;

namespace Calyx.Test.Grammars
{
  public class LooseModeTest
  {
    [Test]
    public void UndefinedRuleGeneratesEmptyAtom()
    {
      Grammar grammar = new Calyx.Grammar(strict: false);
      grammar.Start(new[] { "Hello {world}!" });

      Result result = grammar.Generate();

      Assert.That(result.Text, Is.EqualTo("Hello !"));
    }
  }
}
