# Configuring the PRNG

Requires an instance of `System.Random` not the static class `UnityEngine.Random`.

If you have a predefined integer to use as a seed value, pass it in using the `Seed` option:

```cs
int aSeed = 72365472659210;

Grammar grammar = new Grammar(Seed: aSeed);
```

You can also pass in an instance of `Random` with the `Rng` option.

```cs
int bSeed = 82673452385460;

Grammar grammar = new Grammar(
    Rng: new System.Random(bSeed)
);
```
