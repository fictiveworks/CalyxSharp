using System;

namespace Calyx
{
  public class Options
  {
    public const Random DefaultRng = null;
    public const int DefaultSeed = 0;
    public const bool DefaultStrict = false;


    public readonly int Seed;
    public readonly Random Rng;
    public readonly bool Strict;

    public Options(Random rng = DefaultRng, int seed = DefaultSeed, bool strict = DefaultStrict)
    {
      if (seed > 0) {
        this.Rng = new Random(seed);
        this.Seed = seed;
      } else {
        this.Rng = new Random();
      }

      this.Strict = strict;
    }
  }
}