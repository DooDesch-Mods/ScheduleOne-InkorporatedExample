Inkorporated Example Pack (no code required)
============================================

This folder IS a tattoo pack. To use it:

1. Install MelonLoader, S1API and Inkorporated.
2. Copy this whole folder into:
     <Schedule I>/UserData/Inkorporated/Packs/
   so you end up with:
     <Schedule I>/UserData/Inkorporated/Packs/InkorporatedExamplePack/manifest.json
3. Launch the game and open the tattoo shop - "Example Heart" (Chest) and "Example Star" (Left Arm) appear.

manifest.json explained:
  name / author : shown in the load log.
  tattoos[]     : one entry per tattoo.
    id          : unique within this pack (lowercase, no spaces).
    name        : the label on the shop button.
    placement   : chest | leftarm | rightarm | face.
    file        : the PNG filename in this folder (defaults to "<id>.png" if omitted).
    price       : optional; omit or 0 for "Free".

The PNGs:
  A tattoo is a FULL skin-texture layer, not a centered sticker - the opaque pixels must sit at the right
  UV spot for that body part. The two PNGs here are aligned to the chest and left-arm UV. To make your own,
  use the built-in tattoos as a reference: a DEBUG build of Inkorporated exports them to
  <Schedule I>/UserData/Inkorporated/Templates/. See the Inkorporated wiki for the full authoring guide.
