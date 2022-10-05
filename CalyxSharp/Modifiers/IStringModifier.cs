namespace Calyx.Modifiers
{
  /// <summary>
  /// Represents an object that tranforms string values
  /// </summary>
  public interface IStringModifier {
    /// <summary>
    /// Transform the supplied string
    /// </summary>
    /// <param name="input">The string to transform</param>
    /// <returns></returns>
    string Modify(string input);
  }
}

