namespace Calyx.Test
{
  public class OptionsTest
  {
    [Test]
    public void StrictOffByDefaultTest()
    {
      Options opts = new Options();
      Assert.That(opts.Strict, Is.False);
    }

    [Test]
    public void StrictOnWithFlagTest()
    {
      Options opts = new Options(strict: true);
      Assert.That(opts.Strict, Is.True);
    }

    [Test]
    public void RngFromGivenSeedTest()
    {
      Options opts = new Options(seed: 1234567890);
      Assert.That(opts.Rng.Next(100), Is.EqualTo(54));
      Assert.That(opts.Rng.Next(100), Is.EqualTo(42));
    }

    [Test]
    public void NamedArgsInAnyOrderTest()
    {
      Options opts = new Options(strict: true, seed: 1234567890);
      Assert.That(opts.Rng.Next(100), Is.EqualTo(54));
      Assert.That(opts.Rng.Next(100), Is.EqualTo(42));
      Assert.That(opts.Strict, Is.True);
    }
  }
}
