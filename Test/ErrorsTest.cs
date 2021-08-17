using Calyx;
using Calyx.Errors;
using NUnit.Framework;
using System;

namespace Calyx.Test
{
  public class ErrorsTest
  {
    [Test]
    public void UndefinedRuleMessage()
    {
      UndefinedRule undefined = Assert.Throws<UndefinedRule>(
        delegate { throw new UndefinedRule("next_rule"); });

      Assert.That(undefined.Message, Is.EqualTo("undefined rule: 'next_rule'"));
    }
  }
}
