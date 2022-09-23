using System;

namespace Calyx.Errors
{
  public class UndefinedRule : Exception
  {
    public UndefinedRule(string symbol) : base($"undefined rule: '{symbol}'")
    {
    }
  }
}
