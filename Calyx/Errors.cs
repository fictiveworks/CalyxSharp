using System;

namespace Calyx.Errors
{
  public class UndefinedRule : Exception
  {
    public UndefinedRule(string symbol) : base($"undefined rule: '{symbol}'") { }
  }

  public class UndefinedFilter: Exception {
    public UndefinedFilter(string symbol) : base($"undefined filter: '{symbol}'") { }
  }
  public class IncorrectFilterSignature: Exception {
    public  IncorrectFilterSignature(string symbol): base($"incorrect method signature for filter: '{symbol}'") { }
  }
}
