namespace Calyx.Syntax
{
  public class ExpressionNode : IProduction
  {
    private string reference;
    private Registry registry;

    public static IProduction Parse(string raw, Registry registry)
    {
      if (raw[0].Equals('@')) {
        return new MemoNode(raw.Substring(1), registry);
      } else {
        return new ExpressionNode(raw, registry);
      }
    }

    public ExpressionNode(string reference, Registry registry)
    {
      this.reference = reference;
      this.registry = registry;
    }

    public Expansion Evaluate(Options options)
    {
      Expansion eval = this.registry.Expand(this.reference).Evaluate(options);
      return new Expansion(Exp.Expression, eval.Tail);
    }
  }
}
