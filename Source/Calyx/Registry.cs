using System.Collections.Generic;
using System.Linq;

namespace Calyx
{
  public class Registry
  {
    private Options options;
    private Dictionary<string, Rule> rules;
    private Dictionary<string, Rule> context;
    private Dictionary<string, Expansion> memos;
    private Dictionary<string, Cycle> cycles;

    public Registry(Options options = null)
    {
      this.options = (options != null) ? options : new Options();
      this.rules = new Dictionary<string, Rule>();
    }

    public void DefineRule(string name, string[] productions)
    {
      Rule rule = Rule.Build(name, productions, this);
      this.rules[name] = rule;
    }

    public Expansion Evaluate(string startSymbol)
    {
      this.ResetEvaluationContext();

      Expansion rootExpression = new Expansion(
        Exp.Result,
        this.Expand(startSymbol).Evaluate(this.options)
      );

      return rootExpression;
    }

    public Expansion Evaluate(string startSymbol, Dictionary<string, string[]> context)
    {
      this.ResetEvaluationContext();

      foreach(KeyValuePair<string, string[]> rule in context) {
        // if (this.rules[key] && this.options.Strict) {
        //   throw new Error(`DuplicateRule: ${key}`)
        // }

        this.context[rule.Key] = Rule.Build(rule.Key, rule.Value, this);
      }

      Expansion rootExpression = new Expansion(
        Exp.Result,
        this.Expand(startSymbol).Evaluate(this.options)
      );

      return rootExpression;
    }

    public Expansion MemoizeExpansion(string symbol)
    {
      if (!this.memos.ContainsKey(symbol)) {
        this.memos.Add(symbol, this.Expand(symbol).Evaluate(this.options));
      }

      return this.memos[symbol];
    }

    public Expansion UniqueExpansion(string symbol)
    {
      // If this symbol has not been expanded as a cycle then register it
      if (!this.cycles.ContainsKey(symbol)) {
        int cycleLength = this.Expand(symbol).Length;
        this.cycles.Add(symbol, new Cycle(this.options, cycleLength));
      }

      return this.Expand(symbol).EvaluateAt(this.cycles[symbol].Poll(), this.options);
    }

    public Rule Expand(string symbol)
    {
      Rule production = null;

      if (this.rules.ContainsKey(symbol)) {
        production = this.rules[symbol];
      } else if (this.context.ContainsKey(symbol)) {
        production = this.context[symbol];
      } else {
        if (this.options.Strict) {
          throw new Errors.UndefinedRule(symbol);
        } else {
          production = Rule.Empty();
        }
      }
      return production;
    }

    // public FilterComponent GetFilterComponent(label)
    // {
    //   delegate filter component binding somewhere here
    // }

    public void ResetEvaluationContext()
    {
      this.context = new Dictionary<string, Rule>();
      this.memos = new Dictionary<string, Expansion>();
      this.cycles = new Dictionary<string, Cycle>();
    }
  }

  class Cycle
  {
    Options options;
    int index;
    List<int> sequence;

    public Cycle(Options options, int count)
    {
      this.options = options;
      this.index = 0;
      this.sequence = Enumerable.Range(0, count).ToList();
      this.Shuffle();
    }

    public void Shuffle()
    {
      int current = this.sequence.Count;
      while (current > 1) {
        current--;
        int target = this.options.Rng.Next(current + 1);
        int swap = this.sequence[target];
        this.sequence[target] = this.sequence[current];
        this.sequence[current] = swap;
      }
    }

    public int Poll()
    {
      // TODO: repeat ad infinitum or reshuffle each time final index is polled?
      return this.sequence[this.index++ % this.sequence.Count];
    }
  }
}
