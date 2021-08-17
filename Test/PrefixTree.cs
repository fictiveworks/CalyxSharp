using Calyx;
using NUnit.Framework;

namespace Calyx.Test
{
  public class PrefixTreeTest
  {
    [Test]
    public void LongestCommonPrefix()
    {
      Assert.That(PrefixTree.CommonPrefix("a", "b"), Is.EqualTo(""));
      Assert.That(PrefixTree.CommonPrefix("aaaaa", "aab"), Is.EqualTo("aa"));
      Assert.That(PrefixTree.CommonPrefix("aa", "ab"), Is.EqualTo("a"));
      Assert.That(PrefixTree.CommonPrefix("ababababahahahaha", "ababafgfgbaba"), Is.EqualTo("ababa"));
      Assert.That(PrefixTree.CommonPrefix("abab", "abab"), Is.EqualTo("abab"));
    }
  }
}
