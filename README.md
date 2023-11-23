# Calyx

Generative text processing for C# and Unity applications.

## Build

```bash
dotnet build
```

## Tests

```bash
dotnet test
```

## Install

### Unity

Create a new Unity project or open an existing project.

In the `Assets` directory of your project, create a new directory called `Libraries` (or adapt to suit your preferred style of organising).

Download `Calyx.dll` from the compiled resources under [Build/Release](https://eng-git.canterbury.ac.nz/mor30/calyx-sharp/-/tree/main/Build/Release) on eng-git.

Move `Calyx.dll` into the `Assets/Libraries` path of your Unity project.

Create a new C# Script in your Unity project and assign it to a new empty game object (or add it to an existing game object in your scene).

Add the following grammar script to the `Awake()` or `Start()` hook of your script.

```cs
using UnityEngine;
using Calyx;

public class TextSample : MonoBehaviour
{
  void Start()
  {
    Grammar hello = new Grammar(P => {
      P.Start("Hello from Unity");
    });

    Debug.Log(hello.Generate().text);
  }
}
```

Run the scene in the Unity editor and you should see the phrase `Hello from Unity` appear in the console.

## Roadmap

Rough plan for stabilising the API and features for a `1.0` release.

| Version | Features planned                                                   |
|---------|--------------------------------------------------------------------|
| `0.1`   | ~~block constructors and basic grammar features~~                  |
| `0.2`   | ~~runtime generator context in template substitution~~             |
| `0.3`   | ~~memo nodes and expression syntax~~                               |
| `0.4`   | ~~uniq nodes and expression syntax~~                               |
| `0.5`   | ~~weighted branch productions and definition parser~~               |
| `0.6`   | ~~modifying filter chains and expression syntax~~                   |
| `0.7`   | affix tree productions, definition parser and expression syntax      |
| `0.8`   | error handling and stack trace diagnostics                         |
| `0.9`   | Unity JSON affordances and file format integration                  |
| `0.10`  | Parity check with JavaScript and Ruby engines                      |
| `0.11`  | API documentation and code comments                                |
| `0.12`  | NuGet and Unity package manager nonsense                           |
