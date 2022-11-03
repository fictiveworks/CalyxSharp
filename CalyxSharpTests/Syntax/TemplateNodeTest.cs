using Calyx.Syntax;
using NUnit.Framework;
using System.Collections.Generic;
namespace Calyx.Test.Syntax
{
  public class TemplateNodeTest
  {
    [Test]
    public void TemplateWithNoDelimitersTest()
    {
      TemplateNode node = TemplateNode.Parse("one two three", new Registry());

      Expansion exp = node.Evaluate(new Options());

      Assert.That(exp, Is.EqualTo(new Expansions.Template(new Expansions.Atom("one two three"))));
    }

    [Test]
    public void TemplateWithSingleExpansionTest()
    {
      Registry registry = new Registry();
      registry.DefineRule("one", new[] { "ONE" });
      TemplateNode node = TemplateNode.Parse("{one} two three", registry);

      Expansion exp = node.Evaluate(new Options());

      Assert.That(exp, Is.EqualTo(new Expansions.Template(new List<Expansion> {
        new Expansions.Expression( new Expansions.Template(new Expansions.Atom("ONE"))), 
        new Expansions.Atom(" two three"),        
        })));
    }

    [Test] 
    public void TemplateWithMultipleExpansionsTest()
    {
      Registry registry = new Registry();
      registry.DefineRule("one", new[] { "ONE" });
      registry.DefineRule("two", new[] { "TWO" });
      TemplateNode node = TemplateNode.Parse("{one} {two} three", registry);

      Expansion exp = node.Evaluate(new Options());

      Assert.That(exp, Is.EqualTo(new Expansions.Template(new List<Expansion> {
        new Expansions.Expression(new Expansions.Template("ONE")),
        new Expansions.Atom(" "),
        new Expansions.Expression(new Expansions.Template("TWO")),
        new Expansions.Atom(" three"),
      })));
    }

    [Test]
    public void TemplateWithSingleMemoExpansion()
    {
      Registry registry = new Registry(new Options(1234));
      registry.DefineRule("one", new[] { "ONE", "One", "1" });
      registry.ResetEvaluationContext();
      TemplateNode node = TemplateNode.Parse("{@one}{@one}{@one}", registry);

      Expansion exp = node.Evaluate(new Options());

      Assert.That(exp, Is.EqualTo(new Expansions.Template(new List<Expansion> {
        new Expansions.Memo(new Expansions.UniformBranch(new Expansions.Template("One"))),
        new Expansions.Memo(new Expansions.UniformBranch(new Expansions.Template("One"))),
        new Expansions.Memo(new Expansions.UniformBranch(new Expansions.Template("One"))),
      })));
    }

    [Test]
    public void TemplateWithSingleUniqueExpansion()
    {
      Registry registry = new Registry(new Options(seed: 87654321));
      registry.DefineRule("one", new[] { "ONE", "One", "1" });
      registry.ResetEvaluationContext();
      TemplateNode node = TemplateNode.Parse("{$one}{$one}{$one}", registry);

      Expansion exp = node.Evaluate(new Options());

      Assert.That(exp, Is.EqualTo(new Expansions.Template(new List<Expansion> {
        new Expansions.Uniq(new Expansions.UniformBranch(new Expansions.Template("ONE"))),
        new Expansions.Uniq(new Expansions.UniformBranch(new Expansions.Template("One"))),
        new Expansions.Uniq(new Expansions.UniformBranch(new Expansions.Template("1"))),
      })));
    }
  }
}
