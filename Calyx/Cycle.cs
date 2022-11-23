using System;
using System.Linq;

namespace Calyx
{
  public class Cycle
  {
    Options options;
    int index;
    readonly int count;
    int[] sequence;

    public Cycle(Options options, int count)
    {
      if (count < 1) {
        throw new ArgumentException("'count' must be greater than zero");
      }
      this.options = options;
      this.count = count;
      index = count - 1;  // defer shuffling until the first Poll()
    }

    public void Shuffle()
    {
      sequence = Enumerable.Range(0, count).ToArray();
      int current = count;
      while (current > 1) {
        current--;
        int target = options.Rng.Next(current + 1);
        (sequence[target], sequence[current]) = (sequence[current], sequence[target]);
      }
    }

    public int Poll()
    {
      index++;
      if (index == count)
      {
        Shuffle();
        index = 0;
      }
      return sequence[index];
    }
  }
}
