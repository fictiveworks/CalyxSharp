namespace Calyx
{
  public class Result
  {
    private Expansion root;

    public Result(Expansion expanded)
    {
      this.root = expanded;
    }

    public Expansion Tree
    {
      get => root;
    }

    public string Text
    {
      get => root.Flatten().ToString();
    }

    public override string ToString()
    {
      return Text;
    }
  }
}
