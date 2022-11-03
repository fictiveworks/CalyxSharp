using System.Text;

namespace Calyx
{
  public abstract class Expansion
  {

    public StringBuilder Flatten()
    {
      StringBuilder concat = new StringBuilder();
      CollectAtoms(concat);
      return concat;
    }

    protected internal abstract void CollectAtoms(StringBuilder concat);
  }
}
