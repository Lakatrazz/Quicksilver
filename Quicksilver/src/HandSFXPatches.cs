using HarmonyLib;

using Il2CppSLZ.Marrow;

namespace Quicksilver.Patching;

[HarmonyPatch(typeof(HandSFX))]
public static class HandSFXPatches
{
    [HarmonyPrefix]
    [HarmonyPatch(nameof(HandSFX.Grab))]
    public static void GrabPrefix()
    {
        TimePatches.ForceDefaultTimescale = true;
    }

    [HarmonyPostfix]
    [HarmonyPatch(nameof(HandSFX.Grab))]
    public static void GrabPostfix()
    {
        TimePatches.ForceDefaultTimescale = false;
    }

    [HarmonyPrefix]
    [HarmonyPatch(nameof(HandSFX.BodySlot))]
    public static void BodySlotPrefix()
    {
        TimePatches.ForceDefaultTimescale = true;
    }

    [HarmonyPostfix]
    [HarmonyPatch(nameof(HandSFX.BodySlot))]
    public static void BodySlotPostfix()
    {
        TimePatches.ForceDefaultTimescale = false;
    }

    [HarmonyPrefix]
    [HarmonyPatch(nameof(HandSFX.Drop))]
    public static void DropPrefix()
    {
        TimePatches.ForceDefaultTimescale = true;
    }

    [HarmonyPostfix]
    [HarmonyPatch(nameof(HandSFX.Drop))]
    public static void DropPostfix()
    {
        TimePatches.ForceDefaultTimescale = false;
    }

    [HarmonyPrefix]
    [HarmonyPatch(nameof(HandSFX.ForcePull))]
    public static void ForcePullPrefix(float massDistance)
    {
        TimePatches.ForceDefaultTimescale = true;
    }

    [HarmonyPostfix]
    [HarmonyPatch(nameof(HandSFX.ForcePull))]
    public static void ForcePullPostfix(float massDistance)
    {
        TimePatches.ForceDefaultTimescale = false;
    }

    [HarmonyPrefix]
    [HarmonyPatch(nameof(HandSFX.OnSignificantCollisionEnter))]
    public static void OnSignificantCollisionEnterPrefix()
    {
        TimePatches.ForceDefaultTimescale = true;
    }

    [HarmonyPostfix]
    [HarmonyPatch(nameof(HandSFX.OnSignificantCollisionEnter))]
    public static void OnSignificantCollisionEnterPostfix()
    {
        TimePatches.ForceDefaultTimescale = false;
    }
}