namespace Calyx.Test
{
  public class RegistryTest
  {
    [Test]
    public void EvaluateStartRule()
    {
      Registry registry = new Registry();
      registry.DefineRule("start", new[] { "atom" });
      Expansion exp = registry.Evaluate("start");
      Assert.That(exp.Symbol, Is.EqualTo(Exp.Result));
      Assert.That(exp.Tail[0].Symbol, Is.EqualTo(Exp.UniformBranch));
      Assert.That(exp.Tail[0].Tail[0].Symbol, Is.EqualTo(Exp.Template));
      Assert.That(exp.Tail[0].Tail[0].Tail[0].Symbol, Is.EqualTo(Exp.Atom));
      Assert.That(exp.Tail[0].Tail[0].Tail[0].Term, Is.EqualTo("atom"));
    }

    [Test]
    public void EvaluateRecursiveRules()
    {
      Registry registry = new Registry();
      registry.DefineRule("start", new[] { "{atom}" });
      registry.DefineRule("atom", new[] { "atom" });
      Expansion exp = registry.Evaluate("start");
      Assert.That(exp.Symbol, Is.EqualTo(Exp.Result));
      Assert.That(exp.Tail[0].Symbol, Is.EqualTo(Exp.UniformBranch));
      Assert.That(exp.Tail[0].Tail[0].Symbol, Is.EqualTo(Exp.Template));
      Assert.That(exp.Tail[0].Tail[0].Tail[0].Symbol, Is.EqualTo(Exp.Expression));
      Assert.That(exp.Tail[0].Tail[0].Tail[0].Tail[0].Symbol, Is.EqualTo(Exp.Template));
      Assert.That(exp.Tail[0].Tail[0].Tail[0].Tail[0].Tail[0].Symbol, Is.EqualTo(Exp.Atom));
      Assert.That(exp.Tail[0].Tail[0].Tail[0].Tail[0].Tail[0].Term, Is.EqualTo("atom"));
    }

    [Test]
    public void EvaluateRulesWithInitializedContext()
    {
      Registry registry = new Registry();
      registry.DefineRule("start", new[] { "{atom}" });
      Expansion exp = registry.Evaluate("start", new Dictionary<string, string[]>() {
        ["atom"] = new[] { "atom" }
      });
      Assert.That(exp.Symbol, Is.EqualTo(Exp.Result));
      Assert.That(exp.Tail[0].Symbol, Is.EqualTo(Exp.UniformBranch));
      Assert.That(exp.Tail[0].Tail[0].Symbol, Is.EqualTo(Exp.Template));
      Assert.That(exp.Tail[0].Tail[0].Tail[0].Symbol, Is.EqualTo(Exp.Expression));
      Assert.That(exp.Tail[0].Tail[0].Tail[0].Tail[0].Symbol, Is.EqualTo(Exp.Template));
      Assert.That(exp.Tail[0].Tail[0].Tail[0].Tail[0].Tail[0].Symbol, Is.EqualTo(Exp.Atom));
      Assert.That(exp.Tail[0].Tail[0].Tail[0].Tail[0].Tail[0].Term, Is.EqualTo("atom"));
    }

    [Test]
    public void EvaluateOnlyInitializedContext()
    {
      Registry registry = new Registry();
      registry.DefineRule("start", new[] { "{atom}" });
      Expansion exp = registry.Evaluate("start", new Dictionary<string, string[]>() {
        { "start", new[] { "{atom}" }},
        { "atom", new[] { "atom" }}
      });
      Assert.That(exp.Symbol, Is.EqualTo(Exp.Result));
      Assert.That(exp.Tail[0].Symbol, Is.EqualTo(Exp.UniformBranch));
      Assert.That(exp.Tail[0].Tail[0].Symbol, Is.EqualTo(Exp.Template));
      Assert.That(exp.Tail[0].Tail[0].Tail[0].Symbol, Is.EqualTo(Exp.Expression));
      Assert.That(exp.Tail[0].Tail[0].Tail[0].Tail[0].Symbol, Is.EqualTo(Exp.Template));
      Assert.That(exp.Tail[0].Tail[0].Tail[0].Tail[0].Tail[0].Symbol, Is.EqualTo(Exp.Atom));
      Assert.That(exp.Tail[0].Tail[0].Tail[0].Tail[0].Tail[0].Term, Is.EqualTo("atom"));
    }

    [Test]
    public void MemoizedRulesReturnIdenticalExpansion()
    {
      Registry registry = new Registry(new Options(seed: 556677));
      registry.DefineRule("start", new[] { "{@atom}{@atom}{@atom}" });
      registry.DefineRule("atom", new[] { ",", ":", ";" });
      Expansion exp = registry.Evaluate("start");
      Assert.That(exp.Symbol, Is.EqualTo(Exp.Result));
      Assert.That(exp.Tail[0].Tail[0].Tail[0].Symbol, Is.EqualTo(Exp.Memo));
      Assert.That(exp.Tail[0].Tail[0].Tail[1].Symbol, Is.EqualTo(Exp.Memo));
      Assert.That(exp.Tail[0].Tail[0].Tail[2].Symbol, Is.EqualTo(Exp.Memo));

      string term1 = exp.Tail[0].Tail[0].Tail[0].Tail[0].Term;
      string term2 = exp.Tail[0].Tail[0].Tail[1].Tail[0].Term;
      string term3 = exp.Tail[0].Tail[0].Tail[2].Tail[0].Term;
      Assert.That(new[] { term1, term2 }, Is.All.EqualTo(term3));
    }
  }
}
