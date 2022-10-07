using System;

namespace Calyx
{
  public class FilterNameAttribute : Attribute
  {
    private string name;
    public FilterNameAttribute(string name)
    {
      this.name = name;
    }

    public string Name => name;
  }
}
