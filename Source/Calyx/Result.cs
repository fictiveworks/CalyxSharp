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
      get => this.root;
    }

    public string Text
    {
      get => this.root.Flatten().ToString();
    }

    public override string ToString()
    {
      return this.Text;
    }
  }
}
