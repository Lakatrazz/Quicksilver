using HarmonyLib;

using Il2CppSLZ.Marrow;

namespace Quicksilver.Patching;

[HarmonyPatch(typeof(RemapRig))]
public static class RemapRigPatches
{
    [HarmonyPrefix]
    [HarmonyPatch(nameof(RemapRig.OnEarlyUpdate))]
    public static void OnEarlyUpdatePrefix(RemapRig __instance)
    {
        if (!QuicksilverMod.IsEnabled)
        {
            return;
        }

        if (QuicksilverMod.IsMainRig(__instance))
        {
            TimePatches.ReturnScaled = true;
        }
    }

    [HarmonyPostfix]
    [HarmonyPatch(nameof(RemapRig.OnEarlyUpdate))]
    public static void OnEarlyUpdatePostfix(RemapRig __instance)
    {
        TimePatches.ReturnScaled = false;
    }
}