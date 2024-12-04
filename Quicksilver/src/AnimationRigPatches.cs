using HarmonyLib;

using Il2CppSLZ.Marrow;
using MelonLoader;
using UnityEngine;

namespace Quicksilver.Patching;

[HarmonyPatch(typeof(AnimationRig))]
public static class AnimationRigPatches
{
    [HarmonyPrefix]
    [HarmonyPatch(nameof(AnimationRig.OnEarlyUpdate))]
    public static void OnEarlyUpdatePrefix(AnimationRig __instance)
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
    [HarmonyPatch(nameof(AnimationRig.OnEarlyUpdate))]
    public static void OnEarlyUpdatePostfix(AnimationRig __instance)
    {
        TimePatches.ReturnScaled = false;
    }

    [HarmonyPrefix]
    [HarmonyPatch(nameof(AnimationRig.BodyVelocity))]
    public static void BodyVelocity(AnimationRig __instance, Rig inRig, ref Vector2 vel, ref Vector2 accel, float deltaTime)
    {
        if (!QuicksilverMod.IsEnabled)
        {
            return;
        }

        if (!QuicksilverMod.IsMainRig(__instance))
        {
            return;
        }

        vel *= QuicksilverMod.TargetTimeScale;
        accel *= QuicksilverMod.TargetTimeScale;
    }
}