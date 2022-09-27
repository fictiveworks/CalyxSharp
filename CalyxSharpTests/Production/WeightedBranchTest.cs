using Calyx.Production;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Calyx.Test.Production
{
  internal class WeightedBranchTest
  {
    [Test]
    public void BranchWithMultiChoiceTest()
    {
      WeightedBranch branch = WeightedBranch.Parse(new System.Collections.Generic.Dictionary<string, int> {
        { "lithium", 50 },
        { "silicon", 30 },
        { "carbon", 20 },
      }, new Registry());

      Expansion exp = branch.Evaluate(new Options(seed: 1234));

      Assert.That(exp.Flatten().ToString(), Is.EqualTo("silicon"));
    }

    [Test]
    public void BranchWithDoubleWeights()
    {
      WeightedBranch branch = WeightedBranch.Parse(new System.Collections.Generic.Dictionary<string, double> {
        { "lithium", 0.5 },
        { "silicon", 0.3 },
        { "carbon", 0.2 },
      }, new Registry());

      Expansion exp = branch.Evaluate(new Options(seed: 1234));

      Assert.That(exp.Flatten().ToString(), Is.EqualTo("silicon"));
    }

    [Test]
    public void BranchWithDecimalWeights()
    {
      WeightedBranch branch = WeightedBranch.Parse(new System.Collections.Generic.Dictionary<string, decimal> {
        { "lithium", 0.5m },
        { "silicon", 0.3m },
        { "carbon", 0.2m },
      }, new Registry());

      Expansion exp = branch.Evaluate(new Options(seed: 1234));

      Assert.That(exp.Flatten().ToString(), Is.EqualTo("silicon"));
    }

    [Test]
    public void ResultsRoughlyMatchWeightings()
    {
      int fizzTimes = 700;
      int buzzTimes = 300;

      WeightedBranch branch = WeightedBranch.Parse(new System.Collections.Generic.Dictionary<string, int> {
        { "fizz", fizzTimes },
        { "buzz", buzzTimes },
      },new Registry());

      Expansion[] res = Enumerable.Range(0, fizzTimes + buzzTimes).Select(n => branch.Evaluate(new Options(seed: n))).ToArray();

      int totalFizz = res.Where(exp => exp.Tail[0].Tail[0].Term.Equals("fizz")).Count();
      int totalBuzz = res.Where(exp => exp.Tail[0].Tail[0].Term.Equals("buzz")).Count();

      Assert.That(totalFizz, Is.EqualTo(fizzTimes).Within(1).Percent);
      Assert.That(totalBuzz, Is.EqualTo(buzzTimes).Within(1).Percent);
    }

    [Test]
    public void ResultsApproximateDescendingWeights()
    {
      WeightedBranch branch = WeightedBranch.Parse(new Dictionary<string, int> {
        { "A", 5 },
        { "B", 3 },
        { "C", 2 },
      }, new Registry());

      Expansion[] res = Enumerable.Range(0, 1000).Select(n => branch.Evaluate(new Options(seed: n))).ToArray();

      int countA = res.Where(exp => exp.Tail[0].Tail[0].Term.Equals("A")).Count();
      int countB = res.Where(exp => exp.Tail[0].Tail[0].Term.Equals("B")).Count();
      int countC = res.Where(exp => exp.Tail[0].Tail[0].Term.Equals("C")).Count();

      Assert.That(countA, Is.EqualTo(500).Within(1).Percent);
      Assert.That(countB, Is.EqualTo(300).Within(1).Percent);
      Assert.That(countC, Is.EqualTo(200).Within(1).Percent);
    }

    [Test]
    public void ResultsApproximateAscendingWeights()
    {
      WeightedBranch branch = WeightedBranch.Parse(new Dictionary<string, int> {
        { "C", 2 },
        { "B", 3 },
        { "A", 5 },
      }, new Registry());

      Expansion[] res = Enumerable.Range(0, 1000).Select(n => branch.Evaluate(new Options(seed: n))).ToArray();

      int countA = res.Where(exp => exp.Tail[0].Tail[0].Term.Equals("A")).Count();
      int countB = res.Where(exp => exp.Tail[0].Tail[0].Term.Equals("B")).Count();
      int countC = res.Where(exp => exp.Tail[0].Tail[0].Term.Equals("C")).Count();

      Assert.That(countA, Is.EqualTo(500).Within(1).Percent);
      Assert.That(countB, Is.EqualTo(300).Within(1).Percent);
      Assert.That(countC, Is.EqualTo(200).Within(1).Percent);
    }

    [Test]
    public void ResultsApproximatelyEqualWithUniformWeights()
    {
      WeightedBranch branch = WeightedBranch.Parse(new Dictionary<string, int> {
        { "A", 5 },
        { "B", 5 },
      }, new Registry());

      Expansion[] res = Enumerable.Range(0, 1000).Select(n => branch.Evaluate(new Options(seed: n))).ToArray();

      int countA = res.Where(exp => exp.Tail[0].Tail[0].Term.Equals("A")).Count();
      int countB = res.Where(exp => exp.Tail[0].Tail[0].Term.Equals("B")).Count();

      Assert.That(countA, Is.EqualTo(countB).Within(1).Percent);
    }

    [Test]
    public void SmallestValueDeclaredFirst()
    {
      WeightedBranch branch = WeightedBranch.Parse(new Dictionary<string, int> {
        { "A", 3 },
        { "B", 7 },
      }, new Registry());

      Expansion[] res = Enumerable.Range(0, 1000).Select(n => branch.Evaluate(new Options(seed: n))).ToArray();

      int countA = res.Where(exp => exp.Tail[0].Tail[0].Term.Equals("A")).Count();
      int countB = res.Where(exp => exp.Tail[0].Tail[0].Term.Equals("B")).Count();

      Assert.That(countA, Is.EqualTo(300).Within(1).Percent);
      Assert.That(countB, Is.EqualTo(700).Within(1).Percent);
    }

    [Test]
    public void MultipleValuesOfTheSameRatio()
    {
      WeightedBranch branch = WeightedBranch.Parse(new Dictionary<string, int> {
        { "A", 3 },
        { "B", 4 },
        { "C", 3 },
      }, new Registry());

      Expansion[] res = Enumerable.Range(0, 1000).Select(n => branch.Evaluate(new Options(seed: n))).ToArray();

      int countA = res.Where(exp => exp.Tail[0].Tail[0].Term.Equals("A")).Count();
      int countB = res.Where(exp => exp.Tail[0].Tail[0].Term.Equals("B")).Count();
      int countC = res.Where(exp => exp.Tail[0].Tail[0].Term.Equals("C")).Count();

      Assert.That(countA, Is.EqualTo(300).Within(1).Percent);
      Assert.That(countB, Is.EqualTo(400).Within(1).Percent);
      Assert.That(countC, Is.EqualTo(300).Within(1).Percent);
    }

    [TestCase(0)]
    [TestCase(-1)]
    [TestCase(int.MinValue)]
    [Test]
    public void NonPositiveWeightsAreRejected(int testCase)
    {
      Dictionary<string, int> choices = new Dictionary<string, int> {
        { "yin", 10 },
        { "yang", testCase },
      };

      Assert.Throws<ArgumentOutOfRangeException>(() => WeightedBranch.Parse(choices, new Registry()));
    }

    [Test]
    public void InfiniteWeightsAreRejected()
    {
      Dictionary<string, double> choices = new Dictionary<string, double> {
        { "yin", 1.0 },
        { "yang", double.PositiveInfinity },
      };

      Assert.Throws<ArgumentOutOfRangeException>(() => WeightedBranch.Parse(choices, new Registry()));
    }

    [Test]
    public void NotANumberWeightsAreRejected()
    {
      Dictionary<string, double> choices = new Dictionary<string, double> {
        { "yin", 1.0 },
        { "yang", double.NaN },
      };

      Assert.Throws<ArgumentException>(() => WeightedBranch.Parse(choices, new Registry()));
    }
  }
}
