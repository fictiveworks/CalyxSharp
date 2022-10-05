using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Calyx.Modifiers
{
  public class StringModifier: IStringModifier {
    Func<string, string> transformer;
    public StringModifier(Func<string, string> transformer) {
      this.transformer = transformer;
    }

    public string Modify(string input) {
      return transformer.Invoke(input);
    }

    public static string ToTitleCase(string input) {
      return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(input);
    }    
    
    public static string ToSentenceCase(string input) {
      // draft implementation from https://stackoverflow.com/a/3141467/5307038
      var lowerCase = input.ToLower();
      // matches the first sentence of a string, as well as subsequent sentences
      var r = new Regex(@"(^[a-z])|\.\s+(.)", RegexOptions.ExplicitCapture);
      // MatchEvaluator delegate defines replacement of setence starts to uppercase
      var result = r.Replace(lowerCase, s => s.Value.ToUpper());
      return result;
    }

    /// <summary>
    /// Return a dictionary of the builtin modifiers
    /// </summary>
    public static Dictionary<string, IStringModifier> GetBuiltins() {
      return InstantiateSimpleModifiers(new Dictionary<string, Func<string, string>> {
        { "uppercase", (s) => s.ToUpper() },
        { "lowercase", (s) => s.ToLower() },
        { "titlecase", ToTitleCase },
        { "sentencecase", ToSentenceCase },
        { "emphasis", (s) => $"*{s}*"},
        { "length", (s) => s.Length.ToString() },
      });
    }

    public static Dictionary<string, IStringModifier> InstantiateSimpleModifiers(Dictionary<string, Func<string, string>> modifiers) {
      var dictionary = new Dictionary<string, IStringModifier>();
      foreach(var keyValuePair in modifiers) {
        dictionary.Add(keyValuePair.Key, new StringModifier(keyValuePair.Value));
      }
      return dictionary;
    }
  }
}

