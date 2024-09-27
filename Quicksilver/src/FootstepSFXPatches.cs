using HarmonyLib;

using Il2CppSLZ.Marrow.Audio;

namespace Quicksilver.Patching;

[HarmonyPatch(typeof(FootstepSFX))]
public static class FootstepSFXPatches
{
    [HarmonyPrefix]
    [HarmonyPatch(nameof(FootstepSFX.PlayStep))]
    public static void PlayStepPrefix(float velocitySqr)
    {
        TimePatches.ForceDefaultTimescale = true;
    }

    [HarmonyPostfix]
    [HarmonyPatch(nameof(FootstepSFX.PlayStep))]
    public static void PlayStepPostfix(float velocitySqr)
    {
        TimePatches.ForceDefaultTimescale = false;
    }
}