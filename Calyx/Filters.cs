using System.Globalization;
using System.Text.RegularExpressions;

namespace Calyx
{
  public static class Filters {
    [FilterName("uppercase")]
    public static string UpperCase(string input, Options options) => input.ToUpper();

    [FilterName("lowercase")]
    public static string LowerCase(string input, Options options) => input.ToLower();

    [FilterName("titlecase")]
    public static string TitleCase(string input, Options options) => CultureInfo.InvariantCulture.TextInfo.ToTitleCase(input);

    [FilterName("sentencecase")]
    public static string SentenceCase(string input, Options options) 
    {
      // draft implementation from https://stackoverflow.com/a/3141467/5307038
      var lowerCase = input.ToLower();
      // matches the first sentence of a string, as well as subsequent sentences
      var r = new Regex(@"(^[a-z])|\.\s+(.)", RegexOptions.ExplicitCapture);
      // MatchEvaluator delegate defines replacement of setence starts to uppercase
      var result = r.Replace(lowerCase, s => s.Value.ToUpper());
      return result;
    }
    
    [FilterName("length")]
    public static string Length(string input, Options options) => input.Length.ToString();

    [FilterName("emphasis")]
    public static string Emphasis(string input, Options options) => $"*{input}*";
  }
}
