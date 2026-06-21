using System;
using System.IO;
using System.Reflection;
using Inkorporated;            // the public API
using Inkorporated.Model;      // TattooPlacement
using MelonLoader;
using S1API.Rendering;         // TextureUtils (PNG -> Texture2D)
using UnityEngine;

// Inkorporated must load BEFORE this mod (we call its API), so declare it as a hard dependency.
// S1API comes along as Inkorporated's own dependency.
[assembly: MelonInfo(typeof(InkExample.Core), "InkExample", "1.0.0", "DooDesch", "https://github.com/DooDesch-Mods/ScheduleOne-InkorporatedExample")]
[assembly: MelonGame("TVGS", "Schedule I")]
[assembly: MelonAdditionalDependencies("Inkorporated")]

namespace InkExample
{
    /// <summary>
    /// Minimal example: register a custom tattoo with the Inkorporated framework via its API.
    /// We load a PNG embedded in this DLL and hand it to <see cref="API.RegisterTattoo"/>. Register early
    /// (here, in OnInitializeMelon) - before the tattoo shop UI is built - and the tattoo shows up in the
    /// shop's Face category, rendering/saving/syncing exactly like a vanilla tattoo.
    /// </summary>
    public sealed class Core : MelonMod
    {
        public override void OnInitializeMelon()
        {
            Texture2D tex = LoadEmbeddedTexture("InkExample.Assets.ring_face.png");
            if (tex == null)
            {
                LoggerInstance.Warning("InkExample: demo texture failed to load - nothing registered.");
                return;
            }

            // id is unique within your 'source'; placement is Chest | LeftArm | RightArm | Face; price 0 = Free.
            bool added = API.RegisterTattoo(
                id: "ring_face",
                displayName: "Example Ring (API)",
                placement: TattooPlacement.Face,
                texture: tex,
                price: 0f,
                source: "InkExample");

            LoggerInstance.Msg(added
                ? "InkExample: registered 'Example Ring (API)' via Inkorporated.API - open the tattoo shop (Face)."
                : "InkExample: tattoo already registered (duplicate id).");

            // --- Want to load from a file on disk instead of an embedded resource? ---
            //   API.RegisterTattooFromFile("my_id", "My Tattoo", TattooPlacement.Chest, @"C:\path\to.png", source: "InkExample");
            //
            // --- Want to put this tattoo on an S1API NPC instead of the player? ---
            // Because tattoos register through Resources.Load, an NPC built with S1API can wear it too. Register
            // FIRST (above), then when you build the NPC's appearance use the SAME resource path the framework
            // assigned (Avatar/Layers/Tattoos/custom/Face/InkExample_ring_face):
            //   appearance.WithFaceLayer<FaceTattoos>("Avatar/Layers/Tattoos/custom/Face/InkExample_ring_face", Color.white);
        }

        private Texture2D LoadEmbeddedTexture(string resourceName)
        {
            try
            {
                Assembly asm = Assembly.GetExecutingAssembly();
                using Stream s = asm.GetManifestResourceStream(resourceName);
                if (s == null) { LoggerInstance.Warning("embedded resource not found: " + resourceName); return null; }
                using var ms = new MemoryStream();
                s.CopyTo(ms);
                return TextureUtils.LoadTextureFromBytes(ms.ToArray());
            }
            catch (Exception e)
            {
                LoggerInstance.Warning("InkExample: texture load error - " + e.Message);
                return null;
            }
        }
    }
}
