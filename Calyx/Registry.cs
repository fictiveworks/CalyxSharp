using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Calyx
{
  public class Registry
  {
    private Options options;
    private Dictionary<string, Rule> rules;
    private Dictionary<string, Rule> context;
    private Dictionary<string, Expansion> memos;
    private Dictionary<string, Cycle> cycles;
    private List<Type> filterClasses;

    public Registry(Options options = null)
    {
      this.options = (options != null) ? options : new Options();
      rules = new Dictionary<string, Rule>();
      filterClasses = new List<Type> { typeof (Filters) };
    }

    public void DefineRule(string name, string[] productions)
    {
      Rule rule = Rule.Build(name, productions, this);
      rules[name] = rule;
    }

    public void DefineRule(string name, Dictionary<string, int> productions)
    {
      Rule rule = Rule.Build(name, productions, this);
      rules[name] = rule;
    }

    public void DefineRule(string name, Dictionary<string, double> productions)
    {
      Rule rule = Rule.Build(name, productions, this);
      rules[name] = rule;
    }

    public void DefineRule(string name, Dictionary<string, decimal> productions)
    {
      Rule rule = Rule.Build(name, productions, this);
      rules[name] = rule;
    }

    public Expansion Evaluate(string startSymbol)
    {
      ResetEvaluationContext();

      return new Expansions.Result( Expand(startSymbol).Evaluate(options) );
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

      return new Expansions.Result(Expand(startSymbol).Evaluate(options));
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
        var prod = Expand(symbol);
        int cycleLength = prod.Length;
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
          production = Rule.Empty(symbol);
        }
      }

      return production;
    }

    public Func<string, string> GetFilterComponent(string label)
    {
      MethodInfo matchedFilter = filterClasses.SelectMany((filterClass) => filterClass.GetMethods())
        .Where((m) => m.GetCustomAttributes(typeof(FilterNameAttribute), false).Length > 0)
        .Where((m) => m.GetCustomAttribute<FilterNameAttribute>().Name.Equals(label))
        .FirstOrDefault();

      if (matchedFilter == null)
      {
        throw new Calyx.Errors.UndefinedFilter(label);
      }

      // curry the options object into a method invocation to return a pure modifier function
      return (input) => (string)matchedFilter.Invoke(null, new object[]{ input, options});
    }

    public void AddFilterClass(Type filterClass) {
      filterClasses.Add(filterClass);
    }

    public void ResetEvaluationContext()
    {
      context = new Dictionary<string, Rule>();
      memos = new Dictionary<string, Expansion>();
      cycles = new Dictionary<string, Cycle>();
    }
  }
}
