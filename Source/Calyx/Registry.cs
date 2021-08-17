using System.Collections.Generic;

namespace Calyx
{
  public class Registry
  {
    private Options options;
    private Dictionary<string, Rule> rules;

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

    public Rule Expand(string symbol)
    {
      Rule production = null;

      if (this.rules.ContainsKey(symbol)) {
        production = this.rules[symbol];
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
      //this.memos =
      //this.uniques =
    }
  }
}
