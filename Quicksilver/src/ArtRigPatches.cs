using HarmonyLib;

using Il2CppSLZ.Marrow;

namespace Quicksilver.Patching;

[HarmonyPatch(typeof(ArtRig))]
public static class ArtRigPatches
{
    [HarmonyPrefix]
    [HarmonyPatch(nameof(ArtRig.ArtOutputUpdate))]
    public static void ArtOutputUpdatePrefix(ArtRig __instance, PhysicsRig inRig)
    {
        if (!QuicksilverMod.IsEnabled)
        {
            return;
        }

        if (QuicksilverMod.TargetTimeScale > 0f && QuicksilverMod.IsMainRig(inRig))
        {
            TimePatches.ReturnScaled = true;
        }
    }

    [HarmonyPostfix]
    [HarmonyPatch(nameof(ArtRig.ArtOutputUpdate))]
    public static void ArtOutputUpdatePostfix()
    {
        TimePatches.ReturnScaled = false;
    }
}