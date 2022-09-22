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
        Rng = new Random(seed);
        Seed = seed;
      } else {
        Rng = new Random();
      }

      Strict = strict;
    }
  }
}
