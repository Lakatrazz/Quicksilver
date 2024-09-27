using HarmonyLib;

using Il2CppSLZ.Marrow;
using Il2CppSLZ.Marrow.Interaction;

using UnityEngine;

namespace Quicksilver.Patching;

[HarmonyPatch(typeof(OpenController))]
public static class OpenControllerPatches
{
    [HarmonyPostfix]
    [HarmonyPatch(nameof(OpenController.GetThumbStickAxis))]
    public static void GetThumbStickAxis(OpenController __instance, ref Vector2 __result)
    {
        if (!QuicksilverMod.IsEnabled)
        {
            return;
        }

        var openControllerRig = __instance.contRig.TryCast<OpenControllerRig>();

        if (QuicksilverMod.TargetTimeScale > 0f && QuicksilverMod.IsMainRig(openControllerRig))
        {
            float mult = 1f / QuicksilverMod.TargetTimeScale;

            if (openControllerRig.isRightHanded)
            {
                if (__instance.handedness != Handedness.RIGHT)
                    __result.x *= mult;
            }
            else
            {
                if (__instance.handedness != Handedness.LEFT)
                    __result.x *= mult;
            }

            __result.y *= mult;
        }
    }
}