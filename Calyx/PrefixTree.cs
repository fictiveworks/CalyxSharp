namespace Calyx
{
  public class PrefixTree
  {
    public static string CommonPrefix(string a, string b)
    {
      string selectedPrefix = "";
      int index = 0;
      int minIndexLength = a.Length < b.Length ? a.Length : b.Length;

      while (index < minIndexLength) {
        if (a[index] != b[index]) return selectedPrefix;

        selectedPrefix += a[index];
        index++;
      }

      return selectedPrefix;
    }
  }
}
