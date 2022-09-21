using System;
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
      rules = new Dictionary<string, Rule>();
    }

    public void DefineRule(string name, string[] productions)
    {
      Rule rule = Rule.Build(name, productions, this);
      rules[name] = rule;
    }

    public Expansion Evaluate(string startSymbol)
    {
      ResetEvaluationContext();

      Expansion rootExpression = new Expansion(
        Exp.Result,
        Expand(startSymbol).Evaluate(options)
      );

      return rootExpression;
    }

    public Expansion Evaluate(string startSymbol, Dictionary<string, string[]> context)
    {
      ResetEvaluationContext();

      foreach(KeyValuePair<string, string[]> rule in context) {
        // if (this.rules[key] && this.options.Strict) {
        //   throw new Error(`DuplicateRule: ${key}`)
        // }

        this.context[rule.Key] = Rule.Build(rule.Key, rule.Value, this);
      }

      Expansion rootExpression = new Expansion(
        Exp.Result,
        Expand(startSymbol).Evaluate(options)
      );

      return rootExpression;
    }

    public Expansion MemoizeExpansion(string symbol)
    {
      if (!memos.ContainsKey(symbol)) {
        memos.Add(symbol, Expand(symbol).Evaluate(options));
      }

      return memos[symbol];
    }

    public Expansion UniqueExpansion(string symbol)
    {
      // If this symbol has not been expanded as a cycle then register it
      if (!cycles.ContainsKey(symbol)) {
        int cycleLength = Expand(symbol).Length;
        cycles.Add(symbol, new Cycle(options, cycleLength));
      }

      return Expand(symbol).EvaluateAt(cycles[symbol].Poll(), options);
    }

    public Rule Expand(string symbol)
    {
       Rule production;
       if (rules.ContainsKey(symbol)) {
        production = rules[symbol];
      } else if (context.ContainsKey(symbol)) {
        production = context[symbol];
      } else {
        if (options.Strict) {
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
      context = new Dictionary<string, Rule>();
      memos = new Dictionary<string, Expansion>();
      cycles = new Dictionary<string, Cycle>();
    }
  }

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
