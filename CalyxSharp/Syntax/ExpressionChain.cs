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
      // foreach (string label in components) {
      //   FilterComponent component = this.registry.GetFilterComponent(label);
      //   this.components.Add(component);
      // }
    }

    public Expansion Evaluate(Options options)
    {
      // Dynamic dispatch to string modifiers one after another
      //
      // delegate method call with expression input
      return new Expansion(Exp.Expression, components[0]);
    }
  }
}
