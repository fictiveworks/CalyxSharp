using System.Linq;
namespace Calyx.Syntax
{
  public class ExpressionChain : IProduction
  {
    private string[] components;
    private Registry registry;

    public ExpressionChain(string[] components, Registry registry)
    {
      this.registry = registry;
      this.components = components;
    }

    public Expansion Evaluate(Options options)
    {
      Expansion eval = registry.Expand(components[0]).Evaluate(options);
      string initial =  new Expansion(Exp.Expression, eval.Tail).Flatten().ToString();

      // Dynamic dispatch to string modifiers one after another
      string modified = components
        .Skip(1)
        .Aggregate(initial, (accumulator, filterName) => registry.GetFilterComponent(filterName).Invoke(accumulator));
      
      return new Expansion(Exp.Expression, new Expansion(Exp.Atom, modified));
    }
  }
}
