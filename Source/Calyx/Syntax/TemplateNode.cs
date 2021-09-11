using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Calyx;

namespace Calyx.Syntax
{
  public class TemplateNode : Calyx.IProduction
  {
    private static string EXPRESSION = "(\\{[A-Za-z0-9_@$<>\\.]+\\})";
    private static string START_TOKEN = "{";
    private static string END_TOKEN = "}";
    private static string DEREF_TOKEN = "\\.";

    public static TemplateNode Parse(string raw, Registry registry)
    {
      string[] fragments = Regex.Split(raw, TemplateNode.EXPRESSION);
      List<IProduction> concatNodes = new List<IProduction>();

      foreach (string atom in fragments) {
        if (String.IsNullOrEmpty(atom)) continue;

        // Check if this is a template expression or atom
        if (atom.StartsWith(TemplateNode.START_TOKEN) && atom.EndsWith(TemplateNode.END_TOKEN)) {
          // Remove delimiters
          string expression = atom.Substring(1, atom.Length - 2);

          // Dereference the expression as an array of filter components
          string[] components = Regex.Split(expression, TemplateNode.DEREF_TOKEN);

          // Check if we have a post-processing chain
          if (components.Length > 1) {
            // Generate a chained expression headed by a non-terminal
            concatNodes.Add(new ExpressionChain(components, registry));
          } else {
            // Generate a standalone non-terminal expression
            concatNodes.Add(ExpressionNode.Parse(components[0], registry));
          }
        } else {
          // Collect a string terminal
          concatNodes.Add(new AtomNode(atom));
        }
      }

      return new TemplateNode(concatNodes, registry);
    }

    private Registry registry;
    private List<IProduction> concatNodes;

    public TemplateNode(List<IProduction> concatNodes, Registry registry)
    {
      this.concatNodes = concatNodes;
      this.registry = registry;
    }

    public Expansion Evaluate(Options options)
    {
      List<Expansion> expansion = new List<Expansion>();

      foreach(IProduction syntaxNode in this.concatNodes) {
        expansion.Add(syntaxNode.Evaluate(options));
      }

      return new Expansion(Exp.Template, expansion);
    }
  }
}
