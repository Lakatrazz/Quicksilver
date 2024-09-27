using HarmonyLib;

using Il2CppSLZ.Marrow;

using UnityEngine;

namespace Quicksilver.Patching;

[HarmonyPatch(typeof(AnimationRig))]
public static class AnimationRigPatches
{
    [HarmonyPrefix]
    [HarmonyPatch(nameof(AnimationRig.UpdateHeptaBody2))]
    public static void UpdateHeptaBody2(AnimationRig __instance, Rig inRig, ref float deltaTime, ref Vector2 velocity, ref Vector2 accel)
    {
        if (!QuicksilverMod.IsEnabled)
        {
            return;
        }

        if (QuicksilverMod.TargetTimeScale > 0f && QuicksilverMod.IsMainRig(__instance))
        {
            deltaTime /= QuicksilverMod.TargetTimeScale;
            velocity *= QuicksilverMod.TargetTimeScale;
            accel *= QuicksilverMod.TargetTimeScale;
        }
    }
}