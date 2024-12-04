using HarmonyLib;

using Il2CppSLZ.Marrow;

using UnityEngine;

namespace Quicksilver.Patching;

[HarmonyPatch(typeof(OpenControllerRig))]
public static class OpenControllerRigPatches
{
    [HarmonyPrefix]
    [HarmonyPatch(nameof(OpenControllerRig.UpdateHeptaBody))]
    public static void UpdateHeptaBody(OpenControllerRig __instance, ref float deltaTime)
    {
        if (!QuicksilverMod.IsEnabled)
        {
            return;
        }

        if (QuicksilverMod.TargetTimeScale > 0f && QuicksilverMod.IsMainRig(__instance))
        {
            deltaTime /= QuicksilverMod.TargetTimeScale;
        }
    }

    [HarmonyPrefix]
    [HarmonyPatch(nameof(OpenControllerRig.OnEarlyUpdate))]
    public static void OnEarlyUpdatePrefix(OpenControllerRig __instance)
    {
        if (!QuicksilverMod.IsEnabled)
        {
            return;
        }

        if (QuicksilverMod.IsMainRig(__instance))
        {
            TimePatches.ReturnScaled = true;
            TimePatches.ForceDefaultTimescale = true;
        }
    }

    [HarmonyPostfix]
    [HarmonyPatch(nameof(OpenControllerRig.OnEarlyUpdate))]
    public static void OnEarlyUpdatePostfix(OpenControllerRig __instance)
    {
        TimePatches.ReturnScaled = false;
        TimePatches.ForceDefaultTimescale = false;
    }
}