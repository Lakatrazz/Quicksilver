using HarmonyLib;

using UnityEngine;

namespace Quicksilver.Patching;

[HarmonyPatch(typeof(Time))]
public static class TimePatches
{
    public static bool ReturnScaled = false;

    public static bool ForceDefaultTimescale = false;

    public static bool ReturnInversed = false;

    [HarmonyPostfix]
    [HarmonyPatch(nameof(Time.deltaTime), MethodType.Getter)]
    public static void GetDeltaTime(ref float __result)
    {
        if (!QuicksilverMod.IsEnabled)
        {
            return;
        }

        var timeScale = GetRealTimeScale();

        if (ReturnScaled)
        {
            __result /= timeScale;
        }
        else if (ReturnInversed)
        {
            __result *= timeScale;
        }
    }

    [HarmonyPostfix]
    [HarmonyPatch(nameof(Time.fixedDeltaTime), MethodType.Getter)]
    public static void GetFixedDeltaTime(ref float __result)
    {
        if (!QuicksilverMod.IsEnabled)
        {
            return;
        }

        var timeScale = GetRealTimeScale();

        if (ReturnScaled)
        {
            __result /= timeScale;
        }
        else if (ReturnInversed)
        {
            __result *= timeScale;
        }
    }

    [HarmonyPostfix]
    [HarmonyPatch(nameof(Time.timeScale), MethodType.Getter)]
    public static void GetTimeScale(ref float __result)
    {
        if (!QuicksilverMod.IsEnabled)
        {
            return;
        }

        if (ForceDefaultTimescale)
        {
            __result = 1f;
        }
    }

    private static float GetRealTimeScale()
    {
        var forced = ForceDefaultTimescale;

        ForceDefaultTimescale = false;

        var timeScale = Time.timeScale;

        ForceDefaultTimescale = forced;

        return timeScale;
    }
}