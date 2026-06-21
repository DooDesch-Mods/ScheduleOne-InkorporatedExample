# Inkorporated Example

> 🛟 **Need help or found a bug?** Get support at [support.doodesch.de](https://support.doodesch.de).

A working, copy-me example for [**Inkorporated**](https://github.com/DooDesch-Mods/ScheduleOne-Inkorporated) -
the custom-tattoo framework for Schedule I. It shows the **two ways** to add tattoos to the in-game tattoo
shop, side by side:

| Folder | Route | Needs code? | Best for |
|--------|-------|-------------|----------|
| [`pack/`](pack/) | A drop-in **content pack** (manifest.json + PNGs) | No | Shipping a set of tattoos as a standalone mod |
| [`code-mod/`](code-mod/) | A MelonLoader mod using the **`Inkorporated.API`** | Yes (C#) | Generating/registering tattoos at runtime, NPCs, dynamic packs |

Full docs: the **[Inkorporated Wiki](https://github.com/DooDesch-Mods/ScheduleOne-Inkorporated/wiki)**.

## Requirements (for both routes)

- Schedule I (IL2CPP) + **MelonLoader 0.7.3+**
- **S1API** ([ifBars/S1API_Forked](https://thunderstore.io/c/schedule-i/p/ifBars/S1API_Forked/))
- **Inkorporated** ([the framework](https://github.com/DooDesch-Mods/ScheduleOne-Inkorporated))

## Route 1 - the content pack (no code)

Copy [`pack/`](pack/) into `…/Schedule I/UserData/Inkorporated/Packs/` (rename the folder to your pack's
name) and launch. "Example Heart" (Chest) and "Example Star" (Left Arm) show up in the tattoo shop. See
[`pack/README.txt`](pack/README.txt) for the manifest fields. That folder, zipped, is a complete Thunderstore
content mod - just declare `DooDesch-Inkorporated` as a dependency.

## Route 2 - the code mod (the API)

[`code-mod/`](code-mod/) is a minimal MelonLoader mod. The whole integration is one call in
[`Core.cs`](code-mod/Core.cs):

```csharp
using Inkorporated;            // API
using Inkorporated.Model;      // TattooPlacement

// In OnInitializeMelon (register early - before the tattoo shop UI is built):
API.RegisterTattoo("ring_face", "Example Ring (API)", TattooPlacement.Face, myTexture2D, price: 0f, source: "InkExample");
// or from a PNG on disk:
API.RegisterTattooFromFile("my_id", "My Tattoo", TattooPlacement.Chest, pngPath, source: "InkExample");
```

`TattooPlacement` is `Chest | LeftArm | RightArm | Face`. `source` namespaces your ids. Because tattoos
register through the game's `Resources.Load`, the same registered path can also be applied to an **S1API
NPC** (`WithBodyLayer`/`WithFaceLayer`) - see the commented snippet in `Core.cs`.

### Building the code mod

It references DLLs straight from a normal modded install - no private build libs:

```sh
cd code-mod
dotnet build -c Release /p:GameDir="D:\path\to\Schedule I"
```

(Defaults `GameDir` to the usual Steam path; override as above. Install S1API + Inkorporated first so
`Mods/` has their DLLs.) The build copies `InkExample.dll` into your `Mods/` folder.

## License

MIT - see [LICENSE.md](LICENSE.md). The example tattoo PNGs are CC0 / public domain (own work); reuse freely.
