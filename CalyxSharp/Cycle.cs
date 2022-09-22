namespace Calyx
{
  public class Cycle
  {
    Options options;
    int index;
    List<int> sequence;

    public Cycle(Options options, int count)
    {
      if (count < 1) {
        throw new ArgumentException("'count' must be greater than zero");
      }
      this.options = options;
      index = 0;
      sequence = Enumerable.Range(0, count).ToList();
      Shuffle();
    }

    public void Shuffle()
    {
      int current = sequence.Count;
      while (current > 1) {
        current--;
        int target = options.Rng.Next(current + 1);
        int swap = sequence[target];
        sequence[target] = sequence[current];
        sequence[current] = swap;
      }
    }

    public int Poll()
    {
      // TODO: repeat ad infinitum or reshuffle each time final index is polled?
      return sequence[index++ % sequence.Count];
    }
  }
}
