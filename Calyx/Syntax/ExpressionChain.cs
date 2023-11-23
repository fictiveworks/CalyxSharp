using System;
using System.Linq;
using System.Reflection;
using Calyx.Errors;

namespace Calyx.Syntax
{
  public class ExpressionChain : IProduction
  {
    private string ruleName;
    private string[] components;
    private Registry registry;

    public ExpressionChain(string ruleName, string[] components, Registry registry)
    {
      RemoveSigil(ref ruleName);
      this.ruleName = ruleName;
      this.registry = registry;
      this.components = components.Skip(1).ToArray();
    }

    public Expansion Evaluate(Options options)
    {
      Expansion eval = registry.Expand(ruleName).Evaluate(options);
      string initial =  new Expansion(Exp.Expression, eval.Tail).Flatten().ToString();

      // Dynamic dispatch to string modifiers one after another
      string modified = components
        .Aggregate(initial, (accumulator, filterName) => {
          try {
            return registry.GetFilterComponent(filterName).Invoke(accumulator);
          }
          catch (ArgumentException)
          {
            throw new IncorrectFilterSignature(filterName);
          }
          catch (TargetParameterCountException)
          {
            throw new IncorrectFilterSignature(filterName);
          }
        }); 
      
      return new Expansion(Exp.Expression, new Expansion(Exp.Atom, modified));
    }

    private static void RemoveSigil(ref string ruleName)
    {
      if (ExpressionNode.IsSigil(ruleName[0]))
      {
        ruleName = ruleName.Substring(1);
      }
    }
  }
}
