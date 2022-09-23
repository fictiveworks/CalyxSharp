using Calyx;
using Calyx.Errors;
using NUnit.Framework;
using System;

namespace Calyx.Test
{
  public class CycleTest
  {
    [Test]
    public void CycleLengthOneAlwaysReturnsZerothIndex()
    {
      Cycle cycle = new Cycle(new Options(), 1);

      Assert.That(cycle.Poll(), Is.EqualTo(0));
      Assert.That(cycle.Poll(), Is.EqualTo(0));
      Assert.That(cycle.Poll(), Is.EqualTo(0));
    }

    [Test]
    public void CycleLengthTwoReturnsOngoingSequence()
    {
      Cycle cycle = new Cycle(new Options(seed: 1234567890), 2);

      Assert.That(cycle.Poll(), Is.EqualTo(0));
      Assert.That(cycle.Poll(), Is.EqualTo(1));
      Assert.That(cycle.Poll(), Is.EqualTo(0));
      Assert.That(cycle.Poll(), Is.EqualTo(1));
    }
  }
}
