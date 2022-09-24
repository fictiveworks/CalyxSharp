using Calyx;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Calyx.Production
{
  public class WeightedBranch
  {
    /* if this constructor is ever made public, hoist the bounds checks from the
     * Parse(Dictionary<string, double>, Registry) convenience init into the constructor */
    private WeightedBranch(WeightedProduction[] productions, Registry registry)
    {
      this.productions = productions;
      this.registry = registry;
      if (productions.Any(p => p.Weight <= 0))
      {
        throw new ArgumentOutOfRangeException("All weights must be greater than zero");
      }
      sumOfWeights = productions.Sum(production => production.Weight);
    }

    private WeightedProduction[] productions;
    private Registry registry;
    private double sumOfWeights;

    public Expansion Evaluate(Options options)
    {
      double max = sumOfWeights;
      double waterMark = options.Rng.NextDouble() * sumOfWeights;
      WeightedProduction production = productions.FirstOrDefault(wp => waterMark >= (max -= wp.Weight));
      return new Expansion(Exp.WeightedBranch, production.Production.Evaluate(options));
    }

    /* Until C# 7, which will allow generic types to constrained to `INumber`, we'll support
     * the different numeric types by providing a convenience init for each of them */

    public static WeightedBranch Parse(Dictionary<string, int> choices, Registry registry)
    {
      WeightedProduction[] weightedProductions = choices.Select(choice => new WeightedProduction { 
        Production = Syntax.TemplateNode.Parse(choice.Key, registry),
        Weight = choice.Value,
      }).ToArray();
      return new WeightedBranch(weightedProductions, registry);
    }

    public static WeightedBranch Parse(Dictionary<string, double> choices, Registry registry)
    {
      if (choices.Any(p => double.IsInfinity(p.Value)))
      {
        throw new ArgumentOutOfRangeException("Weights may not be infinite");
      }
      if (choices.Any(p => double.IsNaN(p.Value)))
      {
        throw new ArgumentException("Weights may not be NaN");
      }

      WeightedProduction[] weightedProductions = choices.Select(choice => new WeightedProduction
      {
        Production = Syntax.TemplateNode.Parse(choice.Key, registry),
        Weight = choice.Value,
      }).ToArray();
      return new WeightedBranch(weightedProductions, registry);
    }

    public static WeightedBranch Parse(Dictionary<string, decimal> choices, Registry registry)
    {
      WeightedProduction[] weightedProductions = choices.Select(choice => new WeightedProduction
      {
        Production = Syntax.TemplateNode.Parse(choice.Key, registry),
        Weight = (double)choice.Value,
      }).ToArray();
      return new WeightedBranch(weightedProductions, registry);
    }
    internal struct WeightedProduction
    {
      public double Weight;
      public IProduction Production;
    }
  }
}
