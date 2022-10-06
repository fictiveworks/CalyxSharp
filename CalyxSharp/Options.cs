using System;

namespace Calyx
{
  public class Options
  {
    public const bool DefaultStrict = false;

    public readonly Random Rng;
    public readonly bool Strict;

    /// <summary>
    /// Create a new Options object with the default random number generator
    /// </summary>
    /// <param name="strict">Determines if the parser should throw an error when encountering an undefined key</param>
    /// <returns></returns>
    public Options(bool strict = DefaultStrict) : this(new Random(), strict) {}

    /// <summary>
    /// Create a new options object with the specified random seed
    /// </summary>
    /// <param name="seed">The random seed to use</param>
    /// <param name="strict">Determines if the parser should throw an error when encountering an undefined key</param>
    /// <returns></returns>
    public Options(int seed, bool strict = DefaultStrict) : this(new Random(seed), strict) {}

    /// <summary>
    /// Create a new options object with the specified 
    /// </summary>
    /// <param name="rng"></param>
    /// <param name="strict"></param>
    public Options(Random rng, bool strict = DefaultStrict)
    {
      Rng = rng;
      Strict = strict;
    }
  }
}
