namespace Calyx.Syntax
{
  public class ExpressionNode : IProduction
  {
    private string reference;
    private Registry registry;

    public ExpressionNode(string reference, Registry registry)
    {
      this.reference = reference;
      this.registry = registry;
    }

    public Expansion Evaluate(Options options)
    {
      Expansion eval = this.registry.Expand(this.reference).Evaluate(options);
      return new Expansion(Exp.Expression, eval.tail);
    }
  }
}
