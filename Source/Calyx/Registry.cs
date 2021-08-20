using System.Collections.Generic;

namespace Calyx
{
  public class Registry
  {
    private Options options;
    private Dictionary<string, Rule> rules;
    private Dictionary<string, Rule> context;

    public Registry(Options options = null)
    {
      this.options = (options != null) ? options : new Options();
      this.rules = new Dictionary<string, Rule>();
      //this.memos =
      //this.uniques =
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

    private void ResetEvaluationContext()
    {
      this.context = new Dictionary<string, Rule>();
      //this.memos =
      //this.uniques =
    }
  }
}
