using HarmonyLib;

using Il2CppSLZ.Marrow;

namespace Quicksilver.Patching;

[HarmonyPatch(typeof(AlignPlug))]
public static class AlignPlugPatches
{
    [HarmonyPrefix]
    [HarmonyPatch(nameof(AlignPlug.Update))]
    public static void UpdatePrefix()
    {
        if (!QuicksilverMod.IsEnabled)
            return;

        if (QuicksilverMod.TargetTimeScale > 0f)
        {
            TimePatches.ReturnScaled = true;
        }
    }

    [HarmonyPostfix]
    [HarmonyPatch(nameof(AlignPlug.Update))]
    public static void UpdatePostfix()
    {
        TimePatches.ReturnScaled = false;
    }
}

[HarmonyPatch(typeof(Gun))]
public static class GunPatches
{
    [HarmonyPrefix]
    [HarmonyPatch(nameof(Gun.Update))]
    public static void UpdatePrefix(Gun __instance)
    {
        if (QuicksilverMod.TargetTimeScale > 0f && __instance.triggerGrip && __instance.triggerGrip.GetHand() && __instance.triggerGrip.GetHand().manager == QuicksilverMod.Instance.rigManager)
        {
            if (QuicksilverMod.IsEnabled)
                __instance.fireDuration = (60f / __instance.roundsPerMinute) * QuicksilverMod.TargetTimeScale;
            else
                __instance.fireDuration = 60f / __instance.roundsPerMinute;
        }

        TimePatches.ReturnScaled = true;
    }

    [HarmonyPostfix]
    [HarmonyPatch(nameof(Gun.Update))]
    public static void UpdatePostfix()
    {
        TimePatches.ReturnScaled = false;
    }
}