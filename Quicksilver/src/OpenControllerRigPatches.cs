using HarmonyLib;

using Il2CppSLZ.Marrow;

using UnityEngine;

namespace Quicksilver.Patching;

[HarmonyPatch(typeof(OpenControllerRig))]
public static class OpenControllerRigPatches
{
    private const float RotationPow = 0.4f;

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
    [HarmonyPatch(nameof(OpenControllerRig.SmoothRotate))]
    public static void SmoothRotatePrefix(OpenControllerRig __instance, float input, float deltaTime, out float __state)
    {
        __state = __instance.degreesPerSnap;

        if (!QuicksilverMod.IsEnabled)
        {
            return;
        }

        if (QuicksilverMod.TargetTimeScale > 0f && QuicksilverMod.IsMainRig(__instance))
        {
            __instance.degreesPerSnap /= Mathf.Pow(QuicksilverMod.TargetTimeScale, RotationPow);
            TimePatches.ReturnScaled = true;
        }
    }

    [HarmonyPostfix]
    [HarmonyPatch(nameof(OpenControllerRig.SmoothRotate))]
    public static void SmoothRotatePostfix(OpenControllerRig __instance, float input, float deltaTime, float __state)
    {
        __instance.degreesPerSnap = __state;
        TimePatches.ReturnScaled = false;
    }
}