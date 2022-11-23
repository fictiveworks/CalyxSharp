namespace Calyx.Syntax
{
  public class ExpressionNode : IProduction
  {
    private string reference;
    private Registry registry;

    private static readonly char MEMO_SIGIL = '@';
    private static readonly char UNIQUE_SIGIL = '$';

    public static IProduction Parse(string raw, Registry registry)
    {
      if (raw[0].Equals(MEMO_SIGIL))
      {
        return new MemoNode(raw.Substring(1), registry);
      }
      else if (raw[0].Equals(UNIQUE_SIGIL))
      {
        return new UniqNode(raw.Substring(1), registry);
      }
      else
      {
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
      Expansion eval = registry.Expand(reference).Evaluate(options);
      return new Expansion(Exp.Expression, eval.Tail);
    }
  }
}
