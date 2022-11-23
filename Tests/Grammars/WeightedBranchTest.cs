using System.Collections.Generic;
using Calyx.Errors;
using NUnit.Framework;

namespace Calyx.Test.Grammars
{
  public class WeightedBranchTest
  {
    [Test]
    public void DeclareIntegerWeightedBranchWithRuleBuilder()
    {
      Grammar grammar = new Calyx.Grammar(seed: 1234);
      grammar.Start("{ratio}");
      grammar.Rule("ratio", new Dictionary<string, int> {
        { "4/7", 4 },
        { "2/7", 2 },
        { "1/7", 1 }
      });

      Result result = grammar.Generate();

      Assert.That(result.Text, Is.EqualTo("4/7"));
    }

    public void DeclareDoubleWeightedBranchWithRuleBuilder()
    {
      Grammar grammar = new Calyx.Grammar(seed: 1234);
      grammar.Start("{ratio}");
      grammar.Rule("ratio", new Dictionary<string, double> {
        { "40%", 0.4 },
        { "49%", 0.49 },
        { "11%", 0.11 }
      });

      Result result = grammar.Generate();

      Assert.That(result.Text, Is.EqualTo("4/7"));
    }

    public void DeclareDecimalWeightedBranchWithRuleBuilder()
    {
      Grammar grammar = new Calyx.Grammar(seed: 1234);
      grammar.Start("{ratio}");
      grammar.Rule("ratio", new Dictionary<string, decimal> {
        { "40%", 0.4m },
        { "50%", 0.5m },
        { "10%", 0.1m }
      });

      Result result = grammar.Generate();

      Assert.That(result.Text, Is.EqualTo("4/7"));
    }

    [Test]
    public void DeclareIntegerWeightedBranchWithStartBuilder()
    {
      Grammar grammar = new Calyx.Grammar(seed: 1234);
      grammar.Start(new Dictionary<string, int> {
        { "4/7", 4 },
        { "2/7", 2 },
        { "1/7", 1 }
      });

      Result result = grammar.Generate();

      Assert.That(result.Text, Is.EqualTo("2/7"));
    }

    [Test]
    public void DeclareDoubleWeightedBranchWithStartBuilder()
    {
      Grammar grammar = new Calyx.Grammar(seed: 1234);
      grammar.Start(new Dictionary<string, double> {
        { "40%", 0.4 },
        { "49%", 0.49 },
        { "11%", 0.11 }
      });

      Result result = grammar.Generate();

      Assert.That(result.Text, Is.EqualTo("49%"));
    }

    [Test]
    public void DeclareDecimalWeightedBranchWithStartBuilder()
    {
      Grammar grammar = new Calyx.Grammar(seed: 1234);
      grammar.Start(new Dictionary<string, decimal> {
        { "40%", 0.4m },
        { "50%", 0.5m },
        { "10%", 0.1m }
      });

      Result result = grammar.Generate();

      Assert.That(result.Text, Is.EqualTo("50%"));
    }
  }
}
