using Inkorporated;            // the public API
using Inkorporated.Model;      // TattooPlacement
using MelonLoader;

// Inkorporated must load BEFORE this mod (we call its API), so declare it as a hard dependency.
// S1API comes along as Inkorporated's own dependency.
[assembly: MelonInfo(typeof(InkExample.Core), "InkExample", "1.0.0", "DooDesch", "https://github.com/DooDesch-Mods/ScheduleOne-InkorporatedExample")]
[assembly: MelonGame("TVGS", "Schedule I")]
[assembly: MelonAdditionalDependencies("Inkorporated")]

namespace InkExample
{
    /// <summary>
    /// Minimal example: register a custom tattoo with the Inkorporated framework via its API. The PNG is
    /// embedded in this DLL (see InkExample.csproj's EmbeddedResource), and a single API call loads it and
    /// registers it - no resource-loading boilerplate needed. Register early (here, in OnInitializeMelon),
    /// before the tattoo shop UI is built, and the tattoo shows up in the shop's Face category, rendering/
    /// saving/syncing exactly like a vanilla tattoo.
    /// </summary>
    public sealed class Core : MelonMod
    {
        public override void OnInitializeMelon()
        {
            // One call: loads the embedded PNG from THIS dll and registers it. The resource name is
            // "InkExample.Assets.ring_face.png" (a "ring_face.png" suffix would also match). id is unique
            // within your 'source'; placement is Chest | LeftArm | RightArm | Face; price 0 = Free.
            bool added = API.RegisterTattooFromResource(
                id: "ring_face",
                displayName: "Example Ring (API)",
                placement: TattooPlacement.Face,
                resourceName: "InkExample.Assets.ring_face.png",
                price: 0f,
                source: "InkExample");

            LoggerInstance.Msg(added
                ? "InkExample: registered 'Example Ring (API)' via Inkorporated.API - open the tattoo shop (Face)."
                : "InkExample: tattoo already registered (duplicate id).");

            // --- Other ways to register ---
            // From a PNG file on disk:
            //   API.RegisterTattooFromFile("my_id", "My Tattoo", TattooPlacement.Chest, @"C:\path\to.png", source: "InkExample");
            // From a Texture2D you already built (e.g. generated at runtime):
            //   API.RegisterTattoo("my_id", "My Tattoo", TattooPlacement.Chest, myTexture2D, source: "InkExample");
            //
            // --- Put this tattoo on an S1API NPC instead of the player? ---
            // Tattoos register through Resources.Load, so an NPC built with S1API can wear it too. Register
            // FIRST (above), then use the SAME path the framework assigned when you build the NPC's appearance:
            //   appearance.WithFaceLayer<FaceTattoos>("Avatar/Layers/Tattoos/custom/Face/InkExample_ring_face", Color.white);
        }
    }
}
